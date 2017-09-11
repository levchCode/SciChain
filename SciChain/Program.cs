using System;
using System.IO;
using System.Windows.Forms;

namespace SciChain
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists("user.json"))
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new register());
            }
        }
    }
}
