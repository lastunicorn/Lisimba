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
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;
using LisimbaApplication = DustInTheWind.Lisimba.Services.LisimbaApplication;

namespace DustInTheWind.Lisimba.Observers
{
    class ApplicationExitingObserver : IObserver
    {
        private readonly LisimbaApplication lisimbaApplication;
        private readonly ApplicationStatus applicationStatus;
        private readonly UserInterface userInterface;

        public ApplicationExitingObserver(LisimbaApplication lisimbaApplication, ApplicationStatus applicationStatus, UserInterface userInterface)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");

            this.lisimbaApplication = lisimbaApplication;
            this.applicationStatus = applicationStatus;
            this.userInterface = userInterface;
        }

        public void Start()
        {
            lisimbaApplication.Exiting += HandleLisimbaApplicationExiting;
        }

        public void Stop()
        {
            lisimbaApplication.Exiting -= HandleLisimbaApplicationExiting;
        }

        private void HandleLisimbaApplicationExiting(object sender, CancelEventArgs e)
        {
            userInterface.Exit();
        }
    }
}