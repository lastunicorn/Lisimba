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

using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Comparison.Comparers
{
    public class PictureComparison : ItemComparisonBase<Contact, Picture>
    {
        public PictureComparison(Contact contactLeft, Picture pictureLeft, Contact contactRight, Picture pictureRight)
            : base(contactLeft, pictureLeft, contactRight, pictureRight)
        {
        }

        protected override bool ValuesAreEqual()
        {
            return Picture.Equals(ValueLeft, ValueRight);
        }

        protected override bool ValuesAreSimilar()
        {
            return AreEquals(ValueLeft.Image, ValueRight.Image);
        }

        public static bool AreEquals(Image image1, Image image2)
        {
            if (image1 == null && image2 == null)
                return true;

            if (image1 == null || image2 == null)
                return false;

            //Test to see if we have the same size of image
            if (image1.Size != image2.Size)
                return false;

            //Convert each image to a byte array
            ImageConverter imageConverter = new ImageConverter();
            byte[] btImage1 = (byte[])imageConverter.ConvertTo(image1, typeof(byte[]));
            byte[] btImage2 = (byte[])imageConverter.ConvertTo(image2, typeof(byte[]));

            //Compute a hash for each image
            SHA256Managed shaM = new SHA256Managed();
            byte[] hash1 = shaM.ComputeHash(btImage1);
            byte[] hash2 = shaM.ComputeHash(btImage2);

            if (hash1.Length != hash2.Length)
                return false;

            //Compare the hash values
            return !hash1
                .Where((x, i) => x != hash2[i])
                .Any();
        }
    }
}