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
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        private readonly AvailableGates availableGates;

        public AddressBookGuarder(AddressBookGuarderConsole console, OpenedAddressBooks openedAddressBooks, AvailableGates availableGates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.console = console;
            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookOpened += HandleAddressBookOpened;
            openedAddressBooks.AddressBookSaved += HandleAddressBookSaved;
            openedAddressBooks.AddressBookClosing += HandleAddressBookClosing;
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
                    console.DisplayAddressBookCreateSuccess();
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
            StringBuilder sb = new StringBuilder();

            foreach (Exception warning in warnings)
            {
                sb.AppendLine(warning.Message);
                sb.AppendLine();
            }

            if (sb.Length > 0)
                console.DisplayWarning(sb.ToString());
        }

        private void HandleAddressBookSaved(object sender, EventArgs e)
        {
            console.DisplayAddressBookSaveSuccess(openedAddressBooks.Current.GetFriendlyName(), openedAddressBooks.Current.Location);
        }

        private void HandleAddressBookClosing(object sender, CancelEventArgs e)
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

                if (openedAddressBooks.Current.Gate == null)
                    openedAddressBooks.Current.SaveAddressBook(newLocation, availableGates.DefaultGate);
                else
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
