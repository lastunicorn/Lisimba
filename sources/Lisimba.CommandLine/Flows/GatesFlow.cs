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
using System.Collections.Generic;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.CommandModel;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    class GatesFlow : IFlow
    {
        private readonly EnhancedConsole console;
        private readonly AvailableGates availableGates;

        public GatesFlow(EnhancedConsole console, AvailableGates availableGates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.console = console;
            this.availableGates = availableGates;
        }

        public void Execute()
        {
            IEnumerable<IGate> gates = availableGates.GetAllGates();

            foreach (IGate gate in gates)
            {
                string text = string.Format("{0} : {1}", gate.Id, gate.Name);
                console.WriteLineNormal(text);
            }
        }
    }
}
