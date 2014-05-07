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
using System.Collections;
using System.Data;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    public class DateCollection : CollectionBase
    {
        public Date this[int index]
        {
            get { return ((Date)List[index]); }
            set { List[index] = value; }
        }

        public int Add(Date value)
        {
            return (List.Add(value));
        }

        public int IndexOf(Date value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, Date value)
        {
            List.Insert(index, value);
        }

        public void Remove(Date value)
        {
            List.Remove(value);
        }

        public bool Contains(Date value)
        {
            return (List.Contains(value));
        }

        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();
            DataRow dr;

            foreach (Date date in this)
            {
                dr = dt.NewRow();
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
            if (!(obj is DateCollection))
                return false;

            DateCollection dates = (DateCollection)obj;

            bool b1 = true;
            bool b2;

            for (int i = 0; i < dates.Count; i++)
            {
                b2 = false;
                for (int j = 0; j < List.Count; j++)
                {
                    if (dates[i].Equals(List[j]))
                    {
                        b2 = true;
                        break;
                    }
                }
                if (!b2)
                {
                    b1 = false;
                    break;
                }
            }

            return b1;
        }
    }
}
