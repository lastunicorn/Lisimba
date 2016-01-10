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
using DustInTheWind.Lisimba.Egg;
using Lisimba.Cmd.Business;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Properties;

namespace Lisimba.Cmd.Flows
{
    class OpenFlow : IFlow
    {
        private readonly Command command;
        private readonly OpenFlowConsole console;
        private readonly AddressBooks addressBooks;
        private readonly Gates gates;
        private readonly ApplicationConfiguration config;

        public OpenFlow(Command command, OpenFlowConsole console, AddressBooks addressBooks, Gates gates, ApplicationConfiguration config)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (console == null) throw new ArgumentNullException("console");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (gates == null) throw new ArgumentNullException("gates");
            if (config == null) throw new ArgumentNullException("config");

            this.command = command;
            this.console = console;
            this.addressBooks = addressBooks;
            this.gates = gates;
            this.config = config;
        }

        public void Execute()
        {
            if (command.HasParameters)
                OpenAddressBookFromCommand();
            else
                OpenLastAddressBook();

            DisplayResultMessage();
        }

        private void OpenAddressBookFromCommand()
        {
            IGate gate = GetGate();
            addressBooks.OpenAddressBook(command[1], gate);
        }

        private IGate GetGate()
        {
            if (command.ParameterCount >= 2)
            {
                string gateId = command[2];
                return gates.GetGate(gateId);
            }

            if (gates.DefaultGate == null)
                throw new ApplicationException(Resources.NoDefaultGateError);

            return gates.DefaultGate;
        }

        private void OpenLastAddressBook()
        {
            AddressBookLocationInfo addressBookLocationInfo = config.LastAddressBook;

            if (addressBookLocationInfo == null)
                throw new ApplicationException(Resources.NoAddressBookInConfigFile);

            IGate gate = ChooseGate(addressBookLocationInfo);

            addressBooks.OpenAddressBook(addressBookLocationInfo.FileName, gate);
        }

        private IGate ChooseGate(AddressBookLocationInfo addressBookLocationInfo)
        {
            if (addressBookLocationInfo.GateId != null)
                return gates.GetGate(addressBookLocationInfo.GateId);

            if (gates.DefaultGate == null)
            {
                string message = string.Format(Resources.OpenAddressBookNoGateError, addressBookLocationInfo.FileName);
                throw new ApplicationException(message);
            }

            console.DisplayUsingDefaultGateWarning(addressBookLocationInfo.FileName, gates.DefaultGate.Name);

            return gates.DefaultGate;
        }

        private void DisplayResultMessage()
        {
            if (addressBooks.Current != null)
            {
                string addressBookFileName = addressBooks.Current.Location;
                int contactsCount = addressBooks.Current.AddressBook.Contacts.Count;

                console.DisplayAddressBookOpenSuccess(addressBookFileName, contactsCount);
            }
            else
            {
                console.DisplayNoAddressBookMessage();
            }
        }
    }
}