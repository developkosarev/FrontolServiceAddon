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

        [Fact]
        public void ConnectTest()
        {            
            bool result = storeContext.IsAvailable();

            Assert.True(result);// Equal("New Name", result);
        }

        [Fact]
        public void DeleteReportsTest()
        {

            storeContext.OpenConnection();
            int result = storeContext.DeleteReports();
            storeContext.CloseConnection();

            Assert.Equal(0, result);
        }
    }
}
