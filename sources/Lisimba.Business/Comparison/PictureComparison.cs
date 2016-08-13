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

namespace DustInTheWind.Lisimba.Business.Comparison
{
    public class PictureComparison : ItemComparisonBase
    {
        public PictureComparison(Contact contactLeft, Contact contactRight)
            : base(contactLeft, contactRight)
        {
        }

        protected override bool ContactLeftHasValue()
        {
            return ContactLeft.Picture != null;
        }

        protected override bool ContactRightHasValue()
        {
            return ContactRight.Picture != null;
        }

        protected override bool HaveSameValue()
        {
            return AreEquals(ContactLeft.Picture, ContactRight.Picture);
        }

        protected override bool HaveSimilarValue()
        {
            return AreEquals(ContactLeft.Picture, ContactRight.Picture);
        }

        public static bool AreEquals(Image bmp1, Image bmp2)
        {
            //Test to see if we have the same size of image
            if (bmp1.Size != bmp2.Size)
                return false;

            //Convert each image to a byte array
            ImageConverter imageConverter = new ImageConverter();
            byte[] btImage1 = (byte[])imageConverter.ConvertTo(bmp1, typeof(byte[]));
            byte[] btImage2 = (byte[])imageConverter.ConvertTo(bmp2, typeof(byte[]));

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