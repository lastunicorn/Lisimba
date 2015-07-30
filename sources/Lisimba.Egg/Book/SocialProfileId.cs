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

namespace DustInTheWind.Lisimba.Egg.Book
{
    /// <summary>
    /// Class containing information about a social profile id.
    /// </summary>
    public class SocialProfileId : IObservableEntity, IEquatable<SocialProfileId>
    {
        private string id;
        private string description;

        /// <summary>
        /// The social profile id.
        /// </summary>
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
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnChanged();
            }
        }

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Creates a new empty <see cref="SocialProfileId"/> object.
        /// </summary>
        public SocialProfileId()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new <see cref="SocialProfileId"/> object with the id and description specified.
        /// </summary>
        public SocialProfileId(string id, string description)
        {
            this.id = id;
            this.description = description;
        }

        /// <summary>
        /// Creates a new <see cref="SocialProfileId"/> object with the data copied from the one passed as parameter.
        /// </summary>
        public SocialProfileId(SocialProfileId socialProfile)
        {
            CopyFrom(socialProfile);
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
        /// Copy the data from the <see cref="SocialProfileId"/> object passed as parameter into the current object.
        /// </summary>
        public void CopyFrom(SocialProfileId socialProfile)
        {
            id = socialProfile.id;
            description = socialProfile.description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SocialProfileId)) return false;

            return Equals((SocialProfileId)obj);
        }

        public bool Equals(SocialProfileId socialProfileId)
        {
            if (ReferenceEquals(null, socialProfileId)) return false;
            if (ReferenceEquals(this, socialProfileId)) return true;

            return string.Equals(id, socialProfileId.id) && string.Equals(description, socialProfileId.description);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((id != null ? id.GetHashCode() : 0) * 397) ^ (description != null ? description.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return id + (description.Length > 0 ? " - " + description : string.Empty);
        }
    }
}
