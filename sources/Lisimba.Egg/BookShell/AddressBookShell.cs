using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class AddressBookShell
    {
        private AddressBookStatus status;
        private AddressBook addressBook;

        public event EventHandler<AddressBookChangingEventArgs> AddressBookChanging;
        public event EventHandler<AddressBookChangedEventArgs> AddressBookChanged;
        public event EventHandler AddressBookSaved;
        public event EventHandler StatusChanged;

        /// <summary>
        /// Gets the full file name of the address book or empty string if is a new one.
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

        public AddressBookShell()
        {
            FileName = null;
            Status = AddressBookStatus.New;
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

            OnAddressBookSaved(EventArgs.Empty);
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

            return hasFileName ? FileName : null;
        }

        public void ExportTo(IGate gate, string fileName)
        {
            gate.Save(AddressBook, fileName);
            FileName = fileName;
        }

        public void SaveTo(IGate gate, string fileName)
        {
            gate.Save(AddressBook, fileName);

            Status = AddressBookStatus.Saved;
            OnAddressBookSaved(EventArgs.Empty);
        }
    }
}
