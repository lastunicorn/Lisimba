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