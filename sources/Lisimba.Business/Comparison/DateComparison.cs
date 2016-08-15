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

using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Comparison
{
    public class DateComparison : ItemComparisonBase<Date>
    {
        public DateComparison(Date contactLeft, Date contactRight)
            : base(contactLeft, contactRight)
        {
        }

        protected override bool LeftHasValue()
        {
            return ItemLeft != null && !ItemLeft.IsEmpty;
        }

        protected override bool RightHasValue()
        {
            return ItemRight != null && !ItemRight.IsEmpty;
        }

        protected override bool ValuesAreEqual()
        {
            return Date.Equals(ItemLeft, ItemRight);
        }

        protected override bool ValuesAreSimilar()
        {
            return (ItemLeft.Year == 0 || ItemRight.Year == 0 || ItemLeft.Year == ItemRight.Year) &&
                (ItemLeft.Month == 0 || ItemRight.Month == 0 || ItemLeft.Month == ItemRight.Month) &&
                (ItemLeft.Day == 0 || ItemRight.Day == 0 || ItemLeft.Day == ItemRight.Day);
        }
    }
}