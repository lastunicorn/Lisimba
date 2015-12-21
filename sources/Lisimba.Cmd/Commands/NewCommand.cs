using System;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Commands
{
    class NewCommand : ICommand
    {
        private readonly AddressBooks addressBooks;
        private readonly ConsoleView consoleView;

        public NewCommand(AddressBooks addressBooks, ConsoleView consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            addressBooks.NewAddressBook(commandInfo[1]);
            consoleView.DisplayAddressBookCreateSuccess();
        }
    }
}