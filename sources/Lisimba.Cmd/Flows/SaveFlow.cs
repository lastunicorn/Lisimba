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
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class SaveFlow : IFlow
    {
        private readonly Command command;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableGates availableGates;

        public SaveFlow(Command command, OpenedAddressBooks openedAddressBooks, AvailableGates availableGates)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.command = command;
            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
        }

        public void Execute()
        {
            if (openedAddressBooks.Current == null)
                return;

            if (command.ParameterCount >= 1)
            {
                if (command.ParameterCount >= 2)
                {
                    IGate gate = availableGates.GetGate(command[2]);
                    openedAddressBooks.Current.SaveAddressBook(command[1], gate);
                }
                else
                {
                    openedAddressBooks.Current.SaveAddressBook(command[1]);
                }
            }
            else
            {
                openedAddressBooks.Current.SaveAddressBook();
            }
        }
    }
}