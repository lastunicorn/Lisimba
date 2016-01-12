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

namespace DustInTheWind.Lisimba.Cmd.Business
{
    /// <summary>
    /// Provides a loop for reading commands from the console, parsing them and run the needed flow.
    /// </summary>
    class Prompter
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly PrompterConsole console;
        private readonly FlowFactory flowFactory;
        private bool stopRequested;

        public Prompter(OpenedAddressBooks openedAddressBooks, PrompterConsole console, FlowFactory flowFactory)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (console == null) throw new ArgumentNullException("console");
            if (flowFactory == null) throw new ArgumentNullException("flowFactory");

            this.openedAddressBooks = openedAddressBooks;
            this.console = console;
            this.flowFactory = flowFactory;
        }

        public void Run()
        {
            stopRequested = false;

            while (!stopRequested)
            {
                DisplayPrompter();
                ConsoleCommand consoleCommand = ReadCommand();
                ProcessCommand(consoleCommand);
            }
        }

        private void DisplayPrompter()
        {
            string addressBookName = openedAddressBooks.Current == null ? null : openedAddressBooks.Current.AddressBook.Name;
            bool isModified = openedAddressBooks.Current != null && openedAddressBooks.Current.Status == AddressBookStatus.Modified;

            console.DisplayPrompter(addressBookName, isModified);
        }

        private ConsoleCommand ReadCommand()
        {
            string commandText = console.ReadCommand();
            return new ConsoleCommand(commandText);
        }

        private void ProcessCommand(ConsoleCommand consoleCommand)
        {
            try
            {
                IFlow flow = flowFactory.CreateFlow(consoleCommand);
                flow.Execute();
            }
            catch (Exception ex)
            {
                console.WriteError(ex.Message);
            }
        }

        public void Stop()
        {
            stopRequested = true;
        }
    }
}