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
using System.Linq;

namespace DustInTheWind.ConsoleCommon.ConsoleCommandHandling
{
    /// <summary>
    /// Represents a command written by the user in the console.
    /// </summary>
    public class ConsoleCommand
    {
        public string Name { get; set; }
        public List<string> Parameters { get; set; }

        public ConsoleCommand(string commandText)
        {
            if (commandText == null) throw new ArgumentNullException("commandText");

            ParseString(commandText);
        }

        private void ParseString(string commandText)
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter(commandText);
            string[] items = consoleCommandSplitter.Items;

            Name = items.Length > 0
                ? items[0]
                : string.Empty;

            Parameters = items
                .Skip(1)
                .ToList();
        }
    }
}