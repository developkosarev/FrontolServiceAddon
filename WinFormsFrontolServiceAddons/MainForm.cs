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
using FtpClient;

using System.Net;

namespace FrontolServiceAddon
{
    public partial class MainForm : Form
    {
        private bool closing = true;
        private string CommandLineArg1 = "";
        private FrontolLib frontollib;

        public MainForm()
        {
            InitializeComponent();

            notifyIcon1 = new NotifyIcon();
            //notifyIcon1.Icon = SystemIcons.Asterisk;            
            notifyIcon1.Icon = Properties.Resources.MainIcon;
            notifyIcon1.Visible = true;            
            notifyIcon1.Click += NotifyIcon1_Click;
            notifyIcon1.BalloonTipClicked += NotifyIcon1_BalloonTipClicked;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            SettingsService.ReadSettings();

            frontollib = new FrontolLib();

            Logger.InitLogger();//инициализация - требуется один раз в начале
            Logger.Log.Info("Запуск программы!");            
        }

        #region form

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MessageBox.Show("Вы нажали на подсказку");
        }

        private void NotifyIcon1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left) {
                MessageBox.Show("Вы нажали на значек программы");
                this.Show();
            }
        }

        
        private void MainForm_Shown(object sender, EventArgs e)
        {

            string[] keys = Environment.GetCommandLineArgs();

            if (keys.Length > 1)
                if (keys[1] == "/u")
                {

                    CommandLineArg1 = keys[1];

                    //ButtonExecute_Click(ButtonExecute, null);

                    //this.Close();
                }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            //messageBoxCS.AppendFormat("{0} = {1}", "CloseReason", e.CloseReason);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "Cancel", e.Cancel);
            //messageBoxCS.AppendLine();            

            //Logger.Log.Info( messageBoxCS.ToString() );


            if (System.Environment.MachineName == "ICORE7")
            {
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = closing;
                this.Hide();
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


        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            Logger.Log.Info("Запуск выгрузки в офис");

            notifyIcon1.BalloonTipTitle = "Выгрузка";
            notifyIcon1.BalloonTipText = "Выгрузка данных в офис";
            notifyIcon1.ShowBalloonTip(30);            

            
            if (!frontollib.DbExist)
            {
                Logger.Log.Info("Не найден файл базы данных");
            }

            frontollib.CreateFileSprt(Application.StartupPath.ToString());

            //Close forms            
            if (CommandLineArg1 == "/u")
            {
                Application.Exit();
            }

        }
        
        private void FTPUpload_Click(object sender, EventArgs e)
        {
            string result = "";

            try
            {
                result = frontollib.UploadFileSprt(Application.StartupPath.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ": \n" + ex.Message);
            }

            lbLog.Items.Add(result);
        }
        

        private void Timer1_Tick(object sender, EventArgs e)
        {

            Logger.Log.Info("Start timer");

            if (System.Environment.MachineName == "ICORE7")
            {
                //return;
            }
            

            string filenamesprt = frontollib.filenamesprt;
            FileInfo fileInf = new FileInfo(Application.StartupPath.ToString() + "\\" + filenamesprt + ".zip");

            if (fileInf.Exists)
            {
                Logger.Log.Info( string.Format("Имя файла: {0}", fileInf.Name) );
                Logger.Log.Info( string.Format("Время создания: {0}", fileInf.CreationTime) );
                Logger.Log.Info( string.Format("Время изменения: {0}", fileInf.LastWriteTime) );
                Logger.Log.Info( string.Format("Размер: {0}", fileInf.Length) );

                if (fileInf.LastWriteTime.AddHours(6) < DateTime.Now) //Файл создан более 6 часов назад
                {
                    bbGroup_Click(bbGroup, null);

                    //lbLog.Items.Add("Необходимо выгрузить файл");
                    //ButtonExecute_Click(ButtonExecute, null);
                }
            }
            else {
                bbGroup_Click(bbGroup, null);

                //ButtonExecute_Click(ButtonExecute, null);
            }

            Logger.Log.Info("Finish timer");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //    // открываем нужную ветку в реестре 
            //    // @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\"

            //    Microsoft.Win32.RegistryKey Key =
            //       Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
            //       "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);

            //    //добавляем первый параметр - название ключа
            //    // Второй параметр - это путь к 
            //    // исполняемому файлу нашей программы.
            //    Key.SetValue("myapp", "D:\\myapp.exe");
            //    Key.Close();

            //private void button1_Click(object sender, EventArgs e)
            //{
            //    // открываем нужную ветку в реестре 
            //    // @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\"

            //    Microsoft.Win32.RegistryKey Key =
            //       Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
            //       "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);

            //    //добавляем первый параметр - название ключа
            //    // Второй параметр - это путь к 
            //    // исполняемому файлу нашей программы.
            //    Key.SetValue("myapp", "D:\\myapp.exe");
            //    Key.Close();
            //}

            //Теперь рассмотрим код кнопки для удаления программы из автозапуска:
            //private void button2_Click(object sender, EventArgs e)
            //{
            //    //удаляем
            //    Microsoft.Win32.RegistryKey key =
            //       Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
            //       "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //    key.DeleteValue("DoLinqToSql", false);
            //    key.Close();
            //}


        }


        #region Group

        private void bbGroup_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void bbCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

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
                    frontollib.FillListRemain();
                    //Logger.Log.Info("Конец итерации " + String.Format("{0:d}", i));


                    // Perform a time consuming operation and report progress.
                    //System.Threading.Thread.Sleep(500);
                    //worker.ReportProgress(i * 10);
                    worker.ReportProgress(i/10);
                }
            }

            //Выгрузка файла остатков
            frontollib.CreateFileSprt(Application.StartupPath.ToString());

            //Удалить свертку
            frontollib.RemoveRemaindCollapsed();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
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

        #endregion

    }
}
