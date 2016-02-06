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
    public class WebSiteCollection : CustomObservableCollection<WebSite>
    {
        public void CopyFrom(WebSiteCollection values)
        {
            Clear();

            for (int i = 0; i < values.Count; i++)
            {
                Add(new WebSite(values[i]));
            }
        }

        /// <summary>
        /// Returns the <see cref="WebSite"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="WebSite"/> object that match or <c>null</c>.</returns>
        public WebSite SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (WebSite webSite in Items)
            {
                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (webSite.Description.CompareTo(text) == 0)
                            return webSite;
                        break;

                    case SearchMode.StartingWith:
                        if (webSite.Description.StartsWith(text))
                            return webSite;
                        break;

                    case SearchMode.EndingWith:
                        if (webSite.Description.EndsWith(text))
                            return webSite;
                        break;

                    case SearchMode.Containing:
                        if (webSite.Description.IndexOf(text) > 0)
                            return webSite;
                        break;
                }
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            WebSiteCollection webSites = obj as WebSiteCollection;

            return Equals(webSites);
        }

        public bool Equals(WebSiteCollection webSites)
        {
            if (webSites == null)
                return false;

            if (webSites.Count != Count)
                return false;

            for (int i = 0; i < webSites.Count; i++)
            {
                bool exists = Enumerable.Contains(Items, webSites[i]);

                if (!exists)
                    return false;
            }

            return true;
        }
    }
}