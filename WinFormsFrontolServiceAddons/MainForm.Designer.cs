namespace FrontolServiceAddon
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbLog = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.bbGroup = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bbCancel = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.bbFTPUpload = new System.Windows.Forms.Button();
            this.ButtonExecute = new System.Windows.Forms.Button();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mainMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuItemProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(12, 27);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(226, 121);
            this.lbLog.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showWindowToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(224, 48);
            // 
            // showWindowToolStripMenuItem
            // 
            this.showWindowToolStripMenuItem.Name = "showWindowToolStripMenuItem";
            this.showWindowToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.showWindowToolStripMenuItem.Text = "Показать окно программы";
            this.showWindowToolStripMenuItem.Click += new System.EventHandler(this.ShowWindowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.Interval = 60000;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // bbGroup
            // 
            this.bbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bbGroup.Location = new System.Drawing.Point(244, 98);
            this.bbGroup.Name = "bbGroup";
            this.bbGroup.Size = new System.Drawing.Size(75, 23);
            this.bbGroup.TabIndex = 4;
            this.bbGroup.Text = "Свернуть";
            this.bbGroup.UseVisualStyleBackColor = true;
            this.bbGroup.Click += new System.EventHandler(this.bbGroup_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // bbCancel
            // 
            this.bbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bbCancel.Location = new System.Drawing.Point(244, 125);
            this.bbCancel.Name = "bbCancel";
            this.bbCancel.Size = new System.Drawing.Size(75, 23);
            this.bbCancel.TabIndex = 5;
            this.bbCancel.Text = "Отмена";
            this.bbCancel.UseVisualStyleBackColor = true;
            this.bbCancel.Click += new System.EventHandler(this.bbCancel_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resultLabel.Location = new System.Drawing.Point(12, 154);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(307, 14);
            this.resultLabel.TabIndex = 6;
            this.resultLabel.Text = "0%";
            // 
            // bbFTPUpload
            // 
            this.bbFTPUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bbFTPUpload.Enabled = false;
            this.bbFTPUpload.Location = new System.Drawing.Point(244, 52);
            this.bbFTPUpload.Name = "bbFTPUpload";
            this.bbFTPUpload.Size = new System.Drawing.Size(75, 23);
            this.bbFTPUpload.TabIndex = 3;
            this.bbFTPUpload.Text = "Отправить на FTP";
            this.bbFTPUpload.UseVisualStyleBackColor = true;
            this.bbFTPUpload.Click += new System.EventHandler(this.FTPUpload_Click);
            // 
            // ButtonExecute
            // 
            this.ButtonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonExecute.Enabled = false;
            this.ButtonExecute.Location = new System.Drawing.Point(244, 27);
            this.ButtonExecute.Name = "ButtonExecute";
            this.ButtonExecute.Size = new System.Drawing.Size(75, 23);
            this.ButtonExecute.TabIndex = 0;
            this.ButtonExecute.Text = "Выполнить";
            this.ButtonExecute.UseVisualStyleBackColor = true;
            this.ButtonExecute.Click += new System.EventHandler(this.ButtonExecute_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuItemFile,
            this.mainMenuItemProfile});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(322, 24);
            this.menuMain.TabIndex = 7;
            this.menuMain.Text = "mainMenu";
            // 
            // mainMenuItemFile
            // 
            this.mainMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuItemExit});
            this.mainMenuItemFile.Name = "mainMenuItemFile";
            this.mainMenuItemFile.Size = new System.Drawing.Size(48, 20);
            this.mainMenuItemFile.Text = "Файл";
            // 
            // mainMenuItemExit
            // 
            this.mainMenuItemExit.Name = "mainMenuItemExit";
            this.mainMenuItemExit.Size = new System.Drawing.Size(109, 22);
            this.mainMenuItemExit.Text = "Выход";
            this.mainMenuItemExit.Click += new System.EventHandler(this.MainMenuItemExit_Click);
            // 
            // mainMenuItemProfile
            // 
            this.mainMenuItemProfile.Name = "mainMenuItemProfile";
            this.mainMenuItemProfile.Size = new System.Drawing.Size(79, 20);
            this.mainMenuItemProfile.Text = "Настройки";
            this.mainMenuItemProfile.Click += new System.EventHandler(this.MainMenuItemProfile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 177);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.bbCancel);
            this.Controls.Add(this.bbGroup);
            this.Controls.Add(this.bbFTPUpload);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.ButtonExecute);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainForm";
            this.Text = "Свертка остатков";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Button bbGroup;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button bbCancel;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Button bbFTPUpload;
        private System.Windows.Forms.Button ButtonExecute;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mainMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem mainMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem mainMenuItemProfile;
    }
}

