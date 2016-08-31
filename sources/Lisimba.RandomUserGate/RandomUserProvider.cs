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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.RandomUserGate.RandomUserModel;
using Newtonsoft.Json;

namespace DustInTheWind.Lisimba.RandomUserGate
{
    internal class RandomUserProvider
    {
        public static List<Contact> RetrieveUsers(int count)
        {
            string url = string.Format("https://randomuser.me/api?results={0}", count);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (WebResponse response = request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(responseStream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new JsonSerializer();
                RandomUserResponse randomUserResponse = serializer.Deserialize<RandomUserResponse>(jsonReader);

                return ConvertToContacts(randomUserResponse);
            }
        }

        private static List<Contact> ConvertToContacts(RandomUserResponse randomUserResponse)
        {
            List<Contact> contacts = new List<Contact>();

            Parallel.ForEach(randomUserResponse.Results, x =>
            {
                Contact contact = CreateContact(x);

                lock (contacts)
                    contacts.Add(contact);
            });

            return contacts;
        }

        private static Contact CreateContact(RandomUserResult randomUserResult)
        {
            Contact contact = new Contact
            {
                Name = new PersonName
                {
                    FirstName = randomUserResult.Name.First,
                    LastName = randomUserResult.Name.Last
                },
                Birthday = new Date(DateTime.Parse(randomUserResult.Dob)),
                Picture = new Picture { Image = RetrievePicture(randomUserResult.Picture.Large) }
            };

            List<ContactItem> items = new List<ContactItem>
            {
                new Email {Address = randomUserResult.Email},
                new PostalAddress
                {
                    Street = randomUserResult.Location.Street,
                    City = randomUserResult.Location.City,
                    State = randomUserResult.Location.State,
                    PostalCode = randomUserResult.Location.PostCode
                },
                new Phone {Number = randomUserResult.Phone},
                new Phone {Number = randomUserResult.Cell, Description = "cellphone"}
            };

            contact.Items.AddRange(items);

            return contact;
        }

        private static Image RetrievePicture(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    Image image = Image.FromStream(responseStream);

                    return image;
                }
            }
        }
    }
}