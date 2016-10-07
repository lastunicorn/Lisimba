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
using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;

namespace DustInTheWind.Lisimba.Business.Importing.Importers
{
    public class AddressBookImporter
    {
        private readonly AddressBook addressBookDestination;
        private readonly AddressBook addressBookSource;
        private readonly List<ContactImporter> contactImporters;
        private bool isAnalysed;

        public IReadOnlyList<ContactImporter> ImportRules
        {
            get { return contactImporters.AsReadOnly(); }
        }

        public AddressBookImporter(AddressBook addressBookDestination, AddressBook addressBookSource)
        {
            if (addressBookDestination == null) throw new ArgumentNullException("addressBookDestination");
            if (addressBookSource == null) throw new ArgumentNullException("addressBookSource");

            this.addressBookDestination = addressBookDestination;
            this.addressBookSource = addressBookSource;

            contactImporters = new List<ContactImporter>();

            addressBookDestination.Changed += HandleAddressBookDestinationChanged;
            addressBookSource.Changed += HandleAddressBookSourceChanged;
        }

        private void HandleAddressBookDestinationChanged(object sender, EventArgs e)
        {
            isAnalysed = false;
        }

        private void HandleAddressBookSourceChanged(object sender, EventArgs e)
        {
            isAnalysed = false;
        }

        public void Analyse()
        {
            isAnalysed = false;

            AddressBookComparison addressBookComparison = CompareAddressBooks();
            RecreateImportRules(addressBookComparison);

            isAnalysed = true;
        }

        private AddressBookComparison CompareAddressBooks()
        {
            AddressBookComparison addressBookComparison = new AddressBookComparison(addressBookDestination, addressBookSource);
            addressBookComparison.Compare();

            return addressBookComparison;
        }

        private void RecreateImportRules(AddressBookComparison addressBookComparison)
        {
            contactImporters.Clear();

            IEnumerable<ContactImporter> rules = addressBookComparison.Comparisons
                .Select(x => ImporterFactory.Create(x, addressBookDestination))
                .Where(x => x.ImportType != ImportType.Ignore);

            foreach (ContactImporter importRule in rules)
                contactImporters.Add(importRule);
        }

        public StringBuilder PerformImport(bool simulate = false)
        {
            if (!isAnalysed)
                throw new LisimbaException("The import strategy must be created first. Call the Analyse method.");

            StringBuilder sb = new StringBuilder();

            foreach (ContactImporter importRule in contactImporters)
                importRule.Execute(sb, simulate);

            return sb;
        }
    }
}
