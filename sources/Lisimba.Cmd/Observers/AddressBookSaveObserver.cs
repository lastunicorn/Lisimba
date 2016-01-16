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
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;

namespace DustInTheWind.Lisimba.Cmd.Observers
{
    class AddressBookSaveObserver : IObserver
    {
        private readonly AddressBookSaveObserverConsole console;
        private readonly OpenedAddressBooks openedAddressBooks;

        public AddressBookSaveObserver(OpenedAddressBooks openedAddressBooks, AddressBookSaveObserverConsole console)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.openedAddressBooks = openedAddressBooks;
            this.console = console;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookSaved += HandleAddressBookSaved;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookSaved -= HandleAddressBookSaved;
        }

        private void HandleAddressBookSaved(object sender, EventArgs e)
        {
            console.DisplayAddressBookSaveSuccess(openedAddressBooks.Current.GetFriendlyName(), openedAddressBooks.Current.Location);
        }
    }
}