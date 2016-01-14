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

using System.Configuration;

namespace DustInTheWind.Lisimba.Common.Config
{
    public class LoadFileAtStartConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("type", DefaultValue = "last", IsRequired = true, IsKey = false)]
        public string Type
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("fileName", IsRequired = false, IsKey = false)]
        public string FileName
        {
            get { return (string) this["fileName"]; }
            set { this["fileName"] = value; }
        }
    }
}