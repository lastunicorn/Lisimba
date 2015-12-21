using System;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Commands
{
    class UnknownCommand : ICommand
    {
        private readonly ConsoleView consoleView;

        public UnknownCommand(ConsoleView consoleView)
        {
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            consoleView.WriteUnknownCommand();
        }
    }
}