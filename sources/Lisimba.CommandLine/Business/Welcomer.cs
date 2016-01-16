using System;
using System.Reflection;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Common.GateManagement;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    class Welcomer
    {
        private readonly EnhancedConsole console;
        private readonly AvailableGates availableGates;

        public Welcomer(EnhancedConsole console, AvailableGates availableGates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.console = console;
            this.availableGates = availableGates;
        }

        public void Welcome()
        {
            WriteWelcomeMessage();
            WriteGateInfo(availableGates.DefaultGateName);
        }

        private void WriteWelcomeMessage()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            string title = string.Format(Resources.LisimbaTitle, version);

            console.WriteLineEmphasize(title);
        }

        private void WriteGateInfo(string gateName)
        {
            string text = string.Format(Resources.DefaultGateMessage, gateName);
            console.WriteLineNormal(text);

            console.WriteLine();
        }
    }
}