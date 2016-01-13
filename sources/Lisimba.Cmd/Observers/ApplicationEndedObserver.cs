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

namespace DustInTheWind.Lisimba.Cmd.Observers
{
    class ApplicationEndedObserver : IObserver
    {
        private readonly ApplicationEndedObserverConsole console;
        private readonly LisimbaApplication lisimbaApplication;

        public ApplicationEndedObserver(ApplicationEndedObserverConsole console, LisimbaApplication lisimbaApplication)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");

            this.console = console;
            this.lisimbaApplication = lisimbaApplication;
        }

        public void Start()
        {
            lisimbaApplication.Ended += HandleLisimbaApplicationEnded;
        }

        private void HandleLisimbaApplicationEnded(object sender, EventArgs eventArgs)
        {
            console.WriteGoodByeMessage();
        }
    }
}