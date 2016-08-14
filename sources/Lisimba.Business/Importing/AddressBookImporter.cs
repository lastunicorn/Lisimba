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
        private readonly AddressBook addressBookBase;
        private readonly AddressBook addressBookSource;
        private readonly Collection<ImportRule> importRules;

        public AddressBookImporter(AddressBook addressBookBase, AddressBook addressBookSource)
        {
            if (addressBookBase == null) throw new ArgumentNullException("addressBookBase");
            if (addressBookSource == null) throw new ArgumentNullException("addressBookSource");

            this.addressBookBase = addressBookBase;
            this.addressBookSource = addressBookSource;

            importRules = new Collection<ImportRule>();

            PrepareToImportFrom();
        }

        private void PrepareToImportFrom()
        {
            importRules.Clear();

            AddressBookComparison addressBookComparison = new AddressBookComparison(addressBookBase, addressBookSource);
            addressBookComparison.Compare();

            IEnumerable<ImportRule> rules = addressBookComparison.Results
                .Where(x => x.Equality == ItemEquality.RightExists || x.Equality == ItemEquality.Different || x.Equality == ItemEquality.Similar)
                .Select(x =>
                {
                    switch (x.Equality)
                    {
                        case ItemEquality.RightExists:
                        case ItemEquality.Different:
                            return new ImportRule
                            {
                                Source = x.ContactRight,
                                Destination = null,
                                ImportType = ImportType.AddAsNew
                            };

                        case ItemEquality.Similar:
                            return new ImportRule
                            {
                                Source = x.ContactRight,
                                Destination = x.ContactLeft,
                                ImportType = ImportType.Merge
                            };

                        default:
                            string message = string.Format("Cannot import contact {0}", x.ContactRight);
                            throw new LisimbaException(message);
                    }
                });

            foreach (ImportRule importRule in rules)
                importRules.Add(importRule);
        }

        public void PerformImport()
        {
            if (importRules == null)
                throw new LisimbaException("Prepare the import first.");

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
                        importRule.Destination.Merge(importRule.Source);
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
