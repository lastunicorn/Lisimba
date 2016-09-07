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
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class HelpFlow : IFlow
    {
        private readonly EnhancedConsole console;
        private readonly string generalHelp = BuildGeneralHelp();

        public HelpFlow(EnhancedConsole console)
        {
            if (console == null) throw new ArgumentNullException("console");
            this.console = console;
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters.Count == 0)
            {
                console.WriteLine();
                console.WriteLineNormal(generalHelp);
            }
        }

        private static string BuildGeneralHelp()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("open    - Openes an address book.");
            sb.AppendLine("save    - Saves an address book.");
            sb.AppendLine("new     - Creates a new empty address book.");
            sb.AppendLine("update  - Openes an address book.");
            sb.AppendLine("list    - Displays all contacts from the current address book.");
            sb.AppendLine("search  - Searchs for a contact in the current address book.");
            sb.AppendLine("show    - Displays a contact from the current address book.");
            sb.AppendLine("info    - Displays information about the current address book.");
            sb.AppendLine("gate    - Displays or sets the default gate.");
            sb.AppendLine("exit    - Closes the application.");
            sb.AppendLine("bye     - Closes the application.");
            sb.AppendLine("goodbye - Closes the application.");
            sb.AppendLine("help    - Displayes this help message.");

            return sb.ToString();
        }
    }
}