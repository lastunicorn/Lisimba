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
using Lisimba.Cmd.Properties;

namespace Lisimba.Cmd.Business
{
    class AddressBooks
    {
        private readonly ApplicationConfiguration config;
        private readonly AddressBookGuarder guarder;

        public AddressBookShell Current { get; private set; }

        public event EventHandler Saved;
        public event EventHandler Closed;

        public AddressBooks(ApplicationConfiguration config, AddressBookGuarder guarder)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (guarder == null) throw new ArgumentNullException("guarder");

            this.config = config;
            this.guarder = guarder;

            guarder.AddressBooks = this;
        }

        public void OpenAddressBook(string fileName, IGate gate)
        {
            CloseAddressBook();

            AddressBook addressBook = gate.Load(fileName);
            Current = new AddressBookShell(addressBook, gate, fileName);

            config.LastAddressBook = new AddressBookLocationInfo
            {
                FileName = fileName,
                GateId = gate.Id
            };
        }

        public void CloseAddressBook()
        {
            bool allowToContinue = guarder.EnsureSave();

            if (!allowToContinue)
                return;

            Current = null;
            OnClosed();
        }

        public void NewAddressBook(string name)
        {
            CloseAddressBook();

            string addressBookName = name ?? Resources.DefaultAddressBookName;
            AddressBook addressBook = new AddressBook { Name = addressBookName };
            Current = new AddressBookShell(addressBook);
        }

        public void SaveAddressBook()
        {
            if (Current == null)
                throw new ApplicationException(Resources.NoAddessBookOpenedError);

            Current.SaveAddressBook();
            OnSaved();
        }

        public void SaveAddressBookAs(string newLocation)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");

            if (Current == null)
                throw new ApplicationException(Resources.NoAddessBookOpenedError);

            Current.SaveAddressBook(newLocation);
            OnSaved();
        }

        protected virtual void OnSaved()
        {
            EventHandler handler = Saved;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnClosed()
        {
            EventHandler handler = Closed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}