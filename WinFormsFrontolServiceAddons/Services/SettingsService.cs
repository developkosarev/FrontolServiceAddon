using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FrontolServiceAddon
{
    public static class SettingsService
    {
        public static string settingsFile = "settings.xml";
        public static FirebirdSettings FirebirdSettings { get; private set; }
        public static FtpServerSettings FtpServerSettings { get; private set; }

        static SettingsService()
        {
            FirebirdSettings = new FirebirdSettings();
            FtpServerSettings = new FtpServerSettings();
        }

        public static void SaveSettings()
        {
            string startupPath = Application.StartupPath.ToString();

            XElement settings =
            new XElement("configuration",
                new XElement("DBFrontol",
                    new XElement("Database", SettingsService.FirebirdSettings.Database),
                    new XElement("Login", SettingsService.FirebirdSettings.Login),
                    new XElement("Password", SettingsService.FirebirdSettings.Password)
                ),
                new XElement("FTPServer",
                    new XElement("Url", SettingsService.FtpServerSettings.Url),
                    new XElement("Login", SettingsService.FtpServerSettings.Login),
                    new XElement("Password", SettingsService.FtpServerSettings.Password)
                )
            );

            settings.Save(string.Format("{0}{1}{2}", (object)startupPath, (object)Path.DirectorySeparatorChar, (object)SettingsService.settingsFile));
        }

        public static bool ReadSettings()
        {
            string startupPath = Application.StartupPath.ToString();

            string str = string.Format("{0}{1}{2}", startupPath, (object)Path.DirectorySeparatorChar, (object)SettingsService.settingsFile);
            if (!File.Exists(str))
                return false;

            XElement root = XDocument.Load(str).Root;
            if (root == null)
                return false;

            XElement xelementDBFrontol = root.Element((XName)"DBFrontol");
            if (xelementDBFrontol != null)
            {
                XElement xelementDatabase = xelementDBFrontol.Element((XName)"Database");
                if (xelementDatabase != null)
                    SettingsService.FirebirdSettings.Database = xelementDatabase.Value;

                XElement xelementLogin = xelementDBFrontol.Element((XName)"Login");
                if (xelementLogin != null)
                    SettingsService.FirebirdSettings.Login = xelementLogin.Value;

                XElement xelementPassword = xelementDBFrontol.Element((XName)"Password");
                if (xelementPassword != null)
                    SettingsService.FirebirdSettings.Password = xelementPassword.Value;
            }

            XElement xelementFtpServer = root.Element((XName)"FTPServer");
            if (xelementFtpServer != null)
            {
                XElement xelementUrl = xelementFtpServer.Element((XName)"Url");
                if (xelementUrl != null)
                    SettingsService.FtpServerSettings.Url = xelementUrl.Value;

                XElement xelementLogin = xelementFtpServer.Element((XName)"Login");
                if (xelementLogin != null)
                    SettingsService.FtpServerSettings.Login = xelementLogin.Value;

                XElement xelementPassword = xelementFtpServer.Element((XName)"Password");
                if (xelementPassword != null)
                    SettingsService.FtpServerSettings.Password = xelementPassword.Value;
            }            

            return true;
        }
    }
}