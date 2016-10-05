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

using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Comparison.Comparers
{
    public class PersonNameComparison : ItemComparisonBase<PersonName>
    {
        public PersonNameComparison(PersonName personNameLeft, PersonName personNameRight)
            : base(personNameLeft, personNameRight)
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
            return PersonName.Equals(ValueLeft, ValueRight);
        }

        protected override bool ValuesAreSimilar()
        {
            // Name parts are:
            // - at least one Equal
            // - the rest are (all Equal or Left or BothEmpty) or (all Equal or Right or BothEmpty).

            ItemEquality[] itemEqualitys =
            {
                CompareNamePart(ValueLeft.FirstName, ValueRight.FirstName),
                CompareNamePart(ValueLeft.MiddleName, ValueRight.MiddleName),
                CompareNamePart(ValueLeft.LastName, ValueRight.LastName),
                CompareNamePart(ValueLeft.Nickname, ValueRight.Nickname)
            };

            if (itemEqualitys.All(x => x != ItemEquality.Equal))
                return false;

            if (itemEqualitys.Any(x => x != ItemEquality.Equal && x != ItemEquality.LeftExists && x != ItemEquality.BothEmpty) &&
                itemEqualitys.Any(x => x != ItemEquality.Equal && x != ItemEquality.RightExists && x != ItemEquality.BothEmpty))
                return false;

            return true;
        }

        private static ItemEquality CompareNamePart(string partLeft, string partRight)
        {
            if (string.IsNullOrEmpty(partLeft))
                return string.IsNullOrEmpty(partRight) ? ItemEquality.BothEmpty : ItemEquality.RightExists;

            if (string.IsNullOrEmpty(partRight))
                return ItemEquality.LeftExists;

            return partLeft == partRight
                ? ItemEquality.Equal
                : ItemEquality.Different;
        }
    }
}