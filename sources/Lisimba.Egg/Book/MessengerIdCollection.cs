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

namespace DustInTheWind.Lisimba.Egg.Book
{
    public class MessengerIdCollection : CustomObservableCollection<MessengerId>
    {
        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();

            foreach (MessengerId messenger in this)
            {
                DataRow dr = dt.NewRow();
                dr[0] = messenger.Id;
                dr[1] = messenger.Description;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable("MessengerIds");

            dt.Columns.Add(new DataColumn("Id", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            return dt;
        }

        public void CopyFrom(MessengerIdCollection values)
        {
            Clear();

            for (int i = 0; i < values.Count; i++)
            {
                Add(new MessengerId(values[i]));
            }
        }

        /// <summary>
        /// Returns the <see cref="MessengerId"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="MessengerId"/> object that match or <c>null</c>.</returns>
        public MessengerId SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (MessengerId messengerId in Items)
            {
                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (messengerId.Description.CompareTo(text) == 0)
                            return messengerId;
                        break;

                    case SearchMode.StartingWith:
                        if (messengerId.Description.StartsWith(text))
                            return messengerId;
                        break;

                    case SearchMode.EndingWith:
                        if (messengerId.Description.EndsWith(text))
                            return messengerId;
                        break;

                    case SearchMode.Containing:
                        if (messengerId.Description.IndexOf(text) > 0)
                            return messengerId;
                        break;
                }
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            MessengerIdCollection messengerIds = obj as MessengerIdCollection;

            return Equals(messengerIds);
        }

        public bool Equals(MessengerIdCollection messengerIds)
        {
            if (messengerIds == null)
                return false;

            if (messengerIds.Count != Count)
                return false;

            for (int i = 0; i < messengerIds.Count; i++)
            {
                bool exists = Enumerable.Contains(Items, messengerIds[i]);

                if (!exists)
                    return false;
            }

            return true;
        }
    }
}
