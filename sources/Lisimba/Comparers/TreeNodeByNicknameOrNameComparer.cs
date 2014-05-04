using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba
{
    /// <summary>
    /// Compares two contaCTs by nickname.
    /// </summary>
    class TreeNodeByNicknameOrNameComparer : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x is TreeNode && y is TreeNode)
            {
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

            throw new ArgumentException("One or both of the objects to compare are not TreeNode.");
        }

        #endregion
    }
}
