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

namespace DustInTheWind.Lisimba.Egg.AddressBookModel
{
    public class Phone : ContactItem, IEquatable<Phone>
    {
        private string number;

        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                OnChanged();
            }
        }

        public Phone()
            : this("", "")
        {
        }

        public Phone(string number, string description)
        {
            this.number = number;
            this.description = description;
        }

        public Phone(Phone phone)
        {
            CopyFrom(phone);
        }

        public override void CopyFrom(ContactItem contactItem)
        {
            if (contactItem == null) throw new ArgumentNullException("contactItem");

            Phone phone = contactItem as Phone;

            if (phone != null)
                CopyFrom(phone);
        }

        public void CopyFrom(Phone phone)
        {
            number = phone.number;
            description = phone.description;

            OnChanged();
        }

        public override ContactItem Clone()
        {
            Phone phone = new Phone();
            phone.CopyFrom(this);
            return phone;
        }

        public void Clear()
        {
            number = string.Empty;
            description = string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Phone)) return false;

            return Equals((Phone)obj);
        }

        public bool Equals(Phone phone)
        {
            if (ReferenceEquals(null, phone)) return false;
            if (ReferenceEquals(this, phone)) return true;

            return string.Equals(number, phone.number) &&
                   string.Equals(description, phone.description);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((number != null ? number.GetHashCode() : 0) * 397) ^ (description != null ? description.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return number + (description.Length > 0 ? (" - " + description) : string.Empty);
        }
    }
}