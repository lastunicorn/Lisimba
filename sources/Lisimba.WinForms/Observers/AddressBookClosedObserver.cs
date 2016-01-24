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
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Observers
{
    class AddressBookClosedObserver : IObserver
    {
        private readonly ApplicationStatus applicationStatus;
        private readonly OpenedAddressBooks openedAddressBooks;

        public AddressBookClosedObserver(OpenedAddressBooks openedAddressBooks, ApplicationStatus applicationStatus)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");

            this.openedAddressBooks = openedAddressBooks;
            this.applicationStatus = applicationStatus;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookClosed += HandleAddressBookClosed;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookClosed -= HandleAddressBookClosed;
        }

        private void HandleAddressBookClosed(object sender, AddressBookClosedEventArgs e)
        {
            applicationStatus.StatusText = LocalizedResources.AddressBookClosedSuccessStatus;
        }
    }
}