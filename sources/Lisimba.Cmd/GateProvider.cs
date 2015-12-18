using System;
using DustInTheWind.Lisimba.Egg;
using Microsoft.Practices.Unity;

namespace Lisimba.Cmd
{
    class GateProvider
    {
        private readonly UnityContainer unityContainer;

        public GateProvider(UnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");

            this.unityContainer = unityContainer;
        }

        public IGate GetGate(string gateId)
        {
            try
            {
                return unityContainer.Resolve<IGate>(gateId);
            }
            catch (Exception ex)
            {
                string message = string.Format("There is no gate with id = {0}", gateId);
                throw new Exception(message, ex);
            }
        }
    }
}
