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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public class ContactImport
    {
        public Contact Source { get; private set; }
        public Contact Destination { get; private set; }
        public ImportType ImportType { get; private set; }

        public List<ItemImport> ItemImports { get; private set; }

        public static ContactImport Create(ContactComparison contactComparison)
        {
            switch (contactComparison.Equality)
            {
                case ItemEquality.BothEmpty:
                case ItemEquality.LeftExists:
                case ItemEquality.Equal:
                    return new ContactImport
                    {
                        Source = null,
                        Destination = null,
                        ImportType = ImportType.Ignore
                    };

                case ItemEquality.RightExists:
                case ItemEquality.Different:
                    return new ContactImport
                    {
                        Source = contactComparison.ContactRight,
                        Destination = null,
                        ImportType = ImportType.AddAsNew
                    };

                case ItemEquality.Similar:
                    return new ContactImport
                    {
                        Source = contactComparison.ContactRight,
                        Destination = contactComparison.ContactLeft,
                        ImportType = ImportType.Merge,
                        ItemImports = contactComparison.Comparisons
                            .Select(x=> new ItemImport(x))
                            .ToList()
                    };

                default:
                    string message = string.Format("Cannot import contact {0}", contactComparison.ContactRight);
                    throw new LisimbaException(message);
            }
        }

        public void Merge()
        {
        }
    }
}