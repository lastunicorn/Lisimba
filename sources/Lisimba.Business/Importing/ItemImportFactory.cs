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
using DustInTheWind.Lisimba.Business.Comparison;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;
using DustInTheWind.Lisimba.Business.Importing.Importers;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public static class ItemImportFactory
    {
        private static readonly Dictionary<Type, Type> ImporterTypes;

        static ItemImportFactory()
        {
            ImporterTypes = new Dictionary<Type, Type>
            {
                { typeof(ContactComparison), typeof(ContactImport) },
                { typeof(PhoneComparison), typeof(PhoneImport) },
                { typeof(EmailComparison), typeof(EmailImport) },
                { typeof(WebSiteComparison), typeof(WebSiteImport) },
                { typeof(PostalAddressComparison), typeof(PostalAddressImport) },
                { typeof(DateComparison), typeof(DateImport) },
                { typeof(SocialProfileIdComparison), typeof(SocialProfileIdImport) },
                { typeof(NotesComparison), typeof(NotesImport) }
            };
        }

        public static IItemImport Create(IItemComparison itemComparison, object destinationParent)
        {
            if (itemComparison == null) throw new ArgumentNullException("itemComparison");

            Type comparisonType = itemComparison.GetType();
            IItemImport itemImport = InstantiateItemImport(comparisonType);

            itemImport.SourceValue = itemComparison.ValueRight;
            itemImport.DestinationValue = itemComparison.ValueLeft;
            itemImport.DestinationParent = destinationParent;
            itemImport.ImportType = DecideImportType(itemComparison.Equality);

            if (itemComparison.Comparisons != null)
                itemImport.ItemImports = itemComparison.Comparisons
                    .Select(x => Create(x, itemComparison.ValueLeft))
                    .ToList();

            return itemImport;
        }

        private static IItemImport InstantiateItemImport(Type comparisonType)
        {
            if (!ImporterTypes.ContainsKey(comparisonType))
                return new ObjectImport();

            Type importerType = ImporterTypes[comparisonType];
            return (IItemImport)Activator.CreateInstance(importerType);
        }

        private static ImportType DecideImportType(ItemEquality itemEquality)
        {
            switch (itemEquality)
            {
                case ItemEquality.BothEmpty:
                case ItemEquality.LeftExists:
                case ItemEquality.Equal:
                    return ImportType.Ignore;

                case ItemEquality.RightExists:
                    return ImportType.AddAsNew;

                case ItemEquality.Different:
                    return ImportType.Replace;

                case ItemEquality.Similar:
                    return ImportType.Merge;

                default:
                    throw new LisimbaException("Invalid comparison item.");
            }
        }
    }
}