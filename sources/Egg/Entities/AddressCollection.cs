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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable]
    public class AddressCollection : CustomObservableCollection<Address>
    {
        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();

            foreach (Address address in this)
            {
                DataRow dr = dt.NewRow();
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

            IEnumerable<Address> newAddresses = values.Select(address => new Address(address));

            foreach (Address newAddress in newAddresses)
            {
                Add(newAddress);
            }
        }

        /// <summary>
        /// Returns the <see cref="Address"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="Address"/> object that match or <c>null</c>.</returns>
        public Address SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (Address address in Items)
            {
                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (address.Description.CompareTo(text) == 0)
                            return address;
                        break;

                    case SearchMode.StartingWith:
                        if (address.Description.StartsWith(text))
                            return address;
                        break;

                    case SearchMode.EndingWith:
                        if (address.Description.EndsWith(text))
                            return address;
                        break;

                    case SearchMode.Containing:
                        if (address.Description.IndexOf(text) > 0)
                            return address;
                        break;
                }
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            AddressCollection addresses = obj as AddressCollection;

            return Equals(addresses);
        }

        public bool Equals(AddressCollection addresses)
        {
            if (addresses == null)
                return false;

            if (addresses.Count != Count)
                return false;

            foreach (Address address in addresses)
            {
                bool exists = Enumerable.Contains(Items, address);

                if (!exists)
                    return false;
            }

            return true;
        }
    }
}
