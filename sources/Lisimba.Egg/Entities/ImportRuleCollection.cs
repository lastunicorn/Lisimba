// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable]
    public class ImportRuleCollection : Collection<ImportRule>
    {
        public ImportRuleCollection()
        {
        }

        public ImportRuleCollection(IList<ImportRule> list)
            : base(list)
        {
        }

        public ImportRule this[Contact contact]
        {
            get { return Items.FirstOrDefault(x => x.NewContact == contact); }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].NewContact == contact)
                    {
                        Items[i] = value;
                        return;
                    }
                }
            }
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
            ImportRuleCollection records = obj as ImportRuleCollection;
            return Equals(records);
        }

        private bool Equals(ImportRuleCollection records)
        {
            if (records == null)
                return false;

            for (int i = 0; i < records.Count; i++)
            {
                bool b2 = false;

                for (int j = 0; j < Items.Count; j++)
                {
                    if (records[i].Equals(Items[j]))
                    {
                        b2 = true;
                        break;
                    }
                }

                if (b2)
                    continue;

                return false;
            }

            return true;
        }
    }
}