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
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Comparison.ContactComparisonTests
{
    [TestFixture]
    public class PersonNameComparisonTests
    {
        [Test]
        public void after_Compare_the_Differences_contains_the_PersonNameComparison()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertContainsType(contactComparison.Results, typeof(PersonNameComparison));
        }

        [Test]
        public void only_first_name_exists_and_have_different_values()
        {
            Contact contactLeft = new Contact { Name = { FirstName = "first name 1" } };
            Contact contactRight = new Contact { Name = { FirstName = "first name 2" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Different);
        }

        [Test]
        public void only_first_name_exists_and_have_same_values()
        {
            Contact contactLeft = new Contact { Name = { FirstName = "first name" } };
            Contact contactRight = new Contact { Name = { FirstName = "first name" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Equal);
        }

        [Test]
        public void only_middle_name_exists_and_have_different_values()
        {
            Contact contactLeft = new Contact { Name = { MiddleName = "middle name 1" } };
            Contact contactRight = new Contact { Name = { MiddleName = "middle name 2" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Different);
        }

        [Test]
        public void only_middle_name_exists_and_have_same_values()
        {
            Contact contactLeft = new Contact { Name = { MiddleName = "middle name" } };
            Contact contactRight = new Contact { Name = { MiddleName = "middle name" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Equal);
        }

        [Test]
        public void only_last_name_exists_and_have_different_values()
        {
            Contact contactLeft = new Contact { Name = { LastName = "last name 1" } };
            Contact contactRight = new Contact { Name = { LastName = "last name 2" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Different);
        }

        [Test]
        public void only_last_name_exists_and_have_same_values()
        {
            Contact contactLeft = new Contact { Name = { LastName = "last name" } };
            Contact contactRight = new Contact { Name = { LastName = "last name" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Equal);
        }

        [Test]
        public void only_nickname_exists_and_have_different_values()
        {
            Contact contactLeft = new Contact { Name = { Nickname = "nickname 1" } };
            Contact contactRight = new Contact { Name = { Nickname = "nickname 2" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Different);
        }

        [Test]
        public void only_nickname_exists_and_have_same_values()
        {
            Contact contactLeft = new Contact { Name = { Nickname = "nickname" } };
            Contact contactRight = new Contact { Name = { Nickname = "nickname" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Equal);
        }

        [Test]
        public void left_has_first_and_middle_name_right_has_only_first_name()
        {
            Contact contactLeft = new Contact { Name = { FirstName = "first name", MiddleName = "middle name"} };
            Contact contactRight = new Contact { Name = { FirstName = "first name" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Similar);
        }

        [Test]
        public void left_has_first_and_last_name_right_has_only_first_name()
        {
            Contact contactLeft = new Contact { Name = { FirstName = "first name", LastName = "last name" } };
            Contact contactRight = new Contact { Name = { FirstName = "first name" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Similar);
        }

        [Test]
        public void left_has_first_name_and_nickname_right_has_only_first_name()
        {
            Contact contactLeft = new Contact { Name = { FirstName = "first name", Nickname = "nickname" } };
            Contact contactRight = new Contact { Name = { FirstName = "first name" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Similar);
        }

        private static void AssertEquality(IEnumerable<IItemComparison> comparisons, ItemEquality expectedEquality)
        {
            PersonNameComparison firstNameComparison = comparisons.OfType<PersonNameComparison>().First();
            Assert.That(firstNameComparison.Equality, Is.EqualTo(expectedEquality));
        }

        private static void AssertContainsType(IEnumerable<IItemComparison> differences, Type type)
        {
            bool containsType = differences.Any(itemComparison => itemComparison.GetType() == type);

            if (!containsType)
                Assert.Fail("The list does not contain the comparison of type {0}.", type.FullName);
        }
    }
}
