using System;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;

namespace Lisimba.Cmd.Commands
{
    class ExitCommand : ICommand
    {
        private readonly LisimbaApplication lisimbaApplication;
        private readonly AddressBooks addressBooks;

        public ExitCommand(LisimbaApplication lisimbaApplication, AddressBooks addressBooks)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.lisimbaApplication = lisimbaApplication;
            this.addressBooks = addressBooks;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            lisimbaApplication.ExitRequested = true;
        }
    }
}