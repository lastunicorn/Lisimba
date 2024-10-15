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
using System.IO;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Business.Properties;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;

namespace DustInTheWind.Lisimba.Business.AddressBookManagement
{
    /// <summary>
    /// Contains and manages the opened address books.
    /// </summary>
    public class AddressBooks
    {
        private readonly RecentFiles recentFiles;
        private readonly Gates gates;
        private AddressBookShell current;
        private Contact currentContact;

        public event EventHandler<AddressBookChangedEventArgs> AddressBookChanged;
        public event EventHandler ContactChanging;
        public event EventHandler ContactChanged;
        public event EventHandler<ContactDeletingEventArgs> ContactDeleting;

        public event EventHandler<AddressBookOpenedEventArgs> AddressBookOpened;
        public event EventHandler AddressBookSaved;
        public event EventHandler<AddressBookClosingEventArgs> AddressBookClosing;
        public event EventHandler<AddressBookClosedEventArgs> AddressBookClosed;

        public event EventHandler<NewLocationNeededEventArgs> NewLocationNeeded;
        public event EventHandler<GateNeededEventArgs> GateNeeded;

        public AddressBookShell Current
        {
            get => current;
            private set
            {
                if (current == value)
                    return;

                if (current != null)
                {
                    current.GateNeeded -= HandleGateNeeded;
                    current.NewLocationNeeded -= HandleNewLocationNeeded;
                    current.Saved -= HandleCurrentAddressBookSaved;
                }

                AddressBookShell oldAddressBook = current;

                CurrentContact = null;
                current = value;

                if (current != null)
                {
                    current.GateNeeded += HandleGateNeeded;
                    current.NewLocationNeeded += HandleNewLocationNeeded;
                    current.Saved += HandleCurrentAddressBookSaved;
                }

                OnAddressBookChanged(new AddressBookChangedEventArgs(oldAddressBook, current));
            }
        }

        public Contact CurrentContact
        {
            get => currentContact;
            set
            {
                if (ReferenceEquals(currentContact, value))
                    return;

                OnContactChanging();
                currentContact = value;
                OnContactChanged();
            }
        }

        public AddressBooks(RecentFiles recentFiles, Gates gates)
        {
            this.recentFiles = recentFiles ?? throw new ArgumentNullException(nameof(recentFiles));
            this.gates = gates ?? throw new ArgumentNullException(nameof(gates));
        }

        private void HandleGateNeeded(object sender, GateNeededEventArgs e)
        {
            bool existsDefaultGate = gates.DefaultGate != null && gates.DefaultGate.GetType() != typeof(EmptyGate);

            if (existsDefaultGate)
                e.Gate = gates.DefaultGate;
            else
                OnGateNeeded(e);
        }

        private void HandleNewLocationNeeded(object sender, NewLocationNeededEventArgs e)
        {
            OnNewLocationNeeded(e);
        }

        private void HandleCurrentAddressBookSaved(object sender, EventArgs e)
        {
            AddressBookShell addressBookShell = sender as AddressBookShell;

            if (addressBookShell == null)
                return;

            AddFileToRecentFileList(addressBookShell.Location, addressBookShell.Gate);

            OnAddressBookSaved(EventArgs.Empty);
        }

        public void CreateNewAddressBook(string name)
        {
            bool allowToContinue = CloseCurrentAddressBook();
            if (!allowToContinue)
                return;

            string addressBookName = name ?? Resources.DefaultAddressBookName;
            AddressBook addressBook = new AddressBook { Name = addressBookName };
            Current = new AddressBookShell(addressBook);

            AddressBookOpenResult result = new AddressBookOpenResult { Success = true };

            OnAddressBookOpened(new AddressBookOpenedEventArgs(result));
        }

        public void OpenAddressBook(IGate gate, string connectionString = null)
        {
            if (gate == null) throw new ArgumentNullException(nameof(gate));

            bool allowToContinue = CloseCurrentAddressBook();
            if (!allowToContinue)
                return;

            AddressBook addressBook = gate.Load(connectionString);
            Current = new AddressBookShell(addressBook, gate, connectionString);

            AddFileToRecentFileList(connectionString, gate);

            AddressBookOpenResult result = new AddressBookOpenResult
            {
                Success = true,
                Warnings = gate.Warnings
            };

            OnAddressBookOpened(new AddressBookOpenedEventArgs(result));
        }

        private void AddFileToRecentFileList(string connectionString, IGate gate)
        {
            string fileFullPath = connectionString == null
                ? gate.Name
                : gate is FileGate ? Path.GetFullPath(connectionString) : string.Empty;

            recentFiles.AddRecentFile(fileFullPath, gate);
        }

        public void SaveCurrentAddressBook()
        {
            if (Current == null)
                throw new LisimbaException(Resources.NoAddessBookOpenedError);

            Current.SaveAddressBook();
        }

        public bool CloseCurrentAddressBook()
        {
            if (Current == null)
                return true;

            bool allowToContinue = PrepareForClose();
            if (!allowToContinue)
                return false;

            AddressBookShell oldAddressBookShell = Current;

            CurrentContact = null;
            Current = null;

            OnAddressBookClosed(new AddressBookClosedEventArgs(oldAddressBookShell));

            return true;
        }

        private bool PrepareForClose()
        {
            bool addressBookNeedsSave = Current.Status == AddressBookStatus.Modified;
            AddressBookClosingEventArgs eva = new AddressBookClosingEventArgs(Current);
            OnAddressBookClosing(eva);

            if (eva.Cancel)
                return false;

            if (addressBookNeedsSave)
            {
                if (eva.SaveAddressBook == null)
                    throw new LisimbaException(Resources.CloseAddressBooks_NotSavedError);

                if (eva.SaveAddressBook.Value)
                {
                    bool allowToContinue = Current.SaveAddressBook();

                    if (!allowToContinue)
                        return false;
                }
            }

            return true;
        }

        public void DeleteCurrentContact()
        {
            Contact contactToDelete = CurrentContact;

            if (contactToDelete == null)
                return;

            ContactDeletingEventArgs eva = new ContactDeletingEventArgs(contactToDelete);
            OnContactDeleting(eva);

            if (eva.Cancel)
                return;

            Current.DeleteContact(contactToDelete);

            if (ReferenceEquals(contactToDelete, CurrentContact))
                CurrentContact = null;
        }

        #region Event Invocators

        protected virtual void OnAddressBookChanged(AddressBookChangedEventArgs e)
        {
            AddressBookChanged?.Invoke(this, e);
        }

        protected virtual void OnAddressBookSaved(EventArgs e)
        {
            AddressBookSaved?.Invoke(this, e);
        }

        protected virtual void OnContactChanging()
        {
            ContactChanging?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnContactChanged()
        {
            ContactChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnContactDeleting(ContactDeletingEventArgs e)
        {
            ContactDeleting?.Invoke(this, e);
        }

        protected virtual void OnAddressBookClosing(AddressBookClosingEventArgs e)
        {
            AddressBookClosing?.Invoke(this, e);
        }

        protected virtual void OnAddressBookClosed(AddressBookClosedEventArgs e)
        {
            AddressBookClosed?.Invoke(this, e);
        }

        protected virtual void OnAddressBookOpened(AddressBookOpenedEventArgs e)
        {
            AddressBookOpened?.Invoke(this, e);
        }

        protected virtual void OnNewLocationNeeded(NewLocationNeededEventArgs e)
        {
            NewLocationNeeded?.Invoke(this, e);
        }

        protected virtual void OnGateNeeded(GateNeededEventArgs e)
        {
            GateNeeded?.Invoke(this, e);
        }

        #endregion
    }
}