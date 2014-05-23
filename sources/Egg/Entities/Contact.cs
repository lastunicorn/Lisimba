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
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable]
    [XmlRoot("Contact")]
    public class Contact : IComparable, IObservableEntity
    {
        private PersonName name = new PersonName();

        [XmlElement("Name")]
        public PersonName Name
        {
            get { return name; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                name.Changed -= HandleNameChanged;
                name = value;
                name.Changed += HandleNameChanged;
            }
        }

        private Date birthday = new Date();

        [XmlElement("Birthday")]
        public Date Birthday
        {
            get { return birthday; }
            set
            {
                if (birthday != null)
                    birthday.Changed -= HandleBirthdayChanged;

                birthday = value;

                if (birthday != null)
                    birthday.Changed += HandleBirthdayChanged;

                OnChanged();
            }
        }

        public ZodiacSign ZogiacSign
        {
            get
            {
                if (birthday.Month == 0 || birthday.Day == 0)
                    return ZodiacSign.NotSpecified;

                switch (birthday.Month)
                {
                    case 1:
                        return birthday.Day <= 19 ? ZodiacSign.Capricorn : ZodiacSign.Aquarius;

                    case 2:
                        return birthday.Day <= 18 ? ZodiacSign.Aquarius : ZodiacSign.Pisces;

                    case 3:
                        return birthday.Day <= 20 ? ZodiacSign.Pisces : ZodiacSign.Aries;

                    case 4:
                        return birthday.Day <= 19 ? ZodiacSign.Aries : ZodiacSign.Taurus;

                    case 5:
                        return birthday.Day <= 20 ? ZodiacSign.Taurus : ZodiacSign.Gemini;

                    case 6:
                        return birthday.Day <= 20 ? ZodiacSign.Gemini : ZodiacSign.Cancer;

                    case 7:
                        return birthday.Day <= 22 ? ZodiacSign.Cancer : ZodiacSign.Leo;

                    case 8:
                        return birthday.Day <= 22 ? ZodiacSign.Leo : ZodiacSign.Virgo;

                    case 9:
                        return birthday.Day <= 22 ? ZodiacSign.Virgo : ZodiacSign.Libra;

                    case 10:
                        return birthday.Day <= 22 ? ZodiacSign.Libra : ZodiacSign.Scorpio;

                    case 11:
                        return birthday.Day <= 21 ? ZodiacSign.Scorpio : ZodiacSign.Sagittarius;

                    case 12:
                        return birthday.Day <= 21 ? ZodiacSign.Sagittarius : ZodiacSign.Capricorn;

                    default:
                        return ZodiacSign.NotSpecified;
                }
            }
        }

        private string category = string.Empty;

        [XmlAttribute("Category")]
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnChanged();
            }
        }

        private PhoneCollection phones = new PhoneCollection();

        [XmlArray("Phones"), XmlArrayItem("Phone")]
        public PhoneCollection Phones
        {
            get { return phones; }
        }

        private EmailCollection emails = new EmailCollection();

        [XmlArray("Emails"), XmlArrayItem("Email")]
        public EmailCollection Emails
        {
            get { return emails; }
        }

        private WebSiteCollection webSites = new WebSiteCollection();

        [XmlArray("WebSites"), XmlArrayItem("WebSite")]
        public WebSiteCollection WebSites
        {
            get { return webSites; }
        }

        private AddressCollection addresses = new AddressCollection();

        [XmlArray("Addresses"), XmlArrayItem("Address")]
        public AddressCollection Addresses
        {
            get { return addresses; }
        }

        private DateCollection dates = new DateCollection();

        [XmlArray("Dates"), XmlArrayItem("Date")]
        public DateCollection Dates
        {
            get { return dates; }
        }

        private MessengerIdCollection messengerIds = new MessengerIdCollection();

        [XmlArray("MessengerIds"), XmlArrayItem("MessengerId")]
        public MessengerIdCollection MessengerIds
        {
            get { return messengerIds; }
        }

        private string notes = string.Empty;

        [XmlElement("Notes")]
        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
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

        public Contact()
        {
            phones.CollectionChanged += (sender, e) => OnChanged();
            phones.ItemChanged += (sender, e) => OnChanged();

            emails.CollectionChanged += (sender, e) => OnChanged();
            emails.ItemChanged += (sender, e) => OnChanged();

            webSites.CollectionChanged += (sender, e) => OnChanged();
            webSites.ItemChanged += (sender, e) => OnChanged();

            addresses.CollectionChanged += (sender, e) => OnChanged();
            addresses.ItemChanged += (sender, e) => OnChanged();

            dates.CollectionChanged += (sender, e) => OnChanged();
            dates.ItemChanged += (sender, e) => OnChanged();

            messengerIds.CollectionChanged += (sender, e) => OnChanged();
            messengerIds.ItemChanged += (sender, e) => OnChanged();

            birthday.Changed += (sender, e) => OnChanged();
        }

        private void HandleNameChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void HandleBirthdayChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        /// <summary>
        /// Remove all the information from the current instance.
        /// </summary>
        public void Clear()
        {
            name.Clear();
            birthday.Clear();
            phones.Clear();
            emails.Clear();
            webSites.Clear();
            addresses.Clear();
            dates.Clear();
            messengerIds.Clear();
            notes = string.Empty;

            OnChanged();
        }

        /// <summary>
        /// Copy all the informations from the specified Contact object to the current instance. All the existing data will be lost.
        /// </summary>
        /// <param name="contact">The Contact object to be copied.</param>
        public void CopyFrom(Contact contact)
        {
            name.CopyFrom(contact.name);

            birthday.CopyFrom(contact.birthday);

            phones.CopyFrom(contact.phones);
            emails.CopyFrom(contact.emails);
            webSites.CopyFrom(contact.webSites);
            addresses.CopyFrom(contact.addresses);
            dates.CopyFrom(contact.dates);
            messengerIds.CopyFrom(contact.messengerIds);

            notes = contact.notes;

            OnChanged();
        }

        /// <summary>
        /// Returns a string that represent the current Contact.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name.ToString();
        }

        public int CompareTo(object obj)
        {
            Contact contact = obj as Contact;

            if (contact == null)
                throw new ArgumentException("object is not a Contact.");

            int comparisonResult = name.Nickname.CompareTo(contact.name.Nickname);

            if (comparisonResult != 0)
                return comparisonResult;

            comparisonResult = name.FirstName.CompareTo(contact.name.FirstName);

            if (comparisonResult != 0)
                return comparisonResult;

            comparisonResult = name.MiddleName.CompareTo(contact.name.MiddleName);

            if (comparisonResult != 0)
                return comparisonResult;

            comparisonResult = name.LastName.CompareTo(contact.name.LastName);

            return comparisonResult;
        }

        public override bool Equals(object obj)
        {
            Contact contact = obj as Contact;

            if (contact == null)
                return false;

            return name.Equals(contact.name) &&
                   birthday == contact.birthday &&
                   phones.Equals(contact.phones) &&
                   emails.Equals(contact.emails) &&
                   webSites.Equals(contact.webSites) &&
                   addresses.Equals(contact.addresses) &&
                   dates.Equals(contact.dates) &&
                   messengerIds.Equals(contact.messengerIds) &&
                   notes == contact.notes;
        }
    }
}