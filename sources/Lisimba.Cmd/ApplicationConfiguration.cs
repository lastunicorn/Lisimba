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
using System.Reflection;

namespace DustInTheWind.Lisimba.Cmd
{
    internal class ApplicationConfiguration
    {
        private readonly Configuration config;

        public ApplicationConfiguration()
        {
            string appFilePath = Assembly.GetEntryAssembly().Location;
            string configFilePath = appFilePath + ".config";
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFilePath };
            config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }

        public string DefaultGateName
        {
            get { return config.AppSettings.Settings["DefaultGate"].Value; }
        }

        public AddressBookLocationInfo LastAddressBook
        {
            get
            {
                string text = config.AppSettings.Settings["LastAddressBook"].Value;

                if (text == null)
                    return null;

                return new AddressBookLocationInfo(text);
            }
            set
            {
                config.AppSettings.Settings["LastAddressBook"].Value = value.ToString();
                config.Save();
            }
        }
    }
}