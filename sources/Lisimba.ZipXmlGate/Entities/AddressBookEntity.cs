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
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.ZipXmlGate.Entities
{
    [Serializable]
    [XmlRoot("Book")]
    public class AddressBookEntity
    {
        /// <summary>
        /// The version of the application that created this address book.
        /// </summary>
        [XmlElement("Version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the name of the address book.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a collection of contacts.
        /// </summary>
        [XmlArray("Contacts"), XmlArrayItem("Contact")]
        public List<ContactEntity> Contacts { get; set; }
    }
}