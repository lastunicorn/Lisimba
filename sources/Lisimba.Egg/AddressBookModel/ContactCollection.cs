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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DustInTheWind.Lisimba.Egg.Comparers;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Egg.Importing;

namespace DustInTheWind.Lisimba.Egg.AddressBookModel
{
    public class ContactCollection : CustomObservableCollection<Contact>
    {
        public ContactCollection()
        {
        }

        public ContactCollection(IEnumerable<Contact> contacts)
            : base(contacts)
        {
        }

        public void Sort(ContactsSortingType sortField, SortDirection sortDirection)
        {
            IComparer comparer = ComparerFactory.GetComparer(sortField);
            ArrayList.Adapter((IList)Items).Sort(comparer);
        }

        public void AddRange(ImportRuleCollection importRules)
        {
            foreach (ImportRule importRule in importRules)
            {
                switch (importRule.ImportType)
                {
                    case ImportType.AddAsNew:
                        Add(importRule.Source);
                        break;

                    case ImportType.Merge:
                        importRule.Destination.Merge(importRule.Source);
                        break;

                    case ImportType.Replace:
                        Remove(importRule.Destination);
                        Add(importRule.Source);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public ImportRuleCollection AnalyzeForImport(ContactCollection sourceContacts)
        {
            if (sourceContacts == null) throw new ArgumentNullException("sourceContacts");

            if (sourceContacts.Count == 0)
                return new ImportRuleCollection();

            IEnumerable<ImportRule> importRules = sourceContacts
                .Select(CreateImportRule)
                .Where(x => x.ImportType != ImportType.Ignore);

            return new ImportRuleCollection(importRules);
        }

        private ImportRule CreateImportRule(Contact contact)
        {
            if (Count == 0)
                return new ImportRule
                {
                    Source = contact,
                    ImportType = ImportType.AddAsNew
                };

            ContactMatch bestMatch = this
                .Select(x => new ContactMatch(contact, x))
                .Aggregate((x, y) => x.Percentage >= y.Percentage ? x : y);

            return new ImportRule
            {
                Source = contact,
                Destination = bestMatch.Contact2,
                ImportType = bestMatch.Percentage == 100 ? ImportType.Ignore : ImportType.Merge
            };
        }
    }
}