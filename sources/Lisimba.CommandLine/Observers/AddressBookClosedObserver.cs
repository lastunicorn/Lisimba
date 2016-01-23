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
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.CommandLine.Properties;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;

namespace DustInTheWind.Lisimba.CommandLine.Observers
{
    class AddressBookClosedObserver : IObserver
    {
        private readonly EnhancedConsole console;
        private readonly OpenedAddressBooks openedAddressBooks;

        public AddressBookClosedObserver(EnhancedConsole console, OpenedAddressBooks openedAddressBooks)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.console = console;
            this.openedAddressBooks = openedAddressBooks;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookClosed += HandleAddressBookClosed;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookClosed -= HandleAddressBookClosed;
        }

        private void HandleAddressBookClosed(object sender, AddressBookClosedEventArgs e)
        {
            string addressBookName = e.AddressBookShell.GetFriendlyName();
            string text = string.Format(Resources.AddressBookClosedSuccess, addressBookName);
            
            console.WriteLineSuccess(text);
        }
    }
}