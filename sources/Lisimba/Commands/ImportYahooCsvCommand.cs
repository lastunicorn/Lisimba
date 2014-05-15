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
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Gating;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class ImportYahooCsvCommand : CommandBase<object>
    {
        private readonly CurrentData currentData;
        private readonly UIService uiService;
        private readonly StatusService statusService;

        public override string ShortDescription
        {
            get { return "Import address book from Yahoo! csv format."; }
        }

        public Func<bool> AskIfAllowToContinue;
        public Func<string> AskToOpenYahooCsvFile;

        public ImportYahooCsvCommand(CurrentData currentData, UIService uiService, StatusService statusService)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            if (statusService == null)
                throw new ArgumentNullException("statusService");

            this.currentData = currentData;
            this.uiService = uiService;
            this.statusService = statusService;
        }

        protected override void DoExecute(object parameter)
        {
            try
            {
                bool allowToContinue = AskIfAllowToContinue == null || AskIfAllowToContinue();

                if (!allowToContinue)
                    return;

                string fileName = AskToOpenYahooCsvFile();

                if (fileName == null)
                    return;

                YahooCsvGate yahooCsvGate = new YahooCsvGate();
                AddressBook yahooAddressBook = yahooCsvGate.Load(fileName);

                ContactCollection yahooContacts = yahooAddressBook.Contacts;
                ImportRuleCollection mergeRules = CreateMergeRules(yahooContacts);

                currentData.AddressBook = new AddressBook();
                int countImport = currentData.AddressBook.Contacts.AddRange(yahooContacts, mergeRules);

                statusService.StatusText = string.Format("{0} contacts imported from {1} contacts in .csv file.", countImport, yahooContacts.Count);
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
