using System;
using System.Reflection;
using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Presentation
{
    class ApplicationLoopConsole
    {
        public void WriteWelcomeMessage()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            ConsoleHelper.WriteLineEmphasize("Lisimba ver. " + version);
        }

        public void WriteGoodByeMessage()
        {
            Console.WriteLine("See you soon!");
        }

        public void WriteGateInfo(string gateName)
        {
            Console.WriteLine("DefaultGate: {0}", gateName);
        }

        public void WriteError(string text)
        {
            ConsoleHelper.WriteLineError(text);
        }
    }
}