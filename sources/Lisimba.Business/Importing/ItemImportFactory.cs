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
using DustInTheWind.Lisimba.Business.Comparison;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;
using DustInTheWind.Lisimba.Business.Importing.Importers;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public static class ItemImportFactory
    {
        private static readonly Dictionary<Type, Type> importerTypes;

        static ItemImportFactory()
        {
            importerTypes = new Dictionary<Type, Type>
            {
                { typeof(PhoneComparison), typeof(PhoneImport) },
                { typeof(EmailComparison), typeof(EmailImport) },
                { typeof(WebSiteComparison), typeof(WebSiteImport) },
                { typeof(PostalAddressComparison), typeof(PostalAddressImport) },
                { typeof(DateComparison), typeof(DateImport) },
                { typeof(SocialProfileIdComparison), typeof(SocialProfileIdImport) },
                { typeof(NotesComparison), typeof(NotesImport) }
            };
        }
        public static IItemImport Create(IItemComparison itemComparison)
        {
            if (itemComparison == null) throw new ArgumentNullException("itemComparison");

            Type comparisonType = itemComparison.GetType();

            if (!importerTypes.ContainsKey(comparisonType))
                return ObjectImport.Empty();

            Type importerType = importerTypes[comparisonType];

            return Activator.CreateInstance(importerType, itemComparison) as IItemImport;

            //if (type == typeof(EmailComparison))
            //    return new EmailImport(itemComparison as EmailComparison);

            //if (type == typeof(PhoneComparison))
            //    return new PhoneImport(itemComparison as PhoneComparison);

            //if (type == typeof(WebSiteComparison))
            //    return new WebSiteImport(itemComparison as WebSiteComparison);

            //return ObjectImport.Empty();
        }
    }
}