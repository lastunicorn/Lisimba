// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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

using System.Linq;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.AddressBookModel
{
    public class EmailCollection : CustomObservableCollection<Email>
    {
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

            return emails.All(x => Enumerable.Contains(Items, x));
        }
    }
}