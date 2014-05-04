using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System;
using System.Windows.Forms;

namespace DustInTheWind.Desmond
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Arguments arguments = new Arguments(args);

            string startAs = arguments["startas"];
            if (startAs != null && startAs.ToLower().Equals("app"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DesmondForm());
            }
            else if (startAs == null || startAs.ToLower().Equals("service"))
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new DesmondService() };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}