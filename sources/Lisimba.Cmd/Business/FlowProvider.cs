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

using System;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Cmd.Flows;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    internal class FlowProvider
    {
        private readonly UnityContainer unityContainer;

        public FlowProvider(UnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");
            this.unityContainer = unityContainer;
        }

        public IFlow CreateFlow(Command command)
        {
            var dependencyOverride = new DependencyOverride(typeof (Command), command);

            switch (command.Name)
            {
                case "new":
                    return unityContainer.Resolve<NewFlow>(dependencyOverride);

                case "update":
                    return unityContainer.Resolve<UpdateFlow>(dependencyOverride);

                case "open":
                    return unityContainer.Resolve<OpenFlow>(dependencyOverride);

                case "save":
                    return unityContainer.Resolve<SaveFlow>(dependencyOverride);

                case "show":
                    return unityContainer.Resolve<ShowFlow>(dependencyOverride);

                case "next-birthdays":
                    return unityContainer.Resolve<NextBirthdaysFlow>(dependencyOverride);

                case "close":
                    return unityContainer.Resolve<CloseFlow>(dependencyOverride);

                case "info":
                    return unityContainer.Resolve<InfoFlow>(dependencyOverride);

                case "gate":
                    return unityContainer.Resolve<GateFlow>(dependencyOverride);

                case "exit":
                case "bye":
                case "goodbye":
                    return unityContainer.Resolve<ExitFlow>(dependencyOverride);

                case "":
                    return unityContainer.Resolve<EmptyFlow>(dependencyOverride);

                default:
                    return unityContainer.Resolve<UnknownFlow>(dependencyOverride);
            }
        }
    }
}