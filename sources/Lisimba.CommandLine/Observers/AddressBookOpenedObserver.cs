// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using System.Collections.Generic;
using System.Text;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;

namespace DustInTheWind.Lisimba.Cmd.Observers
{
    class AddressBookOpenedObserver : IObserver
    {
        private readonly AddressBookOpenedObserverConsole console;
        private readonly OpenedAddressBooks openedAddressBooks;

        public AddressBookOpenedObserver(AddressBookOpenedObserverConsole console, OpenedAddressBooks openedAddressBooks)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.console = console;
            this.openedAddressBooks = openedAddressBooks;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookOpened += HandleAddressBookOpened;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookOpened -= HandleAddressBookOpened;
        }

        private void HandleAddressBookOpened(object sender, AddressBookOpenedEventArgs e)
        {
            DisplayOpenSuccessMessage();
            DisplayWarnings(e.Result.Warnings);
        }

        private void DisplayOpenSuccessMessage()
        {
            if (openedAddressBooks.Current != null)
            {
                if (openedAddressBooks.Current.Status == AddressBookStatus.New)
                {
                    string addressBookName = openedAddressBooks.Current.GetFriendlyName();
                    console.DisplayAddressBookCreateSuccess(addressBookName);
                }
                else
                {
                    string addressBookFileName = openedAddressBooks.Current.Location;
                    int contactsCount = openedAddressBooks.Current.AddressBook.Contacts.Count;

                    console.DisplayAddressBookOpenSuccess(addressBookFileName, contactsCount);
                }
            }
            else
            {
                console.DisplayNoAddressBookMessage();
            }
        }

        private void DisplayWarnings(IEnumerable<Exception> warnings)
        {
            if (warnings == null)
                return;

            StringBuilder sb = new StringBuilder();

            foreach (Exception warning in warnings)
            {
                sb.AppendLine(warning.Message);
                sb.AppendLine();
            }

            if (sb.Length > 0)
                console.DisplayWarning(sb.ToString());
        }
    }
}