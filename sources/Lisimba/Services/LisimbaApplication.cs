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
        private readonly RecentFilesService recentFilesService;
        private readonly CommandPool commandPool;
        private readonly UiService uiService;
        private readonly ApplicationService applicationService;
        private readonly CurrentData currentData;

        public LisimbaApplication(StatusService statusService, ProgramArguments programArguments, ConfigurationService configurationService,
            RecentFilesService recentFilesService, CommandPool commandPool, UiService uiService, ApplicationService applicationService,
            CurrentData currentData)
        {
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (recentFilesService == null) throw new ArgumentNullException("recentFilesService");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (uiService == null) throw new ArgumentNullException("uiService");
            if (applicationService == null) throw new ArgumentNullException("applicationService");
            if (currentData == null) throw new ArgumentNullException("currentData");

            this.statusService = statusService;
            this.programArguments = programArguments;
            this.configurationService = configurationService;
            this.recentFilesService = recentFilesService;
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
                    return recentFilesService.GetMostRecentFileName();

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
