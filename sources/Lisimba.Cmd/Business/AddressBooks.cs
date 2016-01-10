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

        public event CancelEventHandler Closing;
        public event EventHandler Closed;
        public event EventHandler Opened;

        public AddressBooks(RecentFiles recentFiles)
        {
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.recentFiles = recentFiles;
        }

        public bool CreateNewAddressBook(string name)
        {
            bool allowToContinue = CloseAddressBook();

            if (!allowToContinue)
                return false;

            string addressBookName = name ?? Resources.DefaultAddressBookName;
            AddressBook addressBook = new AddressBook { Name = addressBookName };
            Current = new AddressBookShell(addressBook);

            OnOpened();

            return true;
        }

        public AddressBookLoadResult OpenAddressBook(string fileName, IGate gate)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (gate == null) throw new ArgumentNullException("gate");

            bool allowToContinue = CloseAddressBook();

            if (!allowToContinue)
                return new AddressBookLoadResult
                {
                    Success = false
                };

            AddressBook addressBook = gate.Load(fileName);
            Current = new AddressBookShell(addressBook, gate, fileName);

            AddFileToRecentFileList(fileName, gate);

            OnOpened();

            return new AddressBookLoadResult
            {
                Success = true,
                Warnings = gate.Warnings
            };
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

            Current = null;
            OnClosed();

            return true;
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

        protected virtual void OnOpened()
        {
            EventHandler handler = Opened;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}