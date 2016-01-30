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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Observers
{
    internal class AddressBookSavingObserver : IObserver
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly UserInterface userInterface;
        private readonly AvailableGates availableGates;

        public AddressBookSavingObserver(OpenedAddressBooks openedAddressBooks, UserInterface userInterface, AvailableGates availableGates)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.openedAddressBooks = openedAddressBooks;
            this.userInterface = userInterface;
            this.availableGates = availableGates;
        }

        public void Start()
        {
            openedAddressBooks.NewLocationNeeded += HandleAddressBooksNewLocationNeeded;
            openedAddressBooks.GateNeeded += HandleOpenedAddressBooksGateNeeded;
        }

        public void Stop()
        {
            openedAddressBooks.NewLocationNeeded -= HandleAddressBooksNewLocationNeeded;
            openedAddressBooks.GateNeeded -= HandleOpenedAddressBooksGateNeeded;
        }

        private void HandleAddressBooksNewLocationNeeded(object sender, NewLocationNeededEventArgs e)
        {
            string newLocation = userInterface.AskToSaveLsbFile();

            if (string.IsNullOrEmpty(newLocation))
                e.Cancel = true;
            else
                e.NewLocation = newLocation;
        }

        private void HandleOpenedAddressBooksGateNeeded(object sender, GateNeededEventArgs e)
        {
            if(availableGates.DefaultGate == null)
                throw new LisimbaException(LocalizedResources.NoDefaultGateExists);

            IGate newGate = availableGates.DefaultGate;

            if (newGate == null)
                e.Cancel = true;
            else
                e.Gate = newGate;
        }
    }
}