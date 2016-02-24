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

using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.ArgumentsManagement;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.WinFormsCommon.Operations;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Setup
{
    internal static class DependencyContainerSetup
    {
        public static UnityContainer CreateContainer()
        {
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(unityContainer);

            unityContainer.RegisterType<LisimbaApplication>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ProgramArguments>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<OpenedAddressBooks>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<AvailableGates>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<AvailableOperations>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ActiveObservers>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ApplicationStatus>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<RecentFiles>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<WindowSystem>(new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<IApplicationConfiguration, ApplicationConfiguration>();
            unityContainer.RegisterType<IObserverProvider, ObserverProvider>();
            unityContainer.RegisterType<IOperationProvider, OperationProvider>();
            unityContainer.RegisterType<IUserInterface, UserInterface>(new ContainerControlledLifetimeManager());

            return unityContainer;
        }
    }
}