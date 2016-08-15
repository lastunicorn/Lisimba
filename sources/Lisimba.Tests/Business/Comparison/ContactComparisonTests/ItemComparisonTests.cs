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
    public class ItemComparisonTests
    {
        [Test]
        public void if_no_item_exists_no_ItemComparison_is_created()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertDoesNotContainType(contactComparison.Results, typeof(ItemComparisonFactory));
        }

        [Test]
        public void left_contains_one_Email_right_is_empty()
        {
            Contact contactLeft = new Contact();
            Email emailLeft = new Email { Address = "aaa@bbb.ccc", Description = "desc" };
            contactLeft.Items.Add(emailLeft);
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            List<EmailComparison> itemComparisons = contactComparison.Results.OfType<EmailComparison>().ToList();
            Assert.That(itemComparisons.Count, Is.EqualTo(1));
            Assert.That(itemComparisons[0].ItemLeft, Is.SameAs(emailLeft));
            Assert.That(itemComparisons[0].ItemRight, Is.Null);
            Assert.That(itemComparisons[0].Equality, Is.EqualTo(ItemEquality.LeftExists));
        }

        [Test]
        public void left_is_empty_right_contains_one_Email()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();
            Email emailRight = new Email { Address = "aaa@bbb.ccc", Description = "desc" };
            contactRight.Items.Add(emailRight);

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            List<EmailComparison> itemComparisons = contactComparison.Results.OfType<EmailComparison>().ToList();
            Assert.That(itemComparisons.Count, Is.EqualTo(1));
            Assert.That(itemComparisons[0].ItemLeft, Is.Null);
            Assert.That(itemComparisons[0].ItemRight, Is.SameAs(emailRight));
            Assert.That(itemComparisons[0].Equality, Is.EqualTo(ItemEquality.RightExists));
        }

        [Test]
        public void both_contains_one_Email_but_different()
        {
            Contact contactLeft = new Contact();
            Email emailLeft = new Email { Address = "aaa@bbb.ccc", Description = "desc 1" };
            contactLeft.Items.Add(emailLeft);
            Contact contactRight = new Contact();
            Email emailRight = new Email { Address = "aaa@bbb.ccc", Description = "desc 2" };
            contactRight.Items.Add(emailRight);

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            List<EmailComparison> itemComparisons = contactComparison.Results.OfType<EmailComparison>().ToList();
            Assert.That(itemComparisons.Count, Is.EqualTo(2));
            Assert.True(itemComparisons.Any(x => x.ItemLeft == emailLeft && x.ItemRight == null && x.Equality == ItemEquality.LeftExists));
            Assert.True(itemComparisons.Any(x => x.ItemLeft == null && x.ItemRight == emailRight && x.Equality == ItemEquality.RightExists));
        }

        [Test]
        public void both_contains_equal_Emails()
        {
            Contact contactLeft = new Contact();
            Email emailLeft = new Email { Address = "aaa@bbb.ccc", Description = "desc" };
            contactLeft.Items.Add(emailLeft);
            Contact contactRight = new Contact();
            Email emailRight = new Email { Address = "aaa@bbb.ccc", Description = "desc" };
            contactRight.Items.Add(emailRight);

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            List<EmailComparison> itemComparisons = contactComparison.Results.OfType<EmailComparison>().ToList();
            Assert.That(itemComparisons.Count, Is.EqualTo(1));
            Assert.True(itemComparisons.Any(x => x.ItemLeft == emailLeft && x.ItemRight == emailRight && x.Equality == ItemEquality.Equal));
        }

        private static void AssertDoesNotContainType(IEnumerable<IItemComparison> differences, Type type)
        {
            bool containsType = differences.Any(itemComparison => itemComparison.GetType() == type);

            if (containsType)
                Assert.Fail("The list does contain at least one comparison of type {0}.", type.FullName);
        }
    }
}
