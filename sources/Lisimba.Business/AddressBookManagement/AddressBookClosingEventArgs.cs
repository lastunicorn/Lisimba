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
using System.ComponentModel;

namespace DustInTheWind.Lisimba.Business.AddressBookManagement
{
    public class AddressBookClosingEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Gets the address book that is about to be closed.
        /// </summary>
        public AddressBookShell AddressBook { get; private set; }

        /// <summary>
        /// Gets or sets a value that specifies if the address book will require saving before closing.
        /// </summary>
        public bool? SaveAddressBook { get; set; }

        public AddressBookClosingEventArgs(AddressBookShell addressBook)
        {
            if (addressBook == null) throw new ArgumentNullException("addressBook");

            AddressBook = addressBook;
        }
    }
}