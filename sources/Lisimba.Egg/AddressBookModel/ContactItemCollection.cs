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
using DustInTheWind.Lisimba.Egg.Searching;

namespace DustInTheWind.Lisimba.Egg.AddressBookModel
{
    public class ContactItemCollection : CustomObservableCollection<ContactItem>
    {
        /// <summary>
        /// Returns the <see cref="ContactItem"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="ContactItem"/> object that match or <c>null</c>.</returns>
        public ContactItem SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (ContactItem item in Items)
            {
                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (item.Description.CompareTo(text) == 0)
                            return item;
                        break;

                    case SearchMode.StartingWith:
                        if (item.Description.StartsWith(text))
                            return item;
                        break;

                    case SearchMode.EndingWith:
                        if (item.Description.EndsWith(text))
                            return item;
                        break;

                    case SearchMode.Containing:
                        if (item.Description.IndexOf(text) > 0)
                            return item;
                        break;
                }
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            ContactItemCollection webSites = obj as ContactItemCollection;

            return Equals(webSites);
        }

        public bool Equals(ContactItemCollection contactItems)
        {
            if (contactItems == null)
                return false;

            if (contactItems.Count != Count)
                return false;

            return contactItems.All(x => Enumerable.Contains(Items, x));
        }
    }
}