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
using System.Drawing;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;

namespace DustInTheWind.Lisimba.Business.AddressBookModel
{
    public class Picture : ContactItem, IEquatable<Picture>
    {
        private Image image;

        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                OnChanged();
            }
        }

        public Picture()
            : this(null, null)
        {
        }

        public Picture(Image image)
            : this(image, null)
        {
        }

        public Picture(Image image, string description)
        {
            this.image = image;
            this.description = description;
        }

        public Picture(Picture picture)
        {
            CopyFrom(picture);
        }

        public override void CopyFrom(ContactItem contactItem)
        {
            if (contactItem == null) throw new ArgumentNullException("contactItem");

            Picture picture = contactItem as Picture;

            if (picture != null)
                CopyFrom(picture);
        }

        public void CopyFrom(Picture picture)
        {
            image = picture.image.Clone() as Image;
            description = picture.description;

            OnChanged();
        }

        public override ContactItem Clone()
        {
            return new Picture(image.Clone() as Image, description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Picture)) return false;

            return Equals((Picture)obj);
        }

        public bool Equals(Picture picture)
        {
            if (ReferenceEquals(null, picture)) return false;
            if (ReferenceEquals(this, picture)) return true;

            return PictureComparison.AreEquals(image, picture.image) &&
                string.Equals(description, picture.description);
        }

        public static bool Equals(Picture picture1, Picture picture2)
        {
            if (picture1 == null)
                return picture2 == null;

            return picture1.Equals(picture2);
        }
    }
}
