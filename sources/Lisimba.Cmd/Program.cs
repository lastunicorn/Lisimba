using System;
using System.Globalization;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Comparers;
using DustInTheWind.Lisimba.Gating;

namespace Lisimba.Cmd
{
    class Program
    {
        private static bool exitRequested;
        private static ConsoleView consoleView;
        private static AddressBook addressBook;

        static void Main(string[] args)
        {
            consoleView = new ConsoleView();

            consoleView.WriteWelcomeMessage();

            while (!exitRequested)
            {
                string addressBookName = GetAddressBookName();
                string commandText = consoleView.ReadCommand(addressBookName);
                Command command = new Command(commandText);
                ProcessCommand(command);
            }

            consoleView.WriteGoodByeMessage();
        }

        private static string GetAddressBookName()
        {
            return addressBook == null ? null : addressBook.Name;
        }

        public static void ProcessCommand(Command command)
        {
            switch (command.Name)
            {
                case "load":
                    ZipXmlGate gate = new ZipXmlGate();
                    addressBook = gate.Load(command[1] ?? "agenda.lsb");
                    consoleView.DisplayAddressBookLoadSuccess(addressBook.Contacts.Count);
                    break;

                case "show":
                    if (command.HasParameters)
                        DisplayContact(command[1]);
                    else
                        DisplayAddressBook();
                    break;

                case "next-birthday":
                    DisplayNextBirthdays();
                    break;

                case "exit":
                case "bye":
                case "goodbye":
                    exitRequested = true;
                    break;

                default:
                    consoleView.WriteUnknownCommand();
                    break;
            }
        }

        private static void DisplayContact(string contactName)
        {
            var contacts = addressBook.Contacts
                .Where(x =>
                    (x.Name.FirstName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.FirstName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.MiddleName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.MiddleName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.LastName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.LastName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.Nickname != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.Nickname, contactName, CompareOptions.IgnoreCase) >= 0));

            foreach (Contact contact in contacts)
            {
                consoleView.WriteInfo(contact.Name.ToString());
            }
        }

        private static void DisplayAddressBook()
        {
            foreach (Contact contact in addressBook.Contacts)
            {
                consoleView.WriteInfo(contact.ToString());
            }
        }

        private static void DisplayNextBirthdays()
        {
            var contacts = addressBook.Contacts
                .Where(x => x.Birthday != null)
                .Where(x => Date.CompareWithoutYear(x.Birthday, DateTime.Today) >= 0)
                .OrderBy(x => x, new ContactByBirthdayComparer())
                .Take(10);

            foreach (Contact contact in contacts)
            {
                consoleView.WriteInfo(contact.Name + " : " + contact.Birthday);
            }
        }
    }
}
