// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using System.ComponentModel;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.ContactEdit;
using DustInTheWind.Lisimba.ContactList;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Main
{
    internal class LisimbaViewModel : ViewModelBase
    {
        private readonly AddressBooks addressBooks;
        private readonly ApplicationStatus applicationStatus;
        private readonly LisimbaApplication lisimbaApplication;

        private string title;
        private string statusText;

        private bool isContactEditVisible;
        private bool isAddressBookViewVisible;

        public ContactListViewModel ContactListViewModel { get; private set; }
        public ContactEditorViewModel ContactEditorViewModel { get; private set; }
        public CreateNewAddressBookOperation CreateNewAddressBookOperation { get; private set; }
        public OpenAddressBookOperation OpenAddressBookOperation { get; private set; }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string StatusText
        {
            get { return statusText; }
            set
            {
                statusText = value;
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
            LisimbaApplication lisimbaApplication, ApplicationStatus applicationStatus, AddressBooks addressBooks, CommandPool commandPool)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (commandPool == null) throw new ArgumentNullException("commandPool");

            this.lisimbaApplication = lisimbaApplication;
            this.applicationStatus = applicationStatus;
            this.addressBooks = addressBooks;

            ContactListViewModel = contactListViewModel;
            ContactEditorViewModel = contactEditorViewModel;
            CreateNewAddressBookOperation = commandPool.CreateNewAddressBookOperation;
            OpenAddressBookOperation = commandPool.OpenAddressBookOperation;

            addressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            addressBooks.ContactChanged += HandleContactChanged;
            addressBooks.Closing += HandleAddressBooksClosing;
            addressBooks.Opened += HandleAddressBooksOpened;

            if (addressBooks.Current != null)
                addressBooks.Current.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            applicationStatus.StatusTextChanged += HandleStatusTextChanged;

            IsContactEditVisible = addressBooks.Contact != null;
            IsAddressBookViewVisible = addressBooks.Current != null;

            StatusText = applicationStatus.StatusText;
            Title = BuildFormTitle();
        }

        private void HandleAddressBooksOpened(object sender, EventArgs e)
        {
            addressBooks.Current.StatusChanged += HandleAddressBookStatusChanged;
        }

        private void HandleAddressBooksClosing(object sender, CancelEventArgs e)
        {
            addressBooks.Current.StatusChanged -= HandleAddressBookStatusChanged;
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            IsContactEditVisible = addressBooks.Contact != null;
            ContactEditorViewModel.Contact = addressBooks.Contact;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
                e.OldAddressBook.AddressBook.Changed -= HandleCurrentAddressBookContentChanged;

            if (e.NewAddressBook != null)
                e.NewAddressBook.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            Title = BuildFormTitle();
            IsAddressBookViewVisible = addressBooks.Current != null;
        }

        private void HandleAddressBookStatusChanged(object sender, EventArgs e)
        {
            Title = BuildFormTitle();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs e)
        {
            Title = BuildFormTitle();
        }

        private void HandleStatusTextChanged(object sender, EventArgs e)
        {
            StatusText = applicationStatus.StatusText;
        }

        private string BuildFormTitle()
        {
            if (addressBooks.Current == null)
                return lisimbaApplication.ProgramName;

            string addressBookName = addressBooks.Current.GetFriendlyName();
            bool isModified = addressBooks.Current != null && addressBooks.Current.Status == AddressBookStatus.Modified;
            string unsavedSign = isModified ? " *" : string.Empty;
            string programName = lisimbaApplication.ProgramName;

            return string.Format("{0}{1} - {2}", addressBookName, unsavedSign, programName);
        }

        public bool WindowIsClosing()
        {
            return addressBooks.CloseAddressBook();
        }
    }
}