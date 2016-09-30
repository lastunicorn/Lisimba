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

using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Importing;
using DustInTheWind.Lisimba.Business.Importing.Importers;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Importing.Importers
{
    [TestFixture]
    public class ExecuteIgnoreTests
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
        public void does_nothing_if_all_values_are_provided()
        {
            Email emailLeftClone = emailLeft.Clone() as Email;

            EmailImport emailImport = new EmailImport(contactLeft, emailLeft, emailRight, ImportType.Ignore);

            emailImport.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.SameAs(emailLeft));
            Assert.That(contactLeft.Items[0], Is.EqualTo(emailLeftClone));
        }

        [Test]
        public void does_nothing_if_sourceValue_is_not_provided()
        {
            Email emailLeftClone = emailLeft.Clone() as Email;

            EmailImport emailImport = new EmailImport(contactLeft, emailLeft, null, ImportType.Ignore);

            emailImport.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.SameAs(emailLeft));
            Assert.That(contactLeft.Items[0], Is.EqualTo(emailLeftClone));
        }

        [Test]
        public void does_nothing_if_destinationValue_is_not_provided()
        {
            Email emailLeftClone = emailLeft.Clone() as Email;

            EmailImport emailImport = new EmailImport(contactLeft, null, emailRight, ImportType.Ignore);

            emailImport.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.SameAs(emailLeft));
            Assert.That(contactLeft.Items[0], Is.EqualTo(emailLeftClone));
        }

        [Test]
        public void does_nothing_if_destinationContact_is_not_provided()
        {
            EmailImport emailImport = new EmailImport(null, emailLeft, emailRight, ImportType.Ignore);

            emailImport.Execute(new StringBuilder(), false);
        }

        [Test]
        public void does_nothing_if_no_values_are_provided()
        {
            EmailImport emailImport = new EmailImport(null, null, null, ImportType.Ignore);

            emailImport.Execute(new StringBuilder(), false);
        }
    }
}
