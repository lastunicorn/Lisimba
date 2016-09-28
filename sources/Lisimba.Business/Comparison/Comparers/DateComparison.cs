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

namespace DustInTheWind.Lisimba.Business.Comparison.Comparers
{
    public class DateComparison : ItemComparisonBase<Contact, Date>
    {
        public DateComparison(Contact contactLeft, Date dateLeft, Contact contactRight, Date dateRight)
            : base(contactLeft, dateLeft, contactRight, dateRight)
        {
        }

        protected override bool LeftHasValue()
        {
            return ValueLeft != null && !ValueLeft.IsEmpty;
        }

        protected override bool RightHasValue()
        {
            return ValueRight != null && !ValueRight.IsEmpty;
        }

        protected override bool ValuesAreEqual()
        {
            return Date.Equals(ValueLeft, ValueRight);
        }

        protected override bool ValuesAreSimilar()
        {
            return (ValueLeft.Year == 0 || ValueRight.Year == 0 || ValueLeft.Year == ValueRight.Year) &&
                (ValueLeft.Month == 0 || ValueRight.Month == 0 || ValueLeft.Month == ValueRight.Month) &&
                (ValueLeft.Day == 0 || ValueRight.Day == 0 || ValueLeft.Day == ValueRight.Day);
        }
    }
}