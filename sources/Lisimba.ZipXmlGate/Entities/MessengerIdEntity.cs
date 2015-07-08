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
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Gating.Entities
{
    /// <summary>
    /// Class containing information about a messenger id.
    /// </summary>
    [Serializable]
    [XmlRoot("MessengerId")]
    public class MessengerIdEntity
    {
        /// <summary>
        /// The messenger id.
        /// </summary>
        [XmlAttribute("Id")]
        public string Id { get; set; }

        /// <summary>
        /// A short description of the e-mail address.
        /// </summary>
        [XmlAttribute("Description")]
        public string Description { get; set; }
    }
}