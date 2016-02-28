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
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.CommandLine.Properties;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.GateModel;

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

            AddressBook addressBook = gate.Load(parameters[0]);

            Compare(openedAddressBooks.Current.AddressBook, addressBook);
        }

        private void Compare(AddressBook addressBook1, AddressBook addressBook2)
        {
            //ComparisonResult result = new ComparisonResult();
            List<Contact> identicalContacts = new List<Contact>();
            List<Contact> unique1Contacts = new List<Contact>();
            List<Contact> unique2Contacts = new List<Contact>();

            foreach (Contact contact in addressBook1.Contacts)
            {
                if (addressBook2.Contacts.Contains(contact))
                    identicalContacts.Add(contact);
                else
                    unique1Contacts.Add(contact);
            }

            foreach (Contact contact in addressBook2.Contacts)
            {
                if (!addressBook1.Contacts.Contains(contact))
                    unique2Contacts.Add(contact);
            }

            console.WriteLineNormal("Identical: " + identicalContacts.Count);

            foreach (Contact contact in identicalContacts)
                console.WriteLineNormal(contact.ToString());
            
            console.WriteLine();
            console.WriteLineNormal("unique1: " + unique1Contacts.Count);

            foreach (Contact contact in unique1Contacts)
                console.WriteLineNormal(contact.ToString());
            
            console.WriteLine();
            console.WriteLineNormal("unique2: " + unique2Contacts.Count);

            foreach (Contact contact in unique2Contacts)
                console.WriteLineNormal(contact.ToString());
        }
    }

    internal struct ComparisonResult
    {
        public int IdenticalCount { get; set; }
        public int Unique1Count { get; set; }
        public int Unique2Count { get; set; }
    }
}