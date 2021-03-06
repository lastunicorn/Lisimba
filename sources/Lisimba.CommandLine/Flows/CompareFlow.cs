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
using DustInTheWind.Lisimba.Business.Comparison.Comparers;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.CommandLine.FlowOptions;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class CompareFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly EnhancedConsole console;
        private readonly Gates gates;
        private CompareFlowOptions options;

        public CompareFlow(AddressBooks addressBooks, EnhancedConsole console, Gates gates)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");
            if (gates == null) throw new ArgumentNullException("gates");

            this.addressBooks = addressBooks;
            this.console = console;
            this.gates = gates;
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");

            if (addressBooks.Current == null)
                throw new LisimbaException(Resources.NoAddessBookOpenedError);

            options = new CompareFlowOptions(parameters);

            IGate gate = gates.GetGate("LisimbaGate");
            AddressBook addressBook = (gate as FileGate).Load(options.AddressBookLocation);

            AddressBook currentaddressBook = addressBooks.Current.AddressBook;

            AddressBookComparison addressBookComparison = new AddressBookComparison(currentaddressBook, addressBook);
            addressBookComparison.Compare();

            DisplayResult(addressBookComparison);
        }

        private void DisplayResult(AddressBookComparison addressBookComparison)
        {
            if (!options.DisplayOnlyDiff)
                DisplayIdenticalContacts(addressBookComparison);

            DisplayContactsOnlyInLeft(addressBookComparison);
            DisplayContactsOnlyInRight(addressBookComparison);
            DisplaySimilarContacts(addressBookComparison);
            DisplayDifferentContacts(addressBookComparison);
        }

        private void DisplayIdenticalContacts(AddressBookComparison addressBookComparison)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.Equal)
                .ToList();

            console.WriteLineEmphasize("Identical: " + contactComparisons.Count);

            if (options.DisplayDetails)
            {
                foreach (ContactComparison contactComparison in contactComparisons)
                    console.WriteLineNormal(contactComparison.ValueLeft.ToString());

                console.WriteLine();
            }
        }

        private void DisplayContactsOnlyInLeft(AddressBookComparison addressBookComparison)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.LeftExists)
                .ToList();

            console.WriteLineEmphasize("Unique in left: " + contactComparisons.Count);

            if (options.DisplayDetails)
            {
                foreach (ContactComparison contactComparison in contactComparisons)
                    console.WriteLineNormal(contactComparison.ValueLeft.ToString());

                console.WriteLine();
            }
        }

        private void DisplayContactsOnlyInRight(AddressBookComparison addressBookComparison)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.RightExists)
                .ToList();

            console.WriteLineEmphasize("Unique in right: " + contactComparisons.Count);

            if (options.DisplayDetails)
            {
                foreach (ContactComparison comparisonResult in contactComparisons)
                    console.WriteLineNormal(comparisonResult.ValueRight.ToString());

                console.WriteLine();
            }
        }

        private void DisplaySimilarContacts(AddressBookComparison addressBookComparison)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.Similar)
                .ToList();

            console.WriteLineEmphasize("Similar: " + contactComparisons.Count);

            if (options.DisplayDetails)
            {
                foreach (ContactComparison contactComparison in contactComparisons)
                {
                    console.WriteLineNormal("[{0}] <-> [{1}]", contactComparison.ValueLeft, contactComparison.ValueRight);

                    foreach (IItemComparison itemComparison in contactComparison.Comparisons)
                    {
                        if (options.DisplayOnlyDiff)
                        {
                            bool itemsAreEqual = itemComparison.Equality == ItemEquality.Equal || itemComparison.Equality == ItemEquality.BothEmpty;

                            if (itemsAreEqual)
                                continue;
                        }

                        console.WriteLineNormal("    - {3} - [{0}] <-> [{1}] - {2}", itemComparison.ValueLeft, itemComparison.ValueRight, itemComparison.Equality, itemComparison.GetType().Name);
                    }
                }

                console.WriteLine();
            }
        }

        private void DisplayDifferentContacts(AddressBookComparison addressBookComparison)
        {
            List<ContactComparison> contactComparisons = addressBookComparison.Comparisons
                .Where(x => x.Equality == ItemEquality.Different)
                .ToList();

            console.WriteLineEmphasize("Different: " + contactComparisons.Count);

            if (options.DisplayDetails)
            {
                foreach (ContactComparison contactComparison in contactComparisons)
                {
                    string text = string.Format("[{0}] <-> [{1}]", contactComparison.ValueLeft, contactComparison.ValueRight);
                    console.WriteLineNormal(text);
                }

                console.WriteLine();
            }
        }
    }
}