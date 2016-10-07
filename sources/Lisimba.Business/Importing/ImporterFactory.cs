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
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;
using DustInTheWind.Lisimba.Business.Importing.Importers;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public static class ImporterFactory
    {
        private static readonly Dictionary<Type, Type> ImporterTypes;

        static ImporterFactory()
        {
            ImporterTypes = new Dictionary<Type, Type>
            {
                { typeof(PhoneComparison), typeof(PhoneImporter) },
                { typeof(EmailComparison), typeof(EmailImporter) },
                { typeof(WebSiteComparison), typeof(WebSiteImporter) },
                { typeof(PostalAddressComparison), typeof(PostalAddressImporter) },
                { typeof(DateComparison), typeof(DateImporter) },
                { typeof(SocialProfileIdComparison), typeof(SocialProfileIdImporter) },
                { typeof(NotesComparison), typeof(NotesImporter) }
            };
        }

        public static ContactImporter Create(ContactComparison contactComparison, AddressBook destinationAddressBook)
        {
            if (contactComparison == null) throw new ArgumentNullException("contactComparison");

            ContactImporter importer = new ContactImporter
            {
                SourceValue = contactComparison.ValueRight,
                DestinationValue = contactComparison.ValueLeft,
                DestinationParent = destinationAddressBook,
                ImportType = DecideImportType(contactComparison.Equality)
            };
            
            if (contactComparison.Comparisons != null)
                importer.ItemImports = contactComparison.Comparisons
                    .Select(x => Create(x, contactComparison.ValueLeft))
                    .ToList();

            return importer;
        }

        private static IImporter Create(IItemComparison itemComparison, object destinationParent)
        {
            if (itemComparison == null) throw new ArgumentNullException("itemComparison");

            Type comparisonType = itemComparison.GetType();
            IImporter importer = InstantiateItemImport(comparisonType);

            importer.SourceValue = itemComparison.ValueRight;
            importer.DestinationValue = itemComparison.ValueLeft;
            importer.DestinationParent = destinationParent;
            importer.ImportType = DecideImportType(itemComparison.Equality);

            return importer;
        }

        private static IImporter InstantiateItemImport(Type comparisonType)
        {
            if (!ImporterTypes.ContainsKey(comparisonType))
                return new ObjectImporter();

            Type importerType = ImporterTypes[comparisonType];
            return (IImporter)Activator.CreateInstance(importerType);
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