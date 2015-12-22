using System;
using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Business
{
    /// <summary>
    /// Reads a command from the console and parses it.
    /// </summary>
    class Prompter
    {
        private readonly AddressBooks addressBooks;
        private readonly PrompterConsole console;

        public Prompter(AddressBooks addressBooks, PrompterConsole console)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.addressBooks = addressBooks;
            this.console = console;
        }

        public Command Read()
        {
            string addressBookName = addressBooks.AddressBookName;
            bool isSaved = addressBooks.IsAddressBookSaved;

            console.DisplayPrompter(addressBookName, isSaved);

            string commandText = console.ReadCommand();

            return new Command(commandText);
        }
    }
}