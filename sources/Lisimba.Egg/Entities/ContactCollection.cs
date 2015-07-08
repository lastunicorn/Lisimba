// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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

using DustInTheWind.Lisimba.Egg.Comparers;
using DustInTheWind.Lisimba.Egg.Enums;
using System;
using System.Collections;
using System.Linq;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class ContactCollection : CustomObservableCollection<Contact>
    {
        public void Sort(ContactsSortingType sortField, SortDirection sortDirection)
        {
            IComparer comparer = ComparerFactory.GetComparer(sortField);
            ArrayList.Adapter((IList)Items).Sort(comparer);

            //switch (sortField)
            //{
            //    case ContactsSortingType.Birthday:
            //        InnerList.Sort(new ContactByBirthdayComparer());
            //        break;

            //    case ContactsSortingType.BirthDate:
            //        InnerList.Sort(new ContactsByBirthdateComparer());
            //        break;

            //    case ContactsSortingType.FirstName:
            //        InnerList.Sort(new ContactByFirstNameComparer());
            //        break;

            //    case ContactsSortingType.LastName:
            //        InnerList.Sort(new ContactByLastNameComparer());
            //        break;

            //    case ContactsSortingType.Nickname:
            //        InnerList.Sort(new ContactByNicknameComparer());
            //        break;
            //}

            //if (sortDirection == SortDirection.Descending)
            //    Items.Reverse();
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

            foreach (Contact contact in contacts)
            {
                ImportRule rule = mergeRules[contact];

                if (rule == null)
                    continue;

                switch (rule.ImportType)
                {
                    case ImportType.AddAsNew:
                        Add(contact);
                        countAdded++;
                        break;

                    case ImportType.Combine:
                        //if (contacts.Contains(rule.OriginalContact))
                        //    rule.OriginalContact.CopyFrom(rule.NewContact);
                        countAdded++;
                        break;

                    case ImportType.Overwrite:
                        if (contacts.Contains(rule.OriginalContact))
                            rule.OriginalContact.CopyFrom(rule.NewContact);
                        countAdded++;
                        break;

                    case ImportType.DoNotAdd:
                        break;
                }
            }

            return countAdded;
        }
    }
}
