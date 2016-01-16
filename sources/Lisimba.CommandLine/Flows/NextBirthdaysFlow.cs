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
using System.Linq;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.CommandModel;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Comparers;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class NextBirthdaysFlow : IFlow
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly EnhancedConsole console;

        public NextBirthdaysFlow(OpenedAddressBooks openedAddressBooks, EnhancedConsole console)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.openedAddressBooks = openedAddressBooks;
            this.console = console;
        }

        public void Execute()
        {
            DisplayNextBirthdays();
        }

        private void DisplayNextBirthdays()
        {
            if (openedAddressBooks.Current != null)
            {
                IEnumerable<Contact> contacts = GetContacts();

                foreach (Contact contact in contacts)
                {
                    string text = string.Format("{0} : {1}", contact.Name, contact.Birthday);
                    console.WriteLineNormal(text);
                }
            }
            else
            {
                console.WriteLineError(Resources.NoAddessBookOpenedError);
            }
        }

        private IEnumerable<Contact> GetContacts()
        {
            return openedAddressBooks.Current.AddressBook.Contacts
                .Where(x => x.Birthday != null)
                .Where(x => Date.CompareWithoutYear(x.Birthday, DateTime.Today) >= 0)
                .OrderBy(x => x, new ContactByBirthdayComparer())
                .Take(10);
        }
    }
}