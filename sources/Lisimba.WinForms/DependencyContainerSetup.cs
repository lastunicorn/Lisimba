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
using System.IO;
using System.Reflection;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Common.Config;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using LisimbaApplication = DustInTheWind.Lisimba.Services.LisimbaApplication;

namespace DustInTheWind.Lisimba
{
    static class DependencyContainerSetup
    {
        public static UnityContainer CreateContainer()
        {
            UnityContainer unityContainer = new UnityContainer();

            LoadFromConfigurationFile(unityContainer);
            RegisterAdditionalTypes(unityContainer);

            return unityContainer;
        }

        private static void LoadFromConfigurationFile(IUnityContainer container)
        {
            UnityConfigurationSection unitySection = GetUnityConfigurationSection();
            container.LoadConfiguration(unitySection);
        }

        private static UnityConfigurationSection GetUnityConfigurationSection()
        {
            string unityConfigFilePath = GetUnityConfigFilePath();

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = unityConfigFilePath };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return (UnityConfigurationSection)configuration.GetSection("unity");
        }

        private static string GetUnityConfigFilePath()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string applicationDirectory = Path.GetDirectoryName(entryAssembly.Location);
            return Path.Combine(applicationDirectory, "Unity.config");
        }

        private static void RegisterAdditionalTypes(UnityContainer container)
        {
            container.RegisterInstance(container);

            container.RegisterType<ProgramArguments>(new ContainerControlledLifetimeManager());
            container.RegisterType<ApplicationConfiguration>(new ContainerControlledLifetimeManager());
            container.RegisterType<ApplicationStatus>(new ContainerControlledLifetimeManager());
            container.RegisterType<RecentFiles>(new ContainerControlledLifetimeManager());
            container.RegisterType<OpenedAddressBooks>(new ContainerControlledLifetimeManager());
            container.RegisterType<UserInterface>(new ContainerControlledLifetimeManager());
            container.RegisterType<LisimbaApplication>(new ContainerControlledLifetimeManager());

            container.RegisterType<OpenAddressBookOperation>(new ContainerControlledLifetimeManager());

            container.RegisterType<IApplicationConfiguration, ApplicationConfiguration>();
        }
    }
}