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
using DustInTheWind.Lisimba.Wpf.Commands;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class StartViewModel : ViewModelBase
    {
        public NewAddressBookCommand NewAddressBookCommand { get; private set; }
        public OpenAddressBookCommand OpenAddressBookCommand { get; private set; }

        public StartViewModel(NewAddressBookCommand newAddressBookCommand, OpenAddressBookCommand openAddressBookCommand)
        {
            if (newAddressBookCommand == null) throw new ArgumentNullException("newAddressBookCommand");
            if (openAddressBookCommand == null) throw new ArgumentNullException("openAddressBookCommand");

            NewAddressBookCommand = newAddressBookCommand;
            OpenAddressBookCommand = openAddressBookCommand;
        }
    }
}
