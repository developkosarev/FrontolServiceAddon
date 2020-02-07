using FrontolService.DAL;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrontolServiceAddon
{
    public class UploadFacade
    {
        public string sprtFile = "sprt";
        private string destination;

        private FtpClient ftpClient;
        private StoreContext storeContext;

        public UploadFacade()
        {
            ftpClient = new FtpClient(SettingsService.FtpServerSettings.Url, SettingsService.FtpServerSettings.Login, SettingsService.FtpServerSettings.Password);
            storeContext = new StoreContext(SettingsService.FirebirdSettings.Database, SettingsService.FirebirdSettings.Login, SettingsService.FirebirdSettings.Password);            
        }

        private void CreateFileSprt(string source)
        {
            Logger.Log.Info("1");

            storeContext.OpenConnection();
            Logger.Log.Info("2");

            List<StocksResult> stocksResult = storeContext.GetAllStocksResults();
            Logger.Log.Info("3");

            destination = storeContext.GetFtpParamsResult().Ftpfolder;
            Logger.Log.Info("4");

            storeContext.CloseConnection();
            Logger.Log.Info("5");

            
            string separator = ";";

            Logger.Log.Info("Записей " + stocksResult.Count);

            StringBuilder sb = new StringBuilder("Привет мир");

            int i = 0;
            foreach (var item in stocksResult) 
            {                
                sb.AppendLine(item.Code + separator 
                            + item.Name + separator
                            + item.Price + separator
                            + item.Summa1 + separator
                            + item.MinPrice);

                if (i % 10000 == 0) 
                {
                    Logger.Log.Info(i + " => " + item.Code);
                }
                
                i++;
            }
            Logger.Log.Info("6");

            string sourceTxtFile = string.Format("{0}{1}{2}.txt", source, (object)Path.DirectorySeparatorChar, (object)sprtFile);

            Logger.Log.Info("7");
            
            File.WriteAllText(sourceTxtFile, sb.ToString());
        }

        private void ZipFileSprt(string source)
        {
            string sourceTxtFile = string.Format("{0}{1}{2}.txt", source, (object)Path.DirectorySeparatorChar, (object)sprtFile);
            string destinationZipFile = string.Format("{0}{1}{2}.zip", source, "/", sprtFile);

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.AddFile(source + "\\" + sprtFile + ".txt", "");
                zip.Save(source + "\\" + sprtFile + ".zip");
            }
        }

        public string UploadFileSprt(string source)
        {                        
            CreateFileSprt(source);
            ZipFileSprt(source);            

            string sourceZipFile = string.Format("{0}{1}{2}.zip", source, (object)Path.DirectorySeparatorChar, sprtFile);
            string destinationZipFile = string.Format("{0}{1}{2}.zip", destination, "/", sprtFile);

            Logger.Log.Info("Старт UploadFile");
            
            //ftpClient.Passive = false;

            string result = ftpClient.UploadFile(sourceZipFile, destinationZipFile);

            Logger.Log.Info("Финиш UploadFile");

            return result;
        }


    }
}
