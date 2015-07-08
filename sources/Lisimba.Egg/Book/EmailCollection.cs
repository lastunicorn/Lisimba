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
    public class EmailCollection : CustomObservableCollection<Email>
    {
        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();

            foreach (Email email in this)
            {
                DataRow dr = dt.NewRow();
                dr[0] = email.Address;
                dr[1] = email.Description;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable("Emails");

            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            return dt;
        }

        public void CopyFrom(EmailCollection values)
        {
            Clear();

            for (int i = 0; i < values.Count; i++)
            {
                Add(new Email(values[i]));
            }
        }

        /// <summary>
        /// Returns the <see cref="Email"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="Email"/> object that match or <c>null</c>.</returns>
        public Email SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (Email email in Items)
            {
                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (email.Description.CompareTo(text) == 0)
                            return email;
                        break;

                    case SearchMode.StartingWith:
                        if (email.Description.StartsWith(text))
                            return email;
                        break;

                    case SearchMode.EndingWith:
                        if (email.Description.EndsWith(text))
                            return email;
                        break;

                    case SearchMode.Containing:
                        if (email.Description.IndexOf(text) > 0)
                            return email;
                        break;
                }
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            EmailCollection emails = obj as EmailCollection;

            return Equals(emails);
        }

        public bool Equals(EmailCollection emails)
        {
            if (emails == null)
                return false;

            if (emails.Count != Count)
                return false;

            for (int i = 0; i < emails.Count; i++)
            {
                bool exists = Enumerable.Contains(Items, emails[i]);

                if (!exists)
                    return false;
            }

            return true;
        }
    }
}
