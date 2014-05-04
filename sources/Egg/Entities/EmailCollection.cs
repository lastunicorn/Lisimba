using System;
using System.Collections;
using System.Data;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    public class EmailCollection : CollectionBase
    {
        public Email this[int index]
        {
            get { return ((Email)List[index]); }
            set { List[index] = value; }
        }

        public int Add(Email value)
        {
            return (List.Add(value));
        }

        public int IndexOf(Email value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, Email value)
        {
            List.Insert(index, value);
        }

        public void Remove(Email value)
        {
            List.Remove(value);
        }

        public bool Contains(Email value)
        {
            return (List.Contains(value));
        }

        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();
            DataRow dr;

            foreach (Email email in this)
            {
                dr = dt.NewRow();
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

        public override bool Equals(object obj)
        {
            if (!(obj is EmailCollection))
                return false;

            EmailCollection emails = (EmailCollection)obj;

            bool b1 = true;
            bool b2;

            for (int i = 0; i < emails.Count; i++)
            {
                b2 = false;
                for (int j = 0; j < List.Count; j++)
                {
                    if (emails[i].Equals(List[j]))
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
