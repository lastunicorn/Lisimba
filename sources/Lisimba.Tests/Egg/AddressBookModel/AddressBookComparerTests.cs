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
        private AddressBook addressBook1;
        private AddressBook addressBook2;
        private AddressBookComparer addressBookComparer;

        [SetUp]
        public void SetUp()
        {
            addressBook1 = new AddressBook();
            addressBook2 = new AddressBook();
            addressBookComparer = new AddressBookComparer();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_addressBook1_is_null()
        {
            addressBookComparer.Compare(null, addressBook2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_addressBook2_is_null()
        {
            addressBookComparer.Compare(addressBook1, null);
        }

        [Test]
        public void comparing_two_empty_address_books()
        {
            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(0));
        }

        [Test]
        public void comparing_address_books_with_one_identical_contact()
        {
            addressBook1.Contacts.Add(new Contact());
            addressBook2.Contacts.Add(new Contact());

            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(1));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(0));
        }

        [Test]
        public void address_book_1_has_one_contact_address_book_2_has_no_contact()
        {
            addressBook1.Contacts.Add(new Contact { Notes = "contact 1" });

            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(1));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(0));
        }

        [Test]
        public void address_book_1_has_no_contact_address_book_2_has_one_contact()
        {
            addressBook2.Contacts.Add(new Contact());

            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(1));
        }

        [Test]
        public void each_address_book_has_one_different_contact()
        {
            addressBook1.Contacts.Add(new Contact { Notes = "contact 1" });
            addressBook2.Contacts.Add(new Contact { Notes = "contact 2" });

            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(0));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(1));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(1));
        }

        [Test]
        public void each_address_book_has_two_contacts_one_identical_the_other_different()
        {
            addressBook1.Contacts.Add(new Contact { Notes = "contact 0" });
            addressBook1.Contacts.Add(new Contact { Notes = "contact 1" });
            addressBook2.Contacts.Add(new Contact { Notes = "contact 0" });
            addressBook2.Contacts.Add(new Contact { Notes = "contact 2" });

            ComparisonResult comparisonResult = addressBookComparer.Compare(addressBook1, addressBook2);

            Assert.That(comparisonResult.IdenticalContacts.Count(), Is.EqualTo(1));
            Assert.That(comparisonResult.Unique1Contacts.Count(), Is.EqualTo(1));
            Assert.That(comparisonResult.Unique2Contacts.Count(), Is.EqualTo(1));
        }
    }
}
