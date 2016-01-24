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

using System;
using System.Collections.Generic;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.CommandLine.Observers;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.CommandLine.Business
{
    internal class ObserverFactory
    {
        private readonly UnityContainer unityContainer;

        public ObserverFactory(UnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");

            this.unityContainer = unityContainer;
        }

        public List<IObserver> CreateObservers()
        {
            return new List<IObserver>
            {
                unityContainer.Resolve<AddressBookOpenedObserver>(),
                unityContainer.Resolve<AddressBookSavedObserver>(),
                unityContainer.Resolve<AddressBookClosingObserver>(),
                unityContainer.Resolve<AddressBookClosedObserver>(),
                unityContainer.Resolve<ApplicationEndedObserver>()
            };
        }
    }
}