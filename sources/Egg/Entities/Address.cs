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

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    [XmlRoot("Address")]
    public class Address : IObservableEntity
    {
        private string street;
        private string city;
        private string state;
        private string postalCode;
        private string country;
        private string description;

        [XmlAttribute("Street")]
        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnChanged();
            }
        }

        [XmlAttribute("City")]
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnChanged();
            }
        }

        [XmlAttribute("State")]
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnChanged();
            }
        }

        [XmlAttribute("PostalCode")]
        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                postalCode = value;
                OnChanged();
            }
        }

        [XmlAttribute("Country")]
        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnChanged();
            }
        }

        [XmlAttribute("Description")]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnChanged();
            }
        }

        #region Event Changed

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        public Address()
            : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        public Address(string address, string city, string state, string postalCode, string country)
            : this(address, city, state, postalCode, country, "")
        {
        }

        public Address(string address, string city, string state, string postalCode, string country, string description)
        {
            Street = address;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            Description = description;
        }

        public Address(Address address)
        {
            CopyFrom(address);
        }

        public void CopyFrom(Address address)
        {
            Street = address.Street;
            City = address.City;
            State = address.State;
            PostalCode = address.PostalCode;
            Country = address.Country;
            Description = address.Description;
        }

        public Address GetCopy()
        {
            return new Address(Street, City, State, PostalCode, Country, Description);
        }

        public override string ToString()
        {
            string tempString = string.Empty;

            if (Street.Length > 0)
            {
                tempString += Street;
            }

            if (City.Length > 0)
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += City;
            }

            if (PostalCode.Length > 0)
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += PostalCode;
            }

            if (State.Length > 0)
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += State;
            }

            if (Country.Length > 0)
            {
                if (tempString.Length > 0) tempString += ", ";
                tempString += Country;
            }

            return tempString;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Address)) return false;

            Address address = (Address)obj;

            if (!Street.Equals(address.Street)) return false;
            if (!City.Equals(address.City)) return false;
            if (!State.Equals(address.State)) return false;
            if (!PostalCode.Equals(address.PostalCode)) return false;
            if (!Country.Equals(address.Country)) return false;
            if (!Description.Equals(address.Description)) return false;

            return true;
        }
    }
}
