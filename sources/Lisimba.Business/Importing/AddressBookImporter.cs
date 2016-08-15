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
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public class AddressBookImporter
    {
        private readonly AddressBook addressBookDestination;
        private readonly AddressBook addressBookSource;
        private readonly Collection<ContactImport> importRules;
        private bool isAnalysed;

        public AddressBookImporter(AddressBook addressBookDestination, AddressBook addressBookSource)
        {
            if (addressBookDestination == null) throw new ArgumentNullException("addressBookDestination");
            if (addressBookSource == null) throw new ArgumentNullException("addressBookSource");

            this.addressBookDestination = addressBookDestination;
            this.addressBookSource = addressBookSource;

            importRules = new Collection<ContactImport>();

            addressBookDestination.Changed += HandleAddressBookBaseChanged;
            addressBookSource.Changed += HandleAddressBookSourceChanged;
        }

        private void HandleAddressBookBaseChanged(object sender, EventArgs eventArgs)
        {
            isAnalysed = false;
        }

        private void HandleAddressBookSourceChanged(object sender, EventArgs eventArgs)
        {
            isAnalysed = false;
        }

        public void Analyse()
        {
            importRules.Clear();

            AddressBookComparison addressBookComparison = new AddressBookComparison(addressBookDestination, addressBookSource);
            addressBookComparison.Compare();

            IEnumerable<ContactImport> rules = addressBookComparison.Results
                .Select(ContactImport.Create)
                .Where(x => x.ImportType != ImportType.Ignore);

            foreach (ContactImport importRule in rules)
                importRules.Add(importRule);
        }

        public void PerformImport()
        {
            if (!isAnalysed)
                throw new LisimbaException("The import strategy must be created first. Call the Analyse method.");

            foreach (ContactImport importRule in importRules)
            {
                switch (importRule.ImportType)
                {
                    case ImportType.Ignore:
                        break;

                    case ImportType.AddAsNew:
                        addressBookDestination.Contacts.Add(importRule.Source);
                        break;

                    case ImportType.Merge:
                        importRule.Merge();
                        break;

                    case ImportType.Replace:
                        addressBookDestination.Contacts.Remove(importRule.Destination);
                        addressBookDestination.Contacts.Add(importRule.Source);
                        break;
                    
                    default:
                        throw new LisimbaException("Invalid Import type.");
                }
            }
        }
    }
}
