using System;

namespace Lisimba.Cmd.Commands
{
    class OpenCommand : ICommand
    {
        private readonly DomainData domainData;
        private readonly ConsoleView consoleView;

        public OpenCommand(DomainData domainData, ConsoleView consoleView)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.domainData = domainData;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            domainData.OpenAddressBook(commandInfo[1]);

            if (domainData.AddressBook == null)
                consoleView.DisplayNoAddressBookMessage();
            else
                consoleView.DisplayAddressBookOpenSuccess(domainData.AddressBookLocation, domainData.AddressBook.Contacts.Count);
        }
    }
}