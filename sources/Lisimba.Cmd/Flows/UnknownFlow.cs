using System;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    class UnknownFlow : IFlow
    {
        private readonly ConsoleView consoleView;

        public UnknownFlow(ConsoleView consoleView)
        {
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.consoleView = consoleView;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            consoleView.DisplayUnknownCommandError();
        }
    }
}