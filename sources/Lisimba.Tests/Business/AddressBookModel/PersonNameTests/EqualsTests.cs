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
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.AddressBookModel.PersonNameTests
{
    [TestFixture]
    public class EqualsTests
    {
        [Test]
        public void one_name_is_different_from_null()
        {
            PersonName personName = new PersonName();

            bool actual = personName.Equals(null);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void one_name_is_equal_with_itself()
        {
            PersonName personName = new PersonName();

            bool actual = personName.Equals(personName);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_empty_names_are_equal()
        {
            PersonName personName1 = new PersonName();
            PersonName personName2 = new PersonName();

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_names_with_same_first_name_are_equal()
        {
            PersonName personName1 = new PersonName { FirstName = "Alexandru" };
            PersonName personName2 = new PersonName { FirstName = "Alexandru" };

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_names_with_different_first_names_are_not_equal()
        {
            PersonName personName1 = new PersonName { FirstName = "Alexandru" };
            PersonName personName2 = new PersonName { FirstName = "Elisabeta" };

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_names_with_same_middle_name_are_equal()
        {
            PersonName personName1 = new PersonName { MiddleName = "Nicolae" };
            PersonName personName2 = new PersonName { MiddleName = "Nicolae" };

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_names_with_different_middle_names_are_not_equal()
        {
            PersonName personName1 = new PersonName { MiddleName = "Nicolae" };
            PersonName personName2 = new PersonName { MiddleName = "Maria" };

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_names_with_same_last_name_are_equal()
        {
            PersonName personName1 = new PersonName { LastName = "Iuga" };
            PersonName personName2 = new PersonName { LastName = "Iuga" };

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_names_with_different_last_names_are_not_equal()
        {
            PersonName personName1 = new PersonName { LastName = "Iuga" };
            PersonName personName2 = new PersonName { LastName = "Câmpean" };

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_names_with_same_nickname_are_equal()
        {
            PersonName personName1 = new PersonName { Nickname = "alez" };
            PersonName personName2 = new PersonName { Nickname = "alez" };

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_names_with_different_nicknames_are_not_equal()
        {
            PersonName personName1 = new PersonName { Nickname = "alez" };
            PersonName personName2 = new PersonName { Nickname = "eliza" };

            bool actual = personName1.Equals(personName2);

            Assert.That(actual, Is.False);
        }
    }
}