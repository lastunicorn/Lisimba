using System;
using Lisimba.Cmd.Commands;

namespace Lisimba.Cmd
{
    class LisimbaApplication
    {
        private readonly AddressBooks addressBooks;
        private readonly Gates gates;
        private readonly CommandProvider commandProvider;
        private readonly ConsoleView consoleView;

        public bool ExitRequested { get; set; }

        public LisimbaApplication(AddressBooks addressBooks, Gates gates, CommandProvider commandProvider, ConsoleView consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (gates == null) throw new ArgumentNullException("gates");
            if (commandProvider == null) throw new ArgumentNullException("commandProvider");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.gates = gates;
            this.commandProvider = commandProvider;
            this.consoleView = consoleView;
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
            CommandReadControl commandReadControl = new CommandReadControl(addressBooks, consoleView);

            while (!ExitRequested)
            {
                CommandInfo command = commandReadControl.Read();
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