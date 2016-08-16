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
using DustInTheWind.Lisimba.Business.Comparison;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
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

            AddressBook currentaddressBook = openedAddressBooks.Current.AddressBook;

            AddressBookComparison addressBookComparison = new AddressBookComparison(currentaddressBook, addressBook);
            addressBookComparison.Compare();

            bool displayDetails = parameters.Count >= 2 && (parameters[1] == "-d" || parameters[1] == "--details");

            DisplayResult(addressBookComparison, displayDetails);
        }

        private void DisplayResult(AddressBookComparison addressBookComparison, bool displayDetails)
        {
            DisplayIdenticalContacts(addressBookComparison, displayDetails);

            if (displayDetails)
                console.WriteLine();

            DisplayContactsOnlyInLeft(addressBookComparison, displayDetails);

            if (displayDetails)
                console.WriteLine();

            DisplayContactsOnlyInRight(addressBookComparison, displayDetails);

            if (displayDetails)
                console.WriteLine();

            DisplaySimilarContacts(addressBookComparison, displayDetails);

            if (displayDetails)
                console.WriteLine();

            DisplayDifferentContacts(addressBookComparison, displayDetails);
        }

        private void DisplayIdenticalContacts(AddressBookComparison addressBookComparison, bool displayDetails)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.Equal)
                .ToList();

            console.WriteLineEmphasize("Identical: " + contactComparisons.Count);

            if (displayDetails)
                foreach (ContactComparison contactComparison in contactComparisons)
                    console.WriteLineNormal(contactComparison.ContactLeft.ToString());
        }

        private void DisplayContactsOnlyInLeft(AddressBookComparison addressBookComparison, bool displayDetails)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.LeftExists)
                .ToList();

            console.WriteLineEmphasize("Unique in left: " + contactComparisons.Count);

            if (displayDetails)
                foreach (ContactComparison contactComparison in contactComparisons)
                    console.WriteLineNormal(contactComparison.ContactLeft.ToString());
        }

        private void DisplayContactsOnlyInRight(AddressBookComparison addressBookComparison, bool displayDetails)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.RightExists)
                .ToList();

            console.WriteLineEmphasize("Unique in right: " + contactComparisons.Count);

            if (displayDetails)
                foreach (ContactComparison comparisonResult in contactComparisons)
                    console.WriteLineNormal(comparisonResult.ContactRight.ToString());
        }

        private void DisplaySimilarContacts(AddressBookComparison addressBookComparison, bool displayDetails)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.Similar)
                .ToList();

            console.WriteLineEmphasize("Similar: " + contactComparisons.Count);

            if (displayDetails)
                foreach (ContactComparison contactComparison in contactComparisons)
                {
                    string text = string.Format("[{0}] <-> [{1}]", contactComparison.ContactLeft, contactComparison.ContactRight);
                    console.WriteLineNormal(text);
                }
        }

        private void DisplayDifferentContacts(AddressBookComparison addressBookComparison, bool displayDetails)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.Different)
                .ToList();

            console.WriteLineEmphasize("Different: " + contactComparisons.Count);

            if (displayDetails)
                foreach (ContactComparison contactComparison in contactComparisons)
                {
                    string text = string.Format("[{0}] <-> [{1}]", contactComparison.ContactLeft, contactComparison.ContactRight);
                    console.WriteLineNormal(text);
                }
        }
    }
}