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

                CommandReadControl commandReadControl = new CommandReadControl(addressBooks, consoleView);

                while (!addressBooks.ExitRequested)
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
                    return new NewCommand(addressBooks, consoleView);

                case "update":
                    return new UpdateCommand(addressBooks, consoleView);

                case "open":
                    return new OpenCommand(addressBooks, consoleView);

                case "save":
                    return new SaveCommand(addressBooks);

                case "show":
                    return new ShowCommand(addressBooks, consoleView);

                case "next-birthdays":
                    return new NextBirthdaysCommand(addressBooks, consoleView);

                case "close":
                    return new CloseCommand(addressBooks, consoleView);

                case "info":
                    return new InfoCommand(addressBooks, consoleView);

                case "gate":
                    return new GateCommand(gates, consoleView);

                case "exit":
                case "bye":
                case "goodbye":
                    return new ExitCommand(addressBooks);

                default:
                    return new UnknownCommand(consoleView);
            }
        }
    }
}
