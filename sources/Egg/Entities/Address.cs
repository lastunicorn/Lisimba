using System;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
	[XmlRoot("Address")]
	public class Address
	{
		#region Fields

        #endregion

		#region Properties
		
		// Address
        [XmlAttribute("Street")]
        public string Street { get; set; }

        // City
        [XmlAttribute("City")]
        public string City { get; set; }

        // State
        [XmlAttribute("State")]
        public string State { get; set; }

        // ZIP
        [XmlAttribute("PostalCode")]
        public string PostalCode { get; set; }

        // Country
        [XmlAttribute("Country")]
        public string Country { get; set; }

        // Description
        [XmlAttribute("Description")]
        public string Description { get; set; }

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
