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
using DustInTheWind.ConsoleCommon.CommandModel;
using DustInTheWind.Lisimba.Cmd.Flows;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    internal class FlowFactory
    {
        private readonly UnityContainer unityContainer;

        public FlowFactory(UnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");
            this.unityContainer = unityContainer;
        }

        public IFlow CreateFlow(ConsoleCommand consoleCommand)
        {
            var dependencyOverride = new DependencyOverride(typeof(ConsoleCommand), consoleCommand);

            bool existsFlow = ApplicationFlows.Flows.ContainsKey(consoleCommand.Name);
            if (!existsFlow)
                return unityContainer.Resolve<UnknownFlow>(dependencyOverride);

            Type flowType = ApplicationFlows.Flows[consoleCommand.Name];
            return (IFlow)unityContainer.Resolve(flowType, dependencyOverride);
        }
    }
}