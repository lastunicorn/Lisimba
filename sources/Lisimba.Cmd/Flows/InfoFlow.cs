using System;
using Lisimba.Cmd.Business;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    class InfoFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly InfoFlowConsole console;

        public InfoFlow(AddressBooks addressBooks, InfoFlowConsole console)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.addressBooks = addressBooks;
            this.console = console;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            if (addressBooks.AddressBook == null)
                console.DisplayNoAddressBookMessage();
            else
                console.DisplayAddressBookInfo(addressBooks.AddressBook, addressBooks.AddressBookLocation);
        }
    }
}