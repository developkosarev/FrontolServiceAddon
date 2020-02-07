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
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mainMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuItemOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuItemProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuItemMakeUpdateFile = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showWindowToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(224, 48);
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
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // timerMain
            // 
            this.timerMain.Interval = 60000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuItemFile,
            this.mainMenuItemOperations,
            this.mainMenuItemProfile,
            this.mainMenuItemHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuMain.Size = new System.Drawing.Size(324, 24);
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
            // mainMenuItemOperations
            // 
            this.mainMenuItemOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemExecute,
            this.ToolStripMenuItemCancel});
            this.mainMenuItemOperations.Name = "mainMenuItemOperations";
            this.mainMenuItemOperations.Size = new System.Drawing.Size(75, 20);
            this.mainMenuItemOperations.Text = "Операции";
            // 
            // ToolStripMenuItemExecute
            // 
            this.ToolStripMenuItemExecute.Name = "ToolStripMenuItemExecute";
            this.ToolStripMenuItemExecute.Size = new System.Drawing.Size(181, 22);
            this.ToolStripMenuItemExecute.Text = "Выполнить свертку";
            this.ToolStripMenuItemExecute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripMenuItemExecute.Click += new System.EventHandler(this.ToolStripMenuItemExecute_Click);
            // 
            // ToolStripMenuItemCancel
            // 
            this.ToolStripMenuItemCancel.Name = "ToolStripMenuItemCancel";
            this.ToolStripMenuItemCancel.Size = new System.Drawing.Size(181, 22);
            this.ToolStripMenuItemCancel.Text = "Отменить свертку";
            this.ToolStripMenuItemCancel.Click += new System.EventHandler(this.ToolStripMenuItemCancel_Click);
            // 
            // mainMenuItemProfile
            // 
            this.mainMenuItemProfile.Name = "mainMenuItemProfile";
            this.mainMenuItemProfile.Size = new System.Drawing.Size(79, 20);
            this.mainMenuItemProfile.Text = "Настройки";
            this.mainMenuItemProfile.Click += new System.EventHandler(this.MainMenuItemProfile_Click);
            // 
            // mainMenuItemHelp
            // 
            this.mainMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuItemMakeUpdateFile});
            this.mainMenuItemHelp.Name = "mainMenuItemHelp";
            this.mainMenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.mainMenuItemHelp.Text = "Help";
            // 
            // MainMenuItemMakeUpdateFile
            // 
            this.MainMenuItemMakeUpdateFile.Name = "MainMenuItemMakeUpdateFile";
            this.MainMenuItemMakeUpdateFile.Size = new System.Drawing.Size(165, 22);
            this.MainMenuItemMakeUpdateFile.Text = "Make Update File";
            this.MainMenuItemMakeUpdateFile.Click += new System.EventHandler(this.MainMenuItemMakeUpdateFile_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 98);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(324, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AccessibleDescription = "";
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Tag = "";
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 120);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainForm";
            this.Text = "Свертка остатков";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.contextMenuStrip.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer timerMain;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mainMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem mainMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem mainMenuItemProfile;
        private System.Windows.Forms.ToolStripMenuItem mainMenuItemOperations;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExecute;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem MainMenuItemMakeUpdateFile;
    }
}

