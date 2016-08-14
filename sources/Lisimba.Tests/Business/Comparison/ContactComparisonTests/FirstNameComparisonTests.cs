﻿// Lisimba
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
    public class FirstNameComparisonTests
    {
        [Test]
        public void after_Compare_the_Differences_contains_the_FirstNameComparison()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertContainsType(contactComparison.Results, typeof(FirstNameComparison));
        }

        [Test]
        public void both_first_names_are_empty()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.BothEmpty);
        }

        [Test]
        public void left_contains_value_right_is_empty()
        {
            Contact contactLeft = new Contact { Name = { FirstName = "first name" } };
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.LeftExists);
        }

        [Test]
        public void left_is_empty_right_contains_value()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact { Name = { FirstName = "first name" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.RightExists);
        }

        [Test]
        public void both_exists_but_different_values()
        {
            Contact contactLeft = new Contact { Name = { FirstName = "first name 1" } };
            Contact contactRight = new Contact { Name = { FirstName = "first name 2" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Different);
        }

        [Test]
        public void both_exists_and_have_same_value()
        {
            Contact contactLeft = new Contact { Name = { FirstName = "first name" } };
            Contact contactRight = new Contact { Name = { FirstName = "first name" } };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Equal);
        }

        private static void AssertEquality(IEnumerable<IItemComparison> comparisons, ItemEquality expectedEquality)
        {
            FirstNameComparison firstNameComparison = comparisons.OfType<FirstNameComparison>().First();
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