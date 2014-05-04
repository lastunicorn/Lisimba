using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DustInTheWind.Lisimba.Egg
{
    /// <summary>
    /// Compares two contacts by last name.
    /// </summary>
    internal class CompareContactByLastNameComparer : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x is Contact && y is Contact)
            {
                Contact p1 = (Contact)x;
                Contact p2 = (Contact)y;

                int value = string.Compare(p1.Name.LastName, p2.Name.LastName);
                if (value == 0)
                {
                    value = string.Compare(p1.Name.FirstName, p2.Name.FirstName);
                    if (value == 0)
                    {
                        value = string.Compare(p1.Name.MiddleName, p2.Name.MiddleName);
                        if (value == 0)
                        {
                            value = string.Compare(p1.Name.Nickname, p2.Name.Nickname);
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
