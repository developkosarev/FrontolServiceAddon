﻿using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontolService.DAL
{
    public class StoreContext
    {
        #region Fields

        //fb ссылается на соединение с нашей базой данных, по-этому она должна быть доступна всем методам нашего класса
        private FbConnection fb;
        private FbConnectionStringBuilder fb_con;
        //private string _connectionString;

        private List<Remain> remains;

        #endregion

        #region Properties

        //public override string ConnectionString
        //{
        //    get { return _connectionString; }
        //    set
        //    {
        //        if (_state == ConnectionState.Closed)
        //        {
        //            if (value == null)
        //            {
        //                value = string.Empty;
        //            }

        //            _options.Load(value);
        //            _options.Validate();
        //            _connectionString = value;
        //        }
        //    }
        //}

        #endregion

        public StoreContext()
        {
            fb = new FbConnection();
            fb_con = new FbConnectionStringBuilder();

            remains = new List<Remain>();
        }

        public void OpenConnection()
        {            
            fb_con.Charset = "WIN1251"; //используемая кодировка
            fb_con.UserID = "SYSDBA"; //логин
            fb_con.Password = "masterkey"; //пароль
            fb_con.Database = @"d:\DBFrontol\MAIN_test.GDB"; //путь к файлу базы данных            
            fb_con.ServerType = 0; //указываем тип сервера (0 - "полноценный Firebird" (classic или super server), 1 - встроенный (embedded))

            //создаем подключение
            fb.ConnectionString = fb_con.ToString();            
            fb.Open(); //открываем БД                        
        }

        public void CloseConnection()
        {
            fb.Close();
        }

        public List<StocksResult> GetAllStocksResults()
        {
            List<StocksResult> stocksResult = new List<StocksResult>();

            string sql = @"Select Sprt.mark, Sprt.name, PriceData.Price, RD.REMAINID, -SUM(IIF(Rd.DTYPE = 0, Rd.DELTA, 0))AS SUMMA0, -SUM(IIF(Rd.DTYPE = 1, Rd.DELTA, 0)) AS SUMMA1, Sprt.MinPrice 
                           From Sprt LEFT JOIN Remain R ON Sprt.id = R.wareid 
                                     LEFT JOIN Remaind Rd ON R.Id = Rd.remainid 
                                     LEFT JOIN PriceData ON R.Id = PriceData.remainid 
                           GROUP BY Sprt.mark, Sprt.name, Sprt.MinPrice, PriceData.Price, RD.REMAINID
                           order by NAME";

            using (FbTransaction fbt = fb.BeginTransaction())  //стартуем транзакцию; стартовать транзакцию можно только для открытой базы (т.е. мутод Open() уже был вызван ранее, иначе ошибка)
            {
                using (FbCommand selectSQL = new FbCommand(sql, fb, fbt)) //задаем запрос на выборку
                {
                    FbDataReader reader = selectSQL.ExecuteReader(); //для запросов, которые возвращают результат в виде набора данных надо использоваться метод ExecuteReader()

                    while (reader.Read()) //пока не прочли все данные выполняем...
                    {                    
                        stocksResult.Add(new StocksResult
                        {
                            Code = (string)reader["mark"],
                            Name = (string)reader["name"],
                            Price = (double)reader["Price"],
                            Summa0 = (double)reader["SUMMA0"],
                            Summa1 = (double)reader["SUMMA1"],
                            MinPrice = (double)reader["MinPrice"]
                        });
                    }

                    reader.Close();
                };

                fbt.Commit();
            }
            
            return stocksResult;
        }

        public FtpParamsResult GetFtpParamsResult()
        {
            FtpParamsResult ftpParamsResult = new FtpParamsResult();
            
            string sql = @"select exchchannel.FOLDER, exchchannel.FTPHOST, exchchannel.FTPPORT, exchchannel.PASSIVE, exchchannel.SERVERTIMEOUT
                          from exchtask left join exchchannel on exchtask.id = exchchannel.EXCHTASKID and exchchannel.direction = 1
                          where exchtask.code = 3";

            using (FbTransaction fbt = fb.BeginTransaction())  //стартуем транзакцию; стартовать транзакцию можно только для открытой базы (т.е. мутод Open() уже был вызван ранее, иначе ошибка)
            {
                using (FbCommand selectSQL = new FbCommand(sql, fb, fbt)) //задаем запрос на выборку
                {
                    FbDataReader reader = selectSQL.ExecuteReader(); //для запросов, которые возвращают результат в виде набора данных надо использоваться метод ExecuteReader()

                    while (reader.Read()) //пока не прочли все данные выполняем...
                    {
                        if (reader.GetString(1) != String.Empty)
                        {
                            ftpParamsResult.Addres = reader.GetString(1).ToString();
                            ftpParamsResult.Addres = @"ftp://" + ftpParamsResult.Addres.Trim() + @"/";
                        }

                        if (reader.GetString(0) != String.Empty)
                        {
                            ftpParamsResult.Ftpfolder = reader.GetString(0).ToString();
                        }                        
                    }

                    reader.Close();
                };

                fbt.Commit();
            }

            return ftpParamsResult;
        }

        private void GetAllRemain()
        {
            remains.Clear();

            string sql = @"SELECT FIRST 10 REMAINID, SUM(IIF(DTYPE = 0, DELTA, 0))AS SUMMA0, SUM(IIF(DTYPE = 1, DELTA, 0)) AS SUMMA1
                           FROM REMAIND RD
                           GROUP BY REMAINID
                           HAVING COUNT(ID) > 1";

            using (FbTransaction fbt = fb.BeginTransaction())
            {
                using (FbCommand selectSQL = new FbCommand(sql, fb, fbt))
                {
                    FbDataReader reader = selectSQL.ExecuteReader();

                    while (reader.Read())
                    {
                        remains.Add(new Remain
                        {
                            RemainId = (string)reader["REMAINID"],                            
                            Summa0 = (double)reader["SUMMA0"],
                            Summa1 = (double)reader["SUMMA1"]                            
                        });
                    }

                    reader.Close();
                };

                fbt.Commit();
            }            
        }

        public void DeleteRemain()
        {            
            this.GetAllRemain();

            foreach (var item in remains)
            {
                string sql = "DELETE FROM REMAIND WHERE REMAINID = " + item.RemainId;
                string sqlInsert0 = "INSERT INTO REMAIND(ID, REMAINID, DELTA, CHNG, DTYPE) VALUES (GEN_ID(GREMAIND, 1), " + item.RemainId + ", " + item.Summa0.ToString() + ", -1, 0)";
                string sqlInsert1 = "INSERT INTO REMAIND(ID, REMAINID, DELTA, CHNG, DTYPE) VALUES (GEN_ID(GREMAIND, 1), " + item.RemainId + ", " + item.Summa1.ToString() + ", -1, 1)";

                using (FbTransaction fbt = fb.BeginTransaction())
                {
                    using (FbCommand deleteSQL = new FbCommand(sql, fb, fbt))
                    {
                        deleteSQL.ExecuteNonQuery();
                    };

                    if (item.Summa0 != 0)
                    {                        
                        using (FbCommand insertSQL = new FbCommand(sqlInsert0, fb, fbt))
                        {
                            insertSQL.ExecuteNonQuery();
                        };
                    }

                    if (item.Summa1 != 0)
                    {                        
                        using (FbCommand insertSQL = new FbCommand(sqlInsert1, fb, fbt))
                        {
                            insertSQL.ExecuteNonQuery();
                        };                        
                    }

                    fbt.Commit();
                }                                
            }            
        }

        public void DeleteRemaindCollapsed()
        {
            string sql = @"DELETE FROM REMAIND_COLLAPSED";

            using (FbTransaction fbt = fb.BeginTransaction())  //стартуем транзакцию; стартовать транзакцию можно только для открытой базы (т.е. мутод Open() уже был вызван ранее, иначе ошибка)
            {
                using (FbCommand deleteSQL = new FbCommand(sql, fb, fbt)) //задаем запрос на выборку
                {
                    deleteSQL.ExecuteNonQuery();
                };

                fbt.Commit();
            }            
        }

    }
}
