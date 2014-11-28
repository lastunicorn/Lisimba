// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Services
{
    class LisimbaApplication
    {
        private readonly StatusService statusService;
        private readonly ProgramArguments programArguments;
        private readonly ConfigurationService configurationService;
        private readonly RecentFiles recentFiles;
        private readonly CommandPool commandPool;
        private readonly UiService uiService;
        private readonly ApplicationService applicationService;
        private readonly CurrentData currentData;

        public LisimbaApplication(StatusService statusService, ProgramArguments programArguments, ConfigurationService configurationService,
            RecentFiles recentFiles, CommandPool commandPool, UiService uiService, ApplicationService applicationService,
            CurrentData currentData)
        {
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (uiService == null) throw new ArgumentNullException("uiService");
            if (applicationService == null) throw new ArgumentNullException("applicationService");
            if (currentData == null) throw new ArgumentNullException("currentData");

            this.statusService = statusService;
            this.programArguments = programArguments;
            this.configurationService = configurationService;
            this.recentFiles = recentFiles;
            this.commandPool = commandPool;
            this.uiService = uiService;
            this.applicationService = applicationService;
            this.currentData = currentData;

            commandPool.OpenAddressBookCommand.AskIfAllowToContinue = AskToSave;
            commandPool.ImportYahooCsvCommand.AskIfAllowToContinue = AskToSave;

            applicationService.Exiting += HandleApplicationExiting;
        }

        private void HandleApplicationExiting(object sender, CancelEventArgs e)
        {
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                e.Cancel = true;
        }

        public void Start()
        {
            statusService.DefaultStatusText = "Ready";

            string fileNameToOpenAtLoad = CalculateFileNameToInitiallyOpen();

            if (string.IsNullOrWhiteSpace(fileNameToOpenAtLoad))
                commandPool.CreateNewAddressBookCommand.Execute();
            else
                commandPool.OpenAddressBookCommand.Execute(fileNameToOpenAtLoad);
        }

        private string CalculateFileNameToInitiallyOpen()
        {
            if (!string.IsNullOrEmpty(programArguments.FileName))
                return programArguments.FileName;

            switch (configurationService.LisimbaConfigSection.LoadFileAtStart.Type)
            {
                case "new":
                    return null;

                case "last":
                    return recentFiles.GetMostRecentFileName();

                case "specified":
                    return configurationService.LisimbaConfigSection.LoadFileAtStart.FileName;

                default:
                    return null;
            }
        }

        private bool AskToSave()
        {
            if (currentData.AddressBook == null || currentData.AddressBook.Status == AddressBookStatus.Saved)
                return true;

            bool? response = uiService.DisplayYesNoQuestion("Current address book is not saved.\nDo you wanna save it before proceedeing?", "Save?");

            if (response == null)
                return false;

            if (response.Value)
                commandPool.SaveAddressBookCommand.Execute();

            return true;
        }
    }
}
