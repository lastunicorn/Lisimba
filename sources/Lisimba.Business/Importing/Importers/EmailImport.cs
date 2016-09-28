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
using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;

namespace DustInTheWind.Lisimba.Business.Importing.Importers
{
    public class EmailImport : ItemImportBase<Contact, Email>
    {
        public EmailImport(EmailComparison emailComparison)
            : base(emailComparison)
        {
        }

        public override void Execute(StringBuilder sb, bool simulate)
        {
            switch (ImportType)
            {
                case ImportType.Ignore:
                    break;

                case ImportType.AddAsNew:
                    AddAsNew(sb, simulate);
                    break;

                case ImportType.Merge:
                    Merge(sb, simulate);
                    break;

                case ImportType.Replace:
                    Replace(sb, simulate);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("simulate");
            }
        }

        private void AddAsNew(StringBuilder sb, bool simulate)
        {
            if (!simulate)
                DestinationParent.Items.Add(SourceValue);

            sb.AppendLine(string.Format("Added item: {0}", SourceValue));
        }

        private void Merge(StringBuilder sb, bool simulate)
        {
            sb.AppendLine(string.Format("Merging items '{0}' and '{1}'.", DestinationValue, SourceValue));

            if (!simulate)
            {
                if (!string.IsNullOrEmpty(SourceValue.Address))
                    DestinationValue.Address = SourceValue.Address;

                if (!string.IsNullOrEmpty(SourceValue.Description))
                    DestinationValue.Description = SourceValue.Description;
            }
        }

        private void Replace(StringBuilder sb, bool simulate)
        {
            if (!simulate)
            {
                DestinationParent.Items.Remove(DestinationValue);
                DestinationParent.Items.Add(SourceValue);
            }

            sb.AppendLine(string.Format("Replaced item '{0}' with '{1}'.", DestinationValue, SourceValue));
        }
    }
}