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

using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Common.Config;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba
{
    static class DependencyContainerSetup
    {
        public static UnityContainer CreateContainer()
        {
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<ProgramArguments>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ApplicationConfiguration>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ApplicationStatus>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<RecentFiles>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<OpenedAddressBooks>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<UserInterface>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<LisimbaApplication>(new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<OpenAddressBookOperation>(new ContainerControlledLifetimeManager());
            //unityContainer.RegisterType<ImportYahooCsvOperation>(new ContainerControlledLifetimeManager());
            //unityContainer.RegisterType<ExportYahooCsvOperation>(new ContainerControlledLifetimeManager());
            
            unityContainer.RegisterType<IApplicationConfiguration, ApplicationConfiguration>();
            
            return unityContainer;
        }
    }
}