using System;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Gating;

namespace Lisimba.Cmd
{
    class DomainData
    {
        private readonly ApplicationConfiguration config;
        public AddressBook AddressBook { get; private set; }
        public string AddressBookLocation { get; private set; }
        public IGate DefaultGate { get; private set; }

        public bool ExitRequested { get; set; }

        public string DefaultGateName
        {
            get { return DefaultGate == null ? string.Empty : DefaultGate.Id; }
        }

        public DomainData(ApplicationConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            this.config = config;
            DefaultGate = CreateDefaultGate();
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
            string addressBookLocation = fileName ?? config.DefaultAddressBookFileName;

            if (addressBookLocation == null)
                return;

            AddressBook = DefaultGate.Load(addressBookLocation);
            AddressBookLocation = addressBookLocation;
        }

        public void CloseAddressBook()
        {
            AddressBook = null;
            AddressBookLocation = null;
        }

        public string GetAddressBookName()
        {
            return AddressBook == null ? null : AddressBook.Name;
        }
    }
}