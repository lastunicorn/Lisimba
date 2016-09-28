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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;

namespace DustInTheWind.Lisimba.Business.Importing.Importers
{
    public class AddressBookImporter
    {
        private readonly AddressBook addressBookDestination;
        private readonly AddressBook addressBookSource;
        private readonly Collection<ContactImport> importRules;
        private bool isAnalysed;

        public AddressBookImporter(AddressBook addressBookDestination, AddressBook addressBookSource)
        {
            if (addressBookDestination == null) throw new ArgumentNullException("addressBookDestination");
            if (addressBookSource == null) throw new ArgumentNullException("addressBookSource");

            this.addressBookDestination = addressBookDestination;
            this.addressBookSource = addressBookSource;

            importRules = new Collection<ContactImport>();

            addressBookDestination.Changed += HandleAddressBookBaseChanged;
            addressBookSource.Changed += HandleAddressBookSourceChanged;
        }

        private void HandleAddressBookBaseChanged(object sender, EventArgs eventArgs)
        {
            isAnalysed = false;
        }

        private void HandleAddressBookSourceChanged(object sender, EventArgs eventArgs)
        {
            isAnalysed = false;
        }

        public void Analyse()
        {
            isAnalysed = false;

            importRules.Clear();

            AddressBookComparison addressBookComparison = new AddressBookComparison(addressBookDestination, addressBookSource);
            addressBookComparison.Compare();

            IEnumerable<ContactImport> rules = addressBookComparison.Comparisons
                .Select(x => new ContactImport(x))
                .Where(x => x.ImportType != ImportType.Ignore);

            foreach (ContactImport importRule in rules)
                importRules.Add(importRule);

            isAnalysed = true;
        }

        public StringBuilder PerformImport(bool simulate = false)
        {
            if (!isAnalysed)
                throw new LisimbaException("The import strategy must be created first. Call the Analyse method.");

            StringBuilder sb = new StringBuilder();

            foreach (ContactImport importRule in importRules)
            {
                switch (importRule.ImportType)
                {
                    case ImportType.Ignore:
                        break;

                    case ImportType.AddAsNew:
                        AddAsNew(importRule, simulate, sb);
                        break;

                    case ImportType.Merge:
                        Merge(importRule, simulate, sb);
                        break;

                    case ImportType.Replace:
                        Replace(importRule, simulate, sb);
                        break;

                    default:
                        sb.AppendLine(string.Format("Invalid import rule for dest: '{0}'; source: '{1}'; import type: {2}.", importRule.DestinationValue, importRule.SourceValue, importRule.ImportType));
                        break;
                }
            }

            return sb;
        }

        private void AddAsNew(ContactImport importRule, bool simulate, StringBuilder sb)
        {
            if (!simulate)
                addressBookDestination.Contacts.Add(importRule.SourceValue);

            sb.AppendLine(string.Format("Added contact: {0}.", importRule.SourceValue));
        }

        private static void Merge(ContactImport importRule, bool simulate, StringBuilder sb)
        {
            sb.AppendLine(string.Format("Merging contacts '{0}' and '{1}'.", importRule.DestinationValue, importRule.SourceValue));

            importRule.Execute(sb, simulate);
        }

        private void Replace(ContactImport importRule, bool simulate, StringBuilder sb)
        {
            if (!simulate)
            {
                addressBookDestination.Contacts.Remove(importRule.DestinationValue);
                addressBookDestination.Contacts.Add(importRule.SourceValue);
            }

            sb.AppendLine(string.Format("Replaced contact '{0}' with '{1}'.", importRule.DestinationValue, importRule.SourceValue));
        }
    }
}
