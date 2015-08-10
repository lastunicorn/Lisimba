// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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

using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.UserControls;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba
{
    internal class Bootstrapper
    {
        private UnityContainer unityContainer;

        public void Run(string[] args)
        {
            unityContainer = CreateUnityContainer();
            InitializeProgramArgumentsService(args);
            StartApplicationMainService();
            InitializeAndStartUi();
        }

        private static UnityContainer CreateUnityContainer()
        {
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<ProgramArguments>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ConfigurationService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ApplicationStatus>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<RecentFiles>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<AddressBookShell>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<UserInterface>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<LisimbaApplication>(new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<OpenAddressBookOperation>(new ContainerControlledLifetimeManager());
            //unityContainer.RegisterType<ImportYahooCsvOperation>(new ContainerControlledLifetimeManager());
            //unityContainer.RegisterType<ExportYahooCsvOperation>(new ContainerControlledLifetimeManager());


            return unityContainer;
        }

        private void InitializeProgramArgumentsService(string[] args)
        {
            ProgramArguments programArguments = unityContainer.Resolve<ProgramArguments>();
            programArguments.Initialize(args);
        }

        private void StartApplicationMainService()
        {
            LisimbaApplication lisimbaApplication = unityContainer.Resolve<LisimbaApplication>();
            lisimbaApplication.Start();
        }

        private void InitializeAndStartUi()
        {
            UserInterface userInterface = unityContainer.Resolve<UserInterface>();

            userInterface.RunAsTrayApp();
            //userInterface.RunAsWindowApp();
        }
    }
}