using System;
using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Business
{
    class PrompterConsole
    {
        public void DisplayPrompter(string addressBookName, bool isSaved)
        {
            Console.WriteLine();
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
            string notSavedMarker = isSaved ? string.Empty : "*";
            return string.Format("[{0}]{1}", addressBookName, notSavedMarker);
        }

        public string ReadCommand()
        {
            return Console.ReadLine();
        }
    }
}