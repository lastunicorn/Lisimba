// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.ArgumentsManagement;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;

namespace DustInTheWind.Lisimba.Business
{
    public class InitialCatalogOpener
    {
        private readonly AddressBooks addressBooks;
        private readonly ProgramArguments programArguments;
        private readonly IApplicationConfiguration applicationConfiguration;
        private readonly RecentFiles recentFiles;
        private readonly Gates gates;

        public InitialCatalogOpener(AddressBooks addressBooks, ProgramArguments programArguments,
            IApplicationConfiguration applicationConfiguration, RecentFiles recentFiles, Gates gates)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.addressBooks = addressBooks;
            this.programArguments = programArguments;
            this.applicationConfiguration = applicationConfiguration;
            this.recentFiles = recentFiles;
            this.gates = gates;
        }

        public void OpenInitialCatalog()
        {
            AddressBookLocationInfo fileNameToOpenAtLoad = GetFileInfoToInitiallyOpen();

            if (fileNameToOpenAtLoad == null)
            {
                addressBooks.CreateNewAddressBook(null);
            }
            else
            {
                if (fileNameToOpenAtLoad.GateId == null)
                {
                    string message = string.Format("No gate is associated with address book '{0}'.", fileNameToOpenAtLoad.FileName);
                    throw new LisimbaException(message);
                }

                IGate gate = gates.GetGate(fileNameToOpenAtLoad.GateId);
                addressBooks.OpenAddressBook(gate, fileNameToOpenAtLoad.FileName);
            }
        }

        private AddressBookLocationInfo GetFileInfoToInitiallyOpen()
        {
            if (!string.IsNullOrEmpty(programArguments.FileName))
                return new AddressBookLocationInfo
                {
                    FileName = programArguments.FileName,
                    GateId = gates.DefaultGate.Id
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