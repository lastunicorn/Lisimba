// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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

using System.Collections.Generic;
using System.Data;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Book
{
    public class PostalAddressCollection : CustomObservableCollection<PostalAddress>
    {
        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();

            foreach (PostalAddress address in this)
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

            dt.Columns.Add(new DataColumn("Address", typeof (string)));
            dt.Columns.Add(new DataColumn("Comment", typeof (string)));

            return dt;
        }

        public void CopyFrom(PostalAddressCollection values)
        {
            Clear();

            IEnumerable<PostalAddress> newAddresses = values.Select(address => new PostalAddress(address));

            foreach (PostalAddress newAddress in newAddresses)
            {
                Add(newAddress);
            }
        }

        /// <summary>
        /// Returns the <see cref="PostalAddress"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="PostalAddress"/> object that match or <c>null</c>.</returns>
        public PostalAddress SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (PostalAddress address in Items)
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
            PostalAddressCollection postalAddresses = obj as PostalAddressCollection;

            return Equals(postalAddresses);
        }

        public bool Equals(PostalAddressCollection postalAddresses)
        {
            if (postalAddresses == null)
                return false;

            if (postalAddresses.Count != Count)
                return false;

            foreach (PostalAddress address in postalAddresses)
            {
                bool exists = Enumerable.Contains(Items, address);

                if (!exists)
                    return false;
            }

            return true;
        }
    }
}