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
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class Contact : IComparable, IObservableEntity
    {
        private PersonName name = new PersonName();

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

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnChanged();
            }
        }

        public PhoneCollection Phones { get; private set; }

        public EmailCollection Emails { get; private set; }

        public WebSiteCollection WebSites { get; private set; }

        public AddressCollection Addresses { get; private set; }

        public DateCollection Dates { get; private set; }

        public MessengerIdCollection MessengerIds { get; private set; }

        private string notes = string.Empty;

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
            Phones = new PhoneCollection();
            Emails = new EmailCollection();
            WebSites = new WebSiteCollection();
            Addresses = new AddressCollection();
            Dates = new DateCollection();
            MessengerIds = new MessengerIdCollection();

            Phones.CollectionChanged += (sender, e) => OnChanged();
            Phones.ItemChanged += (sender, e) => OnChanged();

            Emails.CollectionChanged += (sender, e) => OnChanged();
            Emails.ItemChanged += (sender, e) => OnChanged();

            WebSites.CollectionChanged += (sender, e) => OnChanged();
            WebSites.ItemChanged += (sender, e) => OnChanged();

            Addresses.CollectionChanged += (sender, e) => OnChanged();
            Addresses.ItemChanged += (sender, e) => OnChanged();

            Dates.CollectionChanged += (sender, e) => OnChanged();
            Dates.ItemChanged += (sender, e) => OnChanged();

            MessengerIds.CollectionChanged += (sender, e) => OnChanged();
            MessengerIds.ItemChanged += (sender, e) => OnChanged();

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
            Phones.Clear();
            Emails.Clear();
            WebSites.Clear();
            Addresses.Clear();
            Dates.Clear();
            MessengerIds.Clear();
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

            Phones.CopyFrom(contact.Phones);
            Emails.CopyFrom(contact.Emails);
            WebSites.CopyFrom(contact.WebSites);
            Addresses.CopyFrom(contact.Addresses);
            Dates.CopyFrom(contact.Dates);
            MessengerIds.CopyFrom(contact.MessengerIds);

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
                throw new ArgumentException("obj is not a Contact.");

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
                   Phones.Equals(contact.Phones) &&
                   Emails.Equals(contact.Emails) &&
                   WebSites.Equals(contact.WebSites) &&
                   Addresses.Equals(contact.Addresses) &&
                   Dates.Equals(contact.Dates) &&
                   MessengerIds.Equals(contact.MessengerIds) &&
                   notes == contact.notes;
        }
    }
}