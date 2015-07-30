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
    public class SocialProfileIdCollection : CustomObservableCollection<SocialProfile>
    {
        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();

            foreach (SocialProfile socialProfileId in this)
            {
                DataRow dr = dt.NewRow();
                dr[0] = socialProfileId.Id;
                dr[1] = socialProfileId.Description;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable("SocialProfileIds");

            dt.Columns.Add(new DataColumn("Id", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            return dt;
        }

        public void CopyFrom(SocialProfileIdCollection values)
        {
            Clear();

            for (int i = 0; i < values.Count; i++)
            {
                Add(new SocialProfile(values[i]));
            }
        }

        /// <summary>
        /// Returns the <see cref="SocialProfile"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="SocialProfile"/> object that match or <c>null</c>.</returns>
        public SocialProfile SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (SocialProfile socialProfileId in Items)
            {
                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (socialProfileId.Description.CompareTo(text) == 0)
                            return socialProfileId;
                        break;

                    case SearchMode.StartingWith:
                        if (socialProfileId.Description.StartsWith(text))
                            return socialProfileId;
                        break;

                    case SearchMode.EndingWith:
                        if (socialProfileId.Description.EndsWith(text))
                            return socialProfileId;
                        break;

                    case SearchMode.Containing:
                        if (socialProfileId.Description.IndexOf(text) > 0)
                            return socialProfileId;
                        break;
                }
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            SocialProfileIdCollection socialProfileIds = obj as SocialProfileIdCollection;

            return Equals(socialProfileIds);
        }

        public bool Equals(SocialProfileIdCollection socialProfileIds)
        {
            if (socialProfileIds == null)
                return false;

            if (socialProfileIds.Count != Count)
                return false;

            for (int i = 0; i < socialProfileIds.Count; i++)
            {
                bool exists = Enumerable.Contains(Items, socialProfileIds[i]);

                if (!exists)
                    return false;
            }

            return true;
        }
    }
}
