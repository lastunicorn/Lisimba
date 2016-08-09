// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.Views
{
    /// <summary>
    /// Interaction logic for BirthdayViewer.xaml
    /// </summary>
    public partial class BirthdayViewer : UserControl
    {
        public Date Date
        {
            get { return (Date)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(Date), typeof(BirthdayViewer), new PropertyMetadata(new Date(0, 0, 0)));

        public BirthdayViewer()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
        }

        private void Label1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Label1.Style = FindResource("HoverModeStyle") as Style;
        }

        private void Label1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Label1.Style = FindResource("NormalModeStyle") as Style;
        }

        private void Label1_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EnableEditMode();
        }

        private void TextBox1_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SaveChanges();

            if (IsValid(TextBox1))
                EnableReadOnlyMode();
        }

        private bool IsValid(DependencyObject obj)
        {
            // The dependency object is valid if it has no errors and all
            // of its children (that are dependency objects) are error-free.
            return !Validation.GetHasError(obj) &&
            LogicalTreeHelper.GetChildren(obj)
            .OfType<DependencyObject>()
            .All(IsValid);
        }

        private void TextBox1_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    SaveChanges();

                    if (IsValid(TextBox1))
                        EnableReadOnlyMode();
                    break;

                case Key.Escape:
                    RejectChanges();
                    EnableReadOnlyMode();
                    break;
            }
        }

        private void SaveChanges()
        {
            BindingExpression binding = TextBox1.GetBindingExpression(TextBox.TextProperty);

            if (binding != null)
                binding.UpdateSource();
        }

        private void RejectChanges()
        {
            BindingExpression binding = TextBox1.GetBindingExpression(TextBox.TextProperty);

            if (binding != null)
                binding.UpdateTarget();
        }

        private void EnableEditMode()
        {
            Label1.Visibility = Visibility.Collapsed;
            TextBox1.Visibility = Visibility.Visible;

            TextBox1.SelectAll();
            TextBox1.Focus();
        }

        private void EnableReadOnlyMode()
        {
            Label1.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Collapsed;
        }
    }
}
