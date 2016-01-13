// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Observers;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Services
{
    /// <summary>
    /// Listens for any address book that is being closed and asks the user if
    /// he wants to save it betfore closing.
    /// </summary>
    class ActiveObservers
    {
        private readonly IUnityContainer unityContainer;
        private List<IObserver> observers;

        public ActiveObservers(IUnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");

            this.unityContainer = unityContainer;
        }

        public void Start()
        {
            if (observers == null)
                observers = CreateObservers();

            foreach (IObserver observer in observers)
            {
                observer.Start();
            }
        }

        private List<IObserver> CreateObservers()
        {
            return new List<IObserver>
            {
                unityContainer.Resolve<AddressBookOpenObserver>(), 
                unityContainer.Resolve<AddressBookSaveObserver>(),
                unityContainer.Resolve<AddressBookEnsureSaveObserver>(),
                unityContainer.Resolve<AddressBookClosedObserver>()
            };
        }
    }
}
