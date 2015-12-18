using System;

namespace Lisimba.Cmd.Commands
{
    internal class UpdateCommand : ICommand
    {
        private readonly DomainData domainData;
        private readonly ConsoleView consoleView;

        public UpdateCommand(DomainData domainData, ConsoleView consoleView)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.domainData = domainData;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");
            
            foreach (string actionText in commandInfo)
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
            if (domainData.AddressBook != null)
            {
                domainData.AddressBook.Name = newAddressBookName;
                consoleView.DisplayAddressBookNameChangeSuccess();
            }
            else
            {
                consoleView.DisplayNoAddressBookMessage();
            }
        }
    }
}