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
using System.IO;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class SaveFlow : IFlow
    {
        private readonly EnhancedConsole console;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly Gates gates;

        public SaveFlow(EnhancedConsole console, OpenedAddressBooks openedAddressBooks, Gates gates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (gates == null) throw new ArgumentNullException("gates");

            this.console = console;
            this.openedAddressBooks = openedAddressBooks;
            this.gates = gates;
        }

        public void Execute(IList<string> parameters)
        {
            if (openedAddressBooks.Current == null)
                throw new LisimbaException(Resources.NoAddessBookOpenedError);

            if (parameters.Count > 0)
            {
                string newLocation = parameters[0];

                if (File.Exists(newLocation))
                {
                    bool? allowToOverwrite = console.AskYesNoCancelQuestion(Resources.OverwriteFileQuestion);

                    if (allowToOverwrite == null || allowToOverwrite == false)
                        return;
                }

                if (parameters.Count >= 2)
                {
                    IGate gate = gates.GetGate(parameters[1]);
                    openedAddressBooks.Current.SaveAddressBook(newLocation, gate);
                }
                else
                {
                    // todo: what happens if Current does not have a gate?
                    openedAddressBooks.Current.SaveAddressBook(newLocation);
                }
            }
            else
            {
                // todo: what happens if Current does not have a gate?
                openedAddressBooks.Current.SaveAddressBook();
            }
        }
    }
}