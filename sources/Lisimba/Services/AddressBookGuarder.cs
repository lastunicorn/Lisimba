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
    class AddressBookGuarder
    {
        private readonly List<AddressBookObserver> observers = new List<AddressBookObserver>();

        public AddressBookGuarder(IUnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");

            observers.Add(unityContainer.Resolve<AddressBookOpenObserver>());
            observers.Add(unityContainer.Resolve<AddressBookEnsureSaveObserver>());
        }

        public void Start()
        {
            foreach (AddressBookObserver observer in observers)
            {
                observer.Start();
            }
        }
    }
}
