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
using Lisimba.Cmd.Common;
using Microsoft.Practices.Unity;

namespace Lisimba.Cmd.Flows
{
    class FlowProvider
    {
        private readonly UnityContainer unityContainer;

        public FlowProvider(UnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");
            this.unityContainer = unityContainer;
        }

        public IFlow CreateFlow(string commandName)
        {
            switch (commandName)
            {
                case "new":
                    return unityContainer.Resolve<NewFlow>();

                case "update":
                    return unityContainer.Resolve<UpdateFlow>();

                case "open":
                    return unityContainer.Resolve<OpenFlow>();

                case "save":
                    return unityContainer.Resolve<SaveFlow>();

                case "show":
                    return unityContainer.Resolve<ShowFlow>();

                case "next-birthdays":
                    return unityContainer.Resolve<NextBirthdaysFlow>();

                case "close":
                    return unityContainer.Resolve<CloseFlow>();

                case "info":
                    return unityContainer.Resolve<InfoFlow>();

                case "gate":
                    return unityContainer.Resolve<GateFlow>();

                case "exit":
                case "bye":
                case "goodbye":
                    return unityContainer.Resolve<ExitFlow>();

                case "":
                    return unityContainer.Resolve<EmptyFlow>();

                default:
                    return unityContainer.Resolve<UnknownFlow>();
            }
        }
    }
}