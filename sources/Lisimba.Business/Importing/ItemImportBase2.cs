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
    {
        public TParent SourceParent { get; protected set; }
        public TParent DestinationParent { get; protected set; }

        public TValue SourceValue { get; protected set; }
        public TValue DestinationValue { get; protected set; }

        object IItemImport.SourceParent
        {
            get { return SourceParent; }
        }

        object IItemImport.DestinationParent
        {
            get { return DestinationParent; }
        }

        object IItemImport.SourceValue
        {
            get { return SourceValue; }
        }

        object IItemImport.DestinationValue
        {
            get { return DestinationValue; }
        }

        public ImportType ImportType { get; protected set; }

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
                    SourceParent = itemComparison.ParentRight;
                    SourceValue = itemComparison.ValueRight;
                    ImportType = ImportType.AddAsNew;
                    break;

                case ItemEquality.Different:
                    SourceParent = itemComparison.ParentRight;
                    SourceValue = itemComparison.ValueRight;
                    DestinationParent = itemComparison.ParentLeft;
                    DestinationValue = itemComparison.ValueLeft;
                    ImportType = ImportType.Replace;
                    break;

                case ItemEquality.Similar:
                    SourceParent = itemComparison.ParentRight;
                    SourceValue = itemComparison.ValueRight;
                    DestinationParent = itemComparison.ParentLeft;
                    DestinationValue = itemComparison.ValueLeft;
                    ImportType = ImportType.Merge;
                    break;

                default:
                    throw new LisimbaException("Invalid comparison item.");
            }
        }

        public abstract void Execute(StringBuilder sb, bool simulate);
    }
}