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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;

namespace DustInTheWind.Lisimba.Business.Importing.Importers
{
    public class ContactImport : ItemImportBase<AddressBook, Contact>
    {
        public List<IItemImport> ItemImports { get; private set; }

        public ContactImport(ContactComparison contactComparison)
            : base(contactComparison)
        {
            ItemImports = contactComparison.Comparisons
                .Select(ItemImportFactory.Create)
                .ToList();
        }

        public override void Execute(StringBuilder sb, bool simulate)
        {
            foreach (IItemImport importRule in ItemImports)
            {
                try
                {
                    importRule.Execute(sb, simulate);
                }
                catch (Exception ex)
                {
                    sb.AppendLine(string.Format("Invalid import rule for dest: '{0}'; source: '{1}'; import type: {2}.", importRule.DestinationValue, importRule.SourceValue, importRule.ImportType));
                }
            }
        }
    }
}