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
using DustInTheWind.Lisimba.Business.AddressBookManagement;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class NewFlow : IFlow
    {
        private readonly OpenedAddressBooks openedAddressBooks;

        public NewFlow(OpenedAddressBooks openedAddressBooks)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.openedAddressBooks = openedAddressBooks;
        }

        public void Execute(IList<string> parameters)
        {
            string newName = (parameters != null && parameters.Count > 0)
                ? parameters[0]
                : null;

            openedAddressBooks.CreateNewAddressBook(newName);
        }
    }
}