using System;

namespace Lisimba.Cmd.Commands
{
    class LoadCommand : ICommand
    {
        private readonly DomainData domainData;
        private readonly ConsoleView consoleView;

        public LoadCommand(DomainData domainData, ConsoleView consoleView)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.domainData = domainData;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            domainData.LoadAddressBook(commandInfo[1]);
            if (domainData.AddressBook == null)
                consoleView.WriteInfo("No address book was loaded.");
            else
                consoleView.DisplayAddressBookLoadSuccess(domainData.AddressBookLocation, domainData.AddressBook.Contacts.Count);
        }
    }
}