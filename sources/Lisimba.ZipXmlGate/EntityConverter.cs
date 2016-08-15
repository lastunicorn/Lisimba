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

using System.Drawing;
using System.IO;
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.ZipXmlGate.Entities;

namespace DustInTheWind.Lisimba.ZipXmlGate
{
    internal static class EntityConverter
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
                Picture = ToByteArray(contact.Picture.Image),
                Birthday = new DateEntity
                {
                    Day = contact.Birthday.Day,
                    Month = contact.Birthday.Month,
                    Year = contact.Birthday.Year,
                    Description = contact.Birthday.Description
                },
                Category = contact.Category,
                Phones = contact.Items.OfType<Phone>().Select(ToEntity).ToListOrNull(),
                Emails = contact.Items.OfType<Email>().Select(ToEntity).ToListOrNull(),
                WebSites = contact.Items.OfType<WebSite>().Select(ToEntity).ToListOrNull(),
                Addresses = contact.Items.OfType<PostalAddress>().Select(ToEntity).ToListOrNull(),
                Dates = contact.Items.OfType<Date>().Select(ToEntity).ToListOrNull(),
                SocialProfileIds = contact.Items.OfType<SocialProfile>().Select(ToEntity).ToListOrNull(),
                Notes = contact.Notes
            };
        }

        private static byte[] ToByteArray(Image image)
        {
            if (image == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
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

            Contact[] contacts = addressBookEntity.Contacts.Select(FromEntity).ToArray();
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

            contact.Picture = new Picture(ToImage(contactEntity.Picture));

            contact.Birthday.Day = contactEntity.Birthday.Day;
            contact.Birthday.Month = contactEntity.Birthday.Month;
            contact.Birthday.Year = contactEntity.Birthday.Year;
            contact.Birthday.Description = contactEntity.Birthday.Description;

            Phone[] phones = contactEntity.Phones.Select(FromEntity).ToArray();
            contact.Items.AddRange(phones);

            Email[] emails = contactEntity.Emails.Select(FromEntity).ToArray();
            contact.Items.AddRange(emails);

            WebSite[] webSites = contactEntity.WebSites.Select(FromEntity).ToArray();
            contact.Items.AddRange(webSites);

            PostalAddress[] addresses = contactEntity.Addresses.Select(FromEntity).ToArray();
            contact.Items.AddRange(addresses);

            Date[] dates = contactEntity.Dates.Select(FromEntity).ToArray();
            contact.Items.AddRange(dates);

            SocialProfile[] socialProfileIds = contactEntity.SocialProfileIds.Select(FromEntity).ToArray();
            contact.Items.AddRange(socialProfileIds);

            contact.Notes = contactEntity.Notes;

            return contact;
        }

        private static Image ToImage(byte[] bytes)
        {
            if (bytes == null)
                return null;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
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