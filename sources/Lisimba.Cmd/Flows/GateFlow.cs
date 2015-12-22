﻿// Lisimba
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
using Lisimba.Cmd.Business;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    class GateFlow : IFlow
    {
        private readonly Gates gates;
        private readonly GateFlowConsole console;

        public GateFlow(Gates gates, GateFlowConsole console)
        {
            if (gates == null) throw new ArgumentNullException("gates");
            if (console == null) throw new ArgumentNullException("console");

            this.gates = gates;
            this.console = console;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            if (command.ParameterCount == 0)
            {
                console.DisplayGate(gates.DefaultGate);
            }
            else
            {
                gates.SetDefaultGate(command[1]);

                console.DisplayGateChangeSuccess();
            }
        }
    }
}