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
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.Cmd.Observers
{
    class ApplicationStartedObserver : IObserver
    {
        private readonly ApplicationStartedObserverConsole console;
        private readonly LisimbaApplication lisimbaApplication;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly IApplicationConfiguration applicationConfiguration;
        private readonly RecentFiles recentFiles;
        private readonly AvailableGates availableGates;

        public ApplicationStartedObserver(ApplicationStartedObserverConsole console, LisimbaApplication lisimbaApplication, OpenedAddressBooks openedAddressBooks,
            IApplicationConfiguration applicationConfiguration, RecentFiles recentFiles, AvailableGates availableGates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.console = console;
            this.lisimbaApplication = lisimbaApplication;
            this.openedAddressBooks = openedAddressBooks;
            this.applicationConfiguration = applicationConfiguration;
            this.recentFiles = recentFiles;
            this.availableGates = availableGates;
        }

        public void Start()
        {
            lisimbaApplication.Started += HandleLisimbaApplicationStarted;
        }

        private void HandleLisimbaApplicationStarted(object sender, EventArgs eventArgs)
        {
            console.WriteWelcomeMessage();
            console.WriteGateInfo(availableGates.DefaultGateName);

            OpenInitialCatalog();
        }

        private void OpenInitialCatalog()
        {
            AddressBookLocationInfo fileNameToOpenAtLoad = CalculateInitiallyOpenedFileName();

            if (fileNameToOpenAtLoad == null)
            {
                openedAddressBooks.CreateNewAddressBook(null);
            }
            else
            {
                IGate gate = availableGates.GetGate(fileNameToOpenAtLoad.GateId);
                openedAddressBooks.OpenAddressBook(fileNameToOpenAtLoad.FileName, gate);
            }
        }

        private AddressBookLocationInfo CalculateInitiallyOpenedFileName()
        {
            switch (applicationConfiguration.LoadFileAtStart)
            {
                case "new":
                    return null;

                case "last":
                    return recentFiles.GetMostRecentFile();

                case "specified":
                    return applicationConfiguration.FileToLoadAtStart;

                default:
                    return null;
            }
        }
    }
}