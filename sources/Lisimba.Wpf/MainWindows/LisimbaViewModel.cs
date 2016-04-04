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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class LisimbaViewModel : ViewModelBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly LisimbaWindowTitle lisimbaWindowTitle;

        private string title;
        private bool isContactEditVisible;
        private bool isAddressBookViewVisible;
        private List<Contact> contacts;

        //public ContactListViewModel ContactListViewModel { get; private set; }
        //public ContactEditorViewModel ContactEditorViewModel { get; private set; }

        public List<Contact> Contacts
        {
            get { return contacts; }
            private set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        public LisimbaMainMenuViewModel LisimbaMainMenuViewModel { get; private set; }
        public LisimbaToolBarViewModel LisimbaToolBarViewModel { get; private set; }
        public LisimbaStatusBarViewModel LisimbaStatusBarViewModel { get; private set; }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
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

        public bool IsAddressBookViewVisible
        {
            get { return isAddressBookViewVisible; }
            set
            {
                isAddressBookViewVisible = value;
                OnPropertyChanged();
            }
        }

        public LisimbaViewModel(ContactListViewModel contactListViewModel, ContactEditorViewModel contactEditorViewModel,
            OpenedAddressBooks openedAddressBooks, LisimbaStatusBarViewModel lisimbaStatusBarViewModel,
            LisimbaMainMenuViewModel lisimbaMainMenuViewModel, LisimbaToolBarViewModel lisimbaToolBarViewModel,
            LisimbaWindowTitle lisimbaWindowTitle)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (lisimbaStatusBarViewModel == null) throw new ArgumentNullException("lisimbaStatusBarViewModel");
            if (lisimbaMainMenuViewModel == null) throw new ArgumentNullException("lisimbaMainMenuViewModel");
            if (lisimbaToolBarViewModel == null) throw new ArgumentNullException("lisimbaToolBarViewModel");
            if (lisimbaWindowTitle == null) throw new ArgumentNullException("lisimbaWindowTitle");

            this.openedAddressBooks = openedAddressBooks;
            this.lisimbaWindowTitle = lisimbaWindowTitle;

            LisimbaMainMenuViewModel = lisimbaMainMenuViewModel;
            LisimbaToolBarViewModel = lisimbaToolBarViewModel;
            LisimbaStatusBarViewModel = lisimbaStatusBarViewModel;

            //ContactListViewModel = contactListViewModel;
            //ContactEditorViewModel = contactEditorViewModel;

            Contacts = new List<Contact>
            {
                new Contact {Name = new PersonName("alexandru", "nicolae", "iuga", "alez")},
                new Contact {Name = new PersonName("elisabeta", "maria", "iuga", "eliza")}
            };

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            openedAddressBooks.ContactChanged += HandleContactChanged;
            lisimbaWindowTitle.ValueChanged += HandleLisimbaTitleValueChanged;

            Title = lisimbaWindowTitle.Value;
        }

        private void HandleLisimbaTitleValueChanged(object sender, EventArgs eventArgs)
        {
            Title = lisimbaWindowTitle.Value;
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            IsContactEditVisible = openedAddressBooks.CurrentContact != null;
            //ContactEditorViewModel.ActionQueue = openedAddressBooks.Current.ActionQueue;
            //ContactEditorViewModel.Contact = openedAddressBooks.CurrentContact;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsAddressBookViewVisible = openedAddressBooks.Current != null;

            if (openedAddressBooks.Current != null)
                Contacts = openedAddressBooks.Current.AddressBook.Contacts.ToList();
        }
    }
}