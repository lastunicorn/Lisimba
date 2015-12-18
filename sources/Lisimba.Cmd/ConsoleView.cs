using System;
using System.Reflection;
using System.Text;
using DustInTheWind.Lisimba.Egg.Book;

namespace Lisimba.Cmd
{
    class ConsoleView
    {
        public void WriteWelcomeMessage()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            Console.WriteLine("Lisimba ver. " + version);
        }

        public void WriteGoodByeMessage()
        {
            Console.WriteLine("See you soon!");
        }

        public void WriteUnknownCommand()
        {
            Console.WriteLine("Unknown command");
        }

        public string ReadCommand(string addressBookName, bool isSaved)
        {
            Console.WriteLine();
            DisplayPrompter(addressBookName, isSaved);

            //string command = string.Empty;

            //while (true)
            //{
            //    ConsoleKeyInfo key = Console.ReadKey(false);

            //    if (key.Key == ConsoleKey.Enter)
            //    {
            //        Console.WriteLine();
            //        return command;
            //    }

            //    if (char.IsLetterOrDigit(key.KeyChar))
            //        command += key.KeyChar;
            //}

            return Console.ReadLine();
        }

        private static void DisplayPrompter(string addressBookName, bool isSaved)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("lisimba");

            if (addressBookName != null)
            {
                sb.Append(" [").Append(addressBookName).Append("]");

                if (!isSaved)
                    sb.Append("*");
            }

            sb.Append(" > ");

            Console.Write(sb);
        }

        public void WriteInfo(string text)
        {
            Console.WriteLine(text);
        }

        public void DisplayAddressBookOpenSuccess(string addressBookFileName, int contactsCount)
        {
            string message = string.Format("Successfully loaded {0} contacts from file '{1}'.", contactsCount, addressBookFileName);
            Console.WriteLine(message);
        }

        public void WriteGateInfo(string gateName)
        {
            Console.WriteLine("DefaultGate: {0}", gateName);
        }

        public void DisplayAddressBookCloseSuccess()
        {
            Console.WriteLine("Address book was closed.");
        }

        public void DisplayNoAddressBookMessage()
        {
            Console.WriteLine("No address book is oppened.");
        }

        public void DisplayAddressBookCreateSuccess()
        {
            Console.WriteLine("New address book successfully created.");
        }

        public void DisplayContactWithBirthday(Contact contact)
        {
            string text = string.Format("{0} : {1}", contact.Name, contact.Birthday);
            Console.WriteLine(text);
        }

        public void WriteError(string message)
        {
            Console.WriteLine(message);
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
            Console.WriteLine("Address book name successfully changed.");
        }

        public void DisplayInvalidUpdateActionError()
        {
            Console.WriteLine("Invalid update action.");
        }
    }
}