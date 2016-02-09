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
    /// <summary>
    /// Class containing information about a social profile id.
    /// </summary>
    [Serializable]
    [XmlRoot("MessengerId")]
    public class SocialProfileIdEntity
    {
        /// <summary>
        /// The so profile id.
        /// </summary>
        [XmlAttribute("Id")]
        public string Id { get; set; }

        /// <summary>
        /// A short description of the social profile id.
        /// </summary>
        [XmlAttribute("Description")]
        public string Description { get; set; }
    }
}