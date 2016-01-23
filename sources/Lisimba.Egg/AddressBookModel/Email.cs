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

using System;

namespace DustInTheWind.Lisimba.Egg.AddressBookModel
{
    /// <summary>
    /// Class containing information about an e-mail
    /// </summary>
    public class Email : IObservableEntity, IEquatable<Email>
    {
        private string address;
        private string description;

        /// <summary>
        /// The e-mail address.
        /// </summary>
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnChanged();
            }
        }

        /// <summary>
        /// A short description of the e-mail address.
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnChanged();
            }
        }

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Creates a new empty <see cref="Email"/> object.
        /// </summary>
        public Email()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new <see cref="Email"/> object with the address and description specified.
        /// </summary>
        /// <param name="address">The e-mail address</param>
        /// <param name="description">A short description of the email address.</param>
        public Email(string address, string description)
        {
            this.address = address;
            this.description = description;
        }

        /// <summary>
        /// Creates a new <see cref="Email"/> object with the data copied from the one passed as parameter.
        /// </summary>
        /// <param name="email"></param>
        public Email(Email email)
        {
            CopyFrom(email);
        }

        /// <summary>
        /// Copy the data from the Email object passed as parameter into the current object.
        /// </summary>
        /// <param name="email"></param>
        private void CopyFrom(Email email)
        {
            address = email.address;
            description = email.description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Email)) return false;

            return Equals((Email) obj);
        }

        public bool Equals(Email email)
        {
            if (ReferenceEquals(null, email)) return false;
            if (ReferenceEquals(this, email)) return true;

            return string.Equals(address, email.address) && string.Equals(description, email.description);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((address != null ? address.GetHashCode() : 0)*397) ^ (description != null ? description.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return address + (description.Length > 0 ? (" - " + description) : string.Empty);
        }
    }
}