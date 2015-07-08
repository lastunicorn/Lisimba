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

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class DateCollection : CustomObservableCollection<Date>
    {
        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();

            foreach (Date date in this)
            {
                DataRow dr = dt.NewRow();
                dr[0] = date;
                dr[1] = date.Description;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable("Dates");

            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            return dt;
        }

        public void CopyFrom(DateCollection values)
        {
            Clear();
            for (int i = 0; i < values.Count; i++)
            {
                Add(new Date(values[i]));
            }
        }

        public override bool Equals(object obj)
        {
            DateCollection dates = obj as DateCollection;

            return Equals(dates);
        }

        public bool Equals(DateCollection dates)
        {
            if (dates == null)
                return false;

            if (dates.Count != Count)
                return false;

            for (int i = 0; i < dates.Count; i++)
            {
                bool exists = Enumerable.Contains(Items, dates[i]);

                if (!exists)
                    return false;
            }

            return true;
        }
    }
}
