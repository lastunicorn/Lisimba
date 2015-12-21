using System;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;

namespace Lisimba.Cmd.Presentation
{
    /// <summary>
    /// Reads a command from the console and parses it.
    /// </summary>
    class CommandControl
    {
        private readonly AddressBooks addressBooks;
        private readonly CommandControlView view;

        public CommandControl(AddressBooks addressBooks, CommandControlView view)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (view == null) throw new ArgumentNullException("view");

            this.addressBooks = addressBooks;
            this.view = view;
        }

        public CommandInfo Read()
        {
            string addressBookName = addressBooks.AddressBookName;
            bool isSaved = addressBooks.IsAddressBookSaved;

            view.DisplayPrompter(addressBookName, isSaved);

            string commandText = view.ReadCommand();

            return new CommandInfo(commandText);
        }
    }
}