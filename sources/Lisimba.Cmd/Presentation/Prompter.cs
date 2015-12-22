using System;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Data;

namespace Lisimba.Cmd.Presentation
{
    /// <summary>
    /// Reads a command from the console and parses it.
    /// </summary>
    class Prompter
    {
        private readonly AddressBooks addressBooks;
        private readonly PrompterView view;

        public Prompter(AddressBooks addressBooks, PrompterView view)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (view == null) throw new ArgumentNullException("view");

            this.addressBooks = addressBooks;
            this.view = view;
        }

        public Command Read()
        {
            string addressBookName = addressBooks.AddressBookName;
            bool isSaved = addressBooks.IsAddressBookSaved;

            view.DisplayPrompter(addressBookName, isSaved);

            string commandText = view.ReadCommand();

            return new Command(commandText);
        }
    }
}