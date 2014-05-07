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
