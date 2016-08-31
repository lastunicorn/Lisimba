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

namespace DustInTheWind.Lisimba.LisimbaGate.Entities
{
    [Serializable]
    [XmlRoot("Name")]
    public class PersonNameEntity
    {
        [XmlAttribute("First")]
        public string FirstName { get; set; }

        [XmlAttribute("Middle")]
        public string MiddleName { get; set; }

        [XmlAttribute("Last")]
        public string LastName { get; set; }

        [XmlAttribute("Nickname")]
        public string Nickname { get; set; }
    }
}