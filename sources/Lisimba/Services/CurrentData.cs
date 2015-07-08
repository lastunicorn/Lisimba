// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Services
{
    class CurrentData
    {
        private readonly AddressBookShell addressBookShell;
        private Contact contact;

        public CurrentData()
        {
            addressBookShell = new AddressBookShell();
            addressBookShell.AddressBookChanged += HandleAddressBookChanged;
        }

        private void HandleAddressBookChanged(object sender, AddressBookChangedEventArgs addressBookChangedEventArgs)
        {
            Contact = null;
        }

        public AddressBookShell AddressBookShell
        {
            get { return addressBookShell; }
        }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                if (contact == value)
                    return;

                contact = value;
                OnContactChanged();
            }
        }

        public event EventHandler ContactChanged;

        protected virtual void OnContactChanged()
        {
            EventHandler handler = ContactChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public bool IsSaved
        {
            get
            {
                return AddressBookShell == null ||
                       AddressBookShell.Status == AddressBookStatus.Saved ||
                       AddressBookShell.Status == AddressBookStatus.New;
            }
        }
    }
}
