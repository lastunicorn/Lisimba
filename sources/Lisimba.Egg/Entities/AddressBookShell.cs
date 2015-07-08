using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    class AddressBookShell
    {
        private string version;
        private string name;
        private AddressBookStatus status;

        /// <summary>
        /// The version of the application that created this address book.
        /// </summary>
        public string Version
        {
            get { return version; }
            set
            {
                version = value;

                Status = AddressBookStatus.Modified;
                OnChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name of the address book.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                Status = AddressBookStatus.Modified;
                OnChanged();
            }
        }

        /// <summary>
        /// Gets a collection of Contact.
        /// </summary>
        public ContactCollection Contacts { get; private set; }

        /// <summary>
        /// Gets the full file name of the address book or empty string if is a new one.
        /// </summary>
        public string FileName { get; set; }

        public AddressBookStatus Status
        {
            get { return status; }
            private set
            {
                status = value;
                OnStatusChanged();
            }
        }

        #region Event Changed

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        #region Event AddressBookSaved

        public event EventHandler AddressBookSaved;

        protected virtual void OnAddressBookSaved(EventArgs e)
        {
            EventHandler handler = AddressBookSaved;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region Event StatusChanged

        public event EventHandler StatusChanged;

        protected virtual void OnStatusChanged()
        {
            EventHandler handler = StatusChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        #region Event ContactContentChanged

        public event EventHandler<ContactContentChangedEventArgs> ContactContentChanged;

        protected virtual void OnContactChanged(ContactContentChangedEventArgs e)
        {
            EventHandler<ContactContentChangedEventArgs> handler = ContactContentChanged;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        public AddressBookShell()
        {
            Name = "New Address Book";

            Contacts = new ContactCollection();
            Contacts.CollectionChanged += HandleContactsCollectionChanged;
            Contacts.ItemChanged += HandleContactChanged;

            FileName = null;
            Version = GetCurrentAssemblyVersion();
            Status = AddressBookStatus.New;
        }

        private void HandleContactChanged(object sender, ItemChangedEventArgs<Contact> e)
        {
            Status = AddressBookStatus.Modified;
            OnContactChanged(new ContactContentChangedEventArgs(e.Item));
            OnChanged();
        }

        private void HandleContactsCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            Status = AddressBookStatus.Modified;
            OnChanged();
        }

        private static string GetCurrentAssemblyVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();

            return assemblyName.Version.ToString();
        }

        public void SetAsSaved()
        {
            Status = AddressBookStatus.Saved;
            OnAddressBookSaved(EventArgs.Empty);
        }

        public string GetFriendlyName()
        {
            bool hasName = !string.IsNullOrWhiteSpace(Name);

            if (hasName)
                return Name;

            bool hasFileName = !string.IsNullOrWhiteSpace(FileName);

            return hasFileName ? FileName : null;
        }
    }
}
