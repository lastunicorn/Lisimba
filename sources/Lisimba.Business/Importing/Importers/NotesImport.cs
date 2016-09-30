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
    public class NotesImport : ItemImportBase<Contact, string>
    {
        protected override string Name
        {
            get { return "Note"; }
        }

        public NotesImport(NotesComparison notesComparison)
            : base(notesComparison)
        {
        }

        public NotesImport(Contact destinationParent, string destinationValue, string sourceValue, ImportType importType)
        {
            SourceValue = sourceValue;
            DestinationValue = destinationValue;
            DestinationParent = destinationParent;
            ImportType = importType;
        }

        protected override void AddAsNew()
        {
            DestinationParent.Notes = SourceValue;
        }

        protected override void Replace()
        {
            DestinationParent.Notes = SourceValue;
        }

        protected override void Merge()
        {
            if (MergedValue == null)
                BuildMergedValue();

            DestinationParent.Notes = MergedValue;
        }

        private void BuildMergedValue()
        {
            if (string.IsNullOrEmpty(DestinationValue))
            {
                MergedValue = SourceValue;
                return;
            }

            if (string.IsNullOrEmpty(SourceValue))
            {
                MergedValue = DestinationValue;
                return;
            }

            if (DestinationValue.Contains(SourceValue))
            {
                MergedValue = DestinationValue;
                return;
            }

            if (SourceValue.Contains(DestinationValue))
            {
                MergedValue = SourceValue;
                return;
            }

            throw new MergeConflictException(this);
        }
    }
}