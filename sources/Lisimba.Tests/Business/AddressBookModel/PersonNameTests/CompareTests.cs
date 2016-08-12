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

using DustInTheWind.Lisimba.Business.AddressBookModel;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.AddressBookModel.PersonNameTests
{
    [TestFixture]
    public class CompareTests
    {
        [Test]
        public void returns_NotEqual_for_two_empty_names()
        {
            PersonName personName1 = new PersonName();
            PersonName personName2 = new PersonName();

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_NotEqual_for_first_empty_name_and_the_second_with_first_name()
        {
            PersonName personName1 = new PersonName();
            PersonName personName2 = new PersonName("first", string.Empty, string.Empty, string.Empty);

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_NotEqual_for_first_empty_name_and_the_second_with_middle_name()
        {
            PersonName personName1 = new PersonName();
            PersonName personName2 = new PersonName(string.Empty, "middle", string.Empty, string.Empty);

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_NotEqual_for_first_empty_name_and_the_second_with_last_name()
        {
            PersonName personName1 = new PersonName();
            PersonName personName2 = new PersonName(string.Empty, string.Empty, "last", string.Empty);

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_NotEqual_for_first_empty_name_and_the_second_with_nickname()
        {
            PersonName personName1 = new PersonName();
            PersonName personName2 = new PersonName(string.Empty, string.Empty, string.Empty, "nick");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_Equal_for_two_names_with_identical_components()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName("alexandru", "nicolae", "iuga", "alez");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.Equal));
        }

        [Test]
        public void returns_NotEqual_when_first_name_is_different()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName("different", "nicolae", "iuga", "alez");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_NotEqual_when_middle_name_is_different()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName("alexandru", "different", "iuga", "alez");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_NotEqual_when_last_name_is_different()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName("alexandru", "nicolae", "different", "alez");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_NotEqual_when_nickname_is_different()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName("alexandru", "nicolae", "iuga", "different");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.NotEqual));
        }

        [Test]
        public void returns_AlmostEqual_when_first_name_is_different()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName(string.Empty, "nicolae", "iuga", "alez");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.AlmostEqual));
        }

        [Test]
        public void returns_AlmostEqual_when_middle_name_is_different()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName("alexandru", string.Empty, "iuga", "alez");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.AlmostEqual));
        }

        [Test]
        public void returns_AlmostEqual_when_last_name_is_different()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName("alexandru", "nicolae", string.Empty, "alez");

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.AlmostEqual));
        }

        [Test]
        public void returns_AlmostEqual_when_nickname_is_different()
        {
            PersonName personName1 = new PersonName("alexandru", "nicolae", "iuga", "alez");
            PersonName personName2 = new PersonName("alexandru", "nicolae", "iuga", string.Empty);

            PersonNameMatch personNameMatch = new PersonNameMatch(personName1, personName2);

            Assert.That(personNameMatch.Match, Is.EqualTo(FuzzyMatch.AlmostEqual));
        }
    }
}
