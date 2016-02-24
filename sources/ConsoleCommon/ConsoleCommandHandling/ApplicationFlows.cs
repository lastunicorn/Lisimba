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
    public class ApplicationFlows : IEnumerable<KeyValuePair<string, IFlow>>
    {
        private readonly IFlowProvider flowProvider;
        private readonly Dictionary<string, IFlow> flows;
        private IFlow unknownFlow;

        public ApplicationFlows(IFlowProvider flowProvider)
        {
            if (flowProvider == null) throw new ArgumentNullException("flowProvider");

            this.flowProvider = flowProvider;

            flows = new Dictionary<string, IFlow>();
        }

        public void Initialize()
        {
            foreach (Tuple<string, IFlow> keyValuePair in flowProvider.GetNewFlows())
            {
                string name = keyValuePair.Item1;

                if (flows.ContainsKey(name))
                    throw new ApplicationException("Another flow with name '" + name + "' already exists.");

                flows.Add(name, keyValuePair.Item2);
            }

            unknownFlow = flowProvider.GetNewUnknownFlow();
        }

        public IEnumerator<KeyValuePair<string, IFlow>> GetEnumerator()
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

        public IFlow GetFlow(string commandName)
        {
            bool existsFlow = flows.ContainsKey(commandName);

            return existsFlow
                ? flows[commandName]
                : unknownFlow;
        }
    }
}