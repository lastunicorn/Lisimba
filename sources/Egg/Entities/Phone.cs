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

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class Phone : IObservableEntity
    {
        private string number;
        private string description;

        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                OnChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
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
