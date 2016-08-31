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
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.LisimbaGate.Entities
{
    [Serializable]
    [XmlRoot("Contact")]
    public class ContactEntity
    {
        private byte[] picture;

        [XmlElement("Name")]
        public PersonNameEntity Name { get; set; }

        [XmlIgnore]
        public byte[] Picture
        {
            get { return picture; }
            set
            {
                picture = value;

                PictureHash = ComputeHash(value);
            }
        }

        private static string ComputeHash(byte[] value)
        {
            if (value == null)
                return null;

            using (MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] hash = cryptoServiceProvider.ComputeHash(value);
                return hash.ToHexString();
            }
        }

        [XmlElement("Picture")]
        public string PictureHash { get; set; }

        [XmlElement("Birthday")]
        public DateEntity Birthday { get; set; }

        [XmlAttribute("Category")]
        public string Category { get; set; }

        [XmlArray("Phones"), XmlArrayItem("Phone")]
        public List<PhoneEntity> Phones { get; set; }

        [XmlArray("Emails"), XmlArrayItem("Email")]
        public List<EmailEntity> Emails { get; set; }

        [XmlArray("WebSites"), XmlArrayItem("WebSite")]
        public List<WebSiteEntity> WebSites { get; set; }

        [XmlArray("Addresses"), XmlArrayItem("Address")]
        public List<AddressEntity> Addresses { get; set; }

        [XmlArray("Dates"), XmlArrayItem("Date")]
        public List<DateEntity> Dates { get; set; }

        [XmlArray("MessengerIds"), XmlArrayItem("MessengerId")]
        public List<SocialProfileIdEntity> SocialProfileIds { get; set; }

        [XmlElement("Notes")]
        public string Notes { get; set; }
    }
}