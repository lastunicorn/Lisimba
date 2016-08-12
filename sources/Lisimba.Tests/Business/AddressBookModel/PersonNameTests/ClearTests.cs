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
    public class ClearTests
    {
        private PersonName personName;

        [SetUp]
        public void SetUp()
        {
            personName = new PersonName("first", "middle", "last", "nick");
        }

        [Test]
        public void sets_FirstName_with_string_empty()
        {
            personName.Clear();

            Assert.That(personName.FirstName, Is.Empty);
        }

        [Test]
        public void sets_MiddleName_with_string_empty()
        {
            personName.Clear();

            Assert.That(personName.MiddleName, Is.Empty);
        }

        [Test]
        public void sets_LastName_with_string_empty()
        {
            personName.Clear();

            Assert.That(personName.LastName, Is.Empty);
        }

        [Test]
        public void sets_Nickname_with_string_empty()
        {
            personName.Clear();

            Assert.That(personName.Nickname, Is.Empty);
        }

        [Test]
        public void raises_Changed_event_once()
        {
            int eventRaiseCount = 0;
            personName.Changed += (sender, e) => eventRaiseCount++;

            personName.Clear();

            Assert.That(eventRaiseCount, Is.EqualTo(1));
        }
    }
}
