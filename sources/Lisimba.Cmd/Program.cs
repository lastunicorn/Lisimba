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
        private static CommandProvider commandProvider;

        static void Main(string[] args)
        {
            try
            {
                container = DependencyContainerSetup.CreateContainer();

                consoleView = container.Resolve<ConsoleView>();
                addressBooks = container.Resolve<AddressBooks>();
                gates = container.Resolve<Gates>();
                commandProvider = container.Resolve<CommandProvider>();

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
            CommandReadControl commandReadControl = new CommandReadControl(addressBooks, consoleView);

            while (!addressBooks.ExitRequested)
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
    }
}
