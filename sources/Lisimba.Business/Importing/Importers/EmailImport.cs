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

        public EmailImport(Contact destinationParent, Email destinationValue, Email sourceValue, ImportType importType)
        {
            SourceValue = sourceValue;
            DestinationValue = destinationValue;
            DestinationParent = destinationParent;
            ImportType = importType;
        }

        protected override void AddAsNew(StringBuilder sb, bool simulate)
        {
            if (!simulate)
            {
                if (SourceValue != null)
                    DestinationParent.Items.Add(SourceValue.Clone());
            }

            sb.AppendLine(string.Format("Added email: {0}", SourceValue));
        }

        protected override void Merge(StringBuilder sb, bool simulate)
        {
            sb.AppendLine(string.Format("Merging email '{0}' and '{1}'.", DestinationValue, SourceValue));

            if (!simulate)
            {
                if (MergedValue == null)
                    BuildMergedValue();

                if (DestinationValue != null)
                    DestinationParent.Items.Remove(DestinationValue);

                if (MergedValue != null)
                    DestinationParent.Items.Add(MergedValue.Clone());
            }
        }

        private void BuildMergedValue()
        {
            if (DestinationValue == null)
            {
                MergedValue = SourceValue;
                return;
            }

            if (SourceValue == null)
            {
                MergedValue = DestinationValue;
                return;
            }

            MergedValue = DestinationValue.Clone() as Email;

            try
            {
                MergeAddress();
                MergeDescription();
            }
            catch
            {
                MergedValue = null;
                throw;
            }
        }

        private void MergeAddress()
        {
            if (SourceValue.Address == null)
                return;

            if (string.IsNullOrEmpty(MergedValue.Address))
            {
                MergedValue.Address = SourceValue.Address;
                return;
            }

            if (MergedValue.Address != SourceValue.Address)
                throw new MergeConflictException();
        }

        private void MergeDescription()
        {
            if (SourceValue.Description == null)
                return;

            if (string.IsNullOrEmpty(MergedValue.Description))
            {
                MergedValue.Description = SourceValue.Description;
                return;
            }

            if (MergedValue.Description != SourceValue.Description)
                throw new MergeConflictException();
        }

        protected override void Replace(StringBuilder sb, bool simulate)
        {
            if (!simulate)
            {
                if (DestinationValue != null)
                    DestinationParent.Items.Remove(DestinationValue);

                if (SourceValue != null)
                    DestinationParent.Items.Add(SourceValue.Clone());
            }

            sb.AppendLine(string.Format("Replaced email '{0}' with '{1}'.", DestinationValue, SourceValue));
        }
    }
}