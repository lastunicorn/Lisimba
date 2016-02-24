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

namespace DustInTheWind.Lisimba.CommandLine.Setup
{
    internal class FlowProvider : IFlowProvider
    {
        public IEnumerable<Tuple<string, Type>> GetNewFlows()
        {
            yield return new Tuple<string, Type>("new", typeof(NewFlow));
            yield return new Tuple<string, Type>("update", typeof(UpdateFlow));
            yield return new Tuple<string, Type>("open", typeof(OpenFlow));
            yield return new Tuple<string, Type>("save", typeof(SaveFlow));
            yield return new Tuple<string, Type>("show", typeof(ShowFlow));
            yield return new Tuple<string, Type>("next-birthdays", typeof(NextBirthdaysFlow));
            yield return new Tuple<string, Type>("close", typeof(CloseFlow));
            yield return new Tuple<string, Type>("info", typeof(InfoFlow));
            yield return new Tuple<string, Type>("gate", typeof(GateFlow));
            yield return new Tuple<string, Type>("gates", typeof(GatesFlow));
            yield return new Tuple<string, Type>("lang", typeof(LangFlow));
            yield return new Tuple<string, Type>("exit", typeof(ExitFlow));
            yield return new Tuple<string, Type>("bye", typeof(ExitFlow));
            yield return new Tuple<string, Type>("goodbye", typeof(ExitFlow));
            yield return new Tuple<string, Type>("", typeof(EmptyFlow));
        }
    }
}