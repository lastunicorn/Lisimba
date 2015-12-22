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
using System.Reflection;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Properties;

namespace Lisimba.Cmd.Presentation
{
    class ApplicationLoopConsole
    {
        public void WriteWelcomeMessage()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            string title = string.Format(Resources.LisimbaTitle, version);
            ConsoleHelper.WriteLineEmphasize(title);
        }

        public void WriteGoodByeMessage()
        {
            Console.WriteLine(Resources.GoodByeMessage);
        }

        public void WriteGateInfo(string gateName)
        {
            Console.WriteLine(Resources.DefaultGateMessage, gateName);
        }

        public void WriteError(string text)
        {
            ConsoleHelper.WriteLineError(text);
        }
    }
}