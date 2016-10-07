﻿// Lisimba
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

using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Importing;
using DustInTheWind.Lisimba.Business.Importing.Importers;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Importing.Importers
{
    [TestFixture]
    public class ExecuteAddAsNewTests
    {
        private Contact contactLeft;
        private Contact contactRight;
        private Email emailLeft;
        private Email emailRight;

        [SetUp]
        public void SetUp()
        {
            contactLeft = new Contact();
            emailLeft = new Email("left@bbb.ccc", "desc left");
            contactLeft.Items.Add(emailLeft);

            contactRight = new Contact();
            emailRight = new Email("right@bbb.ccc", "desc right");
            contactRight.Items.Add(emailRight);
        }

        [Test]
        public void adds_a_copy_of_the_Email_in_left_Contact()
        {
            EmailImporter emailImporter = new EmailImporter(contactLeft, emailLeft, emailRight, ImportType.AddAsNew);

            emailImporter.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(2));
            Assert.That(contactLeft.Items[0], Is.SameAs(emailLeft));
            Assert.That(contactLeft.Items[1], Is.EqualTo(emailRight));
            Assert.That(contactLeft.Items[1], Is.Not.SameAs(emailRight));
        }

        [Test]
        public void does_nothing_if_sourceValue_is_null()
        {
            EmailImporter emailImporter = new EmailImporter(contactLeft, emailLeft, null, ImportType.AddAsNew);

            emailImporter.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.SameAs(emailLeft));
        }
    }
}