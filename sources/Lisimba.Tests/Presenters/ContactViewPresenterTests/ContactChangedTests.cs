//// Lisimba
//// Copyright (C) 2007-2016 Dust in the Wind
//// 
//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

//using System;
//using System.Collections.Generic;
//using DustInTheWind.Lisimba.ContactEdit;
//using DustInTheWind.Lisimba.Egg.AddressBookModel;
//using Moq;
//using NUnit.Framework;

//namespace DustInTheWind.Lisimba.Tests.Presenters.ContactViewPresenterTests
//{
//    [TestFixture]
//    public class ContactChangedTests
//    {
//        private ContactEditorViewModel contactEditorViewModel;
//        private Mock<IContactEditorView> view;

//        [SetUp]
//        public void SetUp()
//        {
//            contactEditorViewModel = new ContactEditorViewModel();
//            view = new Mock<IContactEditorView>();
//        }

//        [Test]
//        public void if_View_is_not_set_and_Contact_is_set_nothing_happens()
//        {
//            contactEditorViewModel.Contact = new Contact();
//        }

//        [Test]
//        public void if_Contact_is_set_birthday_is_set_in_view()
//        {
//            contactEditorViewModel.View = view.Object;
//            DateTime birthDateTime = new DateTime(1980, 06, 13);
//            Date birthday = new Date(birthDateTime);

//            contactEditorViewModel.Contact = new Contact
//            {
//                Birthday = birthday
//            };

//            Assert.That(contactEditorViewModel.Birthday, Is.SameAs(birthday));
//        }

//        [Test]
//        public void if_Contact_is_set_to_null_birthday_is_set_to_empty_string()
//        {
//            contactEditorViewModel.View = view.Object;

//            contactEditorViewModel.Contact = null;

//            Assert.That(contactEditorViewModel.Birthday, Is.Null);
//        }

//        [Test]
//        public void if_Contact_is_set_notes_is_set_in_view()
//        {
//            contactEditorViewModel.View = view.Object;
//            const string notes = "some notes";

//            contactEditorViewModel.Contact = new Contact
//            {
//                Notes = notes
//            };

//            Assert.That(contactEditorViewModel.Notes, Is.EqualTo(notes));
//        }

//        [Test]
//        public void if_Contact_is_set_to_null_notes_is_set_to_empty_string()
//        {
//            contactEditorViewModel.View = view.Object;

//            contactEditorViewModel.Contact = null;

//            Assert.That(contactEditorViewModel.Notes, Is.EqualTo(string.Empty));
//        }

//        [Test]
//        public void if_Contact_is_set_phones_list_is_set_in_view()
//        {
//            contactEditorViewModel.View = view.Object;
//            Contact contact = new Contact();
//            List<Phone> phones = new List<Phone> { new Phone(), new Phone() };
//            contact.Items.AddRange(phones);

//            contactEditorViewModel.Contact = contact;

//            Assert.That(contactEditorViewModel.ContactItems, Is.EquivalentTo(phones));
//        }

//        [Test]
//        public void if_Contact_is_set_to_null_phones_list_is_set_to_null_in_view()
//        {
//            contactEditorViewModel.View = view.Object;

//            contactEditorViewModel.Contact = null;

//            Assert.That(contactEditorViewModel.ContactItems, Is.Null);
//        }

//        [Test]
//        public void if_Contact_is_set_emails_list_is_set_in_view()
//        {
//            contactEditorViewModel.View = view.Object;
//            Contact contact = new Contact();
//            List<Email> emails = new List<Email> { new Email(), new Email() };
//            contact.Items.AddRange(emails);

//            contactEditorViewModel.Contact = contact;

//            Assert.That(contactEditorViewModel.ContactItems, Is.EquivalentTo(emails));
//        }

//        [Test]
//        public void if_Contact_is_set_to_null_emails_list_is_set_to_null_in_view()
//        {
//            contactEditorViewModel.View = view.Object;

//            contactEditorViewModel.Contact = null;

//            Assert.That(contactEditorViewModel.ContactItems, Is.Null);
//        }

//        [Test]
//        public void if_Contact_is_set_websites_list_is_set_in_view()
//        {
//            contactEditorViewModel.View = view.Object;
//            Contact contact = new Contact();
//            List<WebSite> webSites = new List<WebSite> { new WebSite(), new WebSite() };
//            contact.Items.AddRange(webSites);

//            contactEditorViewModel.Contact = contact;

//            Assert.That(contactEditorViewModel.ContactItems, Is.EquivalentTo(webSites));
//        }

//        [Test]
//        public void if_Contact_is_set_to_null_websites_list_is_set_to_null_in_view()
//        {
//            contactEditorViewModel.View = view.Object;

//            contactEditorViewModel.Contact = null;

//            Assert.That(contactEditorViewModel.ContactItems, Is.Null);
//        }

//        [Test]
//        public void if_Contact_is_set_addresses_list_is_set_in_view()
//        {
//            contactEditorViewModel.View = view.Object;
//            Contact contact = new Contact();
//            List<PostalAddress> postalAddresses = new List<PostalAddress> { new PostalAddress(), new PostalAddress() };
//            contact.Items.AddRange(postalAddresses);

//            contactEditorViewModel.Contact = contact;

//            Assert.That(contactEditorViewModel.ContactItems, Is.EquivalentTo(postalAddresses));
//        }

//        [Test]
//        public void if_Contact_is_set_to_null_addresses_list_is_set_to_null_in_view()
//        {
//            contactEditorViewModel.View = view.Object;

//            contactEditorViewModel.Contact = null;

//            Assert.That(contactEditorViewModel.ContactItems, Is.Null);
//        }

//        [Test]
//        public void if_Contact_is_set_dates_list_is_set_in_view()
//        {
//            contactEditorViewModel.View = view.Object;
//            Contact contact = new Contact();
//            List<Date> dates = new List<Date> { new Date(), new Date() };
//            contact.Items.AddRange(dates);

//            contactEditorViewModel.Contact = contact;

//            Assert.That(contactEditorViewModel.ContactItems, Is.EquivalentTo(dates));
//        }

//        [Test]
//        public void if_Contact_is_set_to_null_dates_list_is_set_to_null_in_view()
//        {
//            contactEditorViewModel.View = view.Object;

//            contactEditorViewModel.Contact = null;

//            Assert.That(contactEditorViewModel.ContactItems, Is.Null);
//        }

//        [Test]
//        public void if_Contact_is_set_socialprofileids_list_is_set_in_view()
//        {
//            contactEditorViewModel.View = view.Object;
//            Contact contact = new Contact();
//            List<SocialProfile> socialProfileIds = new List<SocialProfile> { new SocialProfile(), new SocialProfile() };
//            contact.Items.AddRange(socialProfileIds);

//            contactEditorViewModel.Contact = contact;

//            Assert.That(contactEditorViewModel.ContactItems, Is.EquivalentTo(socialProfileIds));
//        }

//        [Test]
//        public void if_Contact_is_set_to_null_socialprofileids_list_is_set_to_null_in_view()
//        {
//            contactEditorViewModel.View = view.Object;

//            contactEditorViewModel.Contact = null;

//            Assert.That(contactEditorViewModel.ContactItems, Is.Null);
//        }
//    }
//}