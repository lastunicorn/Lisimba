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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.ArgumentsManagement;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Services;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Setup
{
    static class DependencyContainerSetup
    {
        public static UnityContainer CreateContainer()
        {
            UnityContainer unityContainer = new UnityContainer();

            LoadGates(unityContainer);
            RegisterAdditionalTypes(unityContainer);

            return unityContainer;
        }

        private static void LoadGates(IUnityContainer container)
        {
            string applicationDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string gateDirectory = Path.Combine(applicationDirectory, "Gates");

            if (!Directory.Exists(gateDirectory))
                return;

            string[] assemblyPaths = Directory.GetFiles(gateDirectory, "*.dll");

            IEnumerable<Assembly> assemblies = assemblyPaths
                .Select(Assembly.LoadFrom);

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> gateTypes = assembly.GetExportedTypes()
                    .Where(x => x.IsClass && typeof(IGate).IsAssignableFrom(x));

                foreach (Type gateType in gateTypes)
                {
                    IGate gate = (IGate)Activator.CreateInstanceFrom(assembly.Location, gateType.FullName).Unwrap();

                    container.RegisterType(typeof(IGate), gateType, gate.Id);
                }
            }
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
            container.RegisterType<ApplicationBackEnd>(new ContainerControlledLifetimeManager());
            container.RegisterType<ActiveObservers>(new ContainerControlledLifetimeManager());
            container.RegisterType<AvailableOperations>(new ContainerControlledLifetimeManager());
            container.RegisterType<AvailableGates>(new ContainerControlledLifetimeManager());

            container.RegisterType<IApplicationConfiguration, ApplicationConfiguration>();
        }
    }
}