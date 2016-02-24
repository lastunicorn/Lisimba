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
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleCommon.ConsoleCommandHandling
{
    public class ApplicationFlows : IEnumerable<KeyValuePair<string, Type>>
    {
        private readonly IFlowFactory flowFactory;

        private readonly Dictionary<string, Type> flows;

        public ApplicationFlows(IFlowProvider flowProvider, IFlowFactory flowFactory)
        {
            if (flowProvider == null) throw new ArgumentNullException("flowProvider");
            if (flowFactory == null) throw new ArgumentNullException("flowFactory");

            this.flowFactory = flowFactory;

            flows = new Dictionary<string, Type>();

            foreach (Tuple<string, Type> keyValuePair in flowProvider.GetNewFlows())
                AddFlow(keyValuePair.Item1, keyValuePair.Item2);
        }

        private void AddFlow(string name, Type flowType)
        {
            if (flows.ContainsKey(name))
                throw new ApplicationException("Another flow with name '" + name + "' already exists.");

            if (!typeof(IFlow).IsAssignableFrom(flowType))
            {
                string message = string.Format("Type {0} does not inherit {1}.", flowType.FullName, typeof(IFlow).FullName);
                throw new ApplicationException(message);
            }

            flows.Add(name, flowType);
        }

        public IEnumerator<KeyValuePair<string, Type>> GetEnumerator()
        {
            return flows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainsFlow(string name)
        {
            return flows.ContainsKey(name);
        }

        public IFlow CreateFlow(ConsoleCommand consoleCommand)
        {
            bool existsFlow = flows.ContainsKey(consoleCommand.Name);
            if (!existsFlow)
                return flowFactory.CreateUnknownFlow(consoleCommand);

            Type flowType = flows[consoleCommand.Name];
            return flowFactory.CreateFlow(flowType, consoleCommand);
        }
    }
}