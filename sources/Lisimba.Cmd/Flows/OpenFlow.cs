// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Cmd.Business;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class OpenFlow : IFlow
    {
        private readonly Command command;
        private readonly OpenFlowConsole console;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableGates availableGates;
        private readonly ApplicationConfiguration config;

        public OpenFlow(Command command, OpenFlowConsole console, OpenedAddressBooks openedAddressBooks, AvailableGates availableGates, ApplicationConfiguration config)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (console == null) throw new ArgumentNullException("console");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (config == null) throw new ArgumentNullException("config");

            this.command = command;
            this.console = console;
            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
            this.config = config;
        }

        public void Execute()
        {
            if (command.HasParameters)
                OpenAddressBook();
            else
                OpenLastAddressBook();
        }

        private void OpenAddressBook()
        {
            IGate gate = GetGate();
            openedAddressBooks.OpenAddressBook(command[1], gate);
        }

        private IGate GetGate()
        {
            if (command.ParameterCount >= 2)
            {
                string gateId = command[2];
                return availableGates.GetGate(gateId);
            }

            if (availableGates.DefaultGate == null)
                throw new LisimbaException(Resources.NoDefaultGateError);

            return availableGates.DefaultGate;
        }

        private void OpenLastAddressBook()
        {
            AddressBookLocationInfo addressBookLocationInfo = config.LastAddressBook;

            if (addressBookLocationInfo == null)
                throw new LisimbaException(Resources.NoAddressBookInConfigFile);

            IGate gate = ChooseGate(addressBookLocationInfo);

            openedAddressBooks.OpenAddressBook(addressBookLocationInfo.FileName, gate);
        }

        private IGate ChooseGate(AddressBookLocationInfo addressBookLocationInfo)
        {
            if (addressBookLocationInfo.GateId != null)
                return availableGates.GetGate(addressBookLocationInfo.GateId);

            if (availableGates.DefaultGate == null)
            {
                string message = string.Format(Resources.OpenAddressBookNoGateError, addressBookLocationInfo.FileName);
                throw new LisimbaException(message);
            }

            console.DisplayUsingDefaultGateWarning(addressBookLocationInfo.FileName, availableGates.DefaultGate.Name);

            return availableGates.DefaultGate;
        }
    }
}