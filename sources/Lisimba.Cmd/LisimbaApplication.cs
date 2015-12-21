using System;
using Lisimba.Cmd.Commands;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd
{
    class LisimbaApplication
    {
        private readonly AddressBooks addressBooks;
        private readonly Gates gates;
        private readonly CommandProvider commandProvider;
        private readonly ConsoleView consoleView;
        private readonly CommandControl commandControl;

        public bool ExitRequested { get; set; }

        public LisimbaApplication(AddressBooks addressBooks, Gates gates, CommandProvider commandProvider,
            ConsoleView consoleView, CommandControl commandControl)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (gates == null) throw new ArgumentNullException("gates");
            if (commandProvider == null) throw new ArgumentNullException("commandProvider");
            if (consoleView == null) throw new ArgumentNullException("consoleView");
            if (commandControl == null) throw new ArgumentNullException("commandControl");

            this.addressBooks = addressBooks;
            this.gates = gates;
            this.commandProvider = commandProvider;
            this.consoleView = consoleView;
            this.commandControl = commandControl;
        }

        public void Run()
        {
            consoleView.WriteWelcomeMessage();
            consoleView.WriteGateInfo(gates.DefaultGateName);

            RunMainLoop();

            consoleView.WriteGoodByeMessage();
        }

        private void RunMainLoop()
        {
            while (!ExitRequested)
            {
                CommandInfo command = commandControl.Read();
                ProcessCommand(command);
            }
        }

        private void ProcessCommand(CommandInfo commandInfo)
        {
            try
            {
                ICommand command = commandProvider.CreateCommand(commandInfo.Name);
                command.Execute(commandInfo);
            }
            catch (Exception ex)
            {
                consoleView.WriteError(ex.Message);
            }
        }
    }
}