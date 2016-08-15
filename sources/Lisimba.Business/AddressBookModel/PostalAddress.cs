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

namespace DustInTheWind.Lisimba.Business.AddressBookModel
{
    public class PostalAddress : ContactItem, IEquatable<PostalAddress>
    {
        private string street;
        private string city;
        private string state;
        private string postalCode;
        private string country;

        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnChanged();
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnChanged();
            }
        }

        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnChanged();
            }
        }

        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                postalCode = value;
                OnChanged();
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnChanged();
            }
        }

        public PostalAddress()
            : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        public PostalAddress(string street, string city, string state, string postalCode, string country, string description)
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.postalCode = postalCode;
            this.country = country;
            this.description = description;
        }

        public PostalAddress(PostalAddress postalAddress)
        {
            CopyFrom(postalAddress);
        }

        public override void CopyFrom(ContactItem contactItem)
        {
            if (contactItem == null) throw new ArgumentNullException("contactItem");

            PostalAddress postalAddress = contactItem as PostalAddress;

            if (postalAddress != null)
                CopyFrom(postalAddress);
        }

        public void CopyFrom(PostalAddress postalAddress)
        {
            street = postalAddress.street;
            city = postalAddress.city;
            state = postalAddress.state;
            postalCode = postalAddress.postalCode;
            country = postalAddress.country;
            description = postalAddress.description;

            OnChanged();
        }

        public override ContactItem Clone()
        {
            return new PostalAddress(street, city, state, postalCode, country, description);
        }

        public override string ToString()
        {
            string tempString = string.Empty;

            if (!string.IsNullOrEmpty(Street))
            {
                tempString += Street;
            }

            if (!string.IsNullOrEmpty(City))
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += City;
            }

            if (!string.IsNullOrEmpty(PostalCode))
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += PostalCode;
            }

            if (!string.IsNullOrEmpty(State))
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += State;
            }

            if (!string.IsNullOrEmpty(Country))
            {
                if (tempString.Length > 0) tempString += ", ";
                tempString += Country;
            }

            if (!string.IsNullOrEmpty(Description))
            {
                if (tempString.Length > 0) tempString += " - ";
                tempString += Description;
            }

            return tempString;
        }

        //public override bool Equals(object obj)
        //{
        //    if (!(obj is Address)) return false;

        //    Address address = (Address)obj;

        //    if (!Street.Equals(address.Street)) return false;
        //    if (!City.Equals(address.City)) return false;
        //    if (!State.Equals(address.State)) return false;
        //    if (!PostalCode.Equals(address.PostalCode)) return false;
        //    if (!Country.Equals(address.Country)) return false;
        //    if (!Description.Equals(address.Description)) return false;

        //    return true;
        //}

        public bool Equals(PostalAddress other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(street, other.street) &&
                   string.Equals(city, other.city) &&
                   string.Equals(state, other.state) &&
                   string.Equals(postalCode, other.postalCode) &&
                   string.Equals(country, other.country) &&
                   string.Equals(description, other.description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PostalAddress)) return false;

            return Equals((PostalAddress)obj);
        }

        public static bool Equals(PostalAddress postalAddress1, PostalAddress postalAddress2)
        {
            if (postalAddress1 == null)
                return postalAddress2 == null;

            return postalAddress1.Equals(postalAddress2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (street != null ? street.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (city != null ? city.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (state != null ? state.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (postalCode != null ? postalCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (country != null ? country.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (description != null ? description.GetHashCode() : 0);

                return hashCode;
            }
        }
    }
}