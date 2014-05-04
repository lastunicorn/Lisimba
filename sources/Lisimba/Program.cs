using System;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Forms;

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
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLisimba(arguments.FileName));
		}
	}
}