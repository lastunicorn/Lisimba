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
using System.ComponentModel;
using DustInTheWind.Lisimba.Common;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    /// <summary>
    /// Listens for any address book that is being closed and asks the user if
    /// he wants to save it betfore closing.
    /// </summary>
    class AddressBookGuarder
    {
        private readonly AddressBookGuarderConsole console;
        private readonly OpenedAddressBooks openedAddressBooks;

        public AddressBookGuarder(AddressBookGuarderConsole console, OpenedAddressBooks openedAddressBooks)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.console = console;
            this.openedAddressBooks = openedAddressBooks;
        }

        public void Start()
        {
            openedAddressBooks.Closing += HandleAddressBooksClosing;
        }

        private void HandleAddressBooksClosing(object sender, CancelEventArgs e)
        {
            bool allowToContinue = EnsureAddressBookIsSaved();

            if (!allowToContinue)
                e.Cancel = true;
        }

        /// <returns><c>true</c> if it is allowed to continue; false otherwise.</returns>
        public bool EnsureAddressBookIsSaved()
        {
            if (openedAddressBooks.Current == null || openedAddressBooks.Current.Status != AddressBookStatus.Modified)
                return true;

            bool? needToSave = console.AskToSaveAddressBook();

            if (needToSave == null)
                return false;

            if (!needToSave.Value)
                return true;

            if (openedAddressBooks.Current.Location == null)
            {
                string newLocation = console.AskForNewLocation();

                if (newLocation == null)
                    return false;

                openedAddressBooks.Current.SaveAddressBook(newLocation);
            }
            else
            {
                openedAddressBooks.Current.SaveAddressBook();
            }

            return true;
        }
    }
}
