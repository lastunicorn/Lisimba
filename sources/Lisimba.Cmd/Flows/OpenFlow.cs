using System;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    class OpenFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly ConsoleView consoleView;

        public OpenFlow(AddressBooks addressBooks, ConsoleView consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            addressBooks.OpenAddressBook(command[1]);

            if (addressBooks.AddressBook == null)
                consoleView.DisplayNoAddressBookMessage();
            else
                consoleView.DisplayAddressBookOpenSuccess(addressBooks.AddressBookLocation, addressBooks.AddressBook.Contacts.Count);
        }
    }
}