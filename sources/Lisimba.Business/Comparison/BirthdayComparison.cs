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
    public class BirthdayComparison : ItemComparisonBase
    {
        public BirthdayComparison(Contact contactLeft, Contact contactRight)
            : base(contactLeft, contactRight)
        {
        }

        protected override bool ContactLeftHasValue()
        {
            return ContactLeft.Birthday != null;
        }

        protected override bool ContactRightHasValue()
        {
            return ContactRight.Birthday != null;
        }

        protected override bool HaveSameValue()
        {
            return Date.Equals(ContactLeft.Birthday, ContactRight.Birthday);
        }

        protected override bool HaveSimilarValue()
        {
            return (ContactLeft.Birthday.Year == 0 || ContactRight.Birthday.Year == 0 || ContactLeft.Birthday.Year == ContactRight.Birthday.Year) &&
                (ContactLeft.Birthday.Month == 0 || ContactRight.Birthday.Month == 0 || ContactLeft.Birthday.Month == ContactRight.Birthday.Month) &&
                (ContactLeft.Birthday.Day == 0 || ContactRight.Birthday.Day == 0 || ContactLeft.Birthday.Day == ContactRight.Birthday.Day);
        }
    }
}