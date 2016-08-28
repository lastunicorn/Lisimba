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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.RandomUserGate.Properties;
using Newtonsoft.Json;

namespace DustInTheWind.Lisimba.RandomUserGate
{
    public class RandomUserGate : GateBase
    {
        public override string Id
        {
            get { return "RandomUserGate"; }
        }

        public override string Name
        {
            get { return "Random User Gate"; }
        }

        public override string Description
        {
            get { return "Randomly generates a number of contacts."; }
        }

        public override Image Icon16
        {
            get { return Resources.randomuser_16; }
        }

        public override bool CanLoad
        {
            get { return true; }
        }

        public override bool CanSave
        {
            get { return false; }
        }

        public override void Save(AddressBook addressBook, string connectionString)
        {
            throw new NotSupportedException();
        }

        public override AddressBook Load(string connectionString)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://randomuser.me/api?results=100");

            using (WebResponse response = request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(responseStream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                Stopwatch sw = Stopwatch.StartNew();
                JsonSerializer serializer = new JsonSerializer();

                RandomUserResponse randomUserResponse = serializer.Deserialize<RandomUserResponse>(jsonReader);

                long millis1 = sw.ElapsedMilliseconds;
                AddressBook addressBook = new AddressBook();

                IEnumerable<Contact> contacts = randomUserResponse.Results
                    .Select(CreateContact);

                long millis2 = sw.ElapsedMilliseconds;
                addressBook.Contacts.AddRange(contacts);

                long millis3 = sw.ElapsedMilliseconds;
                return addressBook;
            }
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

            contact.Items.Add(new Email { Address = randomUserResult.Email });
            contact.Items.Add(new PostalAddress
            {
                Street = randomUserResult.Location.Street,
                City = randomUserResult.Location.City,
                State = randomUserResult.Location.State,
                PostalCode = randomUserResult.Location.PostCode
            });
            contact.Items.Add(new Phone { Number = randomUserResult.Phone });
            contact.Items.Add(new Phone { Number = randomUserResult.Cell, Description = "cellphone"});

            return contact;
        }

        private static Image RetrievePicture(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (WebResponse response = request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            {
                return Image.FromStream(responseStream);
            }
        }
    }
}
