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
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.BookShell
{
    /// <summary>
    /// Contains an opened address book and metainformation about it like the location
    /// from where it was openes and the gate used.
    /// </summary>
    class AddressBookShell
    {
        private AddressBookStatus status;

        public AddressBook AddressBook { get; private set; }
        public IGate Gate { get; set; }
        public string Location { get; set; }

        public AddressBookStatus Status
        {
            get { return status; }
            private set
            {
                status = value;
                OnStatusChanged();
            }
        }

        public event EventHandler Saved;
        public event EventHandler StatusChanged;

        public AddressBookShell(AddressBook addressBook)
            : this(addressBook, null, null)
        {
        }

        public AddressBookShell(AddressBook addressBook, IGate gate)
            : this(addressBook, gate, null)
        {
        }

        public AddressBookShell(AddressBook addressBook, IGate gate, string location)
        {
            if (addressBook == null) throw new ArgumentNullException("addressBook");

            AddressBook = addressBook;
            Gate = gate;
            Location = location;

            Status = AddressBookStatus.New;

            AddressBook.Changed += HandleAddressBookChanged;
        }

        private void HandleAddressBookChanged(object sender, EventArgs e)
        {
            Status = AddressBookStatus.Modified;
        }

        public string GetFriendlyName()
        {
            bool hasName = !string.IsNullOrWhiteSpace(AddressBook.Name);
            if (hasName)
                return AddressBook.Name;

            bool hasFileName = !string.IsNullOrWhiteSpace(Location);
            if (hasFileName)
                return Location;

            return "< Unnamed >";
        }

        public void SaveAddressBook()
        {
            if (Gate == null)
                throw new ApplicationException(LocalizedResources.NoGateWasSpecifiedError);

            if (Location == null)
                throw new ApplicationException(LocalizedResources.NoLocationWasSpecifiedError);

            Gate.Save(AddressBook, Location);
            Status = AddressBookStatus.Saved;

            OnSaved();
        }

        public void SaveAddressBook(string newLocation)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");

            if (Gate == null)
                throw new ApplicationException(LocalizedResources.NoGateWasSpecifiedError);

            Gate.Save(AddressBook, newLocation);
            Location = newLocation;
            Status = AddressBookStatus.Saved;

            OnSaved();
        }

        public void SaveAddressBook(string newLocation, IGate gate)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");
            if (gate == null) throw new ArgumentNullException("gate");

            gate.Save(AddressBook, newLocation);
            Location = newLocation;
            Gate = gate;
            Status = AddressBookStatus.Saved;

            OnSaved();
        }

        protected virtual void OnSaved()
        {
            EventHandler handler = Saved;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnStatusChanged()
        {
            EventHandler handler = StatusChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}