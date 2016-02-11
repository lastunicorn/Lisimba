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
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.ConsoleCommon.Templating;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.CommandLine.Business;
using DustInTheWind.Lisimba.CommandLine.Properties;
using DustInTheWind.Lisimba.Egg.GateModel;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    class GateFlow : IFlow
    {
        private readonly ConsoleCommand consoleCommand;
        private readonly AvailableGates availableGates;
        private readonly EnhancedConsole console;

        public GateFlow(ConsoleCommand consoleCommand, AvailableGates availableGates, EnhancedConsole console)
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
                DisplayGate(availableGates.DefaultGate);
            }
            else
            {
                availableGates.SetDefaultGate(consoleCommand[1]);

                DisplayGateChangeSuccess();
                DisplayGate(availableGates.DefaultGate);
            }
        }

        public void DisplayGateChangeSuccess()
        {
            console.WriteLineSuccess(Resources.GateChangesSuccess);
        }

        public void DisplayGate(IGate gate)
        {
            string templateFileName = ViewTemplates.GetFullFileName("GateInfo.t");
            var parameters = new
            {
                DefaultGate = string.Format("{0} ({1})", gate.Name, gate.Id),
                Description = gate.Description
            };

            ConsoleTemplate consoleTemplate = ConsoleTemplate.CreateFromEmbeddedFile(templateFileName, parameters);
            console.DisplayTemplate(consoleTemplate);
        }
    }
}