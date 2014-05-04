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
