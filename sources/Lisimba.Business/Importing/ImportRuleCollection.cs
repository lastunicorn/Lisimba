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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public class ImportRuleCollection : Collection<ImportRule>
    {
        public ImportRuleCollection()
        {
        }

        public ImportRuleCollection(IEnumerable<ImportRule> items)
            : base(items.ToList())
        {
        }

        public ImportRule GetBySource(Contact contact)
        {
            return Items.FirstOrDefault(x => ReferenceEquals(x.Source, contact));
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

            return records.All(x => Enumerable.Contains(Items, x));
        }
    }
}