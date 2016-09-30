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

using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;

namespace DustInTheWind.Lisimba.Business.Importing.Importers
{
    public class EmailImport : ItemImportBase<Contact, Email>
    {
        protected override string Name
        {
            get { return "Email"; }
        }

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

        protected override void AddAsNew()
        {
            if (SourceValue != null)
                DestinationParent.Items.Add(SourceValue.Clone());
        }

        protected override void Replace()
        {
            if (DestinationValue != null)
                DestinationParent.Items.Remove(DestinationValue);

            if (SourceValue != null)
                DestinationParent.Items.Add(SourceValue.Clone());
        }

        protected override void Merge()
        {
            if (MergedValue == null)
                BuildMergedValue();

            if (DestinationValue != null)
                DestinationParent.Items.Remove(DestinationValue);

            if (MergedValue != null)
                DestinationParent.Items.Add(MergedValue.Clone());
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
                MergeAddressIntoMergedValue();
                MergeDescriptionIntoMergedValue();
            }
            catch
            {
                MergedValue = null;
                throw;
            }
        }

        private void MergeAddressIntoMergedValue()
        {
            if (SourceValue.Address == null)
                return;

            if (string.IsNullOrEmpty(MergedValue.Address))
            {
                MergedValue.Address = SourceValue.Address;
                return;
            }

            if (MergedValue.Address != SourceValue.Address)
                throw new MergeConflictException(this);
        }

        private void MergeDescriptionIntoMergedValue()
        {
            if (SourceValue.Description == null)
                return;

            if (string.IsNullOrEmpty(MergedValue.Description))
            {
                MergedValue.Description = SourceValue.Description;
                return;
            }

            if (MergedValue.Description != SourceValue.Description)
                throw new MergeConflictException(this);
        }
    }
}