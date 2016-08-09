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
using System.Drawing;

namespace DustInTheWind.Lisimba.Business.AddressBookModel
{
    public class Contact : IObservableEntity, IComparable, IEquatable<Contact>
    {
        private PersonName name;

        /// <summary>
        /// Gets or sets the name of the person.
        /// It cannot be set to null.
        /// </summary>
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

                OnChanged();
            }
        }

        private Date birthday;

        /// <summary>
        /// Gets or sets the birthday of te person.
        /// </summary>
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

        /// <summary>
        /// Gets the zodiac sign calculated based on the birthday.
        /// </summary>
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

        public ContactItemCollection Items { get; private set; }

        private string notes = string.Empty;
        private Image picture;

        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnChanged();
            }
        }

        public Image Picture
        {
            get { return picture; }
            set
            {
                picture = value;
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

            Items = new ContactItemCollection();

            name.Changed += (sender, e) => OnChanged();
            birthday.Changed += (sender, e) => OnChanged();

            Items.CollectionChanged += (sender, e) => OnChanged();
            Items.ItemChanged += (sender, e) => OnChanged();
        }

        private void HandleNameChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void HandleBirthdayChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        public void Merge(Contact source)
        {
            // merge only the items

            foreach (ContactItem contactItem in source.Items)
            {
                // if contact item exists (identical) - ignore
                // if contact item exists (modified)
            }
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

        public bool Equals(Contact other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(name, other.name) &&
                   Equals(birthday, other.birthday) &&
                   string.Equals(category, other.category) &&
                   string.Equals(notes, other.notes) &&
                   Equals(Items, other.Items);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Contact)) return false;

            return Equals((Contact)obj);
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