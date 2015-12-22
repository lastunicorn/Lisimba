using System;
using Lisimba.Cmd.Business;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Flows;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd
{
    class ApplicationLoop
    {
        private readonly Gates gates;
        private readonly FlowProvider flowProvider;
        private readonly ApplicationLoopConsole console;
        private readonly Prompter prompter;

        public bool ExitRequested { get; set; }

        public ApplicationLoop(Gates gates, FlowProvider flowProvider, ApplicationLoopConsole console, Prompter prompter)
        {
            if (gates == null) throw new ArgumentNullException("gates");
            if (flowProvider == null) throw new ArgumentNullException("flowProvider");
            if (console == null) throw new ArgumentNullException("console");
            if (prompter == null) throw new ArgumentNullException("prompter");

            this.gates = gates;
            this.flowProvider = flowProvider;
            this.console = console;
            this.prompter = prompter;
        }

        public void Run()
        {
            console.WriteWelcomeMessage();
            console.WriteGateInfo(gates.DefaultGateName);

            RunMainLoop();

            console.WriteGoodByeMessage();
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
                IFlow flow = flowProvider.CreateFlow(command.Name);
                flow.Execute(command);
            }
            catch (Exception ex)
            {
                console.WriteError(ex.Message);
            }
        }
    }
}
