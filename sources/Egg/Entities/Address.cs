using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg
{
    [Serializable()]
	[XmlRoot("Address")]
	public class Address
	{
		#region Fields
		
		private string street;
		private string city;
		private string state;
		private string postalCode;
		private string country;
		private string description;

		#endregion

		#region Properties
		
		// Address
        [XmlAttribute("Street")]
		public string Street
		{
			get { return this.street; }
			set { this.street = value; }
		}

		// City
        [XmlAttribute("City")]
		public string City
		{
			get { return this.city; }
			set { this.city = value; }
		}

		// State
        [XmlAttribute("State")]
		public string State
		{
			get { return this.state; }
			set { this.state = value; }
		}

		// ZIP
        [XmlAttribute("PostalCode")]
		public string PostalCode
		{
			get { return this.postalCode; }
			set { this.postalCode = value; }
		}

		// Country
        [XmlAttribute("Country")]
		public string Country
		{
			get { return this.country; }
			set { this.country = value; }
		}

		// Description
        [XmlAttribute("Description")]
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		#endregion

		#region Constructors
		
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
			this.street = address;
			this.city = city;
			this.state = state;
			this.postalCode = postalCode;
			this.country = country;
			this.description = description;
		}

		public Address(Address a)
		{
			this.CopyFrom(a);
		}

		#endregion

		#region Copy
		
		public void CopyFrom(Address a)
		{
			this.street = a.street;
			this.city = a.city;
			this.state = a.state;
			this.postalCode = a.postalCode;
			this.country = a.country;
			this.description = a.description;
		}

		public Address GetCopy()
		{
			return new Address(this.street, this.city, this.state, this.postalCode, this.country, this.description);
		}

		#endregion

        public override string ToString()
        {
            string tempString = string.Empty;

            if (street.Length > 0)
            {
                tempString += street;
            }

            if (city.Length > 0)
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += city;
            }

            if (postalCode.Length > 0)
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += postalCode;
            }

            if (state.Length > 0)
            {
                if (tempString.Length > 0) tempString += " ";
                tempString += state;
            }

            if (country.Length > 0)
            {
                if (tempString.Length > 0) tempString += ", ";
                tempString += country;
            }

            return tempString;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Address)) return false;

            Address address = (Address)obj;

            if (!street.Equals(address.street)) return false;
            if (!city.Equals(address.city)) return false;
            if (!state.Equals(address.state)) return false;
            if (!postalCode.Equals(address.postalCode)) return false;
            if (!country.Equals(address.country)) return false;
            if (!description.Equals(address.description)) return false;

            return true;
        }
	}
}
