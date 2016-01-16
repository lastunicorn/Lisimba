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
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.GateManagement;

namespace DustInTheWind.Lisimba.Cmd.Observers
{
    class ApplicationStartingObserver : IObserver
    {
        private readonly ApplicationStartingObserverConsole console;
        private readonly LisimbaApplication lisimbaApplication;
        private readonly AvailableGates availableGates;

        public ApplicationStartingObserver(ApplicationStartingObserverConsole console, LisimbaApplication lisimbaApplication, AvailableGates availableGates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.console = console;
            this.lisimbaApplication = lisimbaApplication;
            this.availableGates = availableGates;
        }

        public void Start()
        {
            lisimbaApplication.Starting += HandleLisimbaApplicationStarting;
        }

        public void Stop()
        {
            lisimbaApplication.Starting -= HandleLisimbaApplicationStarting;
        }

        private void HandleLisimbaApplicationStarting(object sender, EventArgs eventArgs)
        {
            console.WriteWelcomeMessage();
            console.WriteGateInfo(availableGates.DefaultGateName);
        }
    }
}