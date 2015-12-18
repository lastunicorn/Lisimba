using System;
using DustInTheWind.Lisimba.Egg;

namespace Lisimba.Cmd.Commands
{
    class GateCommand : ICommand
    {
        private readonly DomainData domainData;
        private readonly ConsoleView consoleView;
        private readonly GateProvider gateProvider;

        public GateCommand(DomainData domainData, ConsoleView consoleView, GateProvider gateProvider)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");
            if (consoleView == null) throw new ArgumentNullException("consoleView");
            if (gateProvider == null) throw new ArgumentNullException("gateProvider");

            this.domainData = domainData;
            this.consoleView = consoleView;
            this.gateProvider = gateProvider;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            if (commandInfo.ParameterCount == 0)
            {
                consoleView.DisplayGate(domainData.DefaultGate);
            }
            else
            {
                IGate gate = gateProvider.GetGate(commandInfo[1]);
                domainData.DefaultGate = gate;

                consoleView.DisplayGateChangeSuccess();
            }
        }
    }
}