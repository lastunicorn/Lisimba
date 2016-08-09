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
using System.Linq;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Business.Importing;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class CompareFlow : IFlow
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly EnhancedConsole console;
        private readonly AvailableGates availableGates;

        public CompareFlow(OpenedAddressBooks openedAddressBooks, EnhancedConsole console, AvailableGates availableGates)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (console == null) throw new ArgumentNullException("console");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.openedAddressBooks = openedAddressBooks;
            this.console = console;
            this.availableGates = availableGates;
        }

        public void Execute(IList<string> parameters)
        {
            if (openedAddressBooks.Current == null)
                throw new LisimbaException(Resources.NoAddessBookOpenedError);

            if (parameters.Count == 0)
                throw new LisimbaException("Specify the address book to compare to.");

            IGate gate = availableGates.GetGate("ZipXmlGate");

            AddressBook addressBook = (gate as FileGate).Load(parameters[0]);

            AddressBookComparer addressBookComparer = new AddressBookComparer(openedAddressBooks.Current.AddressBook, addressBook);
            AddressBookComparisonResult result = addressBookComparer.Compare();
            DisplayResult(result);
        }

        private void DisplayResult(AddressBookComparisonResult result)
        {
            console.WriteLineNormal("Identical: " + result.IdenticalContacts.Count());

            foreach (ContactComparisonResult comparisonResult in result.IdenticalContacts)
                console.WriteLineNormal(comparisonResult.Contact1.ToString());

            console.WriteLine();
            console.WriteLineNormal("unique1: " + result.Unique1Contacts.Count());

            foreach (ContactComparisonResult comparisonResult in result.Unique1Contacts)
                console.WriteLineNormal(comparisonResult.Contact1.ToString());

            console.WriteLine();
            console.WriteLineNormal("unique2: " + result.Unique2Contacts.Count());

            foreach (ContactComparisonResult comparisonResult in result.Unique2Contacts)
                console.WriteLineNormal(comparisonResult.Contact2.ToString());
        }
    }
}