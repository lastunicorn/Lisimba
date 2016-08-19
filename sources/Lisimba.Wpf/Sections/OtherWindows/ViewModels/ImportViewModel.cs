// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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
using System.Windows.Data;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.OtherWindows.ViewModels
{
    internal class ImportViewModel : ViewModelBase
    {
        private readonly AddressBooks addressBooks;

        private CustomObservableCollection<Contact> originalContactsCollection;
        private ListCollectionView contacts;
        private Contact selectedContact;

        public ListCollectionView Contacts
        {
            get { return contacts; }
            private set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ImportViewModel(AddressBooks addressBooks)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.addressBooks = addressBooks;

            addressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            ClearContacts();

            if (e.NewAddressBook != null)
                SetContacts(e.NewAddressBook.AddressBook.Contacts);
        }

        private void ClearContacts()
        {
            if (originalContactsCollection != null)
                originalContactsCollection.ItemChanged -= ContactsItemChanged;

            Contacts = null;
            originalContactsCollection = null;
        }

        private void SetContacts(CustomObservableCollection<Contact> contactCollection)
        {
            Contacts = (ListCollectionView)CollectionViewSource.GetDefaultView(contactCollection);

            originalContactsCollection = contactCollection;
            originalContactsCollection.ItemChanged += ContactsItemChanged;
        }

        private void ContactsItemChanged(object sender, ItemChangedEventArgs<Contact> itemChangedEventArgs)
        {
            contacts.Refresh();
        }
    }
}
