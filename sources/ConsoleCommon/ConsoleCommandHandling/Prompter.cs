// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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
using DustInTheWind.ConsoleCommon.Templating;

namespace DustInTheWind.ConsoleCommon.ConsoleCommandHandling
{
    /// <summary>
    /// Provides a loop for reading commands from the console, parsing them and run the needed flow.
    /// </summary>
    public class Prompter
    {
        private readonly EnhancedConsole console;
        private readonly ApplicationFlows applicationFlows;
        private readonly IPrompterTextProvider prompterTextProvider;
        private bool stopRequested;

        public Prompter(EnhancedConsole console, ApplicationFlows applicationFlows, IPrompterTextProvider prompterTextProvider)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (applicationFlows == null) throw new ArgumentNullException("applicationFlows");
            if (prompterTextProvider == null) throw new ArgumentNullException("prompterTextProvider");

            this.console = console;
            this.applicationFlows = applicationFlows;
            this.prompterTextProvider = prompterTextProvider;
        }

        public void Run()
        {
            stopRequested = false;

            applicationFlows.Initialize();

            while (!stopRequested)
            {
                DisplayPrompter();
                ConsoleCommand consoleCommand = ReadCommand();
                ProcessCommand(consoleCommand);
            }
        }

        private void DisplayPrompter()
        {
            ConsoleTemplate consoleTemplate = prompterTextProvider.BuildTemplate();

            console.WriteLine();
            console.DisplayTemplate(consoleTemplate);
        }

        private ConsoleCommand ReadCommand()
        {
            string commandText = console.ReadLine();
            return new ConsoleCommand(commandText);
        }

        private void ProcessCommand(ConsoleCommand consoleCommand)
        {
            try
            {
                applicationFlows.HandleCommand(consoleCommand);
            }
            catch (Exception ex)
            {
                console.WriteError(ex.Message);
                console.WriteLine();
            }
        }

        public void Stop()
        {
            stopRequested = true;
        }
    }
}