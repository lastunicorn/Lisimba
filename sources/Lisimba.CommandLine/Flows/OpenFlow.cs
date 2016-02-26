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
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.CommandLine.Properties;
using DustInTheWind.Lisimba.Egg.GateModel;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class OpenFlow : IFlow
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableGates availableGates;
        private readonly ApplicationConfiguration config;

        public OpenFlow(OpenedAddressBooks openedAddressBooks,
            AvailableGates availableGates, ApplicationConfiguration config)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (config == null) throw new ArgumentNullException("config");

            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
            this.config = config;
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
                OpenAddressBookFromCommand(parameters);
            else
                OpenLastAddressBook();
        }

        private void OpenAddressBookFromCommand(IList<string> parameters)
        {
            string fileName = parameters[0];
            IGate gate = GetGateFromCommand(parameters);

            openedAddressBooks.OpenAddressBook(fileName, gate);
        }

        private IGate GetGateFromCommand(IList<string> parameters)
        {
            if (parameters.Count >= 2)
            {
                string gateId = parameters[1];
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

            try
            {
                return availableGates.GetGate(addressBookLocationInfo.GateId);
            }
            catch (Exception ex)
            {
                string message = string.Format(Resources.OpenAddressBook_GateNotFoundError, addressBookLocationInfo.GateId, addressBookLocationInfo.FileName);
                throw new LisimbaException(message, ex);
            }
        }
    }
}