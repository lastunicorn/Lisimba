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
using DustInTheWind.ConsoleCommon.ObservingModel;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Observers
{
    internal class AddressBookClosingObserver : IObserver
    {
        private readonly EnhancedConsole console;
        private readonly OpenedAddressBooks openedAddressBooks;

        public AddressBookClosingObserver(EnhancedConsole console, OpenedAddressBooks openedAddressBooks)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.console = console;
            this.openedAddressBooks = openedAddressBooks;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookClosing += HandleAddressBookClosing;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookClosing -= HandleAddressBookClosing;
        }

        private void HandleAddressBookClosing(object sender, AddressBookClosingEventArgs e)
        {
            if (e.AddressBook.Status == AddressBookStatus.Modified)
            {
                bool? needToSave = console.AskYesNoCancelQuestion(Resources.AskToSaveAddressBook);

                if (needToSave == null)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.SaveAddressBook = needToSave.Value;
                }
            }
            else
            {
                e.SaveAddressBook = false;
            }
        }
    }
}