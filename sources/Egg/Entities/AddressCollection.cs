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
using System.Data;

namespace DustInTheWind.Lisimba.Egg.Entities
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
            Clear();
            for (int i = 0; i < values.Count; i++)
            {
                Add(new Address(values[i]));
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
