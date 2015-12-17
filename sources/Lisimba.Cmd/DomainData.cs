using System;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Gating;

namespace Lisimba.Cmd
{
    class DomainData
    {
        private const string DefaultAddressBookName = "New Address Book";
        private readonly ApplicationConfiguration config;
        public AddressBook AddressBook { get; private set; }
        public string AddressBookLocation { get; private set; }
        public IGate DefaultGate { get; private set; }

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

        public DomainData(ApplicationConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            this.config = config;
            DefaultGate = CreateDefaultGate();
            IsAddressBookSaved = true;
        }

        private IGate CreateDefaultGate()
        {
            switch (config.DefaultGateName)
            {
                case "ZipXmlGate":
                    return new ZipXmlGate();

                default:
                    return new EmptyGate();
            }
        }

        public void LoadAddressBook(string fileName)
        {
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
    }
}