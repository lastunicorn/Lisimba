using System;
using DustInTheWind.Lisimba.Egg;

namespace Lisimba.Cmd
{
    class Gates
    {
        private readonly ApplicationConfiguration config;
        private readonly GateProvider gateProvider;

        public IGate DefaultGate { get; set; }

        public string DefaultGateName
        {
            get { return DefaultGate == null ? string.Empty : DefaultGate.Id; }
        }

        public Gates(ApplicationConfiguration config, GateProvider gateProvider)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (gateProvider == null) throw new ArgumentNullException("gateProvider");

            this.config = config;
            this.gateProvider = gateProvider;

            DefaultGate = CreateDefaultGate();
        }

        public IGate GetGate(string gateId)
        {
            try
            {
                return gateProvider.GetGate(gateId);
            }
            catch (Exception ex)
            {
                string message = string.Format("There is no gate with id = {0}", gateId);
                throw new Exception(message, ex);
            }
        }

        private IGate CreateDefaultGate()
        {
            try
            {
                return gateProvider.GetGate(config.DefaultGateName);
            }
            catch
            {
                return new EmptyGate();
            }
        }
    }
}