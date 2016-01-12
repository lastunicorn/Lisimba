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
    class GateFlow : IFlow
    {
        private readonly ConsoleCommand consoleCommand;
        private readonly AvailableGates availableGates;
        private readonly GateFlowConsole console;

        public GateFlow(ConsoleCommand consoleCommand, AvailableGates availableGates, GateFlowConsole console)
        {
            if (consoleCommand == null) throw new ArgumentNullException("consoleCommand");
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (console == null) throw new ArgumentNullException("console");

            this.consoleCommand = consoleCommand;
            this.availableGates = availableGates;
            this.console = console;
        }

        public void Execute()
        {
            if (consoleCommand.ParameterCount == 0)
            {
                console.DisplayGate(availableGates.DefaultGate);
            }
            else
            {
                availableGates.SetDefaultGate(consoleCommand[1]);

                console.DisplayGateChangeSuccess();
                console.DisplayGate(availableGates.DefaultGate);
            }
        }
    }
}