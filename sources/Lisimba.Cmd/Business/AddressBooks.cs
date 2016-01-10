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
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    /// <summary>
    /// Contains and manages the opened address books.
    /// </summary>
    class AddressBooks
    {
        private readonly RecentFiles recentFiles;

        public AddressBookShell Current { get; private set; }

        public event EventHandler Saved;
        public event CancelEventHandler Closing;
        public event EventHandler Closed;

        public AddressBooks(RecentFiles recentFiles)
        {
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.recentFiles = recentFiles;
        }

        public void OpenAddressBook(string fileName, IGate gate)
        {
            CancelEventArgs eva = new CancelEventArgs();
            OnClosing(eva);

            if (eva.Cancel)
                return;

            CloseAddressBookInternal();

            AddressBook addressBook = gate.Load(fileName);
            Current = new AddressBookShell(addressBook, gate, fileName);

            recentFiles.AddRecentFile(fileName, gate);

            if (Current != null)
                Current.Saved += HandleCurrentSaved;
        }

        public void CloseAddressBook()
        {
            CancelEventArgs eva = new CancelEventArgs();
            OnClosing(eva);

            if (eva.Cancel)
                return;

            CloseAddressBookInternal();
        }

        private void CloseAddressBookInternal()
        {
            if (Current != null)
                Current.Saved -= HandleCurrentSaved;

            Current = null;
            OnClosed();
        }

        private void HandleCurrentSaved(object sender, EventArgs eventArgs)
        {
            OnSaved();
        }

        public void NewAddressBook(string name)
        {
            CancelEventArgs eva = new CancelEventArgs();
            OnClosing(eva);

            if (eva.Cancel)
                return;

            CloseAddressBookInternal();

            string addressBookName = name ?? Resources.DefaultAddressBookName;
            AddressBook addressBook = new AddressBook { Name = addressBookName };
            Current = new AddressBookShell(addressBook);

            if (Current != null)
                Current.Saved += HandleCurrentSaved;
        }

        public void SaveAddressBook()
        {
            if (Current == null)
                throw new ApplicationException(Resources.NoAddessBookOpenedError);

            Current.SaveAddressBook();
        }

        public void SaveAddressBookAs(string newLocation)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");

            if (Current == null)
                throw new ApplicationException(Resources.NoAddessBookOpenedError);

            Current.SaveAddressBook(newLocation);
        }

        protected virtual void OnSaved()
        {
            EventHandler handler = Saved;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
            CancelEventHandler handler = Closing;
            
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnClosed()
        {
            EventHandler handler = Closed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}