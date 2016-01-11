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

namespace DustInTheWind.Lisimba.Cmd.Observers
{
    class AddressBookOpenObserver : AddressBookObserver
    {
        private readonly AddressBookOpenObserverConsole console;

        public AddressBookOpenObserver(OpenedAddressBooks openedAddressBooks, AddressBookOpenObserverConsole console)
            : base(openedAddressBooks)
        {
            if (console == null) throw new ArgumentNullException("console");
            this.console = console;
        }

        public override void Start()
        {
            OpenedAddressBooks.AddressBookOpened += HandleAddressBookOpened;
        }

        private void HandleAddressBookOpened(object sender, AddressBookOpenedEventArgs e)
        {
            DisplayOpenSuccessMessage();
            DisplayWarnings(e.Result.Warnings);
        }

        private void DisplayOpenSuccessMessage()
        {
            if (OpenedAddressBooks.Current != null)
            {
                if (OpenedAddressBooks.Current.Status == AddressBookStatus.New)
                {
                    string addressBookName = OpenedAddressBooks.Current.GetFriendlyName();
                    console.DisplayAddressBookCreateSuccess(addressBookName);
                }
                else
                {
                    string addressBookFileName = OpenedAddressBooks.Current.Location;
                    int contactsCount = OpenedAddressBooks.Current.AddressBook.Contacts.Count;

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