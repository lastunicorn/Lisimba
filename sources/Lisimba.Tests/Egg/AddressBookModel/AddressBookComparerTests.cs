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
using System.Linq;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Importing;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Egg.AddressBookModel
{
    [TestFixture]
    public class AddressBookComparerTests
    {
        private AddressBookComparer addressBookComparer;

        [SetUp]
        public void SetUp()
        {
            addressBookComparer = new AddressBookComparer();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_addressBook1_is_null()
        {
            AddressBook addressBook2 = new AddressBook();

            addressBookComparer.Compare(null, addressBook2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_addressBook2_is_null()
        {
            AddressBook addressBook1 = new AddressBook();

            addressBookComparer.Compare(addressBook1, null);
        }

        [Test]
        public void comparing_two_empty_address_books()
        {
            AddressBook addressBook1 = new AddressBook();
            AddressBook addressBook2 = new AddressBook();

            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(0));
        }

        [Test]
        public void comparing_address_books_with_one_identical_contact()
        {
            AddressBook addressBook1 = new AddressBook();
            addressBook1.Contacts.Add(new Contact());

            AddressBook addressBook2 = new AddressBook();
            addressBook2.Contacts.Add(new Contact());

            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(1));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(0));
        }

        [Test]
        public void comparing_address_books_containing_one_contact_that_is_different()
        {
            AddressBook addressBook1 = new AddressBook();
            addressBook1.Contacts.Add(new Contact { Notes = "contact 1" });

            AddressBook addressBook2 = new AddressBook();
            addressBook2.Contacts.Add(new Contact { Notes = "contact 2" });

            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(1));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(1));
        }
    }
}
