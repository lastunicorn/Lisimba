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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.CommandModel;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.CommandLine.Properties;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    class ShowFlow : IFlow
    {
        private readonly ConsoleCommand consoleCommand;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly EnhancedConsole console;

        public ShowFlow(ConsoleCommand consoleCommand, OpenedAddressBooks openedAddressBooks, EnhancedConsole console)
        {
            if (consoleCommand == null) throw new ArgumentNullException("consoleCommand");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.consoleCommand = consoleCommand;
            this.openedAddressBooks = openedAddressBooks;
            this.console = console;
        }

        public void Execute()
        {
            if (openedAddressBooks.Current != null)
            {
                if (consoleCommand.HasParameters)
                    DisplayContactDetails(consoleCommand[1]);
                else
                    DisplayAllContacts();
            }
            else
            {
                console.WriteLineError(Resources.NoAddessBookOpenedError);
            }
        }

        private void DisplayContactDetails(string contactName)
        {
            IEnumerable<Contact> contacts = GetContacts(contactName);

            foreach (Contact contact in contacts)
            {
                console.WriteLineNormal(contact.Name.ToString());
            }
        }

        private IEnumerable<Contact> GetContacts(string contactName)
        {
            return openedAddressBooks.Current.AddressBook.Contacts
                .Where(x =>
                    (x.Name.FirstName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.FirstName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.MiddleName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.MiddleName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.LastName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.LastName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.Nickname != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.Nickname, contactName, CompareOptions.IgnoreCase) >= 0));
        }

        private void DisplayAllContacts()
        {
            foreach (Contact contact in openedAddressBooks.Current.AddressBook.Contacts)
            {
                console.WriteLineNormal(contact.ToString());
            }
        }
    }
}