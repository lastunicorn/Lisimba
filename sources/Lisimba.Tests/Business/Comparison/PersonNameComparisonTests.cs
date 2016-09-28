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
using DustInTheWind.Lisimba.Business.Comparison.Comparers;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Comparison
{
    [TestFixture]
    public class PersonNameComparisonTests
    {
        [Test]
        public void Equality_is_BothEmpty_if_both_names_are_null()
        {
            PersonNameComparison personNameComparison = new PersonNameComparison(null, null, null, null);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.BothEmpty));
        }

        [Test]
        public void Equality_is_BothEmpty_if_both_names_are_empty()
        {
            PersonName personNameLeft = new PersonName();
            PersonName personNameRight = new PersonName();

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.BothEmpty));
        }

        [Test]
        public void only_first_name_exists_and_have_different_values()
        {
            PersonName personNameLeft = new PersonName { FirstName = "first name 1" };
            PersonName personNameRight = new PersonName { FirstName = "first name 2" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Different));
        }

        [Test]
        public void only_first_name_exists_and_have_same_values()
        {
            PersonName personNameLeft = new PersonName { FirstName = "first name" };
            PersonName personNameRight = new PersonName { FirstName = "first name" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }

        [Test]
        public void only_middle_name_exists_and_have_different_values()
        {
            PersonName personNameLeft = new PersonName { MiddleName = "middle name 1" };
            PersonName personNameRight = new PersonName { MiddleName = "middle name 2" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Different));
        }

        [Test]
        public void only_middle_name_exists_and_have_same_values()
        {
            PersonName personNameLeft = new PersonName { MiddleName = "middle name" };
            PersonName personNameRight = new PersonName { MiddleName = "middle name" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }

        [Test]
        public void only_last_name_exists_and_have_different_values()
        {
            PersonName personNameLeft = new PersonName { LastName = "last name 1" };
            PersonName personNameRight = new PersonName { LastName = "last name 2" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Different));
        }

        [Test]
        public void only_last_name_exists_and_have_same_values()
        {
            PersonName personNameLeft = new PersonName { LastName = "last name" };
            PersonName personNameRight = new PersonName { LastName = "last name" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }

        [Test]
        public void only_nickname_exists_and_have_different_values()
        {
            PersonName personNameLeft = new PersonName { Nickname = "nickname 1" };
            PersonName personNameRight = new PersonName { Nickname = "nickname 2" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Different));
        }

        [Test]
        public void only_nickname_exists_and_have_same_values()
        {
            PersonName personNameLeft = new PersonName { Nickname = "nickname" };
            PersonName personNameRight = new PersonName { Nickname = "nickname" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }

        [Test]
        public void left_has_first_and_middle_name_right_has_only_first_name()
        {
            PersonName personNameLeft = new PersonName { FirstName = "first name", MiddleName = "middle name" };
            PersonName personNameRight = new PersonName { FirstName = "first name" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Similar));
        }

        [Test]
        public void left_has_first_and_last_name_right_has_only_first_name()
        {
            PersonName personNameLeft = new PersonName { FirstName = "first name", LastName = "last name" };
            PersonName personNameRight = new PersonName { FirstName = "first name" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Similar));
        }

        [Test]
        public void left_has_first_name_and_nickname_right_has_only_first_name()
        {
            PersonName personNameLeft = new PersonName { FirstName = "first name", Nickname = "nickname" };
            PersonName personNameRight = new PersonName { FirstName = "first name" };

            PersonNameComparison personNameComparison = new PersonNameComparison(null, personNameLeft, null, personNameRight);

            Assert.That(personNameComparison.Equality, Is.EqualTo(ItemEquality.Similar));
        }
    }
}
