using System;
using System.Collections;
using DustInTheWind.Lisimba.Egg.Comparers;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    public class ContactCollection : CollectionBase
    {
        public Contact this[int index]
        {
            get { return ((Contact)List[index]); }
            set { List[index] = value; }
        }

        public int Add(Contact value)
        {
            return List.Add(value);
        }

        public int IndexOf(Contact value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, Contact value)
        {
            List.Insert(index, value);
        }

        public void Remove(Contact value)
        {
            List.Remove(value);
        }

        public bool Contains(Contact value)
        {
            return (List.Contains(value));
        }

        public void Sort(ContactsSortingType sortField, SortDirection sortDirection)
        {
            switch (sortField)
            {
                case ContactsSortingType.Birthday:
                    InnerList.Sort(new CompareContactByBirthdayComparer());
                    break;

                case ContactsSortingType.BirthDate:
                    InnerList.Sort(new CompareContactsByBirthdateComparer());
                    break;

                case ContactsSortingType.FirstName:
                    InnerList.Sort(new CompareContactByFirstNameComparer());
                    break;

                case ContactsSortingType.LastName:
                    InnerList.Sort(new CompareContactByLastNameComparer());
                    break;

                case ContactsSortingType.Nickname:
                    InnerList.Sort(new CompareContactByNicknameComparer());
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                InnerList.Reverse();
            }
        }

        public void CopyFrom(ContactCollection values)
        {
            Clear();
            for (int i = 0; i < values.Count; i++)
            {
                Add(new Contact(values[i]));
            }
        }
    }
}
