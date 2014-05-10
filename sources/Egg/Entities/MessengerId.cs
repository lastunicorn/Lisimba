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
    /// <summary>
    /// Class containing information about a messenger id.
    /// </summary>
    [Serializable()]
    [XmlRoot("MessengerId")]
    public class MessengerId : IObservableEntity
    {
        private string id;
        private string description;

        /// <summary>
        /// The messenger id.
        /// </summary>
        [XmlAttribute("Id")]
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnChanged();
            }
        }

        /// <summary>
        /// A short description of the e-mail address.
        /// </summary>
        [XmlAttribute("Description")]
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

        /// <summary>
        /// Creates a new empty MessengerId object.
        /// </summary>
        public MessengerId()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new MessengerId object with the id and description specified.
        /// </summary>
        public MessengerId(string id, string description)
        {
            this.id = id;
            this.description = description;
        }

        /// <summary>
        /// Creates a new MessengerId object with the data copied from the one passed as parameter.
        /// </summary>
        public MessengerId(MessengerId messenger)
        {
            CopyFrom(messenger);
        }

        /// <summary>
        /// Removes the data from all the fields
        /// </summary>
        public void Clear()
        {
            id = string.Empty;
            description = string.Empty;
        }

        /// <summary>
        /// Copy the data from the MessengerId object passed as parameter into the current object.
        /// </summary>
        public void CopyFrom(MessengerId messenger)
        {
            id = messenger.id;
            description = messenger.description;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MessengerId)) return false;

            MessengerId messengerId = (MessengerId)obj;

            if (!id.Equals(messengerId.id)) return false;
            if (!description.Equals(messengerId.description)) return false;

            return true;
        }

        public override string ToString()
        {
            return Id + (description.Length > 0 ? " - " + description : string.Empty);
        }
    }
}
