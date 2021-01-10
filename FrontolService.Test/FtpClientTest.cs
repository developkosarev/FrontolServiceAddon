using FrontolServiceAddon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FrontolService.Test
{
    public class FtpClientTest
    {        
        internal const string Url = @"localhost";
        internal const string User = "anonymous";
        internal const string Password = "anonymous";
        
        internal FtpClient ftpClient;

        private readonly ITestOutputHelper output;

        public FtpClientTest(ITestOutputHelper output)
        {
            ftpClient = new FtpClient("ftp://localhost/", User, Password);
            this.output = output;
        }

        [Fact]
        public void ConnectTest()
        {
            string exe_dir = Directory.GetCurrentDirectory();
            output.WriteLine("This is output from {0}", exe_dir);
            
            string sourceZipFile = Path.Combine(exe_dir, @"log4net.xml");            
            string destinationZipFile = "sprt.zip";

            string result = ftpClient.UploadFile(sourceZipFile, destinationZipFile);

            //Thread.Sleep(8000);

            destinationZipFile = "sprt01.zip";
            result = ftpClient.UploadFile(sourceZipFile, destinationZipFile);

            //Thread.Sleep(8000);

            destinationZipFile = @"sprt02.zip";
            result = ftpClient.UploadFile(sourceZipFile, destinationZipFile);

            Assert.Equal("226 Transfer complete.\r\n", result);
        }

    }
}
