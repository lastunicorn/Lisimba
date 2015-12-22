using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Presentation
{
    class UpdateFlowConsole
    {
        public void DisplayInvalidUpdateActionError()
        {
            ConsoleHelper.WriteLineError("Invalid update action.");
        }

        public void DisplayAddressBookNameChangeSuccess()
        {
            ConsoleHelper.WriteLineSuccess("Address book name successfully changed.");
        }

        public void DisplayNoAddressBookMessage()
        {
            ConsoleHelper.WriteLineError("No address book is oppened.");
        }
    }
}