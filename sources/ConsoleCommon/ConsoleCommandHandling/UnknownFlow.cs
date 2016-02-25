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
using System.Collections.ObjectModel;
using DustInTheWind.ConsoleCommon.Properties;

namespace DustInTheWind.ConsoleCommon.ConsoleCommandHandling
{
    public class UnknownFlow : IFlow
    {
        private readonly EnhancedConsole console;

        public UnknownFlow(EnhancedConsole console)
        {
            if (console == null) throw new ArgumentNullException("console");

            this.console = console;
        }

        public void Execute(ReadOnlyCollection<string> parameters)
        {
            console.WriteLineError(Resources.UnknownCommandError);
        }
    }
}