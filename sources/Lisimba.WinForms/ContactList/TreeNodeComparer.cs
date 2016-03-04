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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Sorting;

namespace DustInTheWind.Lisimba.WinForms.ContactList
{
    internal class TreeNodeComparer : IComparer
    {
        private readonly ContactsSortingType contactsSortingType;

        public TreeNodeComparer(ContactsSortingType contactsSortingType)
        {
            this.contactsSortingType = contactsSortingType;
        }

        public int Compare(object x, object y)
        {
            if (!(x is TreeNode) || !(y is TreeNode))
                throw new ArgumentException("One or both of the objects to compare are not TreeNode.");

            TreeNode treeNodeX = (TreeNode) x;
            TreeNode treeNodeY = (TreeNode) y;

            Contact c1 = treeNodeX.Tag as Contact;
            Contact c2 = treeNodeY.Tag as Contact;

            if (c1 == null || c2 == null) return 0;

            IComparer comparer = ComparerFactory.GetComparer(contactsSortingType);
            return comparer.Compare(c1, c2);
        }
    }
}