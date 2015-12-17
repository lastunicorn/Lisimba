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
            DisplayPrompter(addressBookName);

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

        private static void DisplayPrompter(string addressBookName)
        {
            string prompter = addressBookName == null
                ? "lisimba > "
                : "lisimba (" + addressBookName + ") > ";

            Console.Write(prompter);
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