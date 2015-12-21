using System;
using Lisimba.Cmd.Commands;
using Microsoft.Practices.Unity;

namespace Lisimba.Cmd
{
    class Program
    {
        private static UnityContainer container;

        private static ConsoleView consoleView;
        private static AddressBooks addressBooks;
        private static Gates gates;

        static void Main(string[] args)
        {
            try
            {
                container = DependencyContainerSetup.CreateContainer();

                consoleView = container.Resolve<ConsoleView>();
                addressBooks = container.Resolve<AddressBooks>();
                gates = container.Resolve<Gates>();

                consoleView.WriteWelcomeMessage();
                consoleView.WriteGateInfo(gates.DefaultGateName);

                RunCommandLoop();

                consoleView.WriteGoodByeMessage();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey(true);
            }
        }

        private static void RunCommandLoop()
        {
            CommandReadControl commandReadControl = new CommandReadControl(domainData, consoleView);

            while (!domainData.ExitRequested)
            {
                CommandInfo command = commandReadControl.Read();
                ProcessCommand(command);
            }
        }

        private static void ProcessCommand(CommandInfo commandInfo)
        {
            try
            {
                ICommand command = commandProvider.CreateCommand(commandInfo.Name);
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
