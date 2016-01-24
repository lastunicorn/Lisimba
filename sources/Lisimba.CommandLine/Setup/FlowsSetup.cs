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

using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.CommandLine.Business;
using DustInTheWind.Lisimba.CommandLine.Flows;

namespace DustInTheWind.Lisimba.CommandLine.Setup
{
    class FlowsSetup
    {
        public static void Configure(ApplicationFlows applicationFlows)
        {
            applicationFlows.AddFlow("new", typeof(NewFlow));
            applicationFlows.AddFlow("update", typeof(UpdateFlow));
            applicationFlows.AddFlow("open", typeof(OpenFlow));
            applicationFlows.AddFlow("save", typeof(SaveFlow));
            applicationFlows.AddFlow("show", typeof(ShowFlow));
            applicationFlows.AddFlow("next-birthdays", typeof(NextBirthdaysFlow));
            applicationFlows.AddFlow("close", typeof(CloseFlow));
            applicationFlows.AddFlow("info", typeof(InfoFlow));
            applicationFlows.AddFlow("gate", typeof(GateFlow));
            applicationFlows.AddFlow("gates", typeof(GatesFlow));
            applicationFlows.AddFlow("exit", typeof(ExitFlow));
            applicationFlows.AddFlow("bye", typeof(ExitFlow));
            applicationFlows.AddFlow("goodbye", typeof(ExitFlow));
            applicationFlows.AddFlow("", typeof(EmptyFlow));
        }
    }
}