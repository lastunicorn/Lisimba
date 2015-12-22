using System;
using DustInTheWind.Lisimba.Egg;
using Microsoft.Practices.Unity;

namespace Lisimba.Cmd.Business
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
            return unityContainer.Resolve<IGate>(gateId);
        }
    }
}
