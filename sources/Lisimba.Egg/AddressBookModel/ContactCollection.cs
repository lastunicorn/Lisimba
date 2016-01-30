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

using System.Collections;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Comparers;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Egg.Importing;

namespace DustInTheWind.Lisimba.Egg.AddressBookModel
{
    public class ContactCollection : CustomObservableCollection<Contact>
    {
        public void Sort(ContactsSortingType sortField, SortDirection sortDirection)
        {
            IComparer comparer = ComparerFactory.GetComparer(sortField);
            ArrayList.Adapter((IList) Items).Sort(comparer);
        }

        protected override void InsertItem(int index, Contact item)
        {
            bool existsContactWithSameName = Items.Any(x => PersonName.Equals(x.Name, item.Name));

            if (existsContactWithSameName)
                return;

            base.InsertItem(index, item);
        }

        public int AddRange(ContactCollection contacts, ImportRuleCollection mergeRules)
        {
            int countAdded = 0;

            //foreach (Contact contact in contacts)
            //{
            //    ImportRule rule = mergeRules[contact];

            //    if (rule == null)
            //        continue;

            //    switch (rule.MergeType)
            //    {
            //        case MergeType.AddAsNew:
            //            Add(contact);
            //            countAdded++;
            //            break;

            //        case MergeType.Combine:
            //            //if (contacts.Contains(rule.LeftContact))
            //            //    rule.LeftContact.CopyFrom(rule.RightContact);
            //            countAdded++;
            //            break;

            //        case MergeType.Overwrite:
            //            if (contacts.Contains(rule.LeftContact))
            //                rule.LeftContact.CopyFrom(rule.RightContact);
            //            countAdded++;
            //            break;

            //        case MergeType.DoNotAdd:
            //            break;
            //    }
            //}

            return countAdded;
        }
    }
}