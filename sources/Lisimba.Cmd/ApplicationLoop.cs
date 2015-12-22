using System;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Flows;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd
{
    class ApplicationLoop
    {
        private readonly Gates gates;
        private readonly FlowProvider flowProvider;
        private readonly ConsoleView consoleView;
        private readonly Prompter prompter;

        public bool ExitRequested { get; set; }

        public ApplicationLoop(Gates gates, FlowProvider flowProvider, ConsoleView consoleView, Prompter prompter)
        {
            if (gates == null) throw new ArgumentNullException("gates");
            if (flowProvider == null) throw new ArgumentNullException("flowProvider");
            if (consoleView == null) throw new ArgumentNullException("consoleView");
            if (prompter == null) throw new ArgumentNullException("prompter");

            this.gates = gates;
            this.flowProvider = flowProvider;
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
                Command command = prompter.Read();
                ProcessCommand(command);
            }
        }

        private void ProcessCommand(Command command)
        {
            try
            {
                IFlow flow = flowProvider.CreateCommand(command.Name);
                flow.Execute(command);
            }
            catch (Exception ex)
            {
                consoleView.WriteError(ex.Message);
            }
        }
    }
}
