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
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;
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
            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            Assert.That(contactComparison.ContactRight, Is.SameAs(contactRight));
        }

        [Test]
        public void initially_ContactRight_is_initialized_with_value_received_on_constructor()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();
            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            Assert.That(contactComparison.ContactLeft, Is.SameAs(contactLeft));
        }

        [Test]
        public void throws_if_contactLeft_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CategoryComparison(null, new Contact());
            });
        }

        [Test]
        public void throws_if_contactRight_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CategoryComparison(new Contact(), null);
            });
        }
    }
}
