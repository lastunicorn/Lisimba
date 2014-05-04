using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba
{
    class TreeViewNodeComparer : IComparer
    {
        private SortField sortField = SortField.Nickname;
        public SortField SortField
        {
            get { return sortField; }
            set { sortField = value; }
        }

        public TreeViewNodeComparer()
        {
        }

        public TreeViewNodeComparer(SortField sortField)
        {
            this.sortField = sortField;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x is Contact && y is Contact)
            {
                Contact p1 = (TreeNode)x;
                Contact p2 = (TreeNode)y;

                int value = Date.Compare(p1.Birthday, p2.Birthday);
                if (value == 0)
                {
                    value = string.Compare(p1.Name.Nickname, p2.Name.Nickname);
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
                }

                return value;
            }

            throw new ArgumentException("One or both of the objects to compare are not Contact.");
        }

        #endregion
    }
}
