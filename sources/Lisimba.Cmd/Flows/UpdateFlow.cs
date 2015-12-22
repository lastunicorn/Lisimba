using System;
using Lisimba.Cmd.Business;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    internal class UpdateFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly UpdateFlowConsole consoleView;

        public UpdateFlow(AddressBooks addressBooks, UpdateFlowConsole consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");
            
            foreach (string actionText in command)
            {
                ProcessAction(actionText);
            }
        }

        private void ProcessAction(string actionText)
        {
            Tuple<string, string> action = ParseAction(actionText);

            switch (action.Item1.ToLower())
            {
                case "name":
                    UpdateAddressBookName(action.Item2);
                    break;

                default:
                    consoleView.DisplayInvalidUpdateActionError();
                    break;
            }
        }

        private static Tuple<string, string> ParseAction(string actionText)
        {
            int pos = actionText.IndexOf('=');

            if (pos == -1)
                return null;

            string paramName = actionText.Substring(0, pos);

            string value = actionText.Substring(pos + 1);
            if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"')
                value = value.Substring(1, value.Length - 2);

            return new Tuple<string, string>(paramName, value);
        }

        private void UpdateAddressBookName(string newAddressBookName)
        {
            if (addressBooks.AddressBook != null)
            {
                addressBooks.AddressBook.Name = newAddressBookName;
                consoleView.DisplayAddressBookNameChangeSuccess();
            }
            else
            {
                consoleView.DisplayNoAddressBookMessage();
            }
        }
    }
}