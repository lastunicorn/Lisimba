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

using System;
using System.IO;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.CommandModel;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.CommandLine.Properties;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    class SaveFlow : IFlow
    {
        private readonly EnhancedConsole console;
        private readonly ConsoleCommand consoleCommand;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableGates availableGates;

        public SaveFlow(EnhancedConsole console, ConsoleCommand consoleCommand, OpenedAddressBooks openedAddressBooks, AvailableGates availableGates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (consoleCommand == null) throw new ArgumentNullException("consoleCommand");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.console = console;
            this.consoleCommand = consoleCommand;
            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
        }

        public void Execute()
        {
            if (openedAddressBooks.Current == null)
                throw new LisimbaException(Resources.NoAddessBookOpenedError);

            if (consoleCommand.ParameterCount >= 1)
            {
                string newLocation = consoleCommand[1];

                if (File.Exists(newLocation))
                {
                    bool? allowToOverwrite = console.AskYesNoCancelQuestion(Resources.OverwriteFileQuestion);

                    if (allowToOverwrite == null || allowToOverwrite == false)
                        return;
                }

                if (consoleCommand.ParameterCount >= 2)
                {
                    IGate gate = availableGates.GetGate(consoleCommand[2]);
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
                openedAddressBooks.SaveAddressBook();
            }
        }
    }
}