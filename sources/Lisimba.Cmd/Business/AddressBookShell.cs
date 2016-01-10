using System;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    class AddressBookShell
    {
        public AddressBook AddressBook { get; private set; }
        public IGate Gate { get; set; }
        public string Location { get; set; }

        public bool IsAddressBookSaved { get; private set; }

        public AddressBookShell(AddressBook addressBook)
            : this(addressBook, null, null)
        {
        }

        public AddressBookShell(AddressBook addressBook, IGate gate)
            : this(addressBook, gate, null)
        {
        }

        public AddressBookShell(AddressBook addressBook, IGate gate, string location)
        {
            if (addressBook == null) throw new ArgumentNullException("addressBook");

            AddressBook = addressBook;
            Gate = gate;
            Location = location;

            IsAddressBookSaved = true;

            AddressBook.Changed += HandleAddressBookChanged;
        }

        private void HandleAddressBookChanged(object sender, EventArgs e)
        {
            IsAddressBookSaved = false;
        }

        public void SaveAddressBook()
        {
            if (Gate == null)
                throw new ApplicationException(Resources.NoDefaultGateError);

            if (Location == null)
                throw new ApplicationException(Resources.NoLocationWasSpecifiedError);

            Gate.Save(AddressBook, Location);
            IsAddressBookSaved = true;
        }

        public void SaveAddressBook(string newLocation)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");

            if (Gate == null)
                throw new ApplicationException(Resources.NoDefaultGateError);

            Gate.Save(AddressBook, newLocation);
            Location = newLocation;
            IsAddressBookSaved = true;
        }

        public void SaveAddressBook(string newLocation, IGate gate)
        {
            if (newLocation == null) throw new ArgumentNullException("newLocation");
            if (gate == null) throw new ArgumentNullException("gate");

            gate.Save(AddressBook, newLocation);
            Location = newLocation;
            Gate = gate;
            IsAddressBookSaved = true;
        }
    }
}