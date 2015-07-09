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
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.Comparers
{
    /// <summary>
    /// Compares two contaCTs by nickname.
    /// </summary>
    class TreeNodeByNicknameOrNameComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (!(x is TreeNode) || !(y is TreeNode))
                throw new ArgumentException("One or both of the objects to compare are not TreeNode.");

            Contact c1 = (Contact)((TreeNode)x).Tag;
            Contact c2 = (Contact)((TreeNode)y).Tag;

            if (c1 == null || c2 == null) return 0;

            string name1 = c1.Name.Nickname;

            if (c1.Name.FirstName.Length > 0)
                name1 += (name1.Length > 0 ? " " : string.Empty) + c1.Name.FirstName;
            if (c1.Name.MiddleName.Length > 0)
                name1 += (name1.Length > 0 ? " " : string.Empty) + c1.Name.MiddleName;
            if (c1.Name.LastName.Length > 0)
                name1 += (name1.Length > 0 ? " " : string.Empty) + c1.Name.LastName;

            string name2 = c2.Name.Nickname;

            if (c2.Name.FirstName.Length > 0)
                name2 += (name2.Length > 0 ? " " : string.Empty) + c2.Name.FirstName;
            if (c2.Name.MiddleName.Length > 0)
                name2 += (name2.Length > 0 ? " " : string.Empty) + c2.Name.MiddleName;
            if (c2.Name.LastName.Length > 0)
                name2 += (name2.Length > 0 ? " " : string.Empty) + c2.Name.LastName;

            return string.Compare(name1, name2, true);
        }
    }
}
