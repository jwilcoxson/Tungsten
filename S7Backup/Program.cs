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

            //Killswitch
            if (System.DateTime.Now > new DateTime(2015, 9, 13))
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
