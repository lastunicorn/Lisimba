using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Presentation
{
    class UnknownFlowConsole
    {
        public void DisplayUnknownCommandError()
        {
            ConsoleHelper.WriteLineError("Unknown command");
        }
    }
}