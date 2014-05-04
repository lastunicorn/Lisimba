using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba
{
    /// <summary>
    /// Compares two contaCTs by nickname.
    /// </summary>
    class TreeNodeByNicknameComparer : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x is TreeNode && y is TreeNode)
            {
                Contact c1 = (Contact)((TreeNode)x).Tag;
                Contact c2 = (Contact)((TreeNode)y).Tag;

                if (c1 == null || c2 == null) return 0;

                int value = string.Compare(c1.Name.Nickname, c2.Name.Nickname);
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

                return value;
            }

            throw new ArgumentException("One or both of the objects to compare are not TreeNode.");
        }

        #endregion
    }
}
