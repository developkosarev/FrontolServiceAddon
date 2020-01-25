using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SettingsService.FirebirdSettings.Database = textBox1.Text;
            SettingsService.FirebirdSettings.Login = textBox2.Text;
            SettingsService.FirebirdSettings.Password = textBox3.Text;

            SettingsService.FtpServerSettings.Url = textBox4.Text;
            SettingsService.FtpServerSettings.Login = textBox5.Text;
            SettingsService.FtpServerSettings.Password = textBox6.Text;

            SettingsService.SaveSettings();            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
