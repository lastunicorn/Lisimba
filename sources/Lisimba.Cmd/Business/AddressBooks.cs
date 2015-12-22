using System;
using DustInTheWind.Lisimba.Egg.Book;

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

            IsAddressBookSaved = true;
        }

        public void OpenAddressBook(string fileName)
        {
            if (gates.DefaultGate == null)
                throw new Exception("No default gate is set.");

            CloseAddressBook();

            string addressBookLocation = fileName ?? config.DefaultAddressBookFileName;

            if (addressBookLocation == null)
                return;

            AddressBook = gates.DefaultGate.Load(addressBookLocation);
            AddressBook.Changed += HandleAddressBookChanged;

            AddressBookLocation = addressBookLocation;
        }

        public void CloseAddressBook()
        {
            bool allowToContinue = guarder.EnsureSave();

            if (!allowToContinue)
                return;

            if (AddressBook != null)
                AddressBook.Changed -= HandleAddressBookChanged;

            AddressBook = null;
            AddressBookLocation = null;
            IsAddressBookSaved = true;
        }

        public void NewAddressBook(string name)
        {
            CloseAddressBook();

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
                throw new Exception("No address book is opened.");

            if (gates.DefaultGate == null)
                throw new Exception("No default gate is set.");

            if (AddressBookLocation == null)
                throw new Exception("A location has to be specified.");

            gates.DefaultGate.Save(AddressBook, AddressBookLocation);
            IsAddressBookSaved = true;
        }

        public void SaveAddressBookAs(string newLocation)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");

            if (AddressBook == null)
                throw new Exception("No address book is opened.");

            if (gates.DefaultGate == null)
                throw new Exception("No default gate is set.");

            gates.DefaultGate.Save(AddressBook, newLocation);
            AddressBookLocation = newLocation;
            IsAddressBookSaved = true;
        }
    }
}