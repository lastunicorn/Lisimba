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
using System.ComponentModel;
using System.IO;
using DustInTheWind.Lisimba.Common.Properties;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Common.AddressBookManagement
{
    /// <summary>
    /// Contains and manages the opened address books.
    /// </summary>
    public class OpenedAddressBooks
    {
        private readonly RecentFiles recentFiles;
        private AddressBookShell current;
        private Contact contact;

        public event EventHandler<AddressBookChangedEventArgs> AddressBookChanged;
        public event EventHandler ContactChanged;

        public event EventHandler<AddressBookOpenedEventArgs> AddressBookOpened;
        public event EventHandler AddressBookSaved;
        public event CancelEventHandler AddressBookClosing;
        public event EventHandler<AddressBookClosedEventArgs> AddressBookClosed;

        public AddressBookShell Current
        {
            get { return current; }
            private set
            {
                if (current == value)
                    return;

                AddressBookShell oldAddressBook = current;

                current = value;
                Contact = null;

                OnAddressBookChanged(new AddressBookChangedEventArgs(oldAddressBook, current));
            }
        }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                if (ReferenceEquals(contact, value))
                    return;

                contact = value;
                OnContactChanged();
            }
        }

        public OpenedAddressBooks(RecentFiles recentFiles)
        {
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.recentFiles = recentFiles;
        }

        protected virtual void OnAddressBookChanged(AddressBookChangedEventArgs e)
        {
            EventHandler<AddressBookChangedEventArgs> handler = AddressBookChanged;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnAddressBookSaved(EventArgs e)
        {
            EventHandler handler = AddressBookSaved;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnContactChanged()
        {
            EventHandler handler = ContactChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
            CancelEventHandler handler = AddressBookClosing;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnClosed(AddressBookClosedEventArgs e)
        {
            EventHandler<AddressBookClosedEventArgs> handler = AddressBookClosed;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnOpened(AddressBookOpenedEventArgs e)
        {
            EventHandler<AddressBookOpenedEventArgs> handler = AddressBookOpened;

            if (handler != null)
                handler(this, e);
        }

        public void CreateNewAddressBook(string name)
        {
            bool allowToContinue = CloseAddressBook();

            if (!allowToContinue)
                return;

            string addressBookName = name ?? Resources.DefaultAddressBookName;
            AddressBook addressBook = new AddressBook { Name = addressBookName };
            AddressBookShell addressBookShell = new AddressBookShell(addressBook);
            addressBookShell.Saved += HandleAddressBookShellSaved;

            Current = addressBookShell;

            AddressBookOpenResult result = new AddressBookOpenResult { Success = true };

            OnOpened(new AddressBookOpenedEventArgs(result));
        }

        private void HandleAddressBookShellSaved(object sender, EventArgs e)
        {
            AddressBookShell addressBookShell = sender as AddressBookShell;

            if (addressBookShell == null)
                return;

            AddFileToRecentFileList(addressBookShell.Location, addressBookShell.Gate);

            OnAddressBookSaved(EventArgs.Empty);
        }

        public void OpenAddressBook(string fileName, IGate gate)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (gate == null) throw new ArgumentNullException("gate");

            bool allowToContinue = CloseAddressBook();

            if (!allowToContinue)
                return;

            AddressBook addressBook = gate.Load(fileName);
            AddressBookShell addressBookShell = new AddressBookShell(addressBook, gate, fileName);
            addressBookShell.Saved += HandleAddressBookShellSaved;

            Current = addressBookShell;

            AddFileToRecentFileList(fileName, gate);

            AddressBookOpenResult result = new AddressBookOpenResult
            {
                Success = true,
                Warnings = gate.Warnings
            };

            OnOpened(new AddressBookOpenedEventArgs(result));
        }

        private void AddFileToRecentFileList(string fileName, IGate gate)
        {
            string fileFullPath = Path.GetFullPath(fileName);
            recentFiles.AddRecentFile(fileFullPath, gate);
        }

        public bool CloseAddressBook()
        {
            if (Current == null)
                return true;

            CancelEventArgs eva = new CancelEventArgs();
            OnClosing(eva);

            if (eva.Cancel)
                return false;

            AddressBookShell oldAddressBookShell = Current;

            oldAddressBookShell.Saved -= HandleAddressBookShellSaved;

            Contact = null;
            Current = null;

            OnClosed(new AddressBookClosedEventArgs(oldAddressBookShell));

            return true;
        }
    }
}