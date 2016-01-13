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
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Observers
{
    class ApplicationStartedObserver : IObserver
    {
        private readonly LisimbaApplication lisimbaApplication;
        private readonly CommandPool commandPool;
        private readonly ProgramArguments programArguments;
        private readonly ApplicationConfiguration applicationConfiguration;
        private readonly RecentFiles recentFiles;

        public ApplicationStartedObserver(LisimbaApplication lisimbaApplication, CommandPool commandPool,
            ProgramArguments programArguments, ApplicationConfiguration applicationConfiguration, RecentFiles recentFiles)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.lisimbaApplication = lisimbaApplication;
            this.commandPool = commandPool;
            this.programArguments = programArguments;
            this.applicationConfiguration = applicationConfiguration;
            this.recentFiles = recentFiles;
        }

        public void Start()
        {
            lisimbaApplication.Started += HandleLisimbaApplicationStarted;
        }

        private void HandleLisimbaApplicationStarted(object sender, EventArgs eventArgs)
        {
            OpenInitialCatalog();
        }

        private void OpenInitialCatalog()
        {
            string fileNameToOpenAtLoad = CalculateInitiallyOpenedFileName();

            if (string.IsNullOrWhiteSpace(fileNameToOpenAtLoad))
                commandPool.NewAddressBookOperation.Execute();
            else
                commandPool.OpenAddressBookOperation.Execute(fileNameToOpenAtLoad);
        }

        private string CalculateInitiallyOpenedFileName()
        {
            if (!string.IsNullOrEmpty(programArguments.FileName))
                return programArguments.FileName;

            switch (applicationConfiguration.LoadFileAtStart)
            {
                case "new":
                    return null;

                case "last":
                    return recentFiles.GetMostRecentFile().FileName;

                case "specified":
                    return applicationConfiguration.FileToLoadAtStart.FileName;

                default:
                    return null;
            }
        }
    }
}