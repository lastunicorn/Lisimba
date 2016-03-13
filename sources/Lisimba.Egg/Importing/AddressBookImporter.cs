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
using System.ComponentModel;
using System.Linq;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Egg.Importing
{
    public class AddressBookImporter
    {
        private readonly AddressBook addressBookBase;
        private AddressBook addressBookToImport;
        private ImportRuleCollection importRules;


        public AddressBookImporter(AddressBook addressBookBase)
        {
            if (addressBookBase == null) throw new ArgumentNullException("addressBookBase");

            this.addressBookBase = addressBookBase;
        }

        public ImportRuleCollection PrepareToImportFrom(AddressBook addressBook)
        {
            if (addressBook == null) throw new ArgumentNullException("addressBook");

            addressBookToImport = addressBook;

            AddressBookComparer comparer = new AddressBookComparer(addressBookBase, addressBookToImport);
            AddressBookComparisonResult comparisonResult = comparer.Compare();

            //importRules = new ImportRuleCollection();

            //foreach (Contact contact2 in addressBookToImport.Contacts)
            //{
            //    ContactComparisonResult contactComparisonResult = comparisonResult.First(x => x.Contact2 == contact2);

            //    importRules.Add(new ImportRule
            //    {
            //        Source = contact2,
            //        Destination = contactComparisonResult.Contact1,
            //        ImportType = contactComparisonResult.AreEqual ? ImportType.Ignore : (contactComparisonResult.Contact1 == null ? ImportType.AddAsNew : ImportType.Replace)
            //    });
            //}

            IEnumerable<ImportRule> rules = addressBookToImport.Contacts
                .Select(x =>
                {
                    ContactComparisonResult contactComparisonResult = comparisonResult.First(y => y.Contact2 == x);

                    ImportType importType = contactComparisonResult.AreEqual
                        ? ImportType.Ignore
                        : (contactComparisonResult.Contact1 == null ? ImportType.AddAsNew : ImportType.Replace);

                    return new ImportRule
                    {
                        Source = x,
                        Destination = contactComparisonResult.Contact1,
                        ImportType = importType
                    };
                })
                .Where(x => x.ImportType != ImportType.Ignore);

            importRules = new ImportRuleCollection(rules);

            return importRules;
        }

        public void DoImport()
        {
            if (importRules == null)
                throw new EggException("Prepare the import first.");

            foreach (ImportRule importRule in importRules)
            {
                switch (importRule.ImportType)
                {
                    case ImportType.Ignore:
                        break;

                    case ImportType.AddAsNew:
                        addressBookBase.Contacts.Add(importRule.Source);
                        break;

                    case ImportType.Merge:
                        // todo: The merging algorithm will be implemented at a later time.
                        break;

                    case ImportType.Replace:
                        addressBookBase.Contacts.Remove(importRule.Destination);
                        addressBookBase.Contacts.Add(importRule.Source);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
