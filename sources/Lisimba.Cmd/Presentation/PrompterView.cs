using System;
using System.Text;

namespace Lisimba.Cmd.Presentation
{
    class PrompterView
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
            StringBuilder sb = new StringBuilder();

            sb.Append("[").Append(addressBookName).Append("]");

            if (!isSaved)
                sb.Append("*");

            return sb.ToString();
        }

        public string ReadCommand()
        {
            return Console.ReadLine();
        }
    }
}