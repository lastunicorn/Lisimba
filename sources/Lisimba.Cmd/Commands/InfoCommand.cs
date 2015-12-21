using System;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Commands
{
    class InfoCommand : ICommand
    {
        private readonly AddressBooks addressBooks;
        private readonly ConsoleView consoleView;

        public InfoCommand(AddressBooks addressBooks, ConsoleView consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            if (addressBooks.AddressBook == null)
                consoleView.DisplayNoAddressBookMessage();
            else
                consoleView.DisplayAddressBookInfo(addressBooks.AddressBook, addressBooks.AddressBookLocation);
        }
    }
}