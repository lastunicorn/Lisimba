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
using System.ComponentModel;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Services;
using LisimbaApplication = DustInTheWind.Lisimba.Services.LisimbaApplication;

namespace DustInTheWind.Lisimba.Observers
{
    class ApplicationEndingObserver : IObserver
    {
        private readonly LisimbaApplication lisimbaApplication;
        private readonly UserInterface userInterface;

        public ApplicationEndingObserver(LisimbaApplication lisimbaApplication, UserInterface userInterface)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");

            this.lisimbaApplication = lisimbaApplication;
            this.userInterface = userInterface;
        }

        public void Start()
        {
            lisimbaApplication.Ending += HandleLisimbaApplicationEnding;
        }

        public void Stop()
        {
            lisimbaApplication.Ending -= HandleLisimbaApplicationEnding;
        }

        private void HandleLisimbaApplicationEnding(object sender, CancelEventArgs e)
        {
            userInterface.Exit();
        }
    }
}