using Lisimba.Cmd.Commands;

namespace Lisimba.Cmd
{
    class Program
    {
        private static ConsoleView consoleView;
        private static DomainData domainData;

        static void Main(string[] args)
        {
            consoleView = new ConsoleView();
            domainData = new DomainData();

            consoleView.WriteWelcomeMessage();

            while (!domainData.ExitRequested)
            {
                string addressBookName = domainData.GetAddressBookName();
                string commandText = consoleView.ReadCommand(addressBookName);
                
                CommandInfo commandInfo = new CommandInfo(commandText);
                ProcessCommand(commandInfo);
            }

            consoleView.WriteGoodByeMessage();
        }

        public static void ProcessCommand(CommandInfo commandInfo)
        {
            ICommand command = CreateCommand(commandInfo.Name);
            command.Execute(commandInfo);
        }

        private static ICommand CreateCommand(string commandName)
        {
            switch (commandName)
            {
                case "load":
                    return new LoadCommand(domainData, consoleView);

                case "show":
                    return new ShowCommand(domainData, consoleView);

                case "next-birthdays":
                    return new NextBirthdaysCommand(domainData, consoleView);

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
