using System;
using System.Reflection;
using System.Text;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;

namespace Lisimba.Cmd
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

        public void DisplayPrompter(string addressBookName, bool isSaved)
        {
            ConsoleHelper.WriteEmphasize("lisimba");

            if (addressBookName != null)
            {
                Console.Write(" ");

                string formattedAddressBookName = BuildAddressBookName(addressBookName, isSaved);
                Console.Write(formattedAddressBookName);
            }

            ConsoleHelper.WriteEmphasize(" > ");
        }

        private static string BuildAddressBookName(string addressBookName, bool isSaved)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[").Append(addressBookName).Append("]");

            if (!isSaved)
                sb.Append("*");

            return sb.ToString();
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
            Console.WriteLine(gate.Description);
        }

        public void DisplayGateChangeSuccess()
        {
            ConsoleHelper.WriteLineSuccess("The gate was successfully changed.");
        }
    }
}