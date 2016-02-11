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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleCommon.ConsoleCommandHandling
{
    /// <summary>
    /// Represents a command written by the user in the console.
    /// </summary>
    public class ConsoleCommand : IEnumerable<string>
    {
        private readonly List<string> parameters;

        public string Name { get; private set; }

        public bool HasParameters
        {
            get { return parameters.Any(); }
        }

        public int ParameterCount
        {
            get { return parameters.Count; }
        }

        public string this[int index]
        {
            get
            {
                if (index < 0 || index > parameters.Count)
                    return null;

                if (index == 0)
                    return Name;

                return parameters[index - 1];
            }
        }

        public ConsoleCommand(string commandText)
        {
            if (commandText == null)
            {
                Name = string.Empty;
                parameters = new List<string>();
            }
            else
            {
                CommandSplitter commandSplitter = new CommandSplitter(commandText);
                string[] items = commandSplitter.Items;

                Name = items.Length > 0 ? items[0] : string.Empty;

                parameters = items
                    .Skip(1)
                    .ToList();
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}