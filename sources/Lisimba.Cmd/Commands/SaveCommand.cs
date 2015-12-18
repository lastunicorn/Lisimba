﻿using System;

namespace Lisimba.Cmd.Commands
{
    class SaveCommand : ICommand
    {
        private readonly DomainData domainData;
        private readonly ConsoleView consoleView;

        public SaveCommand(DomainData domainData, ConsoleView consoleView)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.domainData = domainData;
            this.consoleView = consoleView;
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