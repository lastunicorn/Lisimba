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
    public class Contact : IComparable, IObservableEntity, IEquatable<Contact>
    {
        private PersonName name;

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

        private Date birthday;

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

        public ZodiacSign ZodiacSign
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

        public PostalAddressCollection PostalAddresses { get; private set; }

        public DateCollection Dates { get; private set; }

        public SocialProfileIdCollection SocialProfileIds { get; private set; }

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
            name = new PersonName();
            birthday = new Date();

            Phones = new PhoneCollection();
            Emails = new EmailCollection();
            WebSites = new WebSiteCollection();
            PostalAddresses = new PostalAddressCollection();
            Dates = new DateCollection();
            SocialProfileIds = new SocialProfileIdCollection();

            name.Changed += (sender, args) => OnChanged();
            birthday.Changed += (sender, args) => OnChanged();

            Phones.CollectionChanged += (sender, e) => OnChanged();
            Phones.ItemChanged += (sender, e) => OnChanged();

            Emails.CollectionChanged += (sender, e) => OnChanged();
            Emails.ItemChanged += (sender, e) => OnChanged();

            WebSites.CollectionChanged += (sender, e) => OnChanged();
            WebSites.ItemChanged += (sender, e) => OnChanged();

            PostalAddresses.CollectionChanged += (sender, e) => OnChanged();
            PostalAddresses.ItemChanged += (sender, e) => OnChanged();

            Dates.CollectionChanged += (sender, e) => OnChanged();
            Dates.ItemChanged += (sender, e) => OnChanged();

            SocialProfileIds.CollectionChanged += (sender, e) => OnChanged();
            SocialProfileIds.ItemChanged += (sender, e) => OnChanged();
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
            PostalAddresses.Clear();
            Dates.Clear();
            SocialProfileIds.Clear();
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
            PostalAddresses.CopyFrom(contact.PostalAddresses);
            Dates.CopyFrom(contact.Dates);
            SocialProfileIds.CopyFrom(contact.SocialProfileIds);

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

        //public override bool Equals(object obj)
        //{
        //    Contact contact = obj as Contact;

        //    if (contact == null)
        //        return false;

        //    return name.Equals(contact.name) &&
        //           birthday == contact.birthday &&
        //           Phones.Equals(contact.Phones) &&
        //           Emails.Equals(contact.Emails) &&
        //           WebSites.Equals(contact.WebSites) &&
        //           Addresses.Equals(contact.Addresses) &&
        //           Dates.Equals(contact.Dates) &&
        //           MessengerIds.Equals(contact.MessengerIds) &&
        //           notes == contact.notes;
        //}

        public bool Equals(Contact other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(name, other.name) &&
                   Equals(birthday, other.birthday) &&
                   string.Equals(category, other.category) &&
                   string.Equals(notes, other.notes) &&
                   Equals(Phones, other.Phones) &&
                   Equals(Emails, other.Emails) &&
                   Equals(WebSites, other.WebSites) &&
                   Equals(PostalAddresses, other.PostalAddresses) &&
                   Equals(Dates, other.Dates) &&
                   Equals(SocialProfileIds, other.SocialProfileIds);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Contact)) return false;

            return Equals((Contact) obj);
        }

        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        var hashCode = (name != null ? name.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (birthday != null ? birthday.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (category != null ? category.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (notes != null ? notes.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (Phones != null ? Phones.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (Emails != null ? Emails.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (WebSites != null ? WebSites.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (Addresses != null ? Addresses.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (Dates != null ? Dates.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (MessengerIds != null ? MessengerIds.GetHashCode() : 0);

        //        return hashCode;
        //    }
        //}
    }
}