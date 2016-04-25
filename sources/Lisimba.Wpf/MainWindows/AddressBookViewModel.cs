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
