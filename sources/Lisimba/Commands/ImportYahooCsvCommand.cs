// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using System.Linq;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.BookShell;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Gating;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class ImportYahooCsvCommand : CommandBase<object>
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UiService uiService;
        private readonly ApplicationStatus applicationStatus;

        public override string ShortDescription
        {
            get { return "Import address book from Yahoo! csv format."; }
        }

        public Func<bool> AskIfAllowToContinue;

        public ImportYahooCsvCommand(AddressBookShell addressBookShell, UiService uiService, ApplicationStatus applicationStatus)
        {
            if (addressBookShell == null)
                throw new ArgumentNullException("addressBookShell");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            if (applicationStatus == null)
                throw new ArgumentNullException("applicationStatus");

            this.addressBookShell = addressBookShell;
            this.uiService = uiService;
            this.applicationStatus = applicationStatus;
        }

        protected override void DoExecute(object parameter)
        {
            try
            {
                bool allowToContinue = AskIfAllowToContinue == null || AskIfAllowToContinue();

                if (!allowToContinue)
                    return;

                string fileName = uiService.AskToOpenYahooCsvFile();

                if (fileName == null)
                    return;

                addressBookShell.LoadNew();

                YahooCsvGate yahooCsvGate = new YahooCsvGate();
                addressBookShell.LoadFrom(yahooCsvGate, fileName);

                //ContactCollection yahooContacts = addressBookShell.AddressBook.Contacts;
                //ImportRuleCollection mergeRules = CreateMergeRules(yahooContacts);

                //int countImport = addressBookShell.AddressBook.Contacts.AddRange(yahooContacts, mergeRules);

                int countImport = addressBookShell.AddressBook.Contacts.Count;

                //applicationStatus.StatusText = string.Format("{0} contacts imported from {1} contacts in .csv file.", countImport, yahooContacts.Count);
                applicationStatus.StatusText = string.Format("{0} contacts imported.", countImport);
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }

        private static ImportRuleCollection CreateMergeRules(IEnumerable<Contact> contacts)
        {
            IEnumerable<ImportRule> rules = contacts.Select(x => new ImportRule(x));
            return new ImportRuleCollection(rules.ToList());
        }
    }
}
