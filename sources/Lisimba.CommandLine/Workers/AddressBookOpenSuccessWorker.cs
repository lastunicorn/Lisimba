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
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.WorkerModel;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Workers
{
    /// <summary>
    /// When a address book is opened it displays the success message.
    /// </summary>
    internal class AddressBookOpenSuccessWorker : IWorker
    {
        private readonly EnhancedConsole console;
        private readonly AddressBooks addressBooks;

        public AddressBookOpenSuccessWorker(EnhancedConsole console, AddressBooks addressBooks)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.console = console;
            this.addressBooks = addressBooks;
        }

        public void Start()
        {
            addressBooks.AddressBookOpened += HandleAddressBookOpened;
        }

        public void Stop()
        {
            addressBooks.AddressBookOpened -= HandleAddressBookOpened;
        }

        private void HandleAddressBookOpened(object sender, AddressBookOpenedEventArgs e)
        {
            DisplayOpenSuccessMessage();
        }

        private void DisplayOpenSuccessMessage()
        {
            if (addressBooks.Current == null)
            {
                console.WriteLineError(Resources.OpenAddressBookUnknownError);
                return;
            }

            if (addressBooks.Current.Status == AddressBookStatus.New)
            {
                string addressBookName = addressBooks.Current.GetFriendlyName();
                string message = string.Format(Resources.NewAddressBookCreatedSuccess, addressBookName);

                console.WriteLineSuccess(message);
            }
            else
            {
                string addressBookFileName = addressBooks.Current.Location;
                int contactsCount = addressBooks.Current.AddressBook.Contacts.Count;
                string message = string.Format(Resources.AddressBookOpenSuccess, contactsCount, addressBookFileName);

                console.WriteLineSuccess(message);
            }
        }
    }
}