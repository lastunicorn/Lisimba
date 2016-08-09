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
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Business.ObservingModel;
using DustInTheWind.Lisimba.WinForms.LocationProviders;
using DustInTheWind.Lisimba.WinForms.Properties;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.Observers
{
    internal class AddressBookSaveObserver : IObserver
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly FileLocationProvider fileLocationProvider;
        private readonly AvailableGates availableGates;
        private readonly ApplicationStatus applicationStatus;

        public AddressBookSaveObserver(OpenedAddressBooks openedAddressBooks, FileLocationProvider fileLocationProvider,
            AvailableGates availableGates, ApplicationStatus applicationStatus)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (fileLocationProvider == null) throw new ArgumentNullException("fileLocationProvider");
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");

            this.openedAddressBooks = openedAddressBooks;
            this.fileLocationProvider = fileLocationProvider;
            this.availableGates = availableGates;
            this.applicationStatus = applicationStatus;
        }

        public void Start()
        {
            openedAddressBooks.NewLocationNeeded += HandleAddressBooksNewLocationNeeded;
            openedAddressBooks.GateNeeded += HandleOpenedAddressBooksGateNeeded;
            openedAddressBooks.AddressBookSaved += HandleAddressBookSaved;
        }

        public void Stop()
        {
            openedAddressBooks.NewLocationNeeded -= HandleAddressBooksNewLocationNeeded;
            openedAddressBooks.GateNeeded -= HandleOpenedAddressBooksGateNeeded;
            openedAddressBooks.AddressBookSaved -= HandleAddressBookSaved;
        }

        private void HandleAddressBooksNewLocationNeeded(object sender, NewLocationNeededEventArgs e)
        {
            string newLocation = fileLocationProvider.AskToSave();

            if (string.IsNullOrEmpty(newLocation))
                e.Cancel = true;
            else
                e.NewLocation = newLocation;
        }

        private void HandleOpenedAddressBooksGateNeeded(object sender, GateNeededEventArgs e)
        {
            if (availableGates.DefaultGate == null)
                throw new LisimbaException(LocalizedResources.NoDefaultGateExists);

            IGate newGate = availableGates.DefaultGate;

            if (newGate == null)
                e.Cancel = true;
            else
                e.Gate = newGate;
        }

        private void HandleAddressBookSaved(object sender, EventArgs e)
        {
            int contactCount = openedAddressBooks.Current.AddressBook.Contacts.Count;
            applicationStatus.StatusText = string.Format(Resources.AddressBookSaved_StatusText, contactCount);
        }
    }
}