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
using System.Linq;
using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public class ContactImport : ItemImportBase<Contact>
    {
        public List<IItemImport> ItemImports { get; private set; }

        public ContactImport(ContactComparison contactComparison)
            : base(contactComparison)
        {
            ItemImports = contactComparison.Comparisons
                .Select(ItemImportFactory.Create)
                .ToList();
        }

        public override void Merge(StringBuilder sb, bool simulate)
        {
            foreach (IItemImport importRule in ItemImports)
            {
                switch (importRule.ImportType)
                {
                    case ImportType.Ignore:
                        break;

                    case ImportType.AddAsNew:
                        AddAsNew(importRule, sb, simulate);
                        break;

                    case ImportType.Merge:
                        Merge(importRule, sb, simulate);
                        break;

                    case ImportType.Replace:
                        Replace(importRule, sb, simulate);
                        break;

                    default:
                        sb.AppendLine(string.Format("Invalid import rule for dest: '{0}'; source: '{1}'; import type: {2}.", importRule.Destination, importRule.Source, importRule.ImportType));
                        break;
                }
            }
        }

        private void AddAsNew(IItemImport importRule, StringBuilder sb, bool simulate)
        {
            if (!simulate)
                Destination.Items.Add((ContactItem)importRule.Source);

            sb.AppendLine(string.Format("Added item: {0}", importRule.Source));
        }

        private static void Merge(IItemImport importRule, StringBuilder sb, bool simulate)
        {
            sb.AppendLine(string.Format("Merging items '{0}' and '{1}'.", importRule.Destination, importRule.Source));

            importRule.Merge(sb, simulate);
        }

        private void Replace(IItemImport importRule, StringBuilder sb, bool simulate)
        {
            if (!simulate)
            {
                Destination.Items.Remove((ContactItem)importRule.Destination);
                Destination.Items.Add((ContactItem)importRule.Source);
            }

            sb.AppendLine(string.Format("Replaced item '{0}' with '{1}'.", importRule.Destination, importRule.Source));
        }
    }
}