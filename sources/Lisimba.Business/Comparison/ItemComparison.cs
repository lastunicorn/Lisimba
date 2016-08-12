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
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Comparison
{
    public class ItemComparison : IItemComparison
    {
        public ContactItem ItemLeft { get; private set; }
        public ContactItem ItemRight { get; private set; }

        public ItemEquality Equality { get; private set; }

        public ItemComparison(ContactItem itemLeft, ContactItem itemRight)
        {
            if (itemLeft == null && itemRight == null)
                throw new ArgumentNullException("itemRight");

            ItemLeft = itemLeft;
            ItemRight = itemRight;

            Compare();
        }

        private void Compare()
        {
            if (ItemLeft == null)
                Equality = ItemEquality.RightExists;
            else if (ItemRight == null)
                Equality = ItemEquality.LeftExists;
            else if (ItemLeft.Equals(ItemRight))
                Equality = ItemEquality.Equal;
            else
                Equality = ItemEquality.Different;
        }
    }
}