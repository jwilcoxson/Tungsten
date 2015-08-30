using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tungsten
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

            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build);
            DateTime expireDate = buildDate + new TimeSpan(21, 0, 0, 0);

            //Killswitch
            if (System.DateTime.Now > expireDate)
            {
                MessageBox.Show("Sorry, this version of Tungsten Beta has expired. " +
                                "Please visit tungsten-app.xyz to download a new version.");
                Application.Exit();
            }
            else
            {
                Application.Run(new Tungsten());
            }
        }
    }
}
