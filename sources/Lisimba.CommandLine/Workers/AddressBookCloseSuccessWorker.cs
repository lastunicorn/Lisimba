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
    /// This worker displays the success message when a address book is closed.
    /// </summary>
    internal class AddressBookCloseSuccessWorker : IWorker
    {
        private readonly EnhancedConsole console;
        private readonly AddressBooks addressBooks;

        public AddressBookCloseSuccessWorker(EnhancedConsole console, AddressBooks addressBooks)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.console = console;
            this.addressBooks = addressBooks;
        }

        public void Start()
        {
            addressBooks.AddressBookClosed += HandleAddressBookClosed;
        }

        public void Stop()
        {
            addressBooks.AddressBookClosed -= HandleAddressBookClosed;
        }

        private void HandleAddressBookClosed(object sender, AddressBookClosedEventArgs e)
        {
            string addressBookName = e.AddressBookShell.GetFriendlyName();
            string text = string.Format(Resources.AddressBookClosedSuccess, addressBookName);

            console.WriteLineSuccess(text);
        }
    }
}