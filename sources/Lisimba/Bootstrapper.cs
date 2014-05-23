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

using System.Windows.Forms;
using DustInTheWind.Lisimba.Commands;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba
{
    class Bootstrapper
    {
        private UnityContainer unityContainer;

        public void Run(string[] args)
        {
            unityContainer = CreateUnityContainer();
            InitializeProgramArgumentsService(args);
            StartApplicationMainService();
            DisplayMainWindow();
        }

        private static UnityContainer CreateUnityContainer()
        {
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<ProgramArguments>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ConfigurationService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<StatusService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<RecentFilesService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<CurrentData>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ApplicationService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<UiService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<LisimbaApplication>(new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<OpenAddressBookCommand>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ImportYahooCsvCommand>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ExportYahooCsvCommand>(new ContainerControlledLifetimeManager());

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

        private void DisplayMainWindow()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormLisimba formLisimba = unityContainer.Resolve<FormLisimba>();
            UiService uiService = unityContainer.Resolve<UiService>();
            uiService.MainWindow = formLisimba;

            Application.Run(formLisimba);
        }
    }
}
