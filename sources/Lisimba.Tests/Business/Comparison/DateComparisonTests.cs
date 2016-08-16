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
using DustInTheWind.Lisimba.Business.Comparison;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Comparison
{
    [TestFixture]
    public class DateComparisonTests
    {
        [Test]
        public void Equality_is_BothEmpty_if_both_birthdays_are_null()
        {
            DateComparison dateComparison = new DateComparison(null, null);

            Assert.That(dateComparison.Equality, Is.EqualTo(ItemEquality.BothEmpty));
        }

        [Test]
        public void Equality_is_BothEmpty_if_both_birthdays_are_empty()
        {
            Date dateLeft = new Date();
            Date dateRight = new Date();

            DateComparison dateComparison = new DateComparison(dateLeft, dateRight);

            Assert.That(dateComparison.Equality, Is.EqualTo(ItemEquality.BothEmpty));
        }

        [Test]
        public void Equality_is_LeftExists_if_left_contains_value_right_is_empty()
        {
            Date dateLeft = new Date(13, 03, 2000);
            Date dateRight = null;

            DateComparison dateComparison = new DateComparison(dateLeft, dateRight);

            Assert.That(dateComparison.Equality, Is.EqualTo(ItemEquality.LeftExists));
        }

        [Test]
        public void Equality_is_RightExists_if_left_is_empty_right_contains_value()
        {
            Date dateLeft = null;
            Date dateRight = new Date(13, 03, 2000);

            DateComparison dateComparison = new DateComparison(dateLeft, dateRight);

            Assert.That(dateComparison.Equality, Is.EqualTo(ItemEquality.RightExists));
        }

        [Test]
        public void Equality_is_Different_if_both_exists_but_different_values()
        {
            Date dateLeft = new Date(14, 04, 4000);
            Date dateRight = new Date(13, 03, 2000);

            DateComparison dateComparison = new DateComparison(dateLeft, dateRight);

            Assert.That(dateComparison.Equality, Is.EqualTo(ItemEquality.Different));
        }

        [Test]
        public void Equality_is_Equal_if_both_exists_and_have_equal_value()
        {
            Date dateLeft = new Date(13, 03, 2000);
            Date dateRight = new Date(13, 03, 2000);

            DateComparison dateComparison = new DateComparison(dateLeft, dateRight);

            Assert.That(dateComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }

        [Test]
        public void Equality_is_Equal_if_both_exists_and_have_same_value()
        {
            Date birthday = new Date(13, 03, 2000);
            Date dateLeft = birthday;
            Date dateRight = birthday;

            DateComparison dateComparison = new DateComparison(dateLeft, dateRight);

            Assert.That(dateComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }
    }
}
