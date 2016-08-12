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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Comparison.ContactComparisonTests
{
    [TestFixture]
    public class PictureComparisonTests
    {
        private static readonly Random Random = new Random();

        [Test]
        public void after_Compare_the_Differences_contains_the_PictureComparison()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertContainsType(contactComparison.Results, typeof(PictureComparison));
        }

        [Test]
        public void both_pictures_are_empty()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.BothEmpty);
        }

        [Test]
        public void left_contains_value_right_is_empty()
        {
            Contact contactLeft = new Contact { Picture = GetRandomPicture() };
            Contact contactRight = new Contact();

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.LeftExists);
        }

        [Test]
        public void left_is_empty_right_contains_value()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact { Picture = GetRandomPicture() };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.RightExists);
        }

        [Test]
        public void both_exists_but_have_different_values()
        {
            Contact contactLeft = new Contact { Picture = GetRandomPicture() };
            Contact contactRight = new Contact { Picture = GetRandomPicture() };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Different);
        }

        [Test]
        public void both_exists_and_have_same_value()
        {
            Image picture = GetRandomPicture();
            Contact contactLeft = new Contact { Picture = picture };
            Contact contactRight = new Contact { Picture = picture };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Equal);
        }

        [Test]
        public void both_exists_and_have_equal_values()
        {
            Image picture1 = GetRandomPicture();
            Image picture2 = new Bitmap(picture1);
            Contact contactLeft = new Contact { Picture = picture1 };
            Contact contactRight = new Contact { Picture = picture2 };

            ContactComparison contactComparison = new ContactComparison(contactLeft, contactRight);

            AssertEquality(contactComparison.Results, ItemEquality.Equal);
        }

        private static Image GetRandomPicture()
        {
            const int height = 16;
            const int width = 16;

            Bitmap picture = new Bitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color color = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
                    picture.SetPixel(j, i, color);
                }
            }

            return picture;
        }

        private static void AssertEquality(IEnumerable<IItemComparison> comparisons, ItemEquality expectedEquality)
        {
            PictureComparison pictureComparison = comparisons.OfType<PictureComparison>().First();
            Assert.That(pictureComparison.Equality, Is.EqualTo(expectedEquality));
        }

        private static void AssertContainsType(IEnumerable<IItemComparison> differences, Type type)
        {
            bool containsType = differences.Any(itemComparison => itemComparison.GetType() == type);

            if (!containsType)
                Assert.Fail("The list does not contain the comparison of type {0}.", type.FullName);
        }
    }
}
