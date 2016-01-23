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
using DustInTheWind.Lisimba.Cmd.Business;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Common.Config;
using DustInTheWind.Lisimba.Common.GateManagement;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace DustInTheWind.Lisimba.Cmd
{
    static class DependencyContainerSetup
    {
        public static UnityContainer CreateContainer()
        {
            UnityContainer container = new UnityContainer();

            //LoadGates(container);
            LoadFromConfigurationFile(container);
            RegisterAdditionalTypes(container);

            return container;
        }

        //private static void LoadGates(IUnityContainer container)
        //{
        //    string applicationDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        //    string gateDirectory = Path.Combine(applicationDirectory, "Gates");

        //    if (!Directory.Exists(gateDirectory))
        //        return;

        //    string[] assemblyPaths = Directory.GetFiles(gateDirectory, "*.dll");

        //    IEnumerable<Assembly> assemblies = assemblyPaths
        //        .Select(Assembly.Load);

        //    foreach (Assembly assembly in assemblies)
        //    {
        //        IEnumerable<Type> gateTypes = assembly.GetExportedTypes()
        //            .Where(t => typeof(IGate).IsAssignableFrom(t));

        //        foreach (Type gateType in gateTypes)
        //        {
        //            IGate gate = (IGate)Activator.CreateInstanceFrom(assembly.Location, gateType.FullName).Unwrap();

        //            container.RegisterType(typeof(IGate), gateType, gate.Id);
        //        }
        //    }
        //}

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

            container.RegisterType<ApplicationBackEnd>(new ContainerControlledLifetimeManager());
            container.RegisterType<OpenedAddressBooks>(new ContainerControlledLifetimeManager());
            container.RegisterType<ApplicationConfiguration>(new ContainerControlledLifetimeManager());
            container.RegisterType<AvailableGates>(new ContainerControlledLifetimeManager());
            container.RegisterType<UserInterface>(new ContainerControlledLifetimeManager());
            // RecentFiles?

            container.RegisterType<IApplicationConfiguration, ApplicationConfiguration>();
        }
    }
}
