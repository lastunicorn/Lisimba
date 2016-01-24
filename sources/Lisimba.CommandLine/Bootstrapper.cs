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
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.ArgumentsManagement;
using DustInTheWind.Lisimba.CommandLine.Business;
using DustInTheWind.Lisimba.CommandLine.Setup;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.CommandLine
{
    internal class Bootstrapper
    {
        private UnityContainer unityContainer;
        private UserInterface userInterface;

        public void Run(string[] args)
        {
            unityContainer = DependencyContainerSetup.CreateContainer();

            ApplicationFlows applicationFlows = unityContainer.Resolve<ApplicationFlows>();
            FlowsSetup.Configure(applicationFlows);

            ActiveObservers activeObservers = unityContainer.Resolve<ActiveObservers>();
            ObserversSetup.Configure(activeObservers, unityContainer);

            InitializeProgramArguments(args);

            userInterface = CreatreAndInitializeUserInterface();

            StartBackEnd();

            GC.Collect();
            GC.Collect();
            GC.Collect();

            WaitForUserInput();
        }

        private void InitializeProgramArguments(string[] args)
        {
            ProgramArguments programArguments = unityContainer.Resolve<ProgramArguments>();
            programArguments.Initialize(args);
        }

        private UserInterface CreatreAndInitializeUserInterface()
        {
            UserInterface newUserInterface = unityContainer.Resolve<UserInterface>();
            newUserInterface.Initialize();

            return newUserInterface;
        }

        private void StartBackEnd()
        {
            ApplicationBackEnd applicationBackEnd = unityContainer.Resolve<ApplicationBackEnd>();
            applicationBackEnd.Start();
        }

        private void WaitForUserInput()
        {
            userInterface.Start();
        }
    }
}