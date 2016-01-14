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
using System.Collections.Generic;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class GateFlowConsole
    {
        private readonly UserInterface userInterface;

        public GateFlowConsole(UserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;
        }

        public void DisplayGate(IGate gate)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DefaultGate", string.Format("{0} ({1})", gate.Name, gate.Id) },
                { "Description", gate.Description }
            };

            ConsoleTemplate consoleTemplate = ConsoleTemplate.CreateFromFile("GateInfo.t", parameters);
            userInterface.DisplayTemplate(consoleTemplate);
        }

        public void DisplayGateChangeSuccess()
        {
            userInterface.WriteLineSuccess(Resources.GateChangesSuccess);
        }
    }
}