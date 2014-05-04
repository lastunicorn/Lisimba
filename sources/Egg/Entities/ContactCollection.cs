using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DustInTheWind.Lisimba.Egg
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
                    this.InnerList.Sort(new CompareContactByBirthdayComparer());
                    break;

                case ContactsSortingType.BirthDate:
                    this.InnerList.Sort(new CompareContactsByBirthdateComparer());
                    break;

                case ContactsSortingType.FirstName:
                    this.InnerList.Sort(new CompareContactByFirstNameComparer());
                    break;

                case ContactsSortingType.LastName:
                    this.InnerList.Sort(new CompareContactByLastNameComparer());
                    break;

                case ContactsSortingType.Nickname:
                    this.InnerList.Sort(new CompareContactByNicknameComparer());
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                this.InnerList.Reverse();
            }
        }

        public void CopyFrom(ContactCollection values)
        {
            this.Clear();
            for (int i = 0; i < values.Count; i++)
            {
                this.Add(new Contact(values[i]));
            }
        }
    }
}
