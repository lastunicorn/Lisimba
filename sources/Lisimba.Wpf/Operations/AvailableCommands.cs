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
using DustInTheWind.Lisimba.Wpf.Commands;

namespace DustInTheWind.Lisimba.Wpf.Operations
{
    internal class AvailableCommands
    {
        private readonly Dictionary<Type, CommandBase> commands;

        public AvailableCommands(ICommandProvider commandProvider)
        {
            if (commandProvider == null) throw new ArgumentNullException("commandProvider");

            commands = commandProvider.GetNewCommands()
                .ToDictionary(x => x.GetType(), x => x);
        }

        public T GetCommand<T>()
            where T : CommandBase
        {
            Type type = typeof(T);
            CommandBase command = commands[type];
            return (T)command;
        }
    }
}