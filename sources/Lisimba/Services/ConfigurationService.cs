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

using System.Configuration;
using DustInTheWind.Lisimba.Config;
using System.Linq;
using DustInTheWind.Lisimba.Common;

namespace DustInTheWind.Lisimba.Services
{
    internal class ConfigurationService : IApplicationConfiguration
    {
        private Configuration config;

        public LisimbaConfigSection LisimbaConfigSection { get; private set; }

        public ConfigurationService()
        {
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                OpenConfigurationFile();
                ReadLisimbaSection();
            }
            catch
            {
                LisimbaConfigSection = new LisimbaConfigSection();
            }
        }

        private void OpenConfigurationFile()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        private void ReadLisimbaSection()
        {
            LisimbaConfigSection = config.GetSection("lisimba") as LisimbaConfigSection;

            if (LisimbaConfigSection == null)
            {
                LisimbaConfigSection = new LisimbaConfigSection();
                config.Sections.Add("lisimba", LisimbaConfigSection);
                LisimbaConfigSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);
            }
        }

        public void Save()
        {
            config.Save(ConfigurationSaveMode.Full);
        }

        public string DefaultGateName
        {
            get { return LisimbaConfigSection.Gates.Default; }
        }

        public AddressBookLocationInfo LastAddressBook
        {
            get
            {
                RecentFilesConfigElementCollection recentFiles = LisimbaConfigSection.RecentFilesList;

                if (recentFiles.Count == 0)
                    return null;

                RecentFilesConfigElement lastFile = recentFiles[0];

                return new AddressBookLocationInfo
                {
                    FileName = lastFile.FileName,
                    GateId = null
                };
            }
            set
            {
                LisimbaConfigSection.RecentFilesList.AddNewRecentFile(value.FileName);
                config.Save();
            }
        }

        public AddressBookLocationInfo[] RecentFilesList
        {
            get
            {
                return LisimbaConfigSection.RecentFilesList
                    .Cast<RecentFilesConfigElement>()
                    .Select(x => new AddressBookLocationInfo
                    {
                        FileName = x.FileName,
                        GateId = null
                    })
                    .ToArray();
            }
        }
    }
}