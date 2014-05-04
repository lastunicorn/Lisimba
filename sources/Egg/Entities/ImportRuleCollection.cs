using System;
using System.Collections;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    public class ImportRuleCollection : CollectionBase
    {
        public ImportRule this[int index]
        {
            get { return ((ImportRule)List[index]); }
            set { List[index] = value; }
        }

        public ImportRule this[Contact contact]
        {
            get
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (((ImportRule)List[i]).NewContact == contact)
                    {
                        return (ImportRule)List[i];
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (((ImportRule)List[i]).NewContact == contact)
                    {
                        List[i] = value;
                        return;
                    }
                }
            }
        }

        public int Add(ImportRule value)
        {
            return (List.Add(value));
        }

        public int IndexOf(ImportRule value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, ImportRule value)
        {
            List.Insert(index, value);
        }

        public void Remove(ImportRule value)
        {
            List.Remove(value);
        }

        public bool Contains(ImportRule value)
        {
            return (List.Contains(value));
        }

        public void CopyFrom(ImportRuleCollection values)
        {
            Clear();
            for (int i = 0; i < values.Count; i++)
            {
                Add(new ImportRule(values[i]));
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ImportRuleCollection))
                return false;

            ImportRuleCollection records = (ImportRuleCollection)obj;

            bool b1 = true;
            bool b2;

            for (int i = 0; i < records.Count; i++)
            {
                b2 = false;
                for (int j = 0; j < List.Count; j++)
                {
                    if (records[i].Equals(List[j]))
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
