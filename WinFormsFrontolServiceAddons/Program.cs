using System;
using System.Threading;
using System.Windows.Forms;

namespace FrontolServiceAddon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool onlyInstance;

            Mutex mtx = new Mutex(true, "FrontolServiceAddon", out onlyInstance); // используйте имя вашего приложения

            // Если другие процессы не владеют мьютексом, то
            // приложение запущено в единственном экземпляре
            if (onlyInstance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                //MessageBox.Show("Приложение уже запущено","Сообщение",MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
        }
    }
}
