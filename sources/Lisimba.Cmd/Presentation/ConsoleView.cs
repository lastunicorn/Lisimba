using System;
using System.Reflection;
using System.Text;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;

namespace Lisimba.Cmd.Presentation
{
    class ConsoleView
    {
        public void WriteWelcomeMessage()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            ConsoleHelper.WriteLineEmphasize("Lisimba ver. " + version);
        }

        public void WriteGoodByeMessage()
        {
            Console.WriteLine("See you soon!");
        }

        public void WriteUnknownCommand()
        {
            ConsoleHelper.WriteLineError("Unknown command");
        }

        public void WriteError(string text)
        {
            ConsoleHelper.WriteLineError(text);
        }

        public void DisplayAddressBookOpenSuccess(string addressBookFileName, int contactsCount)
        {
            string message = string.Format("Successfully loaded {0} contacts from file '{1}'.", contactsCount, addressBookFileName);
            ConsoleHelper.WriteLineSuccess(message);
        }

        public void WriteGateInfo(string gateName)
        {
            Console.WriteLine("DefaultGate: {0}", gateName);
        }

        public void DisplayAddressBookCloseSuccess()
        {
            ConsoleHelper.WriteLineSuccess("Address book was closed.");
        }

        public void DisplayNoAddressBookMessage()
        {
            ConsoleHelper.WriteLineError("No address book is oppened.");
        }

        public void DisplayAddressBookCreateSuccess()
        {
            ConsoleHelper.WriteLineSuccess("New address book successfully created.");
        }

        public void DisplayContactWithBirthday(Contact contact)
        {
            string text = string.Format("{0} : {1}", contact.Name, contact.Birthday);
            Console.WriteLine(text);
        }

        public void DisplayContactDetails(Contact contact)
        {
            Console.WriteLine(contact.Name.ToString());
        }

        public void DisplayContactShort(Contact contact)
        {
            Console.WriteLine(contact.ToString());
        }

        public void DisplayAddressBookNameChangeSuccess()
        {
            ConsoleHelper.WriteLineSuccess("Address book name successfully changed.");
        }

        public void DisplayInvalidUpdateActionError()
        {
            ConsoleHelper.WriteLineError("Invalid update action.");
        }

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

        public void DisplayGate(IGate gate)
        {
            Console.WriteLine();

            ConsoleHelper.WriteEmphasize("DefaultGate: ");
            Console.WriteLine("{0} ({1})", gate.Name, gate.Id);

            ConsoleHelper.WriteEmphasize("Description: ");
            Console.WriteLine(gate.Description);
        }

        public void DisplayGateChangeSuccess()
        {
            ConsoleHelper.WriteLineSuccess("The gate was successfully changed.");
        }

        public bool? AskToSaveAddressBook()
        {
            Console.WriteLine("Do you want to save current address book? [y-yes; n-no; c-cancel] ");
            ConsoleKeyInfo key = Console.ReadKey(false);

            switch (key.Key)
            {
                case ConsoleKey.Y:
                    return true;

                case ConsoleKey.N:
                    return false;

                default:
                    return null;
            }
        }

        public string AskForLocation()
        {
            Console.WriteLine("Address book file name [empty string to cancel]: ");
            return Console.ReadLine();
        }

        public string ReadCommand()
        {
            return Console.ReadLine();
        }
    }
}