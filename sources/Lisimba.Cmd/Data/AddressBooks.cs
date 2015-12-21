using System;
using DustInTheWind.Lisimba.Egg.Book;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Data
{
    class AddressBooks
    {
        private const string DefaultAddressBookName = "New Address Book";

        private readonly ApplicationConfiguration config;
        private readonly Gates gates;
        private readonly ConsoleView consoleView;

        public AddressBook AddressBook { get; private set; }
        public string AddressBookLocation { get; private set; }

        public string AddressBookName
        {
            get { return AddressBook == null ? null : AddressBook.Name; }
        }

        public bool IsAddressBookSaved { get; private set; }

        public AddressBooks(ApplicationConfiguration config, Gates gates, ConsoleView consoleView)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (gates == null) throw new ArgumentNullException("gates");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.config = config;
            this.gates = gates;
            this.consoleView = consoleView;

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
            bool allowToContinue = EnsureSave();

            if (!allowToContinue)
                return;

            if (AddressBook != null)
                AddressBook.Changed -= HandleAddressBookChanged;

            AddressBook = null;
            AddressBookLocation = null;
            IsAddressBookSaved = true;
        }

        private bool EnsureSave()
        {
            if (IsAddressBookSaved)
                return true;

            bool? needSave = consoleView.AskToSaveAddressBook();

            if (needSave == null)
                return false;

            if (!needSave.Value)
                return true;

            if (AddressBookLocation == null)
            {
                string newLocation = consoleView.AskForLocation();

                if (newLocation == null)
                    return false;

                SaveAddressBookAs(newLocation);
            }
            else
            {
                SaveAddressBook();
            }

            return true;
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