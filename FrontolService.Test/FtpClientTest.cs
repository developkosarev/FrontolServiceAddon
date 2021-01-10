using FrontolServiceAddon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FrontolService.Test
{
    public class FtpClientTest
    {
        internal const string Url = @"localhost";
        internal const string User = "anonymous";
        internal const string Password = "anonymous";

        internal FtpClient ftpClient;

        public FtpClientTest()
        {
            ftpClient = new FtpClient("ftp://localhost/", User, Password);
        }

        [Fact]
        public void ConnectTest()
        {
            string sourceZipFile = @"D:\MyProgram\CCharp\FrontolServiceAddon\src\WinFormsFrontolServiceAddons\bin\Debug\sprt.zip";
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
