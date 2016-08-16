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
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Comparison
{
    [TestFixture]
    public class PictureComparisonTests
    {
        private static readonly Random Random = new Random();

        [Test]
        public void both_pictures_are_empty()
        {
            PictureComparison pictureComparison = new PictureComparison(null, null);

            Assert.That(pictureComparison.Equality, Is.EqualTo(ItemEquality.BothEmpty));
        }

        [Test]
        public void left_contains_value_right_is_empty()
        {
            Picture pictureLeft = GetRandomPicture();
            Picture pictureRight = null;

            PictureComparison pictureComparison = new PictureComparison(pictureLeft, pictureRight);

            Assert.That(pictureComparison.Equality, Is.EqualTo(ItemEquality.LeftExists));
        }

        [Test]
        public void left_is_empty_right_contains_value()
        {
            
            Picture pictureLeft = null;
            Picture pictureRight = GetRandomPicture();

            PictureComparison pictureComparison = new PictureComparison(pictureLeft, pictureRight);

            Assert.That(pictureComparison.Equality, Is.EqualTo(ItemEquality.RightExists));
        }

        [Test]
        public void both_exists_but_have_different_values()
        {
            Picture pictureLeft = GetRandomPicture();
            Picture pictureRight = GetRandomPicture();

            PictureComparison pictureComparison = new PictureComparison(pictureLeft, pictureRight);

            Assert.That(pictureComparison.Equality, Is.EqualTo(ItemEquality.Different));
        }

        [Test]
        public void both_exists_and_have_same_value()
        {
            Picture picture = GetRandomPicture();
            Picture pictureLeft = picture;
            Picture pictureRight = picture;

            PictureComparison pictureComparison = new PictureComparison(pictureLeft, pictureRight);

            Assert.That(pictureComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }

        [Test]
        public void both_exists_and_have_equal_values()
        {
            Picture pictureLeft = GetRandomPicture();
            Picture pictureRight = pictureLeft.Clone() as Picture;

            PictureComparison pictureComparison = new PictureComparison(pictureLeft, pictureRight);

            Assert.That(pictureComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }

        private static Picture GetRandomPicture()
        {
            const int height = 16;
            const int width = 16;

            Bitmap image = new Bitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color color = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
                    image.SetPixel(j, i, color);
                }
            }

            return new Picture(image);
        }
    }
}
