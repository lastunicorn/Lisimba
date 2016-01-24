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
using System.Reflection;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Business
{
    class Welcomer
    {
        private readonly EnhancedConsole console;
        private readonly AvailableGates availableGates;

        public Welcomer(EnhancedConsole console, AvailableGates availableGates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.console = console;
            this.availableGates = availableGates;
        }

        public void SayWelcome()
        {
            WriteWelcomeMessage();
            WriteGateInfo(availableGates.DefaultGateName);
        }

        private void WriteWelcomeMessage()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            string title = string.Format(Resources.LisimbaTitle, version);

            console.WriteLineEmphasize(title);
        }

        private void WriteGateInfo(string gateName)
        {
            string text = string.Format(Resources.DefaultGateMessage, gateName);
            console.WriteLineNormal(text);

            console.WriteLine();
        }

        public void SayGoodBye()
        {
            console.WriteLineNormal(Resources.GoodByeMessage);
        }
    }
}