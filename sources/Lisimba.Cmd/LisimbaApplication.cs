using System;
using Lisimba.Cmd.Commands;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd
{
    class LisimbaApplication
    {
        private readonly Gates gates;
        private readonly CommandProvider commandProvider;
        private readonly ConsoleView consoleView;
        private readonly Prompter prompter;

        public bool ExitRequested { get; set; }

        public LisimbaApplication(Gates gates, CommandProvider commandProvider, ConsoleView consoleView, Prompter prompter)
        {
            if (gates == null) throw new ArgumentNullException("gates");
            if (commandProvider == null) throw new ArgumentNullException("commandProvider");
            if (consoleView == null) throw new ArgumentNullException("consoleView");
            if (prompter == null) throw new ArgumentNullException("prompter");

            this.gates = gates;
            this.commandProvider = commandProvider;
            this.consoleView = consoleView;
            this.prompter = prompter;
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
                CommandInfo commandInfo = prompter.Read();
                ProcessCommand(commandInfo);
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