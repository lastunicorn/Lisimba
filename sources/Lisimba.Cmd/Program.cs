using System;
using Lisimba.Cmd.Commands;
using Microsoft.Practices.Unity;

namespace Lisimba.Cmd
{
    class Program
    {
        private static UnityContainer container;

        private static ConsoleView consoleView;
        private static DomainData domainData;
        private static CommandProvider commandProvider;

        static void Main(string[] args)
        {
            try
            {
                container = DependencyContainerSetup.CreateContainer();

                consoleView = container.Resolve<ConsoleView>();
                domainData = container.Resolve<DomainData>();
                commandProvider = container.Resolve<CommandProvider>();

                consoleView.WriteWelcomeMessage();
                consoleView.WriteGateInfo(domainData.DefaultGateName);

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
    }
}
