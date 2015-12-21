using System;
using Lisimba.Cmd.Commands;

namespace Lisimba.Cmd
{
    /// <summary>
    /// Reads a command from the console and parses it.
    /// </summary>
    class CommandReadControl
    {
        private readonly AddressBooks addressBooks;
        private readonly ConsoleView consoleView;

        public CommandReadControl(AddressBooks addressBooks, ConsoleView consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public CommandInfo Read()
        {
            string addressBookName = addressBooks.AddressBookName;
            bool isSaved = addressBooks.IsAddressBookSaved;

            Console.WriteLine();
            consoleView.DisplayPrompter(addressBookName, isSaved);
            
            string commandText = Console.ReadLine();

            return new CommandInfo(commandText);
        }
    }
}