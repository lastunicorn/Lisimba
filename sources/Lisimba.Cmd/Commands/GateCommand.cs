using System;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Commands
{
    class GateCommand : ICommand
    {
        private readonly Gates gates;
        private readonly ConsoleView consoleView;

        public GateCommand(Gates gates, ConsoleView consoleView)
        {
            if (gates == null) throw new ArgumentNullException("gates");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.gates = gates;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            if (commandInfo.ParameterCount == 0)
            {
                consoleView.DisplayGate(gates.DefaultGate);
            }
            else
            {
                gates.SetDefaultGate(commandInfo[1]);

                consoleView.DisplayGateChangeSuccess();
            }
        }
    }
}