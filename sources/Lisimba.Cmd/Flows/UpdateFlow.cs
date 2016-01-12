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

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    internal class UpdateFlow : IFlow
    {
        private readonly ConsoleCommand consoleCommand;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly UpdateFlowConsole consoleView;

        public UpdateFlow(ConsoleCommand consoleCommand, OpenedAddressBooks openedAddressBooks, UpdateFlowConsole consoleView)
        {
            if (consoleCommand == null) throw new ArgumentNullException("consoleCommand");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.consoleCommand = consoleCommand;
            this.openedAddressBooks = openedAddressBooks;
            this.consoleView = consoleView;
        }

        public void Execute()
        {
            foreach (string actionText in consoleCommand)
            {
                ProcessAction(actionText);
            }
        }

        private void ProcessAction(string actionText)
        {
            Tuple<string, string> action = ParseAction(actionText);

            switch (action.Item1.ToLower())
            {
                case "name":
                    UpdateAddressBookName(action.Item2);
                    break;

                default:
                    consoleView.DisplayInvalidUpdateActionError();
                    break;
            }
        }

        private static Tuple<string, string> ParseAction(string actionText)
        {
            int pos = actionText.IndexOf('=');

            if (pos == -1)
                return null;

            string paramName = actionText.Substring(0, pos);

            string value = actionText.Substring(pos + 1);
            if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"')
                value = value.Substring(1, value.Length - 2);

            return new Tuple<string, string>(paramName, value);
        }

        private void UpdateAddressBookName(string newAddressBookName)
        {
            if (openedAddressBooks.Current != null)
            {
                openedAddressBooks.Current.AddressBook.Name = newAddressBookName;
                consoleView.DisplayAddressBookNameChangeSuccess();
            }
            else
            {
                consoleView.DisplayNoAddressBookMessage();
            }
        }
    }
}