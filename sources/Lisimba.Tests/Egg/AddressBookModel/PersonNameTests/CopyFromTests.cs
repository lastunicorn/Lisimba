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
    public class CopyFromTests
    {
        private PersonName personName;
        private PersonName personName2;

        [SetUp]
        public void SetUp()
        {
            personName = new PersonName("first", "middle", "last", "nick");
            personName2 = new PersonName("first2", "middle2", "last2", "nick2");
        }

        [Test]
        public void sets_FirstName()
        {
            personName.CopyFrom(personName2);

            Assert.That(personName.FirstName, Is.EqualTo("first2"));
        }

        [Test]
        public void sets_MiddleName()
        {
            personName.CopyFrom(personName2);

            Assert.That(personName.MiddleName, Is.EqualTo("middle2"));
        }

        [Test]
        public void sets_LastName()
        {
            personName.CopyFrom(personName2);

            Assert.That(personName.LastName, Is.EqualTo("last2"));
        }

        [Test]
        public void sets_Nickname()
        {
            personName.CopyFrom(personName2);

            Assert.That(personName.Nickname, Is.EqualTo("nick2"));
        }

        [Test]
        public void raises_Changed_event_once()
        {
            int eventRaiseCount = 0;
            personName.Changed += (sender, e) => eventRaiseCount++;

            personName.CopyFrom(personName2);

            Assert.That(eventRaiseCount, Is.EqualTo(1));
        }
    }
}
