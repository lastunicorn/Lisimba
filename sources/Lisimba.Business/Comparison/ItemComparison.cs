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
using System.Collections.Generic;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Comparison
{
    public class ItemComparison
    {
        private static readonly Dictionary<Type, Type> comparisonTypes;

        static ItemComparison()
        {
            comparisonTypes = new Dictionary<Type, Type>
            {
                { typeof(Phone), typeof(PhoneComparison) },
                { typeof(Email), typeof(EmailComparison) },
                { typeof(Date), typeof(DateComparison) },
                { typeof(PostalAddress), typeof(PostalAddressComparison) },
                { typeof(WebSite), typeof(WebSiteComparison) },
                { typeof(SocialProfile), typeof(SocialProfileIdComparison) }
            };
        }

        public static IItemComparison Create(ContactItem itemLeft, ContactItem itemRight)
        {
            if (itemLeft == null && itemRight == null)
                throw new ArgumentNullException("itemRight");

            Type typeLeft = itemLeft == null ? null : itemLeft.GetType();
            Type typeRight = itemRight == null ? null : itemRight.GetType();

            if (typeLeft != null && itemRight != null && typeLeft != typeRight)
                throw new ArgumentException("Both items should be of the same type.", "itemRight");

            Type itemsType = typeLeft ?? typeRight;

            if (!comparisonTypes.ContainsKey(itemsType))
            {
                string message = string.Format("Cannot create comparison for type {0}.", itemsType.FullName);
                throw new LisimbaException(message);
            }

            Type typeComparison = comparisonTypes[itemsType];

            return Activator.CreateInstance(typeComparison, itemLeft, itemRight) as IItemComparison;
        }
    }
}