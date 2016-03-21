using System;
using System.Windows;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void HandleAppStartup(object sender, StartupEventArgs e)
        {
            try
            {
                Bootstrapper bootstrapper = new Bootstrapper();
                bootstrapper.Run(e.Args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LocalizedResources.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}