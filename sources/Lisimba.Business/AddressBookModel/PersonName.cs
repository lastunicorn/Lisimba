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
using System.Text;

namespace DustInTheWind.Lisimba.Business.AddressBookModel
{
    public class PersonName : ContactItem, IEquatable<PersonName>
    {
        private string firstName = string.Empty;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value ?? string.Empty;
                OnChanged();
            }
        }

        public bool HasFirstName
        {
            get { return !string.IsNullOrEmpty(firstName); }
        }

        private string middleName = string.Empty;

        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value ?? string.Empty;
                OnChanged();
            }
        }

        public bool HasMiddleName
        {
            get { return !string.IsNullOrEmpty(middleName); }
        }

        private string lastName = string.Empty;

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value ?? string.Empty;
                OnChanged();
            }
        }

        public bool HasLastName
        {
            get { return !string.IsNullOrEmpty(lastName); }
        }

        private string nickname = string.Empty;

        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value ?? string.Empty;
                OnChanged();
            }
        }

        public bool HasNickname
        {
            get { return !string.IsNullOrEmpty(nickname); }
        }

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public PersonName()
        {
        }

        public PersonName(string firstName, string middleName, string lastName, string nickname)
        {
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.nickname = nickname;
        }

        /// <summary>
        /// Remove all the information from the current instance.
        /// </summary>
        public void Clear()
        {
            firstName = string.Empty;
            middleName = string.Empty;
            lastName = string.Empty;
            nickname = string.Empty;

            OnChanged();
        }

        public override void CopyFrom(ContactItem contactItem)
        {
            if (contactItem == null) throw new ArgumentNullException("contactItem");

            PersonName personName = contactItem as PersonName;

            if (personName != null)
                CopyFrom(personName);
        }

        /// <summary>
        /// Copy all the informations from the specified <see cref="PersonName"/> object to the current instance. All the existing data will be lost.
        /// </summary>
        /// <param name="name">The <see cref="PersonName"/> object to be copied.</param>
        public void CopyFrom(PersonName name)
        {
            if (name == null) throw new ArgumentNullException("name");

            firstName = name.firstName;
            middleName = name.middleName;
            lastName = name.lastName;
            nickname = name.nickname;

            OnChanged();
        }

        public override ContactItem Clone()
        {
            return new PersonName(firstName, middleName, lastName, nickname);
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(firstName) &&
                       string.IsNullOrEmpty(middleName) &&
                       string.IsNullOrEmpty(lastName) &&
                       string.IsNullOrEmpty(nickname) &&
                       description == null;
            }
        }

        public bool ContainsText(string text, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            if (text == null)
                return false;

            return firstName.IndexOf(text, stringComparison) >= 0
                   || middleName.IndexOf(text, stringComparison) >= 0
                   || lastName.IndexOf(text, stringComparison) >= 0
                   || nickname.IndexOf(text, stringComparison) >= 0
                   || (description != null && description.IndexOf(text, stringComparison) >= 0);
        }

        public bool Equals(PersonName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(firstName, other.firstName) &&
                   string.Equals(middleName, other.middleName) &&
                   string.Equals(lastName, other.lastName) &&
                   string.Equals(nickname, other.nickname) &&
                   string.Equals(description, other.description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PersonName)) return false;

            return Equals((PersonName)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (firstName != null ? firstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (middleName != null ? middleName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (lastName != null ? lastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (nickname != null ? nickname.GetHashCode() : 0);

                return hashCode;
            }
        }

        public override string ToString()
        {
            string fullName = GetFullNameWithoutNickname();

            bool nicknameExists = !string.IsNullOrWhiteSpace(nickname);

            if (nicknameExists)
                return string.Format("{0} ({1})", nickname, fullName);

            return fullName.Length == 0 ? "< NA >" : fullName;
        }

        private string GetFullNameWithoutNickname()
        {
            StringBuilder fullName = new StringBuilder();

            bool firstNameExists = !string.IsNullOrWhiteSpace(firstName);
            bool middleNameExists = !string.IsNullOrWhiteSpace(middleName);
            bool lastNameExists = !string.IsNullOrWhiteSpace(lastName);

            if (firstNameExists)
                fullName.Append(firstName);


            if (middleNameExists)
            {
                if (fullName.Length > 0)
                    fullName.Append(" ");

                fullName.Append(middleName);
            }

            if (lastNameExists)
            {
                if (fullName.Length > 0)
                    fullName.Append(" ");

                fullName.Append(lastName);
            }

            return fullName.ToString();
        }

        public static bool Equals(PersonName p1, PersonName p2)
        {
            if (p1 == null)
                return p2 == null;

            return p1.Equals(p2);
        }
    }
}