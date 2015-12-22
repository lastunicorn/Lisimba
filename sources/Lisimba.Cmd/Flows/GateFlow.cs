using System;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    class GateFlow : IFlow
    {
        private readonly Gates gates;
        private readonly ConsoleView consoleView;

        public GateFlow(Gates gates, ConsoleView consoleView)
        {
            if (gates == null) throw new ArgumentNullException("gates");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.gates = gates;
            this.consoleView = consoleView;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            if (command.ParameterCount == 0)
            {
                consoleView.DisplayGate(gates.DefaultGate);
            }
            else
            {
                gates.SetDefaultGate(command[1]);

                consoleView.DisplayGateChangeSuccess();
            }
        }
    }
}