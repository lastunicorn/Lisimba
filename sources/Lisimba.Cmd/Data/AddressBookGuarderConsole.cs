using System;

namespace Lisimba.Cmd.Data
{
    class AddressBookGuarderConsole
    {
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
    }
}
