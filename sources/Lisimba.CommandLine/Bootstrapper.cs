﻿// Lisimba
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
using DustInTheWind.Lisimba.Business.ArgumentsManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.CommandLine.Setup;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.CommandLine
{
    internal class Bootstrapper
    {
        private UnityContainer unityContainer;

        public void Run(string[] args)
        {
            unityContainer = DependencyContainerSetup.CreateContainer();

            InitializeProgramArguments(args);

            ConfigureGates();
            ConfigureObservers();
            ConfigureApplicationFlows();

            StartApplication();
        }

        private void InitializeProgramArguments(string[] args)
        {
            ProgramArguments programArguments = unityContainer.Resolve<ProgramArguments>();
            programArguments.Initialize(args);
        }

        private void ConfigureGates()
        {
            AvailableGates availableGates = unityContainer.Resolve<AvailableGates>();
            GatesSetup.Configure(availableGates, unityContainer);
        }

        private void ConfigureObservers()
        {
            ActiveObservers activeObservers = unityContainer.Resolve<ActiveObservers>();
            ObserversSetup.Configure(activeObservers, unityContainer);
        }

        private void ConfigureApplicationFlows()
        {
            ApplicationFlows applicationFlows = unityContainer.Resolve<ApplicationFlows>();
            FlowsSetup.Configure(applicationFlows);
        }

        private void StartApplication()
        {
            LisimbaApplication applicationBackEnd = unityContainer.Resolve<LisimbaApplication>();
            applicationBackEnd.Start();
        }
    }
}