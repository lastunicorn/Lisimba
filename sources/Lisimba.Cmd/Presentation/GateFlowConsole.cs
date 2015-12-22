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