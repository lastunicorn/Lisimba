using System;

namespace Lisimba.Cmd.Commands
{
    class ExitCommand : ICommand
    {
        private readonly LisimbaApplication lisimbaApplication;

        public ExitCommand(LisimbaApplication lisimbaApplication)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");

            this.lisimbaApplication = lisimbaApplication;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            lisimbaApplication.ExitRequested = true;
        }
    }
}