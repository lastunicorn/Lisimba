using System;
using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Flows
{
    class ExitFlow : IFlow
    {
        private readonly ApplicationLoop applicationLoop;

        public ExitFlow(ApplicationLoop applicationLoop)
        {
            if (applicationLoop == null) throw new ArgumentNullException("applicationLoop");

            this.applicationLoop = applicationLoop;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

            applicationLoop.ExitRequested = true;
        }
    }
}