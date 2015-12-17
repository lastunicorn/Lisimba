using System;

namespace Lisimba.Cmd.Commands
{
    class ExitCommand : ICommand
    {
        private readonly DomainData domainData;

        public ExitCommand(DomainData domainData)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");

            this.domainData = domainData;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            domainData.ExitRequested = true;
        }
    }
}