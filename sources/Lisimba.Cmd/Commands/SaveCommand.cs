using System;

namespace Lisimba.Cmd.Commands
{
    class SaveCommand : ICommand
    {
        private readonly DomainData domainData;

        public SaveCommand(DomainData domainData)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");

            this.domainData = domainData;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            if (commandInfo[1] != null)
                domainData.SaveAddressBookAs(commandInfo[1]);
            else
                domainData.SaveAddressBook();
        }
    }
}