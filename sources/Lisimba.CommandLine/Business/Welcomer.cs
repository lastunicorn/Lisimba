using System;
using System.Reflection;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Common.GateManagement;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    class Welcomer
    {
        private readonly EnhancedConsole enhancedConsole;
        private readonly AvailableGates availableGates;

        public Welcomer(EnhancedConsole enhancedConsole, AvailableGates availableGates)
        {
            if (enhancedConsole == null) throw new ArgumentNullException("enhancedConsole");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.enhancedConsole = enhancedConsole;
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
            enhancedConsole.WriteLineEmphasize(title);
        }

        private void WriteGateInfo(string gateName)
        {
            string text = string.Format(Resources.DefaultGateMessage, gateName);
            enhancedConsole.WriteLineNormal(text);
            enhancedConsole.WriteLine();
        }
    }
}