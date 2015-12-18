using System;
using Lisimba.Cmd.Commands;
using Microsoft.Practices.Unity;

namespace Lisimba.Cmd
{
    class CommandProvider
    {
        private readonly UnityContainer unityContainer;

        public CommandProvider(UnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");
            this.unityContainer = unityContainer;
        }

        public ICommand CreateCommand(string commandName)
        {
            switch (commandName)
            {
                case "new":
                    return unityContainer.Resolve<NewCommand>();

                case "update":
                    return unityContainer.Resolve<UpdateCommand>();

                case "open":
                    return unityContainer.Resolve<OpenCommand>();

                case "save":
                    return unityContainer.Resolve<SaveCommand>();

                case "show":
                    return unityContainer.Resolve<ShowCommand>();

                case "next-birthdays":
                    return unityContainer.Resolve<NextBirthdaysCommand>();

                case "close":
                    return unityContainer.Resolve<CloseCommand>();

                case "info":
                    return unityContainer.Resolve<InfoCommand>();

                case "gate":
                    return unityContainer.Resolve<GateCommand>();

                case "exit":
                case "bye":
                case "goodbye":
                    return unityContainer.Resolve<ExitCommand>();

                case "":
                    return unityContainer.Resolve<EmptyCommand>();

                default:
                    return unityContainer.Resolve<UnknownCommand>();
            }
        }
    }
}