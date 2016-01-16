﻿// Lisimba
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

using System.Linq;
using DustInTheWind.Lisimba.Common.ConfigSection;

namespace DustInTheWind.Lisimba.Common.Config
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private readonly ConfigurationFile configurationFile;

        public ApplicationConfiguration()
        {
            configurationFile = new ConfigurationFile();
        }

        public string DefaultGateName
        {
            get { return configurationFile.LisimbaConfigSection.Gates.Default; }
        }

        public AddressBookLocationInfo LastAddressBook
        {
            get
            {
                RecentFilesConfigElementCollection recentFiles = configurationFile.LisimbaConfigSection.RecentFilesList;

                if (recentFiles.Count == 0)
                    return null;

                RecentFilesConfigElement lastFile = recentFiles[0];

                return new AddressBookLocationInfo
                {
                    FileName = lastFile.FileName,
                    GateId = lastFile.Gate
                };
            }
            set
            {
                configurationFile.LisimbaConfigSection.RecentFilesList.AddNewRecentFile(value.FileName, value.GateId);
                configurationFile.Save();
            }
        }

        public AddressBookLocationInfo[] RecentFilesList
        {
            get
            {
                return configurationFile.LisimbaConfigSection.RecentFilesList
                    .Cast<RecentFilesConfigElement>()
                    .Select(x => new AddressBookLocationInfo
                    {
                        FileName = x.FileName,
                        GateId = null
                    })
                    .ToArray();
            }
        }

        public string LoadFileAtStart
        {
            get { return configurationFile.LisimbaConfigSection.LoadFileAtStart.Type; }
        }

        public AddressBookLocationInfo FileToLoadAtStart
        {
            get { return new AddressBookLocationInfo(configurationFile.LisimbaConfigSection.LoadFileAtStart.FileName); }
        }

        public string DefaultContactSort
        {
            get { return configurationFile.LisimbaConfigSection.SortBy.Value; }
        }
    }
}