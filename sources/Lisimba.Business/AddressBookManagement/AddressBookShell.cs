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
using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Business.Actions;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Business.Properties;

namespace DustInTheWind.Lisimba.Business.AddressBookManagement
{
    /// <summary>
    /// Contains an opened address book and meta information about it, like the location
    /// from where it was openes and the gate used.
    /// </summary>
    public class AddressBookShell
    {
        private AddressBookStatus status;

        public AddressBook AddressBook { get; private set; }
        public IGate Gate { get; private set; }
        public string Location { get; private set; }

        public AddressBookStatus Status
        {
            get { return status; }
            private set
            {
                status = value;
                OnStatusChanged();
            }
        }

        public ActionQueue ActionQueue { get; private set; }

        public event EventHandler Saved;
        public event EventHandler StatusChanged;

        public event EventHandler<NewLocationNeededEventArgs> NewLocationNeeded;
        public event EventHandler<GateNeededEventArgs> GateNeeded;

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

            status = location == null ? AddressBookStatus.New : AddressBookStatus.Saved;

            ActionQueue = new ActionQueue();

            AddressBook.Changed += HandleAddressBookChanged;
            AddressBook.ContactContentChanged += HandleContactContentChanged;
        }

        private void HandleAddressBookChanged(object sender, EventArgs e)
        {
            Status = AddressBookStatus.Modified;
        }

        private void HandleContactContentChanged(object sender, ContactContentChangedEventArgs e)
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

            return Resources.NoAddressBookName;
        }

        public bool SaveAddressBook()
        {
            return SaveInternal(Location, Gate);
        }

        public bool SaveAddressBook(string newLocation)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");

            return SaveInternal(newLocation, Gate);
        }

        public bool SaveAddressBook(string newLocation, IGate gate)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");
            if (gate == null) throw new ArgumentNullException("gate");

            return SaveInternal(newLocation, gate);
        }

        private bool SaveInternal(string newLocation, IGate gate)
        {
            if (gate == null)
                gate = GetGateForSave();

            if (gate == null)
                return false;

            if (newLocation == null)
                newLocation = GetLocationForSave();

            if (newLocation == null)
                return false;

            (gate as FileGate).Save(AddressBook, newLocation);

            Location = newLocation;
            Gate = gate;

            Status = AddressBookStatus.Saved;

            OnSaved();

            return true;
        }

        private string GetLocationForSave()
        {
            NewLocationNeededEventArgs eva = new NewLocationNeededEventArgs(this);
            OnNewLocationNeeded(eva);

            return eva.Cancel
                ? null
                : eva.NewLocation;
        }

        private IGate GetGateForSave()
        {
            GateNeededEventArgs eva = new GateNeededEventArgs(this);
            OnGateNeeded(eva);

            return eva.Cancel
                ? null
                : eva.Gate;
        }

        public void Export(string location, IGate gate)
        {
            if (location == null) throw new ArgumentNullException("location");
            if (gate == null) throw new ArgumentNullException("gate");

            (gate as FileGate).Save(AddressBook, location);
        }

        public void ChangeAddressBookName(string newName)
        {
            IAction action = new RenameAddressBookAction(AddressBook, newName);
            ActionQueue.Do(action);
        }

        public void AddContact(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException("contact");

            IAction action = new AddContactAction(AddressBook, contact);
            ActionQueue.Do(action);
        }

        public void DeleteContact(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException("contact");

            IAction action = new DeleteContactAction(AddressBook, contact);
            ActionQueue.Do(action);
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

        protected virtual void OnNewLocationNeeded(NewLocationNeededEventArgs e)
        {
            EventHandler<NewLocationNeededEventArgs> handler = NewLocationNeeded;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnGateNeeded(GateNeededEventArgs e)
        {
            EventHandler<GateNeededEventArgs> handler = GateNeeded;

            if (handler != null)
                handler(this, e);
        }
    }
}