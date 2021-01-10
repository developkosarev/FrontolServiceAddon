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
            
            sprtFile = SettingsService.FtpServerSettings.FileName;
            destination = SettingsService.FtpServerSettings.Directory;
        }

        private void CreateFileSprt(string source)
        {            
            storeContext.OpenConnection();            
            List<StocksResult> stocksResult = storeContext.GetAllStocksResults();                       
            storeContext.CloseConnection();            
            
            string separator = ";";

            Logger.Log.Info("Записей " + stocksResult.Count);

            StringBuilder sb = new StringBuilder();

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
            string sourceTxtFile = string.Format("{0}{1}{2}.txt", source, (object)Path.DirectorySeparatorChar, (object)this.sprtFile);            
            
            File.WriteAllText(sourceTxtFile, sb.ToString());
        }

        private void ZipFileSprt(string source)
        {
            string sourceTxtFile = string.Format("{0}{1}{2}.txt", source, (object)Path.DirectorySeparatorChar, (object)this.sprtFile);
            string destinationZipFile = string.Format("{0}{1}{2}.zip", source, "/", this.sprtFile);

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.AddFile(source + "\\" + this.sprtFile + ".txt", "");
                zip.Save(source + "\\" + this.sprtFile + ".zip");
            }
        }

        public string UploadFileSprt(string source)
        {                        
            CreateFileSprt(source);
            ZipFileSprt(source);

            //this.destination = storeContext.GetFtpParamsResult().Ftpfolder;

            string sourceZipFile = string.Format("{0}{1}{2}.zip", source, (object)Path.DirectorySeparatorChar, this.sprtFile);
            string destinationZipFile = string.Format("{0}{1}{2}.zip", this.destination, "/", this.sprtFile);

            Logger.Log.Info("Старт UploadFile");
            
            //ftpClient.Passive = false;

            string result = ftpClient.UploadFile(sourceZipFile, destinationZipFile);

            Logger.Log.Info("Финиш UploadFile");

            return result;
        }


    }
}
