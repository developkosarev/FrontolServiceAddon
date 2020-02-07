using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;

using System.Net;
using FrontolService.DAL;
using SharpUpdate;
using System.Reflection;

namespace FrontolServiceAddon
{
    public partial class MainForm : Form
    {
        #region Fields

        private bool closing = true;
        private string CommandLineArg1 = "";
        //private FrontolLib frontollib;                
        private UploadFacade uploadFacade;
        private StoreContext storeContext;

        private SharpUpdater updater;
        private static String exe_dir = Directory.GetCurrentDirectory();
        private static String exe_path = Path.Combine(exe_dir, @"FrontolServiceAddon.exe");

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            Logger.InitLogger();//инициализация - требуется один раз в начале
            Logger.Log.Info("Запуск программы!");

            notifyIcon = new NotifyIcon();            
            notifyIcon.Icon = Properties.Resources.MainIcon;
            notifyIcon.Visible = true;            
            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            notifyIcon.ContextMenuStrip = contextMenuStrip;

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;

            SettingsService.ReadSettings();

            //frontollib = new FrontolLib();            
            uploadFacade = new UploadFacade();
            storeContext = new StoreContext(SettingsService.FirebirdSettings.Database, SettingsService.FirebirdSettings.Login, SettingsService.FirebirdSettings.Password);

            this.Text = this.Text + " " + ProductVersion;

            updater = new SharpUpdater(Assembly.GetExecutingAssembly(), this, new Uri("http://frontol.chance-ltd.ru/FrontolServiceAddon/FrontolServiceAddon.xml"));
            updater.DoUpdate();

            timerMain.Enabled = true;
        }

        #endregion

        #region Form

        private void MainForm_Shown(object sender, EventArgs e)
        {
            string[] keys = Environment.GetCommandLineArgs();

            if (keys.Length > 1)
                if (keys[1] == "/q")
                {
                    CommandLineArg1 = keys[1];                    

                    this.Close();
                }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (System.Diagnostics.Debugger.IsAttached) 
            {
                return;
            };                        

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = closing;
                this.Hide();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
        }

        #endregion

        #region NotifyIcon

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {            
            MessageBox.Show("Вы нажали на подсказку");
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {            
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left) {
                MessageBox.Show("Вы нажали на значек программы");
                this.Show();
            }
        }
                
        private void ShowWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            closing = false;            
            Application.Exit();
        }

        #endregion

        #region Menu

        private void MainMenuItemExit_Click(object sender, EventArgs e)
        {            
            Application.Exit();
        }

        private void MainMenuItemProfile_Click(object sender, EventArgs e)
        {
            ProfileForm newForm = new ProfileForm();
            newForm.ShowDialog(this);
        }

        private void MainMenuItemMakeUpdateFile_Click(object sender, EventArgs e)
        {
            string location = exe_path.Replace(".exe", ".xml");
            Uri uriFolder = new Uri("http://frontol.chance-ltd.ru/FrontolServiceAddon/");

            List<string> files = new List<string>
            {
                "FrontolServiceAddon.exe",
                "FrontolService.DAL.dll",
                "SharpUpdate.dll"
            };

            SharpUpdateXml.SaveSharpUpdateXml(location, uriFolder, files);

        }

        #endregion

        #region Events

        #region Timers

        private void timerMain_Tick(object sender, EventArgs e)
        {
            Logger.Log.Info("Start timer");

            if (System.Environment.MachineName == "ICORE7")
            {
                //return;
            }

            string filenamesprt = uploadFacade.sprtFile; //frontollib.filenamesprt;
            FileInfo fileInf = new FileInfo(Application.StartupPath.ToString() + "\\" + filenamesprt + ".zip");

            if (fileInf.Exists)
            {
                Logger.Log.Info( string.Format("Имя файла: {0}", fileInf.Name) );
                Logger.Log.Info( string.Format("Время создания: {0}", fileInf.CreationTime) );
                Logger.Log.Info( string.Format("Время изменения: {0}", fileInf.LastWriteTime) );
                Logger.Log.Info( string.Format("Размер: {0}", fileInf.Length) );

                if (fileInf.LastWriteTime.AddHours(6) < DateTime.Now) //Файл создан более 6 часов назад
                {
                    RunWorker();
                }
            }
            else {
                RunWorker();
            }

            Logger.Log.Info("Finish timer");
        }

        #endregion

        #region BackgroundWorker

        private void RunWorker()
        {
            if (backgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker.RunWorkerAsync();                                
            }
        }

        private void CancelWorker()
        {
            if (backgroundWorker.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker.CancelAsync();
            }
        }

        private void ToolStripMenuItemExecute_Click(object sender, EventArgs e)
        {
            RunWorker();
            
            ToolStripMenuItemExecute.Enabled = false;
            toolStripProgressBar1.Visible = true;
        }

        private void ToolStripMenuItemCancel_Click(object sender, EventArgs e)
        {
            CancelWorker();
        }
        
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            storeContext.OpenConnection();

            for (int i = 1; i <= 1000; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    //Logger.Log.Info("Старт итерации " + String.Format("{0:d}", i));
                    //frontollib.FillListRemain();
                    
                    storeContext.DeleteRemain();                    

                    //Logger.Log.Info("Конец итерации " + String.Format("{0:d}", i));

                    worker.ReportProgress(i / 10);

                    if (i % 10 == 0)
                    {
                        Logger.Log.Info("Конец итерации " + (i /10) + "% ");
                    }
                }
            }

            storeContext.CloseConnection();

            if (!e.Cancel) {
                Logger.Log.Info("Старт выгрузки UploadFileSprt");
                uploadFacade.UploadFileSprt(Application.StartupPath.ToString());
                Logger.Log.Info("Финиш выгрузки UploadFileSprt");

                storeContext.OpenConnection();
                storeContext.DeleteRemaindCollapsed();
                storeContext.DeleteReports();
                storeContext.CloseConnection();


                //Выгрузка файла остатков
                //frontollib.CreateFileSprt(Application.StartupPath.ToString());

                //Удалить свертку
                //frontollib.RemoveRemaindCollapsed();
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {            
            toolStripStatusLabel1.Text = (e.ProgressPercentage.ToString() + "%");

            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripProgressBar1.ToolTipText = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {                
                toolStripStatusLabel1.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                toolStripStatusLabel1.Text = "Error: " + e.Error.Message;                
            }
            else
            {
                toolStripStatusLabel1.Text = "Done!";                

                notifyIcon.BalloonTipTitle = "Завершение свертки";
                notifyIcon.BalloonTipText = "Завершена свертка";
                notifyIcon.ShowBalloonTip(30);
            }

            ToolStripMenuItemExecute.Enabled = true;
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Value = 0;
        }

        #endregion

        #endregion
        
    }
}
