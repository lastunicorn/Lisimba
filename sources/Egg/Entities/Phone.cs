using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg
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
            get { return this.number; }
            set { this.number = value; this.OnNumberChanged(new NumberChangedEventArgs(value)); }
        }

        //[XmlElement("Description")]
        [XmlAttribute("Description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; this.OnDescriptionChanged(new DescriptionChangedEventArgs(value)); }
        }

        #endregion Properties


        #region Events

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

        #endregion

        #region Constructors

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
            this.CopyFrom(phone);
        }

        #endregion Constructors

        #region public void CopyFrom(Phone phone)

        public void CopyFrom(Phone phone)
        {
            this.number = phone.number;
            this.description = phone.description;
        }

        #endregion

        #region public Phone GetCopy()

        public Phone GetCopy()
        {
            Phone p = new Phone();
            p.CopyFrom(this);
            return p;
        }

        #endregion

        #region public void Clear()

        public void Clear()
        {
            this.number = "";
            this.description = "";
        }

        #endregion

        #region public override bool Equals(object obj)

        public override bool Equals(object obj)
        {
            if (!(obj is Phone)) return false;

            Phone phone = (Phone)obj;

            if (!number.Equals(phone.number)) return false;
            if (!description.Equals(phone.description)) return false;

            return true;
        }

        #endregion

        #region public override string ToString()

        public override string ToString()
        {
            return this.number + (this.description.Length > 0 ? (" - " + this.description) : string.Empty);
        }

        #endregion
    }
}
