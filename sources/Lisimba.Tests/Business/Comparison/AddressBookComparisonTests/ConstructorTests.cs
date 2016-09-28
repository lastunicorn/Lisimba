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
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Comparison.AddressBookComparisonTests
{
    [TestFixture]
    public class ConstructorTests
    {
        private AddressBook addressBook1;
        private AddressBook addressBook2;

        [SetUp]
        public void SetUp()
        {
            addressBook1 = new AddressBook();
            addressBook2 = new AddressBook();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_addressBook1_is_null()
        {
            AddressBookComparison addressBookComparison = new AddressBookComparison(null, addressBook2);
            addressBookComparison.Compare();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_addressBook2_is_null()
        {
            AddressBookComparison addressBookComparison = new AddressBookComparison(addressBook1, null);
            addressBookComparison.Compare();
        }
    }
}
