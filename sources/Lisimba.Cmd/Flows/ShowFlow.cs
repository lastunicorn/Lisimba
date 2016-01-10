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
using System.Globalization;
using System.Linq;
using DustInTheWind.Lisimba.Cmd.Business;
using DustInTheWind.Lisimba.Cmd.Common;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class ShowFlow : IFlow
    {
        private readonly Command command;
        private readonly AddressBooks addressBooks;
        private readonly ShowFlowConsole console;

        public ShowFlow(Command command, AddressBooks addressBooks, ShowFlowConsole console)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.command = command;
            this.addressBooks = addressBooks;
            this.console = console;
        }

        public void Execute()
        {
            if (addressBooks.Current != null)
            {
                if (command.HasParameters)
                    DisplayContactDetails(command[1]);
                else
                    DisplayAllContacts();
            }
            else
            {
                console.DisplayNoAddressBookMessage();
            }
        }

        private void DisplayContactDetails(string contactName)
        {
            IEnumerable<Contact> contacts = GetContacts(contactName);

            foreach (Contact contact in contacts)
            {
                console.DisplayContactDetails(contact);
            }
        }

        private IEnumerable<Contact> GetContacts(string contactName)
        {
            return addressBooks.Current.AddressBook.Contacts
                .Where(x =>
                    (x.Name.FirstName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.FirstName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.MiddleName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.MiddleName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.LastName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.LastName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.Nickname != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.Nickname, contactName, CompareOptions.IgnoreCase) >= 0));
        }

        private void DisplayAllContacts()
        {
            foreach (Contact contact in addressBooks.Current.AddressBook.Contacts)
            {
                console.DisplayContactShort(contact);
            }
        }
    }
}