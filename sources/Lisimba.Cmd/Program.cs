﻿using System;
using Lisimba.Cmd.Commands;

namespace Lisimba.Cmd
{
    class Program
    {
        private static ConsoleView consoleView;
        private static DomainData domainData;
        private static ApplicationConfiguration config;

        static void Main(string[] args)
        {
            config = new ApplicationConfiguration();
            consoleView = new ConsoleView();
            domainData = new DomainData(config);

            consoleView.WriteWelcomeMessage();
            consoleView.WriteGateInfo(domainData.DefaultGateName);

            while (!domainData.ExitRequested)
            {
                string addressBookName = domainData.AddressBookName;
                string commandText = consoleView.ReadCommand(addressBookName, domainData.IsAddressBookSaved);

                ProcessCommand(commandText);
            }

            consoleView.WriteGoodByeMessage();
        }

        private static void ProcessCommand(string commandText)
        {
            try
            {
                CommandInfo commandInfo = new CommandInfo(commandText);
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
                    return new SaveCommand(domainData, consoleView);

                case "show":
                    return new ShowCommand(domainData, consoleView);

                case "next-birthdays":
                    return new NextBirthdaysCommand(domainData, consoleView);

                case "close":
                    return new CloseCommand(domainData, consoleView);

                case "info":
                    return new InfoCommand(domainData, consoleView);

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