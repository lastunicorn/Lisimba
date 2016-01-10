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

using Lisimba.Cmd.Common;
using Lisimba.Cmd.Properties;

namespace Lisimba.Cmd.Flows
{
    class OpenFlowConsole
    {
        public void DisplayAddressBookOpenSuccess(string addressBookFileName, int contactsCount)
        {
            string message = string.Format(Resources.AddressBookOpenSuccess, contactsCount, addressBookFileName);
            ConsoleHelper.WriteLineSuccess(message);
        }

        public void DisplayNoAddressBookMessage()
        {
            ConsoleHelper.WriteLineError(Resources.OpenAddressBookUnknownError);
        }

        public void DisplayUsingDefaultGateWarning(string addressBookFileName, string gateId)
        {
            string message = string.Format(Resources.AddressBookOpenUseDefaultGateWarning, addressBookFileName, gateId);
            ConsoleHelper.WriteLineWarning(message);
        }
    }
}