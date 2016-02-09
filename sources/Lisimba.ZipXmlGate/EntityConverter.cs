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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Gating.Entities;

namespace DustInTheWind.Lisimba.Gating
{
    static class EntityConverter
    {
        #region ToEntity

        public static AddressBookEntity ToEntity(AddressBook addressBook)
        {
            return new AddressBookEntity
            {
                Version = addressBook.Version,
                Name = addressBook.Name,
                Contacts = addressBook.Contacts.Select(ToEntity).ToList()
            };
        }

        private static ContactEntity ToEntity(Contact contact)
        {
            return new ContactEntity
            {
                Name = new PersonNameEntity
                {
                    FirstName = contact.Name.FirstName,
                    MiddleName = contact.Name.MiddleName,
                    LastName = contact.Name.LastName,
                    Nickname = contact.Name.Nickname
                },
                Birthday = new DateEntity
                {
                    Day = contact.Birthday.Day,
                    Month = contact.Birthday.Month,
                    Year = contact.Birthday.Year,
                    Description = contact.Birthday.Description
                },
                Category = contact.Category,
                Phones = contact.Items.OfType<Phone>().Select(ToEntity).ToList(),
                Emails = contact.Items.OfType<Email>().Select(ToEntity).ToList(),
                WebSites = contact.Items.OfType<WebSite>().Select(ToEntity).ToList(),
                Addresses = contact.Items.OfType<PostalAddress>().Select(ToEntity).ToList(),
                Dates = contact.Items.OfType<Date>().Select(ToEntity).ToList(),
                SocialProfileIds = contact.Items.OfType<SocialProfile>().Select(ToEntity).ToList(),
                Notes = contact.Notes
            };
        }

        private static PhoneEntity ToEntity(Phone phone)
        {
            return new PhoneEntity
            {
                Number = phone.Number,
                Description = phone.Description
            };
        }

        private static EmailEntity ToEntity(Email email)
        {
            return new EmailEntity
            {
                Address = email.Address,
                Description = email.Description
            };
        }

        private static WebSiteEntity ToEntity(WebSite webSite)
        {
            return new WebSiteEntity
            {
                Address = webSite.Address,
                Description = webSite.Description
            };
        }

        private static AddressEntity ToEntity(PostalAddress postalAddress)
        {
            return new AddressEntity
            {
                Street = postalAddress.Street,
                City = postalAddress.City,
                State = postalAddress.State,
                PostalCode = postalAddress.PostalCode,
                Country = postalAddress.Country,
                Description = postalAddress.Description
            };
        }

        private static DateEntity ToEntity(Date date)
        {
            return new DateEntity
            {
                Day = date.Day,
                Month = date.Month,
                Year = date.Year,
                Description = date.Description
            };
        }

        private static SocialProfileIdEntity ToEntity(SocialProfile socialProfile)
        {
            return new SocialProfileIdEntity
            {
                Id = socialProfile.Id,
                Description = socialProfile.Description
            };
        }

        #endregion

        #region From Entity
        
        public static AddressBook FromEntity(AddressBookEntity addressBookEntity)
        {
            AddressBook addressBook = new AddressBook
            {
                Version = addressBookEntity.Version,
                Name = addressBookEntity.Name
            };

            IEnumerable<Contact> contacts = addressBookEntity.Contacts.Select(FromEntity);
            addressBook.Contacts.AddRange(contacts);

            return addressBook;
        }

        private static Contact FromEntity(ContactEntity contactEntity)
        {
            Contact contact = new Contact();

            contact.Name.FirstName = contactEntity.Name.FirstName;
            contact.Name.MiddleName = contactEntity.Name.MiddleName;
            contact.Name.LastName = contactEntity.Name.LastName;
            contact.Name.Nickname = contactEntity.Name.Nickname;

            contact.Birthday.Day = contactEntity.Birthday.Day;
            contact.Birthday.Month = contactEntity.Birthday.Month;
            contact.Birthday.Year = contactEntity.Birthday.Year;
            contact.Birthday.Description = contactEntity.Birthday.Description;

            IEnumerable<Phone> phones = contactEntity.Phones.Select(FromEntity);
            contact.Items.AddRange(phones);

            IEnumerable<Email> emails = contactEntity.Emails.Select(FromEntity);
            contact.Items.AddRange(emails);

            IEnumerable<WebSite> webSites = contactEntity.WebSites.Select(FromEntity);
            contact.Items.AddRange(webSites);

            IEnumerable<PostalAddress> addresses = contactEntity.Addresses.Select(FromEntity);
            contact.Items.AddRange(addresses);

            IEnumerable<Date> dates = contactEntity.Dates.Select(FromEntity);
            contact.Items.AddRange(dates);

            IEnumerable<SocialProfile> socialProfileIds = contactEntity.SocialProfileIds.Select(FromEntity);
            contact.Items.AddRange(socialProfileIds);

            contact.Notes = contactEntity.Notes;

            return contact;
        }

        private static Phone FromEntity(PhoneEntity phoneEntity)
        {
            return new Phone
            {
                Number = phoneEntity.Number,
                Description = phoneEntity.Description
            };
        }

        private static Email FromEntity(EmailEntity emailEntity)
        {
            return new Email
            {
                Address = emailEntity.Address,
                Description = emailEntity.Description
            };
        }

        private static WebSite FromEntity(WebSiteEntity webSiteEntity)
        {
            return new WebSite
            {
                Address = webSiteEntity.Address,
                Description = webSiteEntity.Description
            };
        }

        private static PostalAddress FromEntity(AddressEntity addeEntity)
        {
            return new PostalAddress
            {
                Street = addeEntity.Street,
                City = addeEntity.City,
                State = addeEntity.State,
                PostalCode = addeEntity.PostalCode,
                Country = addeEntity.Country,
                Description = addeEntity.Description
            };
        }

        private static Date FromEntity(DateEntity dateEntity)
        {
            return new Date
            {
                Day = dateEntity.Day,
                Month = dateEntity.Month,
                Year = dateEntity.Year,
                Description = dateEntity.Description
            };
        }

        private static SocialProfile FromEntity(SocialProfileIdEntity socialProfileIdEntity)
        {
            return new SocialProfile
            {
                Id = socialProfileIdEntity.Id,
                Description = socialProfileIdEntity.Description
            };
        }

        #endregion
    }
}