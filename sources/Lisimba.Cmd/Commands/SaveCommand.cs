using System;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;

namespace Lisimba.Cmd.Commands
{
    class SaveCommand : ICommand
    {
        private readonly AddressBooks addressBooks;

        public SaveCommand(AddressBooks addressBooks)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.addressBooks = addressBooks;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            if (commandInfo[1] != null)
                addressBooks.SaveAddressBookAs(commandInfo[1]);
            else
                addressBooks.SaveAddressBook();
        }
    }
}