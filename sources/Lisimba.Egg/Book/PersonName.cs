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
using System.Text;

namespace DustInTheWind.Lisimba.Egg.Book
{
    public class PersonName : IEquatable<PersonName>
    {
        private string firstName = string.Empty;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
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
                middleName = value;
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
                lastName = value;
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
                nickname = value;
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

        /// <summary>
        /// Copy all the informations from the specified <see cref="PersonName"/> object to the current instance. All the existing data will be lost.
        /// </summary>
        /// <param name="name">The <see cref="PersonName"/> object to be copied.</param>
        public void CopyFrom(PersonName name)
        {
            firstName = name.firstName;
            middleName = name.middleName;
            lastName = name.lastName;
            nickname = name.nickname;

            OnChanged();
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(firstName) &&
                   string.IsNullOrEmpty(middleName) &&
                   string.IsNullOrEmpty(lastName) &&
                   string.IsNullOrEmpty(nickname);
        }

        public static int Compare(PersonName n1, PersonName n2)
        {
            int response = 0;

            // -1 = different. At least one of the name components are different.
            // 0 = equals
            // 1 = almost equals. Some name components are missing (empty string) in one of the names.

            // Test the first names
            if (!n1.firstName.Equals(n2.firstName))
            {
                if (n1.firstName.Length == 0 || n2.firstName.Length == 0)
                    response = 1; // almost
                else
                    response = -1; // different
            }

            // If different, return.
            if (response == -1) return response;

            // Test the last names
            if (!n1.lastName.Equals(n2.lastName))
            {
                if (n1.lastName.Length == 0 || n2.lastName.Length == 0)
                    response = 1; // almost
                else
                    response = -1; // different
            }

            // If different, return.
            if (response == -1) return response;

            // Test the middle names
            if (!n1.middleName.Equals(n2.middleName))
            {
                if (n1.middleName.Length == 0 || n2.middleName.Length == 0)
                    response = 1; // almost
                else
                    response = -1; // different
            }

            // If different, return.
            if (response == -1) return response;

            // Test the nicknames
            if (!n1.nickname.Equals(n2.nickname))
            {
                if (n1.nickname.Length == 0 || n2.nickname.Length == 0)
                    response = 1; // almost
                else
                    response = -1; // different
            }

            return response;
        }

        //public override bool Equals(object obj)
        //{
        //    return Equals(obj as PersonName);
        //}

        //public bool Equals(PersonName personName)
        //{
        //    if (personName == null)
        //        return false;

        //    return firstName == personName.firstName &&
        //           middleName == personName.middleName &&
        //           lastName == personName.lastName &&
        //           nickname == personName.nickname;
        //}

        public bool Equals(PersonName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(firstName, other.firstName) &&
                   string.Equals(middleName, other.middleName) &&
                   string.Equals(lastName, other.lastName) &&
                   string.Equals(nickname, other.nickname);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PersonName)) return false;

            return Equals((PersonName) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (firstName != null ? firstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (middleName != null ? middleName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (lastName != null ? lastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (nickname != null ? nickname.GetHashCode() : 0);

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