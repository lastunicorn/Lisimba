using System;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Commands;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ProgramArguments arguments = null;

            try
            {
                arguments = new ProgramArguments(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ConfigurationService configurationService = new ConfigurationService();
            StatusService statusService = new StatusService { DefaultStatusText = "Ready" };
            RecentFilesService recentFilesService = new RecentFilesService(configurationService);
            CurrentAddressBook currentAddressBook = new CurrentAddressBook(statusService, recentFilesService);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormLisimba formLisimba = new FormLisimba(arguments, configurationService, statusService, recentFilesService, currentAddressBook);
            Application.Run(formLisimba);
        }
    }
}