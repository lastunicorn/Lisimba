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
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.ZipXmlGate.Entities
{
    [Serializable]
    [XmlRoot("Address")]
    public class AddressEntity
    {
        [XmlAttribute("Street")]
        public string Street { get; set; }

        [XmlAttribute("City")]
        public string City { get; set; }

        [XmlAttribute("State")]
        public string State { get; set; }

        [XmlAttribute("PostalCode")]
        public string PostalCode { get; set; }

        [XmlAttribute("Country")]
        public string Country { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }
    }
}