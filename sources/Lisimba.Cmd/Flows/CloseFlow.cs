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
using DustInTheWind.Lisimba.Cmd.Business;
using DustInTheWind.Lisimba.Cmd.Common;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class CloseFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly CloseFlowConsole console;

        public CloseFlow(AddressBooks addressBooks, CloseFlowConsole console)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.addressBooks = addressBooks;
            this.console = console;
        }

        public void Execute()
        {
            if (addressBooks.Current != null)
            {
                addressBooks.CloseAddressBook();
                console.DisplayAddressBookCloseSuccess();
            }
            else
            {
                console.DisplayNoAddressBookMessage();
            }
        }
    }
}