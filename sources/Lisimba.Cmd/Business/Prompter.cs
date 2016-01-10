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
using DustInTheWind.Lisimba.Cmd.Common;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    /// <summary>
    /// Provides a loop for reading commands from the console, parsing them and run the needed flow.
    /// </summary>
    class Prompter
    {
        private readonly AddressBooks addressBooks;
        private readonly PrompterConsole console;
        private readonly FlowProvider flowProvider;

        public Prompter(AddressBooks addressBooks, PrompterConsole console, FlowProvider flowProvider)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");
            if (flowProvider == null) throw new ArgumentNullException("flowProvider");

            this.addressBooks = addressBooks;
            this.console = console;
            this.flowProvider = flowProvider;
        }

        public void Run()
        {
            while (!LisimbaApplication.ExitRequested)
            {
                DisplayPrompter();
                Command command = ReadCommand();
                ProcessCommand(command);
            }
        }

        private void DisplayPrompter()
        {
            string addressBookName = addressBooks.Current == null ? null : addressBooks.Current.AddressBook.Name;
            bool isSaved = addressBooks.Current == null || addressBooks.Current.IsAddressBookSaved;

            console.DisplayPrompter(addressBookName, isSaved);
        }

        private Command ReadCommand()
        {
            string commandText = console.ReadCommand();
            return new Command(commandText);
        }

        private void ProcessCommand(Command command)
        {
            try
            {
                IFlow flow = flowProvider.CreateFlow(command);
                flow.Execute();
            }
            catch (Exception ex)
            {
                console.WriteError(ex.Message);
            }
        }
    }
}