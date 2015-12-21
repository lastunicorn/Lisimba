using System;
using DustInTheWind.Lisimba.Egg;

namespace Lisimba.Cmd.Data
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

            InitializeDefaultGate();
        }

        private void InitializeDefaultGate()
        {
            try
            {
                DefaultGate = gateProvider.GetGate(config.DefaultGateName);
            }
            catch
            {
                DefaultGate = new EmptyGate();
            }
        }

        public void SetDefaultGate(string gateId)
        {
            try
            {
                DefaultGate = gateProvider.GetGate(gateId);
            }
            catch (Exception ex)
            {
                string message = string.Format("There is no gate with id = {0}", gateId);
                throw new Exception(message, ex);
            }
        }
    }
}