using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Presentation
{
    class OpenFlowConsole
    {
        public void DisplayAddressBookOpenSuccess(string addressBookFileName, int contactsCount)
        {
            string message = string.Format("Successfully loaded {0} contacts from file '{1}'.", contactsCount, addressBookFileName);
            ConsoleHelper.WriteLineSuccess(message);
        }

        public void DisplayNoAddressBookMessage()
        {
            ConsoleHelper.WriteLineError("No address book is oppened.");
        }
    }
}