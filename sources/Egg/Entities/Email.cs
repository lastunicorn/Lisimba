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
    /// <summary>
    /// Class containing information about an e-mail
    /// </summary>
    [Serializable()]
    [XmlRoot("Email")]
    public class Email
    {
        #region Fields

        private string address;
        private string description;

        #endregion Fields

        #region Properties

        //[XmlElement("Address")]
        /// <summary>
        /// The e-mail address.
        /// </summary>
        [XmlAttribute("Address")]
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnAddressChanged(new AddressChangedEventArgs(value));
            }
        }

        //[XmlElement("Description")]
        /// <summary>
        /// A short description of the e-mail address.
        /// </summary>
        //[XmlText()]
        [XmlAttribute("Description")]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnDescriptionChanged(new DescriptionChangedEventArgs(value));
            }
        }

        #endregion Properties

        #region Event AddressChanged

        public event EventHandler<AddressChangedEventArgs> AddressChanged;

        public class AddressChangedEventArgs : EventArgs
        {
            public string NewValue { get; private set; }

            public AddressChangedEventArgs(string newValue)
            {
                NewValue = newValue;
            }
        }

        protected void OnAddressChanged(AddressChangedEventArgs e)
        {
            if (AddressChanged != null)
                AddressChanged(this, e);
        }

        #endregion Event AddressChanged

        #region Event DescriptionChanged

        public event EventHandler<DescriptionChangedEventArgs> DescriptionChanged;

        public class DescriptionChangedEventArgs : EventArgs
        {
            public string NewValue { get; private set; }

            public DescriptionChangedEventArgs(string newValue)
            {
                NewValue = newValue;
            }
        }

        protected void OnDescriptionChanged(DescriptionChangedEventArgs e)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, e);
        }

        #endregion Event DescriptionChanged

        /// <summary>
        /// Creates a new empty Email object.
        /// </summary>
        public Email()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new Email object with the address specified. The description text is an empty string.
        /// </summary>
        /// <param name="address"></param>
        public Email(string address)
            : this(address, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new Email object with the address and description specified.
        /// </summary>
        /// <param name="address">The e-mail address</param>
        /// <param name="description">A short description of the email address.</param>
        public Email(string address, string description)
        {
            this.address = address;
            this.description = description;
        }

        /// <summary>
        /// Creates a new Email object with the data copied from the one passed as parameter.
        /// </summary>
        /// <param name="email"></param>
        public Email(Email email)
        {
            CopyFrom(email);
        }

        /// <summary>
        /// Removes the data from all the fields
        /// </summary>
        public void Clear()
        {
            address = string.Empty;
            description = string.Empty;
        }

        /// <summary>
        /// Copy the data from the Email object passed as parameter into the current object.
        /// </summary>
        /// <param name="email"></param>
        public void CopyFrom(Email email)
        {
            address = email.address;
            description = email.description;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Email)) return false;

            Email email = (Email)obj;

            if (!address.Equals(email.address)) return false;
            if (!description.Equals(email.description)) return false;

            return true;
        }

        public override string ToString()
        {
            return address + (description.Length > 0 ? (" - " + description) : string.Empty);
        }
    }
}
