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
using System.Reflection;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.BookShell;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.Services
{
    class LisimbaApplication
    {
        private readonly ApplicationStatus applicationStatus;
        private readonly ProgramArguments programArguments;
        private readonly ConfigurationService configurationService;
        private readonly RecentFiles recentFiles;
        private readonly CommandPool commandPool;
        private readonly UiService uiService;
        private readonly AddressBookShell addressBookShell;

        public string ProgramName { get; private set; }

        public LisimbaApplication(ApplicationStatus applicationStatus, ProgramArguments programArguments, ConfigurationService configurationService,
            RecentFiles recentFiles, CommandPool commandPool, UiService uiService, ApplicationService applicationService,
            AddressBookShell addressBookShell)
        {
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (uiService == null) throw new ArgumentNullException("uiService");
            if (applicationService == null) throw new ArgumentNullException("applicationService");
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");

            this.applicationStatus = applicationStatus;
            this.programArguments = programArguments;
            this.configurationService = configurationService;
            this.recentFiles = recentFiles;
            this.commandPool = commandPool;
            this.uiService = uiService;
            this.addressBookShell = addressBookShell;

            ProgramName = GetProgramName();

            commandPool.OpenAddressBookCommand.AskIfAllowToContinue = addressBookShell.EnsureIsSaved;
            commandPool.ImportYahooCsvCommand.AskIfAllowToContinue = addressBookShell.EnsureIsSaved;

            applicationService.Exiting += HandleApplicationExiting;
        }

        private static string GetProgramName()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = executingAssembly.GetName();

            return string.Format("{0} {1}", Application.ProductName, assemblyName.Version.ToString(2));
        }

        private void HandleApplicationExiting(object sender, CancelEventArgs e)
        {
            bool allowToContinue = addressBookShell.EnsureIsSaved();

            if (!allowToContinue)
                e.Cancel = true;
        }

        public void Start()
        {
            applicationStatus.DefaultStatusText = Resources.DefaultStatusText;

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
    }
}
