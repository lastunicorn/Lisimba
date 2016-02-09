// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.GateModel;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Setup
{
    internal class GatesSetup
    {
        public static void Configure(AvailableGates availableGates, UnityContainer unityContainer)
        {
            GateProvider gateProvider = unityContainer.Resolve<GateProvider>();

            foreach (IGate gate in gateProvider.GetAllGates())
                availableGates.Add(gate);
        }
    }
}