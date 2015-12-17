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

            for (int i = 1; i <= commandInfo.ParameterCount; i++)
            {
                string action = commandInfo[i];
                ProcessAction(action);
            }
        }

        private void ProcessAction(string action)
        {
            int pos = action.IndexOf('=');

            if (pos == -1)
                return;

            string paramName = action.Substring(0, pos);
            string value = action.Substring(pos + 1);

            switch (paramName.ToLower())
            {
                case "name":
                    if (domainData.AddressBook != null)
                    {
                        domainData.AddressBook.Name = value;
                        consoleView.WriteInfo("Address book name changed.");
                    }
                    else
                    {
                        consoleView.WriteInfo("No address book is loaded.");
                    }
                    break;
            }
        }
    }
}