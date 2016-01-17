// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.ContactEdit;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Services;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Presenters.ContactViewPresenterTests
{
    [TestFixture]
    public class ContactChangedTests
    {
        private ContactEditorViewModel contactEditorViewModel;
        private Mock<IContactEditorView> view;

        [SetUp]
        public void SetUp()
        {
            contactEditorViewModel = new ContactEditorViewModel(new Zodiac());
            view = new Mock<IContactEditorView>();
        }

        [Test]
        public void if_View_is_not_set_and_Contact_is_set_nothing_happens()
        {
            contactEditorViewModel.Contact = new Contact();
        }

        [Test]
        public void if_Contact_is_set_birthday_is_set_in_view()
        {
            contactEditorViewModel.View = view.Object;
            DateTime birthDateTime = new DateTime(1980, 06, 13);
            Date birthday = new Date(birthDateTime);

            contactEditorViewModel.Contact = new Contact
            {
                Birthday = birthday
            };

            Assert.That(contactEditorViewModel.Birthday, Is.EqualTo(birthday.ToString()));
        }

        [Test]
        public void if_Contact_is_set_to_null_birthday_is_set_to_empty_string()
        {
            contactEditorViewModel.View = view.Object;

            contactEditorViewModel.Contact = null;

            Assert.That(contactEditorViewModel.Birthday, Is.EqualTo(string.Empty));
        }

        [Test]
        public void if_Contact_is_set_notes_is_set_in_view()
        {
            contactEditorViewModel.View = view.Object;
            const string notes = "some notes";

            contactEditorViewModel.Contact = new Contact
            {
                Notes = notes
            };

            Assert.That(contactEditorViewModel.Notes, Is.EqualTo(notes));
        }

        [Test]
        public void if_Contact_is_set_to_null_notes_is_set_to_empty_string()
        {
            contactEditorViewModel.View = view.Object;

            contactEditorViewModel.Contact = null;

            Assert.That(contactEditorViewModel.Notes, Is.EqualTo(string.Empty));
        }

        [Test]
        public void if_Contact_is_set_phones_list_is_set_in_view()
        {
            contactEditorViewModel.View = view.Object;
            Contact contact = new Contact();
            PhoneCollection phones = new PhoneCollection {new Phone(), new Phone()};
            contact.Phones.AddRange(phones);

            contactEditorViewModel.Contact = contact;

            Assert.That(contactEditorViewModel.Phones, Is.EquivalentTo(phones));
        }

        [Test]
        public void if_Contact_is_set_to_null_phones_list_is_set_to_null_in_view()
        {
            contactEditorViewModel.View = view.Object;

            contactEditorViewModel.Contact = null;

            Assert.That(contactEditorViewModel.Phones, Is.Null);
        }

        [Test]
        public void if_Contact_is_set_emails_list_is_set_in_view()
        {
            contactEditorViewModel.View = view.Object;
            Contact contact = new Contact();
            EmailCollection emails = new EmailCollection {new Email(), new Email()};
            contact.Emails.AddRange(emails);

            contactEditorViewModel.Contact = contact;

            Assert.That(contactEditorViewModel.Emails, Is.EquivalentTo(emails));
        }

        [Test]
        public void if_Contact_is_set_to_null_emails_list_is_set_to_null_in_view()
        {
            contactEditorViewModel.View = view.Object;

            contactEditorViewModel.Contact = null;

            Assert.That(contactEditorViewModel.Emails, Is.Null);
        }

        [Test]
        public void if_Contact_is_set_websites_list_is_set_in_view()
        {
            contactEditorViewModel.View = view.Object;
            Contact contact = new Contact();
            WebSiteCollection webSites = new WebSiteCollection {new WebSite(), new WebSite()};
            contact.WebSites.AddRange(webSites);

            contactEditorViewModel.Contact = contact;

            Assert.That(contactEditorViewModel.WebSites, Is.EquivalentTo(webSites));
        }

        [Test]
        public void if_Contact_is_set_to_null_websites_list_is_set_to_null_in_view()
        {
            contactEditorViewModel.View = view.Object;

            contactEditorViewModel.Contact = null;

            Assert.That(contactEditorViewModel.WebSites, Is.Null);
        }

        [Test]
        public void if_Contact_is_set_addresses_list_is_set_in_view()
        {
            contactEditorViewModel.View = view.Object;
            Contact contact = new Contact();
            PostalAddressCollection postalAddresses = new PostalAddressCollection {new PostalAddress(), new PostalAddress()};
            contact.PostalAddresses.AddRange(postalAddresses);

            contactEditorViewModel.Contact = contact;

            Assert.That(contactEditorViewModel.PostalAddresses, Is.EquivalentTo(postalAddresses));
        }

        [Test]
        public void if_Contact_is_set_to_null_addresses_list_is_set_to_null_in_view()
        {
            contactEditorViewModel.View = view.Object;

            contactEditorViewModel.Contact = null;

            Assert.That(contactEditorViewModel.PostalAddresses, Is.Null);
        }

        [Test]
        public void if_Contact_is_set_dates_list_is_set_in_view()
        {
            contactEditorViewModel.View = view.Object;
            Contact contact = new Contact();
            DateCollection dates = new DateCollection {new Date(), new Date()};
            contact.Dates.AddRange(dates);

            contactEditorViewModel.Contact = contact;

            Assert.That(contactEditorViewModel.Dates, Is.EquivalentTo(dates));
        }

        [Test]
        public void if_Contact_is_set_to_null_dates_list_is_set_to_null_in_view()
        {
            contactEditorViewModel.View = view.Object;

            contactEditorViewModel.Contact = null;

            Assert.That(contactEditorViewModel.Dates, Is.Null);
        }

        [Test]
        public void if_Contact_is_set_socialprofileids_list_is_set_in_view()
        {
            contactEditorViewModel.View = view.Object;
            Contact contact = new Contact();
            SocialProfileIdCollection socialProfileIds = new SocialProfileIdCollection {new SocialProfile(), new SocialProfile()};
            contact.SocialProfileIds.AddRange(socialProfileIds);

            contactEditorViewModel.Contact = contact;

            Assert.That(contactEditorViewModel.SocialProfileIds, Is.EquivalentTo(socialProfileIds));
        }

        [Test]
        public void if_Contact_is_set_to_null_socialprofileids_list_is_set_to_null_in_view()
        {
            contactEditorViewModel.View = view.Object;

            contactEditorViewModel.Contact = null;

            Assert.That(contactEditorViewModel.SocialProfileIds, Is.Null);
        }
    }
}