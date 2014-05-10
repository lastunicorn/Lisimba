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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Comparers
{
    /// <summary>
    /// Compares two contacts only by day and month of their birthdate. (Ignores the year)
    /// </summary>
    class TreeNodeByBirthdayComparer : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x is TreeNode && y is TreeNode)
            {
                Contact c1 = (Contact)((TreeNode)x).Tag;
                Contact c2 = (Contact)((TreeNode)y).Tag;

                if (c1 == null || c2 == null) return 0;
                
                int value = Date.CompareWithoutYear(c1.Birthday, c2.Birthday);
                if (value == 0)
                {
                    value = string.Compare(c1.Name.Nickname, c2.Name.Nickname);
                    if (value == 0)
                    {
                        value = string.Compare(c1.Name.FirstName, c2.Name.FirstName);
                        if (value == 0)
                        {
                            value = string.Compare(c1.Name.LastName, c2.Name.LastName);
                            if (value == 0)
                            {
                                value = string.Compare(c1.Name.MiddleName, c2.Name.MiddleName);
                            }
                        }
                    }
                }

                return value;
            }

            throw new ArgumentException("One or both of the objects to compare are not TreeNode.");
        }

        #endregion
    }
}
