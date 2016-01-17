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
    public class WebSite : IObservableEntity, IEquatable<WebSite>
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

        #region Event Changed

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        /// <summary>
        /// Creates a new empty WebSite object.
        /// </summary>
        public WebSite()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new WebSite object with the address and description specified.
        /// </summary>
        /// <param name="address">The web site address</param>
        /// <param name="description">A short description of the web site address.</param>
        public WebSite(string address, string description)
        {
            this.address = address;
            this.description = description;
        }

        /// <summary>
        /// Creates a new WebSite object with the data copied from the one passed as parameter.
        /// </summary>
        /// <param name="webSite"></param>
        public WebSite(WebSite webSite)
        {
            CopyFrom(webSite);
        }

        /// <summary>
        /// Removes the data from all the fields.
        /// </summary>
        public void Clear()
        {
            address = string.Empty;
            description = string.Empty;
        }

        public void CopyFrom(WebSite webSite)
        {
            address = webSite.address;
            description = webSite.description;
        }

        //public override bool Equals(object obj)
        //{
        //    WebSite webSite = obj as WebSite;

        //    if (webSite == null)
        //        return false;

        //    if (!address.Equals(webSite.address)) return false;
        //    if (!description.Equals(webSite.description)) return false;

        //    return true;
        //}

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (WebSite)) return false;

            return Equals((WebSite) obj);
        }

        public bool Equals(WebSite webSite)
        {
            if (ReferenceEquals(null, webSite)) return false;
            if (ReferenceEquals(this, webSite)) return true;

            return string.Equals(address, webSite.address) && string.Equals(description, webSite.description);
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
            return address + (description.Length > 0 ? " - " + description : string.Empty);
        }
    }
}