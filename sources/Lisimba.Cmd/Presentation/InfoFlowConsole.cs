using System;
using DustInTheWind.Lisimba.Egg.Book;
using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Presentation
{
    class InfoFlowConsole
    {
        public void DisplayAddressBookInfo(AddressBook addressBook, string addressBookLocation)
        {
            Console.WriteLine();

            ConsoleHelper.WriteEmphasize("Address book: ");
            Console.WriteLine(addressBook.Name);

            ConsoleHelper.WriteEmphasize("Location: ");
            Console.WriteLine(addressBookLocation);

            ConsoleHelper.WriteEmphasize("Contacts: ");
            Console.WriteLine(addressBook.Contacts.Count);
        }

        public void DisplayNoAddressBookMessage()
        {
            ConsoleHelper.WriteLineError("No address book is oppened.");
        }
    }
}