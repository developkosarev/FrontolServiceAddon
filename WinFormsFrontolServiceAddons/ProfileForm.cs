using System;
using System.Windows.Forms;

namespace FrontolServiceAddon
{
    public partial class ProfileForm : Form
    {
        public ProfileForm()
        {
            InitializeComponent();
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = SettingsService.FirebirdSettings.Database;
            textBox2.Text = SettingsService.FirebirdSettings.Login;
            textBox3.Text = SettingsService.FirebirdSettings.Password;

            textBox4.Text = SettingsService.FtpServerSettings.Url;
            textBox5.Text = SettingsService.FtpServerSettings.Login;
            textBox6.Text = SettingsService.FtpServerSettings.Password;
            textBox8.Text = SettingsService.FtpServerSettings.Directory;
            textBox9.Text = SettingsService.FtpServerSettings.FileName;

            textBox7.Text = SettingsService.TaskSettings.Interval.ToString();
            checkBox1.Checked = SettingsService.TaskSettings.CollapseRemaind;
            checkBox2.Checked = SettingsService.TaskSettings.DeleteRemaindCollapsed;
            checkBox3.Checked = SettingsService.TaskSettings.SendToFtp;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SettingsService.FirebirdSettings.Database = textBox1.Text;
            SettingsService.FirebirdSettings.Login = textBox2.Text;
            SettingsService.FirebirdSettings.Password = textBox3.Text;

            SettingsService.FtpServerSettings.Url = textBox4.Text;
            SettingsService.FtpServerSettings.Login = textBox5.Text;
            SettingsService.FtpServerSettings.Password = textBox6.Text;
            SettingsService.FtpServerSettings.Directory = textBox8.Text;
            SettingsService.FtpServerSettings.FileName = textBox9.Text;
            
            SettingsService.TaskSettings.Interval = int.Parse(textBox7.Text);
            SettingsService.TaskSettings.CollapseRemaind = checkBox1.Checked;
            SettingsService.TaskSettings.DeleteRemaindCollapsed = checkBox2.Checked;
            SettingsService.TaskSettings.SendToFtp = checkBox3.Checked;

            SettingsService.SaveSettings();            
        }
        
    }
}
