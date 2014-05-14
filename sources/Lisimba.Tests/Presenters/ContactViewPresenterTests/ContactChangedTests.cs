// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.UserControls;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Presenters.ContactViewPresenterTests
{
    [TestFixture]
    public class ContactChangedTests
    {
        private ContactViewPresenter contactViewPresenter;
        private Mock<IContactView> view;

        [SetUp]
        public void SetUp()
        {
            contactViewPresenter = new ContactViewPresenter(new ZodiacService());
            view = new Mock<IContactView>();
        }

        [Test]
        public void if_View_is_not_set_and_Contact_is_set_nothing_happens()
        {
            contactViewPresenter.Contact = new Contact();
        }

        [Test]
        public void if_Contact_is_set_first_name_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            const string firstName = "alexandru";

            contactViewPresenter.Contact = new Contact
            {
                Name = new PersonName { FirstName = firstName }
            };

            view.VerifySet(x => x.FirstName = firstName, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_to_null_first_name_is_set_to_empty_string()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.FirstName = string.Empty, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_middle_name_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            const string middleName = "nicolae";

            contactViewPresenter.Contact = new Contact
            {
                Name = new PersonName { MiddleName = middleName }
            };

            view.VerifySet(x => x.MiddleName = middleName, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_to_null_middle_name_is_set_to_empty_string()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.MiddleName = string.Empty, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_last_name_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            const string lastName = "iuga";

            contactViewPresenter.Contact = new Contact
            {
                Name = new PersonName { LastName = lastName }
            };

            view.VerifySet(x => x.LastName = lastName, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_to_null_last_name_is_set_to_empty_string()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.LastName = string.Empty, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_nickname_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            const string nickname = "alez";

            contactViewPresenter.Contact = new Contact
            {
                Name = new PersonName { Nickname = nickname }
            };

            view.VerifySet(x => x.Nickname = nickname, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_to_null_nickname_is_set_to_empty_string()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.Nickname = string.Empty, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_birthday_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            DateTime birthDateTime = new DateTime(1980, 06, 13);
            Date birthday = new Date(birthDateTime);

            contactViewPresenter.Contact = new Contact
            {
                Birthday = birthday
            };

            view.VerifySet(x => x.Birthday = birthday.ToString(), Times.Once);
        }

        [Test]
        public void if_Contact_is_set_to_null_birthday_is_set_to_empty_string()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.Birthday = string.Empty, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_notes_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            const string notes = "some notes";

            contactViewPresenter.Contact = new Contact
            {
                Notes = notes
            };

            view.VerifySet(x => x.Notes = notes, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_to_null_notes_is_set_to_empty_string()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.Notes = string.Empty, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_phones_list_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            Contact contact = new Contact();
            PhoneCollection phones = new PhoneCollection { new Phone(), new Phone() };
            contact.Phones.AddRange(phones);
            PhoneCollection actual = null;
            view.SetupSet(x => x.Phones = It.IsAny<PhoneCollection>())
                .Callback<PhoneCollection>(x => actual = x);

            contactViewPresenter.Contact = contact;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EquivalentTo(phones));
        }

        [Test]
        public void if_Contact_is_set_to_null_phones_list_is_set_to_null_in_view()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.Phones = null, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_emails_list_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            Contact contact = new Contact();
            EmailCollection emails = new EmailCollection { new Email(), new Email() };
            contact.Emails.AddRange(emails);
            EmailCollection actual = null;
            view.SetupSet(x => x.Emails = It.IsAny<EmailCollection>())
                .Callback<EmailCollection>(x => actual = x);

            contactViewPresenter.Contact = contact;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EquivalentTo(emails));
        }

        [Test]
        public void if_Contact_is_set_to_null_emails_list_is_set_to_null_in_view()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.Emails = null, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_websites_list_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            Contact contact = new Contact();
            WebSiteCollection webSites = new WebSiteCollection { new WebSite(), new WebSite() };
            contact.WebSites.AddRange(webSites);
            WebSiteCollection actual = null;
            view.SetupSet(x => x.WebSites = It.IsAny<WebSiteCollection>())
                .Callback<WebSiteCollection>(x => actual = x);

            contactViewPresenter.Contact = contact;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EquivalentTo(webSites));
        }

        [Test]
        public void if_Contact_is_set_to_null_websites_list_is_set_to_null_in_view()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.WebSites = null, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_addresses_list_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            Contact contact = new Contact();
            AddressCollection addresses = new AddressCollection { new Address(), new Address() };
            contact.Addresses.AddRange(addresses);
            AddressCollection actual = null;
            view.SetupSet(x => x.Addresses = It.IsAny<AddressCollection>())
                .Callback<AddressCollection>(x => actual = x);

            contactViewPresenter.Contact = contact;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EquivalentTo(addresses));
        }

        [Test]
        public void if_Contact_is_set_to_null_addresses_list_is_set_to_null_in_view()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.Addresses = null, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_dates_list_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            Contact contact = new Contact();
            DateCollection dates = new DateCollection { new Date(), new Date() };
            contact.Dates.AddRange(dates);
            DateCollection actual = null;
            view.SetupSet(x => x.Dates = It.IsAny<DateCollection>())
                .Callback<DateCollection>(x => actual = x);

            contactViewPresenter.Contact = contact;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EquivalentTo(dates));
        }

        [Test]
        public void if_Contact_is_set_to_null_dates_list_is_set_to_null_in_view()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.Dates = null, Times.Once);
        }

        [Test]
        public void if_Contact_is_set_messengerids_list_is_set_in_view()
        {
            contactViewPresenter.View = view.Object;
            Contact contact = new Contact();
            MessengerIdCollection messengerIds = new MessengerIdCollection { new MessengerId(), new MessengerId() };
            contact.MessengerIds.AddRange(messengerIds);
            MessengerIdCollection actual = null;
            view.SetupSet(x => x.MessengerIds = It.IsAny<MessengerIdCollection>())
                .Callback<MessengerIdCollection>(x => actual = x);

            contactViewPresenter.Contact = contact;

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EquivalentTo(messengerIds));
        }

        [Test]
        public void if_Contact_is_set_to_null_messengerids_list_is_set_to_null_in_view()
        {
            contactViewPresenter.View = view.Object;

            contactViewPresenter.Contact = null;

            view.VerifySet(x => x.MessengerIds = null, Times.Once);
        }
    }
}
