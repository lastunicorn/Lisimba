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

using System;
using System.Collections;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Comparers;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable]
    public class ContactCollection : CustomObservableCollection<Contact>
    {
        public void Sort(ContactsSortingType sortField, SortDirection sortDirection)
        {
            IComparer comparer = GetComparer(sortField);
            ArrayList.Adapter((IList)Items).Sort(comparer);

            //switch (sortField)
            //{
            //    case ContactsSortingType.Birthday:
            //        InnerList.Sort(new CompareContactByBirthdayComparer());
            //        break;

            //    case ContactsSortingType.BirthDate:
            //        InnerList.Sort(new CompareContactsByBirthdateComparer());
            //        break;

            //    case ContactsSortingType.FirstName:
            //        InnerList.Sort(new CompareContactByFirstNameComparer());
            //        break;

            //    case ContactsSortingType.LastName:
            //        InnerList.Sort(new CompareContactByLastNameComparer());
            //        break;

            //    case ContactsSortingType.Nickname:
            //        InnerList.Sort(new CompareContactByNicknameComparer());
            //        break;
            //}

            //if (sortDirection == SortDirection.Descending)
            //    InnerList.Reverse();
        }

        private IComparer GetComparer(ContactsSortingType sortField)
        {
            switch (sortField)
            {
                case ContactsSortingType.Birthday:
                    return new CompareContactByBirthdayComparer();

                case ContactsSortingType.BirthDate:
                    return new CompareContactsByBirthdateComparer();

                case ContactsSortingType.FirstName:
                    return new CompareContactByFirstNameComparer();

                case ContactsSortingType.LastName:
                    return new CompareContactByLastNameComparer();

                case ContactsSortingType.Nickname:
                    return new CompareContactByNicknameComparer();

                default:
                    return new CompareContactByBirthdayComparer();
            }
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
