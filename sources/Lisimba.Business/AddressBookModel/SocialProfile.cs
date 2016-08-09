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

namespace DustInTheWind.Lisimba.Business.AddressBookModel
{
    /// <summary>
    /// Class containing information about a social profile id.
    /// </summary>
    public class SocialProfile : ContactItem, IEquatable<SocialProfile>
    {
        private string id;

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
        /// Creates a new empty <see cref="SocialProfile"/> object.
        /// </summary>
        public SocialProfile()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new <see cref="SocialProfile"/> object with the id and description specified.
        /// </summary>
        public SocialProfile(string id, string description)
        {
            this.id = id;
            this.description = description;
        }

        /// <summary>
        /// Creates a new <see cref="SocialProfile"/> object with the data copied from the one passed as parameter.
        /// </summary>
        public SocialProfile(SocialProfile socialProfile)
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

        public override void CopyFrom(ContactItem contactItem)
        {
            if (contactItem == null) throw new ArgumentNullException("contactItem");

            SocialProfile socialProfile = contactItem as SocialProfile;

            if (socialProfile != null)
                CopyFrom(socialProfile);
        }

        /// <summary>
        /// Copy the data from the <see cref="SocialProfile"/> object passed as parameter into the current object.
        /// </summary>
        public void CopyFrom(SocialProfile socialProfile)
        {
            id = socialProfile.id;
            description = socialProfile.description;

            OnChanged();
        }

        public override ContactItem Clone()
        {
            return new SocialProfile(Id, description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SocialProfile)) return false;

            return Equals((SocialProfile)obj);
        }

        public bool Equals(SocialProfile socialProfile)
        {
            if (ReferenceEquals(null, socialProfile)) return false;
            if (ReferenceEquals(this, socialProfile)) return true;

            return string.Equals(id, socialProfile.id) && string.Equals(description, socialProfile.description);
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