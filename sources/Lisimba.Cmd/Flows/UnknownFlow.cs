using System;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    class UnknownFlow : IFlow
    {
        private readonly UnknownFlowConsole console;

        public UnknownFlow(UnknownFlowConsole console)
        {
            if (console == null) throw new ArgumentNullException("console");

            this.console = console;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            console.DisplayUnknownCommandError();
        }
    }
}