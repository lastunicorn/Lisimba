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
using DustInTheWind.Lisimba.Business.Comparison;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public abstract class ItemImportBase<TParent, TValue> : IItemImport
        where TParent : class
        where TValue : class
    {
        protected TValue SourceValue { get; set; }
        protected TValue DestinationValue { get; set; }
        protected TParent DestinationParent { get; set; }
        public ImportType ImportType { get; set; }
        public TValue MergedValue { get; set; }

        object IItemImport.SourceValue
        {
            get { return SourceValue; }
            set { SourceValue = (TValue)value; }
        }

        object IItemImport.DestinationValue
        {
            get { return DestinationValue; }
            set { DestinationValue = (TValue)value; }
        }

        object IItemImport.DestinationParent
        {
            get { return DestinationParent; }
            set { DestinationParent = (TParent)value; }
        }

        object IItemImport.MergedValue
        {
            get { return MergedValue; }
            set { MergedValue = (TValue)value; }
        }

        protected ItemImportBase()
        {
        }

        protected ItemImportBase(IItemComparison<TParent, TValue> itemComparison)
        {
            if (itemComparison == null) throw new ArgumentNullException("itemComparison");

            switch (itemComparison.Equality)
            {
                case ItemEquality.BothEmpty:
                case ItemEquality.LeftExists:
                case ItemEquality.Equal:
                    ImportType = ImportType.Ignore;
                    break;

                case ItemEquality.RightExists:
                    SourceValue = itemComparison.ValueRight;
                    ImportType = ImportType.AddAsNew;
                    break;

                case ItemEquality.Different:
                    SourceValue = itemComparison.ValueRight;
                    DestinationValue = itemComparison.ValueLeft;
                    DestinationParent = itemComparison.ParentLeft;
                    ImportType = ImportType.Replace;
                    break;

                case ItemEquality.Similar:
                    SourceValue = itemComparison.ValueRight;
                    DestinationValue = itemComparison.ValueLeft;
                    DestinationParent = itemComparison.ParentLeft;
                    ImportType = ImportType.Merge;
                    break;

                default:
                    throw new LisimbaException("Invalid comparison item.");
            }
        }

        public void Execute(StringBuilder sb, bool simulate)
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
                    string message = string.Format("Invalid import rule for dest: '{0}'; source: '{1}'; import type: {2}.", DestinationValue, SourceValue, ImportType);
                    throw new LisimbaException(message);
            }
        }

        protected abstract void AddAsNew(StringBuilder sb, bool simulate);
        protected abstract void Merge(StringBuilder sb, bool simulate);
        protected abstract void Replace(StringBuilder sb, bool simulate);
    }
}