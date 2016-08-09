using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.Views
{
    /// <summary>
    /// Interaction logic for PhoneViewer.xaml
    /// </summary>
    public partial class PhoneViewer : UserControl
    {
        public PhoneViewer()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonDescription.Visibility = Visibility.Collapsed;
            LabelDescription.Visibility = Visibility.Visible;
            TextBoxDescription.Visibility = Visibility.Visible;
            TextBoxDescription.Focus();
        }
    }
}
