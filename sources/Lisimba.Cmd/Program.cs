using System;
using DustInTheWind.Lisimba.Egg;
using Lisimba.Cmd.Commands;
using Microsoft.Practices.Unity;

namespace Lisimba.Cmd
{
    class Program
    {
        private static UnityContainer container;

        private static ApplicationConfiguration config;
        private static ConsoleView consoleView;
        private static DomainData domainData;

        static void Main(string[] args)
        {
            try
            {
                container = DependencyContainerSetup.CreateContainer();

                config = container.Resolve<ApplicationConfiguration>();
                consoleView = container.Resolve<ConsoleView>();
                domainData = container.Resolve<DomainData>();

                domainData.DefaultGate = CreateDefaultGate();

                consoleView.WriteWelcomeMessage();
                consoleView.WriteGateInfo(domainData.DefaultGateName);

                CommandReadControl commandReadControl = new CommandReadControl(domainData, consoleView);

                while (!domainData.ExitRequested)
                {
                    CommandInfo command = commandReadControl.Read();
                    ProcessCommand(command);
                }

                consoleView.WriteGoodByeMessage();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey(true);
            }
        }

        private static IGate CreateDefaultGate()
        {
            try
            {
                return container.Resolve<IGate>(config.DefaultGateName);
            }
            catch
            {
                return new EmptyGate();
            }
        }

        private static void ProcessCommand(CommandInfo commandInfo)
        {
            try
            {
                ICommand command = CreateCommand(commandInfo.Name);
                command.Execute(commandInfo);
            }
            catch (Exception ex)
            {
                consoleView.WriteError(ex.Message);
            }
        }

        private static ICommand CreateCommand(string commandName)
        {
            switch (commandName)
            {
                case "new":
                    return new NewCommand(domainData, consoleView);

                case "update":
                    return new UpdateCommand(domainData, consoleView);

                case "open":
                    return new OpenCommand(domainData, consoleView);

                case "save":
                    return new SaveCommand(domainData);

                case "show":
                    return new ShowCommand(domainData, consoleView);

                case "next-birthdays":
                    return new NextBirthdaysCommand(domainData, consoleView);

                case "close":
                    return new CloseCommand(domainData, consoleView);

                case "info":
                    return new InfoCommand(domainData, consoleView);

                case "gate":
                    return new GateCommand(domainData, consoleView, container.Resolve<GateProvider>());

                case "exit":
                case "bye":
                case "goodbye":
                    return new ExitCommand(domainData);

                default:
                    return new UnknownCommand(consoleView);
            }
        }
    }
}
