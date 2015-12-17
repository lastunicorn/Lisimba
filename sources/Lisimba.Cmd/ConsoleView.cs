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
            string prompter = addressBookName == null
                ? "lisimba > "
                : "lisimba [" + addressBookName + (isSaved ? string.Empty : " *") + "] > ";

            Console.Write(prompter);
        }

        public void WriteInfo(string text)
        {
            Console.WriteLine(text);
        }

        public void DisplayAddressBookLoadSuccess(string addressBookFileName, int contactsCount)
        {
            string message = string.Format("Successfully loaded {0} contacts from file '{1}'.", contactsCount, addressBookFileName);
            Console.WriteLine(message);
        }

        public void WriteGateInfo(string gateName)
        {
            Console.WriteLine("DefaultGate: {0}", gateName);
        }
    }
}