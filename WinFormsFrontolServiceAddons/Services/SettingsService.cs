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
        public static TaskSettings TaskSettings { get; private set; }
        public static FtpServerSettings FtpServerSettings { get; private set; }

        static SettingsService()
        {
            FirebirdSettings = new FirebirdSettings();
            TaskSettings = new TaskSettings();
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
                new XElement("Task",
                    new XElement("Interval", SettingsService.TaskSettings.Interval),
                    new XElement("CollapseRemaind", SettingsService.TaskSettings.CollapseRemaind),
                    new XElement("DeleteRemaindCollapsed", SettingsService.TaskSettings.DeleteRemaindCollapsed),
                    new XElement("SendToFtp", SettingsService.TaskSettings.SendToFtp)
                ),
                new XElement("FTPServer",
                    new XElement("Url", SettingsService.FtpServerSettings.Url),
                    new XElement("Login", SettingsService.FtpServerSettings.Login),
                    new XElement("Password", SettingsService.FtpServerSettings.Password),
                    new XElement("Directory", SettingsService.FtpServerSettings.Directory),
                    new XElement("FileName", SettingsService.FtpServerSettings.FileName)
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
            
            ReadSettingsDBFrontol(root);
            ReadSettingsTask(root);
            ReadSettingsFTPServer(root);
            
            return true;
        }

        private static void ReadSettingsDBFrontol(XElement root) 
        {
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
        }
        private static void ReadSettingsTask(XElement root)
        {
            XElement xelement = root.Element((XName)"Task");
            if (xelement != null)
            {
                XElement xelementInterval = xelement.Element((XName)"Interval");
                if (xelementInterval != null)
                    SettingsService.TaskSettings.Interval = Convert.ToInt32(xelementInterval.Value);

                XElement xelementCollapseRemaind = xelement.Element((XName)"CollapseRemaind");
                if (xelementCollapseRemaind != null)
                    SettingsService.TaskSettings.CollapseRemaind = Convert.ToBoolean(xelementCollapseRemaind.Value);

                XElement xelementDeleteRemaindCollapsed = xelement.Element((XName)"DeleteRemaindCollapsed");
                if (xelementDeleteRemaindCollapsed != null)
                    SettingsService.TaskSettings.DeleteRemaindCollapsed = Convert.ToBoolean(xelementDeleteRemaindCollapsed.Value);

                XElement xelementSendToFtp = xelement.Element((XName)"SendToFtp");
                if (xelementSendToFtp != null)
                    SettingsService.TaskSettings.SendToFtp = Convert.ToBoolean(xelementSendToFtp.Value);
                
            }
        }
        private static void ReadSettingsFTPServer(XElement root)
        {
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

                XElement xelementDirectory = xelementFtpServer.Element((XName)"Directory");
                if (xelementDirectory != null)
                    SettingsService.FtpServerSettings.Directory = xelementDirectory.Value;

                XElement xelementFileName = xelementFtpServer.Element((XName)"FileName");
                if (xelementFileName != null)
                    SettingsService.FtpServerSettings.FileName = xelementFileName.Value;
            }

        }

    }
}