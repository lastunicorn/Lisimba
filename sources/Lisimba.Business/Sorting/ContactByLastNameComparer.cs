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
using System.Collections;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Properties;

namespace DustInTheWind.Lisimba.Business.Sorting
{
    /// <summary>
    /// Compares two contacts by last name.
    /// </summary>
    internal class ContactByLastNameComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Contact contactX = x as Contact;
            Contact contactY = y as Contact;

            if (contactX == null)
                throw new ArgumentException(Resources.ContactComparer_XIsNotContact, "x");

            if (contactY == null)
                throw new ArgumentException(Resources.ContactComparer_YIsNotContact, "y");

            return string.Compare(contactX.Name.LastName, contactY.Name.LastName);
        }
    }
}