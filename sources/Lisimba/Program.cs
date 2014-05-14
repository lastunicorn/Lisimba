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

using System;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<ConfigurationService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<StatusService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<RecentFilesService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<CurrentData>(new ContainerControlledLifetimeManager());

            try
            {
                ProgramArguments programArguments = new ProgramArguments(args);
                unityContainer.RegisterInstance(programArguments, new ContainerControlledLifetimeManager());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            unityContainer.Resolve<StatusService>().DefaultStatusText = "Ready";

            FormLisimba formLisimba = unityContainer.Resolve<FormLisimba>();

            UIService uiService = new UIService(formLisimba);
            unityContainer.RegisterInstance(uiService, new ContainerControlledLifetimeManager());

            Application.Run(formLisimba);
        }
    }
}