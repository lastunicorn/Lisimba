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

using System.Linq;
using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Importing;
using DustInTheWind.Lisimba.Business.Importing.Importers;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Importing.Importers
{
    [TestFixture]
    public class ExecuteMergeTests
    {
        [Test]
        public void replaces_the_Email_in_left_Contact_with_MergedValue_if_one_provided()
        {
            Contact contactLeft = CreateContactWithOneEmail("left@bbb.ccc", "desc left");
            Email emailLeft = contactLeft.Items.First() as Email;
            Contact contactRight = CreateContactWithOneEmail("right@bbb.ccc", "desc right");
            Email emailRight = contactRight.Items.First() as Email;
            EmailImport emailImport = new EmailImport(contactLeft, emailLeft, emailRight, ImportType.Merge);
            Email emailMerged = new Email();
            emailImport.MergedValue = emailMerged;

            emailImport.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.EqualTo(emailMerged));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailMerged));
        }

        [Test]
        public void replaces_the_Email_in_left_Contact_with_generated_MergedValue_if_no_conflicts_exists_because_email_address_is_null()
        {
            Contact contactLeft = CreateContactWithOneEmail(null, "desc1");
            Email emailLeft = contactLeft.Items.First() as Email;
            Contact contactRight = CreateContactWithOneEmail("address1@email.com", "desc1");
            Email emailRight = contactRight.Items.First() as Email;
            EmailImport emailImport = new EmailImport(contactLeft, emailLeft, emailRight, ImportType.Merge);

            emailImport.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.EqualTo(new Email { Address = "address1@email.com", Description = "desc1" }));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailLeft));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailRight));
        }

        [Test]
        public void replaces_the_Email_in_left_Contact_with_generated_MergedValue_if_no_conflicts_exists_because_email_address_is_empty_string()
        {
            Contact contactLeft = CreateContactWithOneEmail("", "desc1");
            Email emailLeft = contactLeft.Items.First() as Email;
            Contact contactRight = CreateContactWithOneEmail("address1@email.com", "desc1");
            Email emailRight = contactRight.Items.First() as Email;
            EmailImport emailImport = new EmailImport(contactLeft, emailLeft, emailRight, ImportType.Merge);

            emailImport.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.EqualTo(new Email { Address = "address1@email.com", Description = "desc1" }));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailLeft));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailRight));
        }

        [Test]
        public void replaces_the_Email_in_left_Contact_with_generated_MergedValue_if_no_conflicts_exists_because_description_is_null()
        {
            Contact contactLeft = CreateContactWithOneEmail("address1@email.com", null);
            Email emailLeft = contactLeft.Items.First() as Email;
            Contact contactRight = CreateContactWithOneEmail("address1@email.com", "desc1");
            Email emailRight = contactRight.Items.First() as Email;
            EmailImport emailImport = new EmailImport(contactLeft, emailLeft, emailRight, ImportType.Merge);

            emailImport.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.EqualTo(new Email { Address = "address1@email.com", Description = "desc1" }));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailLeft));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailRight));
        }

        [Test]
        public void replaces_the_Email_in_left_Contact_with_generated_MergedValue_if_no_conflicts_exists_because_description_is_empty_string()
        {
            Contact contactLeft = CreateContactWithOneEmail("address1@email.com", "");
            Email emailLeft = contactLeft.Items.First() as Email;
            Contact contactRight = CreateContactWithOneEmail("address1@email.com", "desc1");
            Email emailRight = contactRight.Items.First() as Email;
            EmailImport emailImport = new EmailImport(contactLeft, emailLeft, emailRight, ImportType.Merge);

            emailImport.Execute(new StringBuilder(), false);

            Assert.That(contactLeft.Items.Count, Is.EqualTo(1));
            Assert.That(contactLeft.Items[0], Is.EqualTo(new Email { Address = "address1@email.com", Description = "desc1" }));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailLeft));
            Assert.That(contactLeft.Items[0], Is.Not.SameAs(emailRight));
        }

        [Test]
        [ExpectedException(typeof(MergeConflictException))]
        public void throws_if_conflicts_exist()
        {
            Contact contactLeft = CreateContactWithOneEmail("address1@email.com", "desc2");
            Email emailLeft = contactLeft.Items.First() as Email;
            Contact contactRight = CreateContactWithOneEmail("address1@email.com", "desc1");
            Email emailRight = contactRight.Items.First() as Email;
            EmailImport emailImport = new EmailImport(contactLeft, emailLeft, emailRight, ImportType.Merge);

            emailImport.Execute(new StringBuilder(), false);
        }

        private static Contact CreateContactWithOneEmail(string emailAddress, string description)
        {
            Contact contact = new Contact();

            Email email = new Email(emailAddress, description);
            contact.Items.Add(email);

            return contact;
        }
    }
}