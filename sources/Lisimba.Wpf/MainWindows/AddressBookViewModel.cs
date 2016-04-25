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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Sorting;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    class AddressBookViewModel : ViewModelBase
    {
        private readonly ContactListViewModel contactListViewModel;
        private readonly ContactEditorViewModel contactEditorViewModel;
        private readonly OpenedAddressBooks openedAddressBooks;
        private List<Contact> contacts;
        private Contact selectedContact;
        private bool isContactEditVisible;

        public List<Contact> Contacts
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
                openedAddressBooks.CurrentContact = value;
                OnPropertyChanged();
            }
        }

        public bool IsContactEditVisible
        {
            get { return isContactEditVisible; }
            set
            {
                isContactEditVisible = value;
                OnPropertyChanged();
            }
        }

        //public ContactListViewModel ContactListViewModel { get; private set; }
        //public ContactEditorViewModel ContactEditorViewModel { get; private set; }

        public AddressBookViewModel(ContactListViewModel contactListViewModel, ContactEditorViewModel contactEditorViewModel,
            OpenedAddressBooks openedAddressBooks)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.contactListViewModel = contactListViewModel;
            this.contactEditorViewModel = contactEditorViewModel;
            this.openedAddressBooks = openedAddressBooks;

            //ContactListViewModel = contactListViewModel;
            //ContactEditorViewModel = contactEditorViewModel;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            openedAddressBooks.ContactChanged += HandleContactChanged;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.NewAddressBook == null)
            {
                Contacts = null;
            }
            else
            {
                if (openedAddressBooks.Current != null)
                {
                    Contacts = openedAddressBooks.Current.AddressBook.Contacts
                        .OrderBy(x => x, new ContactByBirthdayComparer())
                        .ToList();
                }
                else
                {
                    Contacts = null;
                }
            }
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            SelectedContact = openedAddressBooks.CurrentContact;
            IsContactEditVisible = openedAddressBooks.CurrentContact != null;
            //ContactEditorViewModel.ActionQueue = openedAddressBooks.Current.ActionQueue;
            //ContactEditorViewModel.Contact = openedAddressBooks.CurrentContact;
        }
    }
}
