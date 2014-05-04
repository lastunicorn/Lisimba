using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg
{
    /// <summary>
    /// Class containing information about an e-mail
    /// </summary>
    [Serializable()]
    [XmlRoot("WebSite")]
    public class WebSite
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
            get { return this.address; }
            set { this.address = value; this.OnAddressChanged(new AddressChangedEventArgs(value)); }
        }

        //[XmlElement("Description")]
        /// <summary>
        /// A short description of the e-mail address.
        /// </summary>
        //[XmlText()]
        [XmlAttribute("Description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; this.OnDescriptionChanged(new DescriptionChangedEventArgs(value)); }
        }

        #endregion Properties

        #region Events

        #region Event AddressChanged

        public event AddressChangedHandler AddressChanged;
        public delegate void AddressChangedHandler(object sender, AddressChangedEventArgs e);

        public class AddressChangedEventArgs : EventArgs
        {
            private string newValue;

            public string NewValue
            {
                get { return newValue; }
            }

            public AddressChangedEventArgs(string newValue)
            {
                this.newValue = newValue;
            }
        }

        protected void OnAddressChanged(AddressChangedEventArgs e)
        {
            if (AddressChanged != null)
            {
                AddressChanged(this, e);
            }
        }

        #endregion Event AddressChanged

        #region Event DescriptionChanged

        public event DescriptionChangedHandler DescriptionChanged;
        public delegate void DescriptionChangedHandler(object sender, DescriptionChangedEventArgs e);

        public class DescriptionChangedEventArgs : EventArgs
        {
            private string newValue;

            public string NewValue
            {
                get { return newValue; }
            }

            public DescriptionChangedEventArgs(string newValue)
            {
                this.newValue = newValue;
            }
        }

        protected void OnDescriptionChanged(DescriptionChangedEventArgs e)
        {
            if (DescriptionChanged != null)
            {
                DescriptionChanged(this, e);
            }
        }

        #endregion Event DescriptionChanged

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new empty WebSite object.
        /// </summary>
        public WebSite()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new WebSite object with the address specified. The description text is an empty string.
        /// </summary>
        /// <param name="address"></param>
        public WebSite(string address)
            : this(address, string.Empty)
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
        /// <param name="email"></param>
        public WebSite(WebSite webSite)
        {
            this.CopyFrom(webSite);
        }

        #endregion Constructors

        #region public void Clear()

        /// <summary>
        /// Removes the data from all the fields
        /// </summary>
        public void Clear()
        {
            this.address = string.Empty;
            this.description = string.Empty;
        }

        #endregion

        #region public void CopyFrom(WebSite webSite)

        /// <summary>
        /// Copy the data from the Email object passed as parameter into the current object.
        /// </summary>
        /// <param name="email"></param>
        public void CopyFrom(WebSite webSite)
        {
            this.address = webSite.address;
            this.description = webSite.description;
        }

        #endregion

        #region public override bool Equals(object obj)

        public override bool Equals(object obj)
        {
            if (!(obj is WebSite)) return false;

            WebSite webSite = (WebSite)obj;

            if (!address.Equals(webSite.address)) return false;
            if (!description.Equals(webSite.description)) return false;

            return true;
        }

        #endregion

        #region public override string ToString()

        public override string ToString()
        {
            return this.address + (this.description.Length > 0 ? " - " + this.description : string.Empty);
        }

        #endregion
    }
}
