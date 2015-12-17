using System;
using System.Reflection;

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

        public string ReadCommand(string addressBookName)
        {
            Console.WriteLine();

            string prompter = GetPrompter(addressBookName);
            Console.Write(prompter);

            return Console.ReadLine();
        }

        private static string GetPrompter(string addressBookName)
        {
            return addressBookName == null
                ? "lisimba > "
                : "lisimba (" + addressBookName + ") > ";
        }

        public void WriteInfo(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteInfo(int number)
        {
            Console.WriteLine(number);
        }

        public void DisplayAddressBookLoadSuccess(int contactsCount)
        {
            string message = string.Format("Successfully loaded {0} contacts.", contactsCount);
            Console.WriteLine(message);
        }
    }
}