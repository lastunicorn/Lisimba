using System;
using System.ComponentModel;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.BookShell
{
    class AddressBookShell
    {
        private readonly UserInterface userInterface;
        private readonly CommandPool commandPool;
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
                if (ReferenceEquals(contact, value))
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
                    addressBook.Changed -= HandleAddressBookChanged;

                AddressBook oldAddressBook = addressBook;

                addressBook = value;

                if (addressBook != null)
                    addressBook.Changed += HandleAddressBookChanged;

                OnAddressBookChanged(new AddressBookChangedEventArgs(oldAddressBook, addressBook));
                Contact = null;
            }
        }

        public AddressBookShell(LisimbaApplication lisimbaApplication, UserInterface userInterface, CommandPool commandPool)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (commandPool == null) throw new ArgumentNullException("commandPool");

            this.userInterface = userInterface;
            this.commandPool = commandPool;

            status = AddressBookStatus.New;

            lisimbaApplication.Exiting += HandleLisimbaApplicationExiting;
        }

        private void HandleLisimbaApplicationExiting(object sender, CancelEventArgs e)
        {
            bool allowToContinue = CloseAddressBook();

            if (!allowToContinue)
                e.Cancel = true;
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

        private void HandleAddressBookChanged(object sender, EventArgs e)
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

            return "< Unnamed >";
        }

        public bool LoadNew()
        {
            bool allowToContinue = CloseAddressBook();

            if (!allowToContinue)
                return false;

            Contact = null;
            AddressBook = new AddressBook();
            FileName = null;
            Status = AddressBookStatus.New;

            return true;
        }

        public bool LoadFrom(IGate gate, string fileName)
        {
            bool succeeded = CloseAddressBook();

            if (!succeeded)
                return false;

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = userInterface.AskToOpenLsbFile();

                if (fileName == null)
                    return false;
            }

            Contact = null;
            AddressBook = gate.Load(fileName);
            FileName = fileName;
            Status = AddressBookStatus.Saved;

            return true;
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

        public bool IsModified
        {
            get { return AddressBook != null && Status == AddressBookStatus.Modified; }
        }

        public bool CloseAddressBook()
        {
            bool allowToContinue = EnsureIsSaved();

            if (!allowToContinue)
                return false;

            Contact = null;
            AddressBook = null;
            FileName = null;
            Status = AddressBookStatus.None;

            return true;
        }

        private bool EnsureIsSaved()
        {
            if (!IsModified)
                return true;

            string text = LocalizedResources.EnsureAddressBookIsSaved_Question;
            string title = LocalizedResources.EnsureAddressBookIsSaved_Title;

            bool? response = userInterface.DisplayYesNoCancelQuestion(text, title);

            if (response == null)
                return false;

            if (response.Value)
                commandPool.SaveAddressBookOperation.Execute();

            return true;
        }

        public void DeleteCurrentContact()
        {
            Contact contactToDelete = Contact;

            if (contactToDelete == null)
                return;

            bool allowToContinue = ConfirmDeleteContact(contactToDelete);

            if (allowToContinue)
            {
                AddressBook.Contacts.Remove(contactToDelete);

                if (ReferenceEquals(contactToDelete, Contact))
                    Contact = null;
            }
        }

        private bool ConfirmDeleteContact(Contact contactToDelete)
        {
            string text = string.Format(LocalizedResources.ContactDelete_ConfirametionQuestion, contactToDelete.Name);
            string title = LocalizedResources.ContactDelete_ConfirmationTitle;

            return userInterface.DisplayYesNoExclamation(text, title);
        }
    }
}
