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