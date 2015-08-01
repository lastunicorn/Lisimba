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

namespace DustInTheWind.Lisimba.Egg.Book
{
    public class AddressBook
    {
        private string version;
        private string name;

        /// <summary>
        /// Gets or sets the version of the application that created this address book.
        /// </summary>
        public string Version
        {
            get { return version; }
            set
            {
                version = value;
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
                OnChanged();
            }
        }

        /// <summary>
        /// Gets a collection of Contact.
        /// </summary>
        public ContactCollection Contacts { get; private set; }

        public event EventHandler Changed;
        public event EventHandler<ContactContentChangedEventArgs> ContactContentChanged;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnContactChanged(ContactContentChangedEventArgs e)
        {
            EventHandler<ContactContentChangedEventArgs> handler = ContactContentChanged;

            if (handler != null)
                handler(this, e);
        }

        public AddressBook()
        {
            Name = "New Address Book";

            Contacts = new ContactCollection();
            Contacts.CollectionChanged += HandleContactsCollectionChanged;
            Contacts.ItemChanged += HandleContactChanged;

            Version = GetCurrentAssemblyVersion();
        }

        private void HandleContactChanged(object sender, ItemChangedEventArgs<Contact> e)
        {
            OnContactChanged(new ContactContentChangedEventArgs(e.Item));
            OnChanged();
        }

        private void HandleContactsCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            OnChanged();
        }

        private static string GetCurrentAssemblyVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();

            return assemblyName.Version.ToString();
        }
    }
}