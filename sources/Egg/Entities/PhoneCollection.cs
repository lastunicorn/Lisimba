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

using System.Data;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class PhoneCollection : CustomObservableCollection<Phone>
    {
        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();

            foreach (Phone phone in this)
            {
                DataRow dr = dt.NewRow();
                dr[0] = phone.Number;
                dr[1] = phone.Description;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable("Phones");

            dt.Columns.Add(new DataColumn("Phone", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            return dt;
        }

        public void CopyFrom(PhoneCollection values)
        {
            Clear();

            for (int i = 0; i < values.Count; i++)
            {
                Add(new Phone(values[i]));
            }
        }

        /// <summary>
        /// Returns the <see cref="Phone"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="Phone"/> object that match or <c>null</c>.</returns>
        public Phone SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (Phone phone in Items)
            {
                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (phone.Description.CompareTo(text) == 0)
                            return phone;
                        break;

                    case SearchMode.StartingWith:
                        if (phone.Description.StartsWith(text))
                            return phone;
                        break;

                    case SearchMode.EndingWith:
                        if (phone.Description.EndsWith(text))
                            return phone;
                        break;

                    case SearchMode.Containing:
                        if (phone.Description.IndexOf(text) > 0)
                            return phone;
                        break;
                }
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            PhoneCollection phones = obj as PhoneCollection;

            return Equals(phones);
        }

        public bool Equals(PhoneCollection phones)
        {
            if (phones == null)
                return false;

            if (phones.Count != Count)
                return false;

            for (int i = 0; i < phones.Count; i++)
            {
                bool exists = Enumerable.Contains(Items, phones[i]);

                if (!exists)
                    return false;
            }

            return true;
        }
    }
}
