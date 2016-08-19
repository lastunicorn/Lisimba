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

using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.ArgumentsManagement;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.ObservingModel;
using DustInTheWind.Lisimba.CommandLine.Business;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.CommandLine.Setup
{
    internal static class DependencyContainerSetup
    {
        public static UnityContainer CreateContainer()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterInstance(container);

            container.RegisterType<ProgramArguments>(new ContainerControlledLifetimeManager());
            container.RegisterType<LisimbaApplication>(new ContainerControlledLifetimeManager());
            container.RegisterType<OpenedAddressBooks>(new ContainerControlledLifetimeManager());
            container.RegisterType<ApplicationConfiguration>(new ContainerControlledLifetimeManager());
            container.RegisterType<Gates>(new ContainerControlledLifetimeManager());
            container.RegisterType<ActiveObservers>(new ContainerControlledLifetimeManager());
            container.RegisterType<ApplicationFlows>(new ContainerControlledLifetimeManager());

            container.RegisterType<IApplicationConfiguration, ApplicationConfiguration>();
            container.RegisterType<IFlowProvider, FlowProvider>();
            container.RegisterType<IObserverProvider, ObserverProvider>();
            container.RegisterType<IPrompterTextProvider, PrompterTextProvider>();
            container.RegisterType<IUserInterface, UserInterface>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}