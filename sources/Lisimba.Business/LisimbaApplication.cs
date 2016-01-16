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
    public class LisimbaApplication
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly IApplicationConfiguration applicationConfiguration;
        private readonly RecentFiles recentFiles;
        private readonly AvailableGates availableGates;

        public event EventHandler Starting;
        public event EventHandler Started;
        public event EventHandler Ended;

        public LisimbaApplication(OpenedAddressBooks openedAddressBooks,
            IApplicationConfiguration applicationConfiguration, RecentFiles recentFiles, AvailableGates availableGates)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.openedAddressBooks = openedAddressBooks;
            this.applicationConfiguration = applicationConfiguration;
            this.recentFiles = recentFiles;
            this.availableGates = availableGates;
        }

        public void Start()
        {
            OnStarting();

            OpenInitialCatalog();

            OnStarted();
        }

        public void Exit()
        {
            OnEnded();
        }

        protected virtual void OnStarting()
        {
            EventHandler handler = Starting;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnEnded()
        {
            EventHandler handler = Ended;

            if (handler != null)
                handler(this, EventArgs.Empty);
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