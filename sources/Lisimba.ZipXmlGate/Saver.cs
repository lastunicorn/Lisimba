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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Exceptions;
using DustInTheWind.Lisimba.Gating.Entities;
using ICSharpCode.SharpZipLib.Zip;

namespace DustInTheWind.Lisimba.Gating
{
    internal class Saver
    {
        public void Save(AddressBook addressBook, string fileName)
        {
            try
            {
                // Create serializer
                XmlSerializer serializer = new XmlSerializer(typeof (AddressBookEntity));

                // Create settings object
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                settings.NewLineChars = "\r\n";
                settings.Encoding = Encoding.UTF8;
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.NewLineHandling = NewLineHandling.None;

                // Create memory stream to write the xml file to.
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create writer
                    using (XmlWriter xw = XmlWriter.Create(ms, settings))
                    {
                        AddressBookEntity addressBookEntity = ToEntity(addressBook);

                        // Serialize to xml
                        serializer.Serialize(xw, addressBookEntity);

                        ms.Position = 0;

                        // Zip the xml file

                        using (ZipOutputStream zs = new ZipOutputStream(File.OpenWrite(fileName)))
                        {
                            zs.SetLevel(9);

                            ZipEntry zentry = new ZipEntry("file.xml");
                            zentry.CompressionMethod = CompressionMethod.Deflated;

                            zs.PutNextEntry(zentry);

                            int size;
                            byte[] buffer = new byte[10240];
                            do
                            {
                                size = ms.Read(buffer, 0, buffer.Length);
                                zs.Write(buffer, 0, size);
                            } while (size > 0);

                            zs.Finish();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new EggException("Error saving. [" + ex.Message + "]", ex);
            }
        }

        private AddressBookEntity ToEntity(AddressBook addressBook)
        {
            return new AddressBookEntity
            {
                Version = addressBook.Version,
                Name = addressBook.Name,
                Contacts = addressBook.Contacts.Select(ToEntity).ToList()
            };
        }

        private ContactEntity ToEntity(Contact contact)
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
                Phones = contact.Phones.Select(ToEntity).ToList(),
                Emails = contact.Emails.Select(ToEntity).ToList(),
                WebSites = contact.WebSites.Select(ToEntity).ToList(),
                Addresses = contact.PostalAddresses.Select(ToEntity).ToList(),
                Dates = contact.Dates.Select(ToEntity).ToList(),
                SocialProfileIds = contact.SocialProfileIds.Select(ToEntity).ToList(),
                Notes = contact.Notes
            };
        }

        private PhoneEntity ToEntity(Phone phone)
        {
            return new PhoneEntity
            {
                Number = phone.Number,
                Description = phone.Description
            };
        }

        private EmailEntity ToEntity(Email email)
        {
            return new EmailEntity
            {
                Address = email.Address,
                Description = email.Description
            };
        }

        private WebSiteEntity ToEntity(WebSite webSite)
        {
            return new WebSiteEntity
            {
                Address = webSite.Address,
                Description = webSite.Description
            };
        }

        private AddressEntity ToEntity(PostalAddress postalAddress)
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

        private DateEntity ToEntity(Date date)
        {
            return new DateEntity
            {
                Day = date.Day,
                Month = date.Month,
                Year = date.Year,
                Description = date.Description
            };
        }

        private SocialProfileIdEntity ToEntity(SocialProfile socialProfile)
        {
            return new SocialProfileIdEntity
            {
                Id = socialProfile.Id,
                Description = socialProfile.Description
            };
        }
    }
}