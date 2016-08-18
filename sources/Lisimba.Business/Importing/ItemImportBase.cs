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
    public abstract class ItemImportBase<T> : IItemImport
        where T : class
    {
        public T Source { get; protected set; }
        public T Destination { get; protected set; }

        object IItemImport.Source
        {
            get { return Source; }
        }

        object IItemImport.Destination
        {
            get { return Destination; }
        }

        public ImportType ImportType { get; protected set; }

        protected ItemImportBase(IItemComparison<T> itemComparison)
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
                    Source = itemComparison.ItemRight;
                    ImportType = ImportType.AddAsNew;
                    break;

                case ItemEquality.Different:
                    Source = itemComparison.ItemRight;
                    Destination = itemComparison.ItemLeft;
                    ImportType = ImportType.Replace;
                    break;

                case ItemEquality.Similar:
                    Source = itemComparison.ItemRight;
                    Destination = itemComparison.ItemLeft;
                    ImportType = ImportType.Merge;
                    break;

                default:
                    throw new LisimbaException("Invalid comparison item.");
            }
        }

        public abstract void Merge(StringBuilder sb, bool simulate);
    }
}