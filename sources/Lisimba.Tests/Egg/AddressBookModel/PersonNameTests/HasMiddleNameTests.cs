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
    public class HasMiddleNameTests
    {
        [Test]
        public void returns_false_if_MiddleName_not_set_in_constructor()
        {
            PersonName personName = new PersonName();

            bool actual = personName.HasMiddleName;

            Assert.That(actual, Is.False);
        }

        [Test]
        public void returns_true_if_MiddleName_is_set_in_constructor()
        {
            PersonName personName = new PersonName(string.Empty, "middle", string.Empty, string.Empty);

            bool actual = personName.HasMiddleName;

            Assert.That(actual, Is.True);
        }

        [Test]
        public void returns_true_after_MiddleName_is_set()
        {
            PersonName personName = new PersonName();
            personName.MiddleName = "middle";

            bool actual = personName.HasMiddleName;

            Assert.That(actual, Is.True);
        }

        [Test]
        public void returns_false_after_MiddleName_is_set_to_empty()
        {
            PersonName personName = new PersonName(string.Empty, "middle", string.Empty, string.Empty);
            personName.MiddleName = string.Empty;

            bool actual = personName.HasMiddleName;

            Assert.That(actual, Is.False);
        }
    }
}
