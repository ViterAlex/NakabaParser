using System;
using System.Windows.Forms;

namespace SiteParser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm = new MainForm();
            Presenter presenter = new Presenter(mainForm);
            Application.Run(mainForm);
        }
    }
}
