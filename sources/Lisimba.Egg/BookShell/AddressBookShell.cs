using System;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.Egg.BookShell
{
    public class AddressBookShell
    {
        private AddressBookStatus status;
        private AddressBook addressBook;
        private Contact contact;

        public event EventHandler<AddressBookChangingEventArgs> AddressBookChanging;
        public event EventHandler<AddressBookChangedEventArgs> AddressBookChanged;
        public event EventHandler AddressBookSaved;
        public event EventHandler StatusChanged;
        public event EventHandler ContactChanged;

        /// <summary>
        /// Gets the full file name of the address book or null if it's a new one.
        /// </summary>
        public string FileName { get; private set; }

        public AddressBookStatus Status
        {
            get { return status; }
            private set
            {
                status = value;
                OnStatusChanged();
            }
        }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                if (contact == value)
                    return;

                contact = value;
                OnContactChanged();
            }
        }

        public AddressBook AddressBook
        {
            get { return addressBook; }
            private set
            {
                if (addressBook == value)
                    return;

                AddressBookChangingEventArgs eva = new AddressBookChangingEventArgs();
                OnAddressBookChanging(eva);

                if (eva.Cancel)
                    return;

                if (addressBook != null)
                    addressBook.Changed -= HandleChanged;

                AddressBook oldAddressBook = addressBook;

                addressBook = value;

                if (addressBook != null)
                    addressBook.Changed += HandleChanged;

                OnAddressBookChanged(new AddressBookChangedEventArgs(oldAddressBook, addressBook));
                Contact = null;
            }
        }

        protected virtual void OnAddressBookChanging(AddressBookChangingEventArgs e)
        {
            EventHandler<AddressBookChangingEventArgs> handler = AddressBookChanging;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnAddressBookChanged(AddressBookChangedEventArgs e)
        {
            EventHandler<AddressBookChangedEventArgs> handler = AddressBookChanged;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnAddressBookSaved(EventArgs e)
        {
            EventHandler handler = AddressBookSaved;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnStatusChanged()
        {
            EventHandler handler = StatusChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnContactChanged()
        {
            EventHandler handler = ContactChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public AddressBookShell()
        {
            FileName = null;
            status = AddressBookStatus.New;
        }

        private void HandleChanged(object sender, EventArgs e)
        {
            Status = AddressBookStatus.Modified;
        }

        public string GetFriendlyName()
        {
            bool hasName = addressBook != null && !string.IsNullOrWhiteSpace(addressBook.Name);
            if (hasName)
                return addressBook.Name;

            bool hasFileName = !string.IsNullOrWhiteSpace(FileName);
            if (hasFileName) return
                FileName;

            return null;
        }

        public void LoadNew()
        {
            AddressBook = new AddressBook();
            FileName = null;
            Status = AddressBookStatus.New;
        }

        public void LoadFrom(IGate gate, string fileName)
        {
            AddressBook = gate.Load(fileName);
            FileName = fileName;
            Status = AddressBookStatus.Saved;
        }

        public void ExportTo(IGate gate, string fileName)
        {
            gate.Save(AddressBook, fileName);
        }

        public void SaveTo(IGate gate, string fileName)
        {
            gate.Save(AddressBook, fileName);
            FileName = fileName;
            Status = AddressBookStatus.Saved;

            OnAddressBookSaved(EventArgs.Empty);
        }

        public bool IsSaved
        {
            get { return Status == AddressBookStatus.Saved || Status == AddressBookStatus.New; }
        }
    }
}
