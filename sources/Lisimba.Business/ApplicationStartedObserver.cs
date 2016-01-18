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
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Common.Config;
using DustInTheWind.Lisimba.Common.GateManagement;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba.Common
{
    public class ApplicationStartedObserver : IObserver
    {
        private readonly LisimbaApplication lisimbaApplication;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly ProgramArguments programArguments;
        private readonly IApplicationConfiguration applicationConfiguration;
        private readonly RecentFiles recentFiles;
        private readonly AvailableGates availableGates;

        public ApplicationStartedObserver(LisimbaApplication lisimbaApplication, OpenedAddressBooks openedAddressBooks, ProgramArguments programArguments,
            IApplicationConfiguration applicationConfiguration, RecentFiles recentFiles, AvailableGates availableGates)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.lisimbaApplication = lisimbaApplication;
            this.openedAddressBooks = openedAddressBooks;
            this.programArguments = programArguments;
            this.applicationConfiguration = applicationConfiguration;
            this.recentFiles = recentFiles;
            this.availableGates = availableGates;
        }

        public void Start()
        {
            lisimbaApplication.Started += HandleLisimbaApplicationStarted;
        }

        public void Stop()
        {
            lisimbaApplication.Started -= HandleLisimbaApplicationStarted;
        }

        private void HandleLisimbaApplicationStarted(object sender, EventArgs eventArgs)
        {
            OpenInitialCatalog();
        }

        private void OpenInitialCatalog()
        {
            AddressBookLocationInfo fileNameToOpenAtLoad = GetFileInfoToInitiallyOpen();

            if (fileNameToOpenAtLoad == null)
            {
                openedAddressBooks.CreateNewAddressBook(null);
            }
            else
            {
                if (fileNameToOpenAtLoad.GateId == null)
                {
                    string message = string.Format("No gate is associated with address book '{0}'.", fileNameToOpenAtLoad.FileName);
                    throw new LisimbaException(message);
                }

                IGate gate = availableGates.GetGate(fileNameToOpenAtLoad.GateId);
                openedAddressBooks.OpenAddressBook(fileNameToOpenAtLoad.FileName, gate);
            }
        }

        private AddressBookLocationInfo GetFileInfoToInitiallyOpen()
        {
            if (!string.IsNullOrEmpty(programArguments.FileName))
                return new AddressBookLocationInfo
                {
                    FileName = programArguments.FileName,
                    GateId = availableGates.DefaultGate.Id
                };

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