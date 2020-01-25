using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontolService.DAL
{
    class Program
    {
        static void Main(string[] args)
        {            
            FbConnectionStringBuilder fb_con = new FbConnectionStringBuilder();
            fb_con.Charset = "WIN1251"; //используемая кодировка
            fb_con.UserID = "SYSDBA"; //логин
            fb_con.Password = "masterkey"; //пароль
            fb_con.Database = @"d:\DBFrontol\MAIN_test.GDB"; //путь к файлу базы данных            
            fb_con.ServerType = 0; //указываем тип сервера (0 - "полноценный Firebird" (classic или super server), 1 - встроенный (embedded))

            //создаем подключение
            //fb = new FbConnection(fb_con.ToString()); //передаем нашу строку подключения объекту класса FbConnection
            //fb.Open(); //открываем БД

            StoreContext storeContext = new StoreContext();
            storeContext.OpenConnection();

            List<StocksResult> res = storeContext.GetAllStocksResults();

            foreach (StocksResult item in res)
            {
                Console.Write($"{item.Code} ");
            }

            storeContext.CloseConnection();


            Console.WriteLine("Test");
            Console.ReadKey();
        }
    }
}
