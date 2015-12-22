using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Presentation
{
    class CloseFlowConsole
    {
        public void DisplayAddressBookCloseSuccess()
        {
            ConsoleHelper.WriteLineSuccess("Address book was closed.");
        }

        public void DisplayNoAddressBookMessage()
        {
            ConsoleHelper.WriteLineError("No address book is oppened.");
        }
    }
}