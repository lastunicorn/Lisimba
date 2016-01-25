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

using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Setup
{
    internal class OperationsSetup
    {
        public static void Configure(AvailableOperations availableOperations, UnityContainer unityContainer)
        {
            availableOperations.Add(unityContainer.Resolve<ShowMainOperation>());
            availableOperations.Add(unityContainer.Resolve<NewAddressBookOperation>());
            availableOperations.Add(unityContainer.Resolve<OpenAddressBookOperation>());
            availableOperations.Add(unityContainer.Resolve<SaveAddressBookOperation>());
            availableOperations.Add(unityContainer.Resolve<SaveAsAddressBookOperation>());
            availableOperations.Add(unityContainer.Resolve<CloseAddressBookOperation>());
            availableOperations.Add(unityContainer.Resolve<ShowAddressBookPropertiesOperation>());
            availableOperations.Add(unityContainer.Resolve<ShowAboutOperation>());
            availableOperations.Add(unityContainer.Resolve<NewContactOperation>());
            availableOperations.Add(unityContainer.Resolve<DeleteCurrentContactOperation>());
            availableOperations.Add(unityContainer.Resolve<ApplicationExitOperation>());
        }
    }
}