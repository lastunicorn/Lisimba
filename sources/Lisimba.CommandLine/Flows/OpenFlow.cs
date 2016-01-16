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
using DustInTheWind.ConsoleCommon.CommandModel;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Common.Config;
using DustInTheWind.Lisimba.Common.GateManagement;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class OpenFlow : IFlow
    {
        private readonly ConsoleCommand consoleCommand;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableGates availableGates;
        private readonly ApplicationConfiguration config;

        public OpenFlow(ConsoleCommand consoleCommand, OpenedAddressBooks openedAddressBooks,
            AvailableGates availableGates, ApplicationConfiguration config)
        {
            if (consoleCommand == null) throw new ArgumentNullException("consoleCommand");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (config == null) throw new ArgumentNullException("config");

            this.consoleCommand = consoleCommand;
            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
            this.config = config;
        }

        public void Execute()
        {
            if (consoleCommand.HasParameters)
                OpenAddressBookFromCommand();
            else
                OpenLastAddressBook();
        }

        private void OpenAddressBookFromCommand()
        {
            string fileName = consoleCommand[1];
            IGate gate = GetGateFromCommand();

            openedAddressBooks.OpenAddressBook(fileName, gate);
        }

        private IGate GetGateFromCommand()
        {
            if (consoleCommand.ParameterCount >= 2)
            {
                string gateId = consoleCommand[2];
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
                throw new LisimbaException(Resources.OpenAddressBook_NoLastAddressBook);

            IGate gate = GetGate(addressBookLocationInfo);

            openedAddressBooks.OpenAddressBook(addressBookLocationInfo.FileName, gate);
        }

        private IGate GetGate(AddressBookLocationInfo addressBookLocationInfo)
        {
            if (addressBookLocationInfo.GateId == null)
            {
                string message = string.Format(Resources.OpenAddressBook_NoGateError, addressBookLocationInfo.FileName);
                throw new LisimbaException(message);
            }

            IGate gate = availableGates.GetGate(addressBookLocationInfo.GateId);

            if (gate == null)
            {
                string message = string.Format(Resources.OpenAddressBook_GateNotFoundError, addressBookLocationInfo.GateId, addressBookLocationInfo.FileName);
                throw new LisimbaException(message);
            }

            return gate;
        }
    }
}