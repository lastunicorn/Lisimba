using System;

namespace Lisimba.Cmd.Commands
{
    class ExitCommand : ICommand
    {
        private readonly AddressBooks addressBooks;

        public ExitCommand(AddressBooks addressBooks)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.addressBooks = addressBooks;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            addressBooks.ExitRequested = true;
        }
    }
}