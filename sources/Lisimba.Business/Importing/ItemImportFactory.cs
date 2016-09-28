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
using DustInTheWind.Lisimba.Business.Comparison;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;
using DustInTheWind.Lisimba.Business.Importing.Importers;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public class ItemImportFactory
    {
        public static IItemImport Create(IItemComparison itemComparison)
        {
            if (itemComparison == null) throw new ArgumentNullException("itemComparison");

            Type type = itemComparison.GetType();

            if (type == typeof(EmailComparison))
                return new EmailImport(itemComparison as EmailComparison);

            if (type == typeof(PhoneComparison))
                return new PhoneImport(itemComparison as PhoneComparison);

            return ObjectImport.Empty();







            //switch (itemComparison.Equality)
            //{
            //    case ItemEquality.BothEmpty:
            //    case ItemEquality.LeftExists:
            //    case ItemEquality.Equal:
            //        return new ItemImport
            //        {
            //            Source = null,
            //            Destination = null,
            //            ImportType = ImportType.Ignore
            //        };

            //    case ItemEquality.RightExists:
            //    case ItemEquality.Different:
            //        return new ItemImport
            //        {
            //            Source = itemComparison.ItemRight,
            //            Destination = null,
            //            ImportType = ImportType.AddAsNew
            //        };

            //    case ItemEquality.Similar:
            //        Type type = itemComparison.ItemLeft.GetType();

            //        if (type == typeof(Email))
            //            return new ItemImport
            //            {
            //                Source = itemComparison.ItemRight,
            //                Destination = null,
            //                ImportType = ImportType.AddAsNew
            //            };

            //        return new ItemImport
            //        {
            //            Source = null,
            //            Destination = null,
            //            ImportType = ImportType.Ignore
            //        };

            //    default:
            //        throw new LisimbaException("Invalid comparison item.");
            //}
        }
    }
}