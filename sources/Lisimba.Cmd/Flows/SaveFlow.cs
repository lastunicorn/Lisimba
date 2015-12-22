using System;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Data;

namespace Lisimba.Cmd.Flows
{
    class SaveFlow : IFlow
    {
        private readonly AddressBooks addressBooks;

        public SaveFlow(AddressBooks addressBooks)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.addressBooks = addressBooks;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            if (command[1] != null)
                addressBooks.SaveAddressBookAs(command[1]);
            else
                addressBooks.SaveAddressBook();
        }
    }
}