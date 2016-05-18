using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    /// <summary>
    /// Interaction logic for BirthdayViewer.xaml
    /// </summary>
    public partial class BirthdayViewer : UserControl
    {
        public BirthdayViewer()
        {
            InitializeComponent();
        }

        private void BirthdayLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BirthdayLabel.Visibility = Visibility.Collapsed;
            BirthdayEditor.Visibility = Visibility.Visible;
        }

        private void BirthdayEditor_LostFocus(object sender, RoutedEventArgs e)
        {
            BirthdayLabel.Visibility = Visibility.Visible;
            BirthdayEditor.Visibility = Visibility.Collapsed;
        }
    }
}
