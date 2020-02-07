using FirebirdSql.Data.FirebirdClient;
using FrontolService.DAL;
using System;
using System.Text;
using Xunit;

namespace FrontolService.Tests
{
    public class ConnectionTest
    {        
        internal const string Database = @"D:\DBFrontol\MAIN_test.GDB";
        internal const string UserID = "SYSDBA";
        internal const string Password = "masterkey";

        internal StoreContext storeContext;

        public ConnectionTest()
        {            
            storeContext = new StoreContext(Database);            
        }

        //[Fact]
        //public void ConnectTest()
        //{            
        //    bool result = storeContext.IsAvailable();

        //    Assert.True(result);// Equal("New Name", result);
        //}

        [Fact]
        public void NewConnectTest()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);            

            FbConnection fb = new FbConnection();

            FbConnectionStringBuilder fb_con = new FbConnectionStringBuilder();            

            fb_con.Charset = "WIN1251";            
            fb_con.UserID = UserID;
            fb_con.Password = Password;
            fb_con.Database = Database;
            fb_con.ServerType = 0;
            
            fb.ConnectionString = fb_con.ToString();
            fb.Open();
            fb.Close();

            Assert.True(true);// Equal("New Name", result);
        }

    }
}
