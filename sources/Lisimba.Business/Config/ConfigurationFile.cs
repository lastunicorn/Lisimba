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

using System.Configuration;
using DustInTheWind.Lisimba.Common.ConfigSection;

namespace DustInTheWind.Lisimba.Common.Config
{
    internal class ConfigurationFile
    {
        private Configuration config;
        private LisimbaConfigSection lisimbaConfigSection;

        public LisimbaConfigSection LisimbaConfigSection
        {
            get
            {
                if (lisimbaConfigSection == null)
                    Initialize();

                return lisimbaConfigSection;
            }
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
                lisimbaConfigSection = new LisimbaConfigSection();
            }
            config.Save(ConfigurationSaveMode.Full);
        }

        private void OpenConfigurationFile()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
        }

        private void ReadLisimbaSection()
        {
            lisimbaConfigSection = config.GetSection("lisimba") as LisimbaConfigSection;

            if (lisimbaConfigSection == null)
            {
                lisimbaConfigSection = new LisimbaConfigSection();
                config.Sections.Add("lisimba", lisimbaConfigSection);
                lisimbaConfigSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);
            }
        }

        public void Save()
        {
            config.Save(ConfigurationSaveMode.Full);
        }
    }
}