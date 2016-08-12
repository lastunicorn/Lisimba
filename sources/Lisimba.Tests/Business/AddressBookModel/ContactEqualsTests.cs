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

namespace DustInTheWind.Lisimba.Tests.Business.AddressBookModel
{
    [TestFixture]
    public class ContactEqualsTests
    {
        [Test]
        public void contact_is_not_equal_to_null()
        {
            Contact contact = new Contact();

            bool actual = contact.Equals(null);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void contact_is_equal_to_itself()
        {
            Contact contact = new Contact();

            bool actual = contact.Equals(contact);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_empty_contacts_are_equal()
        {
            Contact contact1 = new Contact();
            Contact contact2 = new Contact();

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_contacts_with_same_name_are_equal()
        {
            Contact contact1 = new Contact { Name = new PersonName("Alexandru", "Nicolae", "Iuga", "alez") };
            Contact contact2 = new Contact { Name = new PersonName("Alexandru", "Nicolae", "Iuga", "alez") };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_contacts_with_different_names_are_not_equal()
        {
            Contact contact1 = new Contact { Name = new PersonName("Alexandru", "Nicolae", "Iuga", "alez") };
            Contact contact2 = new Contact { Name = new PersonName("Elisabeta", "Maria", "Iuga", "eliza") };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_contacts_with_same_birthday_are_equal()
        {
            Contact contact1 = new Contact { Birthday = new Date(13, 06, 1980) };
            Contact contact2 = new Contact { Birthday = new Date(13, 06, 1980) };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_contacts_with_different_birthdays_are_not_equal()
        {
            Contact contact1 = new Contact { Birthday = new Date(13, 06, 1980) };
            Contact contact2 = new Contact { Birthday = new Date(05, 03, 1990) };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_contacts_with_same_category_are_equal()
        {
            Contact contact1 = new Contact { Category = "category 1" };
            Contact contact2 = new Contact { Category = "category 1" };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_contacts_with_different_categories_are_not_equal()
        {
            Contact contact1 = new Contact { Category = "category 1" };
            Contact contact2 = new Contact { Category = "category 2" };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_contacts_with_same_notes_are_equal()
        {
            Contact contact1 = new Contact { Notes = "some notes here" };
            Contact contact2 = new Contact { Notes = "some notes here" };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_contacts_with_different_notes_are_not_equal()
        {
            Contact contact1 = new Contact { Notes = "some notes here" };
            Contact contact2 = new Contact { Notes = "some different notes here" };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void two_contacts_with_same_phone_are_equal()
        {
            Contact contact1 = new Contact { Items = { new Phone("0723 012345", "orange") } };
            Contact contact2 = new Contact { Items = { new Phone("0723 012345", "orange") } };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void two_contacts_with_different_phone_are_not_equal()
        {
            Contact contact1 = new Contact { Items = { new Phone("0723 012345", "orange") } };
            Contact contact2 = new Contact { Items = { new Phone("0723 543210", "orange") } };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void one_contact_with_a_phone_and_one_with_no_phone_are_not_equal()
        {
            Contact contact1 = new Contact { Items = { new Phone("0723 012345", "orange") } };
            Contact contact2 = new Contact();

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void one_contact_with_a_phone_and_one_with_a_website_are_not_equal()
        {
            Contact contact1 = new Contact { Items = { new Phone("0723 012345", "orange") } };
            Contact contact2 = new Contact { Items = { new WebSite("http://alez.ro", "My web site.") } };

            bool actual = contact1.Equals(contact2);

            Assert.That(actual, Is.False);
        }
    }
}