using FirebirdSql.Data.FirebirdClient;
using FtpClient;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace FrontolServiceAddon
{
    class FrontolLib
    {
        private List<Remain> listRemain;

        //fb ссылается на соединение с нашей базой данных, по-этому она должна быть доступна всем методам нашего класса
        private FbConnection fb;
        private FbConnectionStringBuilder fb_con;

        #region FTP
        public string addres;
        public string ftpfolder;
        public string login;
        public string password;
        public string filenamesprt = "sprt";
        #endregion
        
        public bool DbExist = true;

        public FrontolLib()
        {
            listRemain = new List<Remain>();

            addres = SettingsService.FtpServerSettings.Url;
            login = SettingsService.FtpServerSettings.Login;
            password = SettingsService.FtpServerSettings.Password;

            //формируем connection string для последующего соединения с нашей базой данных
            fb_con = new FbConnectionStringBuilder();
            fb_con.Charset = "WIN1251"; //используемая кодировка
            fb_con.UserID = SettingsService.FirebirdSettings.Login;
            fb_con.Password = SettingsService.FirebirdSettings.Password;
            fb_con.Database = SettingsService.FirebirdSettings.Database; //@"d:\DBFrontol\MAIN.GDB";
            fb_con.ServerType = 0; //указываем тип сервера (0 - "полноценный Firebird" (classic или super server), 1 - встроенный (embedded))
        }

        public void CreateFileSprt(string appPatch)
        {
            Logger.Log.Info("Start CreateFileSprt");
                        
            //создаем подключение
            fb = new FbConnection(fb_con.ToString()); //передаем нашу строку подключения объекту класса FbConnection

            fb.Open(); //открываем БД            

            Logger.Log.Info("Подключение к БД выполнение запроса");

            //так проверять состояние соединения (активно или не активно)
            if (fb.State == ConnectionState.Closed)
                fb.Open();

            FbTransaction fbt = fb.BeginTransaction(); //стартуем транзакцию; стартовать транзакцию можно только для открытой базы (т.е. мутод Open() уже был вызван ранее, иначе ошибка)            

            FbCommand selectSQL = new FbCommand(@"Select Sprt.mark, Sprt.name, PriceData.Price, RD.REMAINID, -SUM(IIF(Rd.DTYPE = 0, Rd.DELTA, 0))AS SUMMA0, -SUM(IIF(Rd.DTYPE = 1, Rd.DELTA, 0)) AS SUMMA1, Sprt.MinPrice 
                                                From Sprt LEFT JOIN Remain R ON Sprt.id = R.wareid 
                                                          LEFT JOIN Remaind Rd ON R.Id = Rd.remainid 
                                                          LEFT JOIN PriceData ON R.Id = PriceData.remainid 
                                                GROUP BY Sprt.mark, Sprt.name, Sprt.MinPrice, PriceData.Price, RD.REMAINID
                                                order by NAME", fb); //задаем запрос на выборку

            selectSQL.Transaction = fbt; //необходимо проинициализить транзакцию для объекта SelectSQL
            FbDataReader reader = selectSQL.ExecuteReader(); //для запросов, которые возвращают результат в виде набора данных надо использоваться метод ExecuteReader()

            Logger.Log.Info("Запрос выполнен, перебор результата");

            string select_result = ""; //в эту переменную будем складывать результат запроса Select

            try
            {
                while (reader.Read()) //пока не прочли все данные выполняем...
                {
                    //select_result = select_result + reader.GetInt32(0).ToString() + ";" + reader.GetString(1) + ";" + reader.GetString(2) + "\n";
                    select_result = select_result + reader.GetString(0).ToString() + ";" + reader.GetString(1) + ";" + reader.GetString(2) + ";" + reader.GetString(5) + ";" + reader.GetString(6) + "\n";
                }
            }
            finally
            {
                //всегда необходимо вызывать метод Close(), когда чтение данных завершено
                reader.Close();
                fbt.Commit();
                //fb.Close(); //закрываем соединение, т.к. оно нам больше не нужно                                
            }

            File.WriteAllText(appPatch + "\\" + filenamesprt + ".txt", select_result);
            //MessageBox.Show(select_result); //выводим результат запроса

            Logger.Log.Info("Результат записан в файл");





            fbt = fb.BeginTransaction(); //стартуем транзакцию; стартовать транзакцию можно только для открытой базы (т.е. мутод Open() уже был вызван ранее, иначе ошибка)

            selectSQL.CommandText = @"select exchchannel.FOLDER, exchchannel.FTPHOST, exchchannel.FTPPORT, exchchannel.PASSIVE, exchchannel.SERVERTIMEOUT
                                      from exchtask left join exchchannel on exchtask.id = exchchannel.EXCHTASKID and exchchannel.direction = 1
                                      where exchtask.code = 3";

            selectSQL.Transaction = fbt; //необходимо проинициализить транзакцию для объекта SelectSQL
            reader = selectSQL.ExecuteReader(); //для запросов, которые возвращают результат в виде набора данных надо использоваться метод ExecuteReader()

            select_result = ""; //в эту переменную будем складывать результат запроса Select

            try
            {
                while (reader.Read()) //пока не прочли все данные выполняем...
                {
                    //select_result = select_result + reader.GetInt32(0).ToString() + ";" + reader.GetString(1) + ";" + reader.GetString(2) + "\n";
                    //select_result = select_result + reader.GetString(0).ToString() + ";" + reader.GetString(1) + ";" + reader.GetString(2) + ";" + reader.GetString(3) + "\n";

                    addres = reader.GetString(1).ToString();
                    addres = @"ftp://" + addres.Trim() + @"/";
                    ftpfolder = reader.GetString(0).ToString();
                }
            }
            finally
            {
                //всегда необходимо вызывать метод Close(), когда чтение данных завершено
                reader.Close();
                //fb.Close(); //закрываем соединение, т.к. оно нам больше не нужно                                
            }
            //MessageBox.Show(select_result); //выводим результат запроса


            fb.Close(); //закрываем соединение, т.к. оно нам больше не нужно                                
            selectSQL.Dispose(); //в документации написано, что ОЧЕНЬ рекомендуется убивать объекты этого типа, если они больше не нужны

            //lbLog.Items.Add("Close " + Application.StartupPath.ToString());

            Logger.Log.Info("Начало архивации");

            //http://dotnet-am.livejournal.com/5730.html
            using (ZipFile zip = new ZipFile()) // Создаем объект для работы с архивом
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // Задаем максимальную степень сжатия                 
                //zip.AddDirectory(@"C:\project\"); // Кладем в архив папку вместе с содежимым
                zip.AddFile(appPatch + "\\" + filenamesprt + ".txt", ""); // Кладем в архив одиночный файл                
                zip.Save(appPatch + "\\" + filenamesprt + ".zip"); // Создаем архив     
            }

            Logger.Log.Info("Завершение архивации, выгрузка файла FTP на сервер");            

            Client client = new Client(addres, login, password);
            client.UploadFile(appPatch + "\\" + filenamesprt + ".zip", ftpfolder + "/" + filenamesprt + ".zip");

            Logger.Log.Info("Завершение выгрузки в офис");
        }

        public string UploadFileSprt(string Path)
        {
            Client client = new Client(addres, login, password);
            //client.Passive = false;

            return client.UploadFile(Path + "\\" + filenamesprt + ".zip", ftpfolder + "/" + filenamesprt + ".zip");
        }


        public void FillListRemain()
        {
            listRemain.Clear();

            //создаем подключение
            fb = new FbConnection(fb_con.ToString()); //передаем нашу строку подключения объекту класса FbConnection
            fb.Open(); //открываем БД                                    

            Logger.Log.Info("Подключение к БД выполнение запроса");

            //так проверять состояние соединения (активно или не активно)
            if (fb.State == ConnectionState.Closed)
                fb.Open();
            
            FbCommand selectSQL = new FbCommand(
                "SELECT FIRST 10 REMAINID, SUM(IIF(DTYPE = 0, DELTA, 0))AS SUMMA0, SUM(IIF(DTYPE = 1, DELTA, 0)) AS SUMMA1 " +
                "FROM REMAIND RD " +
                "GROUP BY REMAINID " +
                "HAVING COUNT(ID) > 1"
                , fb); //задаем запрос на выборку

            FbTransaction fbt = fb.BeginTransaction(); //стартуем транзакцию; стартовать транзакцию можно только для открытой базы (т.е. мутод Open() уже был вызван ранее, иначе ошибка)

            selectSQL.Transaction = fbt; //необходимо проинициализить транзакцию для объекта SelectSQL
            FbDataReader reader = selectSQL.ExecuteReader(); //для запросов, которые возвращают результат в виде набора данных надо использоваться метод ExecuteReader()            

            Logger.Log.Info("Запрос выполнен, перебор результата");
            
            try
            {
                while (reader.Read()) //пока не прочли все данные выполняем...
                {
                    Remain remain = new Remain
                    {
                        RemainId = reader.GetString(0),
                        Summa0 = reader.GetFloat(1),
                        Summa1 = reader.GetFloat(2)
                    };

                    listRemain.Add(remain);
                }                
            }
            finally
            {
                //всегда необходимо вызывать метод Close(), когда чтение данных завершено                
                reader.Close();
                fbt.Commit();
                fb.Close(); //закрываем соединение, т.к. оно нам больше не нужно                                

                selectSQL.Dispose(); //в документации написано, что ОЧЕНЬ рекомендуется убивать объекты этого типа, если они больше не нужны
            }

            Logger.Log.Info("Список listRemain заполнен");

            RemoveListRemain();

        }

        private void RemoveListRemain()
        {            
            //создаем подключение
            fb = new FbConnection(fb_con.ToString()); //передаем нашу строку подключения объекту класса FbConnection
            fb.Open(); //открываем БД                                    

            Logger.Log.Info("Подключение к БД выполнение запроса");

            //так проверять состояние соединения (активно или не активно)
            if (fb.State == ConnectionState.Closed)
                fb.Open();

            FbCommand deleteSql = new FbCommand();
            deleteSql.Connection = fb;

            FbCommand insertSql = new FbCommand();
            insertSql.Connection = fb;

            try
            {                
                foreach (var item in listRemain)
                {
                    FbTransaction fbt = fb.BeginTransaction();
                    
                    string sqlText = "DELETE FROM REMAIND WHERE REMAINID = " + item.RemainId;
                    deleteSql.CommandText = sqlText;
                    //FbCommand deleteSql = new FbCommand(sqlText, fb);

                    deleteSql.Transaction = fbt; //необходимо проинициализить транзакцию для объекта SelectSQL
                    deleteSql.ExecuteNonQuery(); //для запросов, которые возвращают результат в виде набора данных надо использоваться метод ExecuteReader()            
                    //deleteSql.Dispose();

                    if (item.Summa0 != 0)
                    {                        
                        sqlText = "INSERT INTO REMAIND(ID, REMAINID, DELTA, CHNG, DTYPE) VALUES (GEN_ID(GREMAIND, 1), " + item.RemainId + ", " + item.Summa0.ToString() + ", -1, 0)";
                        insertSql.CommandText = sqlText;
                        //FbCommand insertSql = new FbCommand(sqlText, fb);

                        insertSql.Transaction = fbt;
                        insertSql.ExecuteNonQuery();
                        //insertSql.Dispose();                        
                    }

                    if (item.Summa1 != 0)
                    {                        
                        sqlText = "INSERT INTO REMAIND(ID, REMAINID, DELTA, CHNG, DTYPE) VALUES (GEN_ID(GREMAIND, 1), " + item.RemainId + ", " + item.Summa1.ToString() + ", -1, 1)";
                        insertSql.CommandText = sqlText;
                        //FbCommand insertSQL = new FbCommand(sqltext, fb);                        

                        insertSql.Transaction = fbt;
                        insertSql.ExecuteNonQuery();
                        //insertSql.Dispose();                       
                    }

                    fbt.Commit();
                }                
            }
            finally
            {
                deleteSql.Dispose();
                insertSql.Dispose();

                //всегда необходимо вызывать метод Close(), когда чтение данных завершено                                
                fb.Close(); //закрываем соединение, т.к. оно нам больше не нужно                                                
            }            

        }

        public void RemoveRemaindCollapsed()
        {
            //REMAIND_COLLAPSED

            //создаем подключение
            fb = new FbConnection(fb_con.ToString()); //передаем нашу строку подключения объекту класса FbConnection
            fb.Open(); //открываем БД                                    

            Logger.Log.Info("Подключение к БД выполнение запроса");

            //так проверять состояние соединения (активно или не активно)
            if (fb.State == ConnectionState.Closed)
                fb.Open();

            FbCommand deleteSql = new FbCommand();
            deleteSql.Connection = fb;            

            try
            {                
                FbTransaction fbt = fb.BeginTransaction();

                string sqlText = "DELETE FROM REMAIND_COLLAPSED";
                deleteSql.CommandText = sqlText;
                //FbCommand deleteSql = new FbCommand(sqlText, fb);

                deleteSql.Transaction = fbt; //необходимо проинициализить транзакцию для объекта SelectSQL
                deleteSql.ExecuteNonQuery(); //для запросов, которые возвращают результат в виде набора данных надо использоваться метод ExecuteReader()            
                //deleteSql.Dispose();
                    
                fbt.Commit();                
            }
            finally
            {
                deleteSql.Dispose();                

                //всегда необходимо вызывать метод Close(), когда чтение данных завершено                                
                fb.Close(); //закрываем соединение, т.к. оно нам больше не нужно                                                
            }

        }
    }


    class ActionFrontol
    {
        public string UserID { get; set; }
        public string Password { get; set; }
    }

    class Remain
    {
        public string RemainId { get; set; }
        public float Summa0 { get; set; }
        public float Summa1 { get; set; }
    }
}
