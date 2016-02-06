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
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Importing;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Egg.AddressBookModel
{
    [TestFixture]
    public class ContactCollectionTests
    {
        [Test]
        public void importing_empty_collection_into_empty_collection_returns_no_import_rules()
        {
            ContactCollection destinationContacts = new ContactCollection();
            ContactCollection sourceContacts = new ContactCollection();

            ImportRuleCollection importRules = destinationContacts.AnalyzeForImport(sourceContacts);

            Assert.That(importRules, Is.Empty);
        }

        [Test]
        public void importing_one_contact_into_empty_collection_returns_one_add_import_rule()
        {
            ContactCollection destinationContacts = new ContactCollection();
            ContactCollection sourceContacts = new ContactCollection
            {
                new Contact()
            };

            ImportRuleCollection importRules = destinationContacts.AnalyzeForImport(sourceContacts);

            Assert.That(importRules.Count, Is.EqualTo(1));
            Assert.That(importRules[0].ImportType, Is.EqualTo(ImportType.AddAsNew));
        }

        [Test]
        public void importing_two_contacts_into_empty_collection_returns_two_add_import_rule()
        {
            ContactCollection destinationContacts = new ContactCollection();
            ContactCollection sourceContacts = new ContactCollection
            {
                new Contact(),
                new Contact()
            };

            ImportRuleCollection importRules = destinationContacts.AnalyzeForImport(sourceContacts);

            Assert.That(importRules.Count, Is.EqualTo(2));
            Assert.That(importRules.All(x => x.ImportType == ImportType.AddAsNew));
        }

        [Test]
        public void importing_empty_collection_into_non_empty_collection_returns_no_import_rules()
        {
            ContactCollection destinationContacts = new ContactCollection
            {
                new Contact(),
                new Contact(),
                new Contact()
            };
            ContactCollection sourceContacts = new ContactCollection();

            ImportRuleCollection importRules = destinationContacts.AnalyzeForImport(sourceContacts);

            Assert.That(importRules, Is.Empty);
        }

        [Test]
        public void importing_one_contact_that_already_exists_returns_no_import_rules()
        {
            ContactCollection destinationContacts = new ContactCollection
            {
                new Contact { Name = new PersonName("alexandru", "iuga") }
            };
            ContactCollection sourceContacts = new ContactCollection
            {
                new Contact { Name = new PersonName("alexandru", "iuga") }
            };

            ImportRuleCollection importRules = destinationContacts.AnalyzeForImport(sourceContacts);

            Assert.That(importRules, Is.Empty);
        }

        [Test]
        public void importing_one_contact_that_exists_but_changed_returns_one_merge_import_rule()
        {
            ContactCollection destinationContacts = new ContactCollection
            {
                new Contact { Name = new PersonName("alexandru", "iuga") }
            };
            ContactCollection sourceContacts = new ContactCollection
            {
                new Contact
                {
                    Name = new PersonName("alexandru", "iuga"),
                    Birthday = new Date(13, 06, 1980)
                }
            };

            ImportRuleCollection importRules = destinationContacts.AnalyzeForImport(sourceContacts);

            Assert.That(importRules.Count, Is.EqualTo(1));
            Assert.That(importRules[0].ImportType, Is.EqualTo(ImportType.Merge));
        }
    }
}
