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
using DustInTheWind.ConsoleCommon.CommandModel;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.CommandLine.Flows;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.CommandLine.Business
{
    internal class FlowFactory : IFlowFactory
    {
        private readonly UnityContainer unityContainer;

        public FlowFactory(UnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");

            this.unityContainer = unityContainer;
        }

        public IFlow CreateUnknownFlow(ConsoleCommand consoleCommand)
        {
            var dependencyOverride = new DependencyOverride(typeof(ConsoleCommand), consoleCommand);
            return unityContainer.Resolve<UnknownFlow>(dependencyOverride);
        }

        public IFlow CreateFlow(Type flowType, ConsoleCommand consoleCommand)
        {
            if (!typeof(IFlow).IsAssignableFrom(flowType))
            {
                string message = string.Format("Type {0} does not inherit {1}.", flowType.FullName, typeof(IFlow).FullName);
                throw new LisimbaException(message);
            }

            var dependencyOverride = new DependencyOverride(typeof(ConsoleCommand), consoleCommand);
            return (IFlow)unityContainer.Resolve(flowType, dependencyOverride);
        }
    }
}