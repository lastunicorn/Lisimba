using System;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;

namespace Lisimba.Cmd
{
    class DomainData
    {
        private const string DefaultAddressBookName = "New Address Book";
        private readonly ApplicationConfiguration config;
        private readonly GateProvider gateProvider;
        public AddressBook AddressBook { get; private set; }
        public string AddressBookLocation { get; private set; }
        public IGate DefaultGate { get; set; }

        public bool ExitRequested { get; set; }

        public string DefaultGateName
        {
            get { return DefaultGate == null ? string.Empty : DefaultGate.Id; }
        }

        public string AddressBookName
        {
            get { return AddressBook == null ? null : AddressBook.Name; }
        }

        public bool IsAddressBookSaved { get; private set; }

        public DomainData(ApplicationConfiguration config, GateProvider gateProvider)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (gateProvider == null) throw new ArgumentNullException("gateProvider");

            this.config = config;
            this.gateProvider = gateProvider;

            IsAddressBookSaved = true;

            DefaultGate = CreateDefaultGate();
        }

        private IGate CreateDefaultGate()
        {
            try
            {
                return gateProvider.GetGate(config.DefaultGateName);
            }
            catch
            {
                return new EmptyGate();
            }
        }

        public void OpenAddressBook(string fileName)
        {
            if (DefaultGate == null)
                throw new Exception("No default gate is set.");

            CloseAddressBook();

            string addressBookLocation = fileName ?? config.DefaultAddressBookFileName;

            if (addressBookLocation == null)
                return;

            AddressBook = DefaultGate.Load(addressBookLocation);
            AddressBook.Changed += HandleAddressBookChanged;

            AddressBookLocation = addressBookLocation;
        }

        public void CloseAddressBook()
        {
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

            if (DefaultGate == null)
                throw new Exception("No default gate is set.");

            if (AddressBookLocation == null)
                throw new Exception("A location has to be specified.");

            DefaultGate.Save(AddressBook, AddressBookLocation);
            IsAddressBookSaved = true;
        }

        public void SaveAddressBookAs(string newLocation)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");

            if (AddressBook == null)
                throw new Exception("No address book is opened.");

            if (DefaultGate == null)
                throw new Exception("No default gate is set.");

            DefaultGate.Save(AddressBook, newLocation);
            AddressBookLocation = newLocation;
            IsAddressBookSaved = true;
        }
    }
}