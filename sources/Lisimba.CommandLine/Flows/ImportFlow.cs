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
using System.Text;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Business.Importing;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class ImportFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly EnhancedConsole console;
        private readonly Gates gates;

        public ImportFlow(AddressBooks addressBooks, EnhancedConsole console, Gates gates)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");
            if (gates == null) throw new ArgumentNullException("gates");

            this.addressBooks = addressBooks;
            this.console = console;
            this.gates = gates;
        }

        public void Execute(IList<string> parameters)
        {
            if (addressBooks.Current == null)
                throw new LisimbaException(Resources.NoAddessBookOpenedError);

            if (parameters.Count == 0)
                throw new LisimbaException("Specify the address book to import from.");

            IGate gate = gates.GetGate("LisimbaGate");
            AddressBook addressBook = (gate as FileGate).Load(parameters[0]);

            AddressBook currentaddressBook = addressBooks.Current.AddressBook;

            AddressBookImporter addressBookImporter = new AddressBookImporter(currentaddressBook, addressBook);
            addressBookImporter.Analyse();

            bool shouldSimulate = parameters.Count >= 2 && (parameters[1] == "-s" || parameters[1] == "--simulate");
            
            StringBuilder sb = addressBookImporter.PerformImport(shouldSimulate);

            console.WriteLineNormal(sb.ToString());
        }
    }
}
