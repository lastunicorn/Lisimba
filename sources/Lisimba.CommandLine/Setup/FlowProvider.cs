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
using System.Collections.Generic;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.CommandLine.Flows;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.CommandLine.Setup
{
    internal class FlowProvider : IFlowProvider
    {
        private readonly IUnityContainer unityContainer;

        public FlowProvider(IUnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");
            this.unityContainer = unityContainer;
        }

        public IEnumerable<Tuple<string, IFlow>> GetNewFlows()
        {
            yield return new Tuple<string, IFlow>("new", unityContainer.Resolve<NewFlow>());
            yield return new Tuple<string, IFlow>("update", unityContainer.Resolve<UpdateFlow>());
            yield return new Tuple<string, IFlow>("open", unityContainer.Resolve<OpenFlow>());
            yield return new Tuple<string, IFlow>("save", unityContainer.Resolve<SaveFlow>());
            yield return new Tuple<string, IFlow>("show", unityContainer.Resolve<ShowFlow>());
            yield return new Tuple<string, IFlow>("next-birthdays", unityContainer.Resolve<NextBirthdaysFlow>());
            yield return new Tuple<string, IFlow>("close", unityContainer.Resolve<CloseFlow>());
            yield return new Tuple<string, IFlow>("info", unityContainer.Resolve<InfoFlow>());
            yield return new Tuple<string, IFlow>("gate", unityContainer.Resolve<GateFlow>());
            yield return new Tuple<string, IFlow>("gates", unityContainer.Resolve<GatesFlow>());
            yield return new Tuple<string, IFlow>("lang", unityContainer.Resolve<LangFlow>());
            yield return new Tuple<string, IFlow>("compare", unityContainer.Resolve<CompareFlow>());
            yield return new Tuple<string, IFlow>("import", unityContainer.Resolve<ImportFlow>());
            yield return new Tuple<string, IFlow>("exit", unityContainer.Resolve<ExitFlow>());
            yield return new Tuple<string, IFlow>("bye", unityContainer.Resolve<ExitFlow>());
            yield return new Tuple<string, IFlow>("goodbye", unityContainer.Resolve<ExitFlow>());
            yield return new Tuple<string, IFlow>("help", unityContainer.Resolve<HelpFlow>());
            yield return new Tuple<string, IFlow>("version", unityContainer.Resolve<VersionFlow>());
            yield return new Tuple<string, IFlow>("ver", unityContainer.Resolve<VersionFlow>());
            yield return new Tuple<string, IFlow>("", unityContainer.Resolve<EmptyFlow>());
        }

        public IFlow GetNewUnknownFlow()
        {
            return unityContainer.Resolve<UnknownFlow>();
        }
    }
}