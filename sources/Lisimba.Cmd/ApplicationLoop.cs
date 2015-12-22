// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
