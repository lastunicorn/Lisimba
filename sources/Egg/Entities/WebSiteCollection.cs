using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace DustInTheWind.Lisimba.Egg
{
    [Serializable()]
    public class WebSiteCollection : CollectionBase
    {
        public WebSite this[int index]
        {
            get { return ((WebSite)List[index]); }
            set { List[index] = value; }
        }

        public int Add(WebSite value)
        {
            return (List.Add(value));
        }

        public int IndexOf(WebSite value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, WebSite value)
        {
            List.Insert(index, value);
        }

        public void Remove(WebSite value)
        {
            List.Remove(value);
        }

        public bool Contains(WebSite value)
        {
            return (List.Contains(value));
        }

        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();
            DataRow dr;

            foreach (WebSite webSite in this)
            {
                dr = dt.NewRow();
                dr[0] = webSite.Address;
                dr[1] = webSite.Description;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable("WebSites");

            dt.Columns.Add(new DataColumn("Web Site", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            return dt;
        }

        public void CopyFrom(WebSiteCollection values)
        {
            this.Clear();
            for (int i = 0; i < values.Count; i++)
            {
                this.Add(new WebSite(values[i]));
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WebSiteCollection))
                return false;

            WebSiteCollection webSites = (WebSiteCollection)obj;

            bool b1 = true;
            bool b2;

            for (int i = 0; i < webSites.Count; i++)
            {
                b2 = false;
                for (int j = 0; j < List.Count; j++)
                {
                    if (webSites[i].Equals(List[j]))
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
