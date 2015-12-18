using System;

namespace Lisimba.Cmd.Commands
{
    class CloseCommand : ICommand
    {
        private readonly DomainData domainData;
        private readonly ConsoleView consoleView;

        public CloseCommand(DomainData domainData, ConsoleView consoleView)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.domainData = domainData;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            if (domainData.AddressBook != null)
            {
                domainData.CloseAddressBook();
                consoleView.DisplayAddressBookCloseSuccess();
            }
            else
            {
                consoleView.DisplayNoAddressBookMessage();
            }
        }
    }
}