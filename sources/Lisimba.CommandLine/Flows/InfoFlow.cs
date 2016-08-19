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
using System.IO;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.ConsoleCommon.Templating;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.CommandLine.Business;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class InfoFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly EnhancedConsole console;

        public InfoFlow(AddressBooks addressBooks, EnhancedConsole console)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.addressBooks = addressBooks;
            this.console = console;
        }

        public void Execute(IList<string> parameters)
        {
            if (addressBooks.Current != null)
            {
                string addressBookLocation = addressBooks.Current.Location == null
                    ? "< not saved yet >"
                    : Path.GetFullPath(addressBooks.Current.Location);

                DisplayAddressBookInfo(addressBooks.Current.AddressBook, addressBookLocation);
            }
            else
            {
                console.WriteLineError(Resources.NoAddessBookOpenedError);
            }
        }

        public void DisplayAddressBookInfo(AddressBook addressBook, string addressBookLocation)
        {
            string fileName = ViewTemplates.GetFullFileName("AddressBookInfo.t");

            var parameters = new
            {
                AddressBookName = addressBook.Name,
                AddressBookLocation = addressBookLocation,
                ContactCount = addressBook.Contacts.Count
            };

            ConsoleTemplate consoleTemplate = ConsoleTemplate.CreateFromEmbeddedFile(fileName, parameters);
            console.DisplayTemplate(consoleTemplate);
        }
    }
}