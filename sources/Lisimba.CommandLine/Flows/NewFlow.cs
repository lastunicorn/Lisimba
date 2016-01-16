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
using DustInTheWind.Lisimba.Common.AddressBookManagement;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class NewFlow : IFlow
    {
        private readonly ConsoleCommand consoleCommand;
        private readonly OpenedAddressBooks openedAddressBooks;

        public NewFlow(ConsoleCommand consoleCommand, OpenedAddressBooks openedAddressBooks)
        {
            if (consoleCommand == null) throw new ArgumentNullException("consoleCommand");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.consoleCommand = consoleCommand;
            this.openedAddressBooks = openedAddressBooks;
        }

        public void Execute()
        {
            openedAddressBooks.CreateNewAddressBook(consoleCommand[1]);
        }
    }
}