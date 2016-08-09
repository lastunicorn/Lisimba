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

namespace DustInTheWind.Lisimba.Tests.Egg.AddressBookModel.PersonNameTests
{
    [TestFixture]
    public class NamePartsTests
    {
        private PersonName personName;

        [SetUp]
        public void SetUp()
        {
            personName = new PersonName("first", "middle", "last", "nick");
        }

        [Test]
        public void FirstName_is_string_empty_if_set_to_null()
        {
            personName.FirstName = null;

            Assert.That(personName.FirstName, Is.Empty);
        }

        [Test]
        public void MiddleName_is_string_empty_if_set_to_null()
        {
            personName.MiddleName = null;

            Assert.That(personName.MiddleName, Is.Empty);
        }

        [Test]
        public void LastName_is_string_empty_if_set_to_null()
        {
            personName.LastName = null;

            Assert.That(personName.LastName, Is.Empty);
        }

        [Test]
        public void Nickname_is_string_empty_if_set_to_null()
        {
            personName.Nickname = null;

            Assert.That(personName.Nickname, Is.Empty);
        }
    }
}
