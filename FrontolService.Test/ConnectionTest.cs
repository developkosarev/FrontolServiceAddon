using FrontolService.DAL;
using FrontolService.Test;
using System;
using System.Text;
using Xunit;

namespace FrontolService.Tests
{
    public class ConnectionTest
    {                
        private StoreContext storeContext;

        public ConnectionTest()
        {            
            storeContext = new StoreContext(TestsSetup.Database());
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
