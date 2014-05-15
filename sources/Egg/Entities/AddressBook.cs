// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [XmlRoot("Book")]
    [Serializable()]
    public class AddressBook
    {
        private string version;
        private string name;

        /// <summary>
        /// The version of the application that created this address book.
        /// </summary>
        [XmlElement("Version")]
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
        [XmlElement("Name")]
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
        [XmlArray("Contacts"), XmlArrayItem("Contact")]
        public ContactCollection Contacts { get; private set; }

        /// <summary>
        /// Gets the full file name of the address book or empty string if is a new one.
        /// </summary>
        [XmlIgnore()]
        public string FileName { get; set; }

        [XmlIgnore]
        public AddressBookStatus Status { get; private set; }

        #region Event Changed

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

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

        public AddressBook()
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
        }

        public void SetAsNew()
        {
            Status = AddressBookStatus.Saved;
        }
    }
}
