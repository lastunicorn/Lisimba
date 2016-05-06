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

using System;
using System.Windows;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    /// <summary>
    /// Interaction logic for AddressBookPropertiesWindow.xaml
    /// </summary>
    internal partial class AddressBookPropertiesWindow : Window
    {
        public AddressBookPropertiesWindow(AddressBookPropertiesViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            InitializeComponent();

            Loaded += (sender, e) => DataContext = viewModel;
        }

        private void HandleButtonOkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}