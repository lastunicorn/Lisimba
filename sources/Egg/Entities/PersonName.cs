using System;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    [XmlRoot("Name")]
    public class PersonName
    {
        private string firstName = string.Empty;

        [XmlAttribute("First")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string middleName = string.Empty;

        [XmlAttribute("Middle")]
        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        private string lastName = string.Empty;

        [XmlAttribute("Last")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string nickname = string.Empty;

        [XmlAttribute("Nickname")]
        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
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

        public override bool Equals(object obj)
        {
            PersonName personName = obj as PersonName;

            if (personName == null)
                return false;

            if (firstName.Equals(personName.firstName) &&
                middleName.Equals(personName.middleName) &&
                lastName.Equals(personName.lastName) &&
                nickname.Equals(personName.nickname))
            {
                return true;
            }
                
            return false;
        }

        public override string ToString()
        {
            string str = string.Empty;
            string name = string.Empty;

            name = (firstName.Length > 0 ? firstName : string.Empty);
            name += (middleName.Length > 0 ? (name.Length > 0 ? " " : string.Empty) + middleName : string.Empty);
            name += (lastName.Length > 0 ? (name.Length > 0 ? " " : string.Empty) + lastName : string.Empty);

            if (nickname.Length > 0)
                str += nickname + " (" + name + ")";
            else
                str += name;

            if (str.Length == 0)
                str = "< NA >";

            return str;
        }
    }
}
