using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace DustInTheWind.Lisimba.Egg
{
    [Serializable()]
    public class AddressCollection : CollectionBase
    {

        public Address this[int index]
        {
            get { return ((Address)List[index]); }
            set { List[index] = value; }
        }

        public int Add(Address value)
        {
            return (List.Add(value));
        }

        public int IndexOf(Address value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, Address value)
        {
            List.Insert(index, value);
        }

        public void Remove(Address value)
        {
            List.Remove(value);
        }

        public bool Contains(Address value)
        {
            return (List.Contains(value));
        }

        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();
            DataRow dr;

            foreach (Address address in this)
            {
                dr = dt.NewRow();
                dr[0] = address;
                dr[1] = address.Description;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable("Addresses");

            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            return dt;
        }

        public void CopyFrom(AddressCollection values)
        {
            this.Clear();
            for (int i = 0; i < values.Count; i++)
            {
                this.Add(new Address(values[i]));
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AddressCollection))
                return false;

            AddressCollection addresses = (AddressCollection)obj;

            bool b1 = true;
            bool b2;

            for (int i = 0; i < addresses.Count; i++)
            {
                b2 = false;
                for (int j = 0; j < List.Count; j++)
                {
                    if (addresses[i].Equals(List[j]))
                    {
                        b2 = true;
                        break;
                    }
                }
                if (!b2)
                {
                    b1 = false;
                    break;
                }
            }

            return b1;
        }
    }
}
