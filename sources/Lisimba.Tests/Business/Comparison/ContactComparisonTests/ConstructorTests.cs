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
using DustInTheWind.Lisimba.Business.Comparison.Comparers;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Comparison.ContactComparisonTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void initially_ContactLeft_is_initialized_with_value_received_on_constructor()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(null, contactLeft, null, contactRight);

            Assert.That(contactComparison.ValueLeft, Is.SameAs(contactLeft));
        }

        [Test]
        public void initially_ContactRight_is_initialized_with_value_received_on_constructor()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(null, contactLeft, null, contactRight);

            Assert.That(contactComparison.ValueRight, Is.SameAs(contactRight));
        }

        [Test]
        public void Equality_is_RightExists_if_contactLeft_is_null()
        {
            ContactComparison contactComparison = new ContactComparison(null, null, null, new Contact());

            Assert.That(contactComparison.Equality, Is.EqualTo(ItemEquality.RightExists));
        }

        [Test]
        public void Equality_is_LeftExists_if_contactRight_is_null()
        {
            ContactComparison contactComparison = new ContactComparison(null, new Contact(), null, null);

            Assert.That(contactComparison.Equality, Is.EqualTo(ItemEquality.LeftExists));
        }

        [Test]
        public void Equality_is_BothEmpty_if_both_contactLeft_and_contactRight_are_null()
        {
            ContactComparison contactComparison = new ContactComparison(null, null, null, null);

            Assert.That(contactComparison.Equality, Is.EqualTo(ItemEquality.BothEmpty));
        }

        [Test]
        public void after_Compare_the_Differences_contains_the_BirthdayComparison()
        {
            Contact contact1 = new Contact();
            Contact contact2 = new Contact();

            ContactComparison contactComparison = new ContactComparison(null, contact1, null, contact2);

            AssertContainsType(contactComparison.Comparisons, typeof(DateComparison));
        }

        [Test]
        public void after_Compare_the_Differences_contains_the_CategoryComparison()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(null, contactLeft, null, contactRight);

            AssertContainsType(contactComparison.Comparisons, typeof(CategoryComparison));
        }

        [Test]
        public void after_Compare_the_Differences_contains_the_NotesComparison()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(null, contactLeft, null, contactRight);

            AssertContainsType(contactComparison.Comparisons, typeof(NotesComparison));
        }

        [Test]
        public void after_Compare_the_Differences_contains_the_PersonNameComparison()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(null, contactLeft, null, contactRight);

            AssertContainsType(contactComparison.Comparisons, typeof(PersonNameComparison));
        }

        [Test]
        public void after_Compare_the_Differences_contains_the_PictureComparison()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(null, contactLeft, null, contactRight);

            AssertContainsType(contactComparison.Comparisons, typeof(PictureComparison));
        }

        [Test]
        public void if_no_item_exists_no_ItemComparison_is_created()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(null, contactLeft, null, contactRight);

            AssertDoesNotContainType(contactComparison.Comparisons, typeof(ItemComparisonFactory));
        }

        private static void AssertContainsType(IEnumerable<IItemComparison> differences, Type type)
        {
            bool containsType = differences.Any(itemComparison => itemComparison.GetType() == type);

            if (!containsType)
                Assert.Fail("The list does not contain the comparison of type {0}.", type.FullName);
        }

        private static void AssertDoesNotContainType(IEnumerable<IItemComparison> differences, Type type)
        {
            bool containsType = differences.Any(itemComparison => itemComparison.GetType() == type);

            if (containsType)
                Assert.Fail("The list does contain at least one comparison of type {0}.", type.FullName);
        }
    }
}
