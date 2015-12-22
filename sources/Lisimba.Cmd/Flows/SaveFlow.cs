using System;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    class SaveFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly ConsoleView consoleView;

        public SaveFlow(AddressBooks addressBooks, ConsoleView consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            if (command[1] != null)
                addressBooks.SaveAddressBookAs(command[1]);
            else
                addressBooks.SaveAddressBook();

            consoleView.DisplayAddressBookSaveSuccess();
        }
    }
}