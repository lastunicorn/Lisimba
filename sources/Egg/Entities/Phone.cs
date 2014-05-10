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

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    [XmlRoot("Phone")]
    public class Phone
    {
        #region Fields

        private string number;
        private string description;

        #endregion Fields

        #region Properties

        //[XmlElement("Number")]
        [XmlAttribute("Number")]
        public string Number
        {
            get { return number; }
            set { number = value; OnNumberChanged(new NumberChangedEventArgs(value)); }
        }

        //[XmlElement("Description")]
        [XmlAttribute("Description")]
        public string Description
        {
            get { return description; }
            set { description = value; OnDescriptionChanged(new DescriptionChangedEventArgs(value)); }
        }

        #endregion Properties


        #region Event NumberChanged

        public event NumberChangedHandler NumberChanged;
        public delegate void NumberChangedHandler(object sender, NumberChangedEventArgs e);

        public class NumberChangedEventArgs : EventArgs
        {
            private string newValue;

            public string NewValue
            {
                get { return newValue; }
            }

            public NumberChangedEventArgs(string newValue)
            {
                this.newValue = newValue;
            }
        }

        private void OnNumberChanged(NumberChangedEventArgs e)
        {
            if (NumberChanged != null)
            {
                NumberChanged(this, e);
            }
        }

        #endregion Event NumberChanged

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

        public Phone()
            : this("", "")
        {
        }

        public Phone(string number)
            : this(number, "")
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

        public void CopyFrom(Phone phone)
        {
            number = phone.number;
            description = phone.description;
        }

        public Phone GetCopy()
        {
            Phone p = new Phone();
            p.CopyFrom(this);
            return p;
        }

        public void Clear()
        {
            number = "";
            description = "";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Phone)) return false;

            Phone phone = (Phone)obj;

            if (!number.Equals(phone.number)) return false;
            if (!description.Equals(phone.description)) return false;

            return true;
        }

        public override string ToString()
        {
            return number + (description.Length > 0 ? (" - " + description) : string.Empty);
        }
    }
}
