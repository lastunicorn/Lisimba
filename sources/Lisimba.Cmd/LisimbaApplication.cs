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
using DustInTheWind.Lisimba.Common;

namespace DustInTheWind.Lisimba.Cmd
{
    internal class LisimbaApplication
    {
        private readonly LisimbaApplicationConsole console;
        private readonly AvailableGates availableGates;
        private readonly Prompter prompter;
        private readonly AddressBookGuarder addressBookGuarder;

        public LisimbaApplication(AvailableGates availableGates, LisimbaApplicationConsole console,
            Prompter prompter, AddressBookGuarder addressBookGuarder)
        {
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (console == null) throw new ArgumentNullException("console");
            if (prompter == null) throw new ArgumentNullException("prompter");
            if (addressBookGuarder == null) throw new ArgumentNullException("addressBookGuarder");

            this.availableGates = availableGates;
            this.console = console;
            this.prompter = prompter;
            this.addressBookGuarder = addressBookGuarder;
        }

        public void Run()
        {
            console.WriteWelcomeMessage();
            console.WriteGateInfo(availableGates.DefaultGateName);

            addressBookGuarder.Start();
            prompter.Run();

            console.WriteGoodByeMessage();
        }

        public void Exit()
        {
            prompter.Stop();
        }
    }
}