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
using DustInTheWind.Lisimba.Egg;
using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Presentation
{
    class GateFlowConsole
    {
        public void DisplayGate(IGate gate)
        {
            Console.WriteLine();

            ConsoleHelper.WriteEmphasize("DefaultGate: ");
            Console.WriteLine("{0} ({1})", gate.Name, gate.Id);

            ConsoleHelper.WriteEmphasize("Description: ");
            Console.WriteLine(gate.Description);
        }

        public void DisplayGateChangeSuccess()
        {
            ConsoleHelper.WriteLineSuccess("The gate was successfully changed.");
        }
    }
}