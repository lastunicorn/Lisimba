// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Egg.Comparers
{
    /// <summary>
    /// Compares two contacts by nickname.
    /// </summary>
    internal class CompareContactByNicknameComparer : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x is Contact && y is Contact)
            {
                Contact p1 = (Contact)x;
                Contact p2 = (Contact)y;

                int value = string.Compare(p1.Name.Nickname, p2.Name.Nickname);
                if (value == 0)
                {
                    value = string.Compare(p1.Name.FirstName, p2.Name.FirstName);
                    if (value == 0)
                    {
                        value = string.Compare(p1.Name.LastName, p2.Name.LastName);
                        if (value == 0)
                        {
                            value = string.Compare(p1.Name.MiddleName, p2.Name.MiddleName);
                        }
                    }
                }

                return value;
            }

            throw new ArgumentException("One or both of the objects to compare are not Contact.");
        }

        #endregion
    }
}
