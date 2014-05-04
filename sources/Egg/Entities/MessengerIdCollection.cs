using System.Collections;
using System.Data;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class MessengerIdCollection : CollectionBase
    {
        public MessengerId this[int index]
        {
            get { return ((MessengerId)List[index]); }
            set { List[index] = value; }
        }

        public int Add(MessengerId value)
        {
            return (List.Add(value));
        }

        public int IndexOf(MessengerId value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, MessengerId value)
        {
            List.Insert(index, value);
        }

        public void Remove(MessengerId value)
        {
            List.Remove(value);
        }

        public bool Contains(MessengerId value)
        {
            return (List.Contains(value));
        }

        public DataTable ToDataTable()
        {
            DataTable dt = GetEmptyDataTable();
            DataRow dr;

            foreach (MessengerId messenger in this)
            {
                dr = dt.NewRow();
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

        public override bool Equals(object obj)
        {
            if (!(obj is MessengerIdCollection))
                return false;

            MessengerIdCollection messengerIds = (MessengerIdCollection)obj;

            bool b1 = true;
            bool b2;

            for (int i = 0; i < messengerIds.Count; i++)
            {
                b2 = false;
                for (int j = 0; j < List.Count; j++)
                {
                    if (messengerIds[i].Equals(List[j]))
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
