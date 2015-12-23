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
        private const string DefaultAddressBookName = "New Address Book";

        private readonly ApplicationConfiguration config;
        private readonly Gates gates;
        private readonly AddressBookGuarder guarder;

        public AddressBook AddressBook { get; private set; }
        public string AddressBookLocation { get; private set; }

        public string AddressBookName
        {
            get { return AddressBook == null ? null : AddressBook.Name; }
        }

        public bool IsAddressBookSaved { get; private set; }

        public AddressBooks(ApplicationConfiguration config, Gates gates, AddressBookGuarder guarder)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (gates == null) throw new ArgumentNullException("gates");
            if (guarder == null) throw new ArgumentNullException("guarder");

            this.config = config;
            this.gates = gates;
            this.guarder = guarder;

            guarder.AddressBooks = this;

            IsAddressBookSaved = true;
        }

        public void OpenAddressBook(string fileName, IGate gate)
        {
            bool allowToContinue = guarder.EnsureSave();

            if (!allowToContinue)
                return;

            CloseAddressBookInternal();

            AddressBook = gate.Load(fileName);
            AddressBook.Changed += HandleAddressBookChanged;

            AddressBookLocation = fileName;

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

            CloseAddressBookInternal();
        }

        private void CloseAddressBookInternal()
        {
            if (AddressBook != null)
                AddressBook.Changed -= HandleAddressBookChanged;

            AddressBook = null;
            AddressBookLocation = null;
            IsAddressBookSaved = true;
        }

        public void NewAddressBook(string name)
        {
            bool allowToContinue = guarder.EnsureSave();

            if (!allowToContinue)
                return;

            CloseAddressBookInternal();

            string addressBookName = name ?? DefaultAddressBookName;
            AddressBook = new AddressBook { Name = addressBookName };
            AddressBook.Changed += HandleAddressBookChanged;
        }

        private void HandleAddressBookChanged(object sender, EventArgs eventArgs)
        {
            IsAddressBookSaved = false;
        }

        public void SaveAddressBook()
        {
            if (AddressBook == null)
                throw new ApplicationException(Resources.NoAddessBookOpenedError);

            if (gates.DefaultGate == null)
                throw new ApplicationException(Resources.NoDefaultGateError);

            if (AddressBookLocation == null)
                throw new ApplicationException(Resources.NoLocationWasSpecifiedError);

            gates.DefaultGate.Save(AddressBook, AddressBookLocation);
            IsAddressBookSaved = true;
        }

        public void SaveAddressBookAs(string newLocation)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");

            if (AddressBook == null)
                throw new ApplicationException(Resources.NoAddessBookOpenedError);

            if (gates.DefaultGate == null)
                throw new ApplicationException(Resources.NoDefaultGateError);

            gates.DefaultGate.Save(AddressBook, newLocation);
            AddressBookLocation = newLocation;
            IsAddressBookSaved = true;
        }
    }
}