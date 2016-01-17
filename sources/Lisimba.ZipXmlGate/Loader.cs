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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Exceptions;
using DustInTheWind.Lisimba.Gating.Entities;
using ICSharpCode.SharpZipLib.Zip;

namespace DustInTheWind.Lisimba.Gating
{
    internal class Loader
    {
        private readonly List<Exception> warnings;

        public IEnumerable<Exception> Warnings
        {
            get { return warnings; }
        }

        public Loader()
        {
            warnings = new List<Exception>();
        }

        public AddressBook Load(string fileName)
        {
            warnings.Clear();

            AddressBook book;

            Version eggVersion = Assembly.GetExecutingAssembly().GetName().Version;

            //System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            //book = (Book)formatter.Deserialize(stream);
            //stream.Close();

            //return book;

            // Create unzipper.
            using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(fileName)))
            {
                ZipEntry zipEntry;

                // Search for the "file.xml" file
                do
                {
                    zipEntry = zipInputStream.GetNextEntry();

                    if (zipEntry == null)
                        throw new EggException("Incorrect file. The archive does not contains the \"file.xml\" file.");
                } while (!zipEntry.Name.Equals("file.xml"));

                // Unzip the "file.xml" file into memory.
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[10240];

                    while (true)
                    {
                        int size = zipInputStream.Read(buffer, 0, buffer.Length);

                        if (size > 0)
                            ms.Write(buffer, 0, size);
                        else
                            break;
                    }

                    ms.Position = 0;

                    // Read lsb version
                    Version lsbVersion = ReadLsbVersion(ms);

                    // Compare versions
                    if (lsbVersion == null)
                    {
                        string warningText = string.Format("The version of the file \"{0}\" could not be determined.", fileName);
                        EggIncorrectVersionException warning = new EggIncorrectVersionException(warningText);
                        warnings.Add(warning);
                    }
                    else if (lsbVersion.ToString(2) != eggVersion.ToString(2))
                    {
                        string warningText = string.Format("The file \"{0}\" is created with another version of the Egg.\n\nCurrent Egg version = {1}\nFile was created by Egg version = {2}", fileName, eggVersion.ToString(2), lsbVersion.ToString(2));
                        EggIncorrectVersionException warning = new EggIncorrectVersionException(warningText);
                        warnings.Add(warning);
                    }

                    ms.Position = 0;

                    // Deserialize the unzipped "file.xml" file
                    using (XmlTextReader xr = new XmlTextReader(ms))
                    {
                        // Create serializer
                        XmlSerializer serializer = new XmlSerializer(typeof (AddressBookEntity));

                        AddressBookEntity addressBookEntity = (AddressBookEntity) serializer.Deserialize(xr);
                        book = FromEntity(addressBookEntity);
                    }
                }
            }

            book.Version = eggVersion.ToString();

            return book;
        }

        private AddressBook FromEntity(AddressBookEntity addressBookEntity)
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

        private Contact FromEntity(ContactEntity contactEntity)
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
            contact.Phones.AddRange(phones);

            IEnumerable<Email> emails = contactEntity.Emails.Select(FromEntity);
            contact.Emails.AddRange(emails);

            IEnumerable<WebSite> webSites = contactEntity.WebSites.Select(FromEntity);
            contact.WebSites.AddRange(webSites);

            IEnumerable<PostalAddress> addresses = contactEntity.Addresses.Select(FromEntity);
            contact.PostalAddresses.AddRange(addresses);

            IEnumerable<Date> dates = contactEntity.Dates.Select(FromEntity);
            contact.Dates.AddRange(dates);

            IEnumerable<SocialProfile> socialProfileIds = contactEntity.SocialProfileIds.Select(FromEntity);
            contact.SocialProfileIds.AddRange(socialProfileIds);

            contact.Notes = contactEntity.Notes;

            return contact;
        }

        private Phone FromEntity(PhoneEntity phoneEntity)
        {
            return new Phone
            {
                Number = phoneEntity.Number,
                Description = phoneEntity.Description
            };
        }

        private Email FromEntity(EmailEntity emailEntity)
        {
            return new Email
            {
                Address = emailEntity.Address,
                Description = emailEntity.Description
            };
        }

        private WebSite FromEntity(WebSiteEntity webSiteEntity)
        {
            return new WebSite
            {
                Address = webSiteEntity.Address,
                Description = webSiteEntity.Description
            };
        }

        private PostalAddress FromEntity(AddressEntity addeEntity)
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

        private Date FromEntity(DateEntity dateEntity)
        {
            return new Date
            {
                Day = dateEntity.Day,
                Month = dateEntity.Month,
                Year = dateEntity.Year,
                Description = dateEntity.Description
            };
        }

        private SocialProfile FromEntity(SocialProfileIdEntity socialProfileIdEntity)
        {
            return new SocialProfile
            {
                Id = socialProfileIdEntity.Id,
                Description = socialProfileIdEntity.Description
            };
        }

        private Version ReadLsbVersion(Stream stream)
        {
            Version ver = null;
            XmlTextReader xmlTextReader = new XmlTextReader(stream);
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
            xmlTextReader.MoveToContent();

            while (xmlTextReader.Read())
            {
                if (!xmlTextReader.Name.Equals("Version") || xmlTextReader.NodeType != XmlNodeType.Element)
                    continue;

                if (xmlTextReader.IsEmptyElement)
                    break;

                if (xmlTextReader.Read() && xmlTextReader.NodeType == XmlNodeType.Text)
                    ver = new Version(xmlTextReader.Value);

                break;
            }

            return ver;
        }
    }
}