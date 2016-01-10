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
        private readonly AddressBooks addressBooks;

        public AddressBookGuarder(AddressBookGuarderConsole console, AddressBooks addressBooks)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.console = console;
            this.addressBooks = addressBooks;
        }

        public void Start()
        {
            addressBooks.Closing += HandleAddressBooksClosing;
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
            if (addressBooks.Current == null || addressBooks.Current.Status != AddressBookStatus.Modified)
                return true;

            bool? needToSave = console.AskToSaveAddressBook();

            if (needToSave == null)
                return false;

            if (!needToSave.Value)
                return true;

            if (addressBooks.Current.Location == null)
            {
                string newLocation = console.AskForNewLocation();

                if (newLocation == null)
                    return false;

                addressBooks.Current.SaveAddressBook(newLocation);
            }
            else
            {
                addressBooks.Current.SaveAddressBook();
            }

            return true;
        }
    }
}
