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
using DustInTheWind.ConsoleCommon.Templating;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class GateFlowConsole
    {
        private readonly EnhancedConsole enhancedConsole;

        public GateFlowConsole(EnhancedConsole enhancedConsole)
        {
            if (enhancedConsole == null) throw new ArgumentNullException("enhancedConsole");

            this.enhancedConsole = enhancedConsole;
        }

        public void DisplayGate(IGate gate)
        {
            const string fileName = "DustInTheWind.Lisimba.Cmd.Templates.GateInfo.t";
            var parameters = new
            {
                DefaultGate = string.Format("{0} ({1})", gate.Name, gate.Id),
                Description = gate.Description
            };

            ConsoleTemplate consoleTemplate = ConsoleTemplate.CreateFromEmbeddedFile(fileName, parameters);
            enhancedConsole.DisplayTemplate(consoleTemplate);
        }

        public void DisplayGateChangeSuccess()
        {
            enhancedConsole.WriteLineSuccess(Resources.GateChangesSuccess);
        }
    }
}