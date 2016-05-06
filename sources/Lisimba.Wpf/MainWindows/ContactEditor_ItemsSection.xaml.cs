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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    /// <summary>
    /// Interaction logic for ContactEditor_ItemsSection.xaml
    /// </summary>
    public partial class ContactEditor_ItemsSection : UserControl
    {
        public ContactEditor_ItemsSection()
        {
            InitializeComponent();
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox == null)
                return;

            listBox.SelectedItem = null;
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled)
                return;

            e.Handled = true;

            MouseWheelEventArgs eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = MouseWheelEvent,
                Source = sender
            };

            UIElement parent = ((Control)sender).Parent as UIElement;

            if (parent != null)
                parent.RaiseEvent(eventArg);
        }
    }
}
