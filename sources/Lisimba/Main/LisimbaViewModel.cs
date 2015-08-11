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
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.ContactEdit;
using DustInTheWind.Lisimba.ContactList;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Main
{
    internal class LisimbaViewModel : ViewModelBase
    {
        private readonly AddressBookShell addressBookShell;
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
            LisimbaApplication lisimbaApplication, ApplicationStatus applicationStatus, AddressBookShell addressBookShell, CommandPool commandPool)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");
            if (commandPool == null) throw new ArgumentNullException("commandPool");

            this.lisimbaApplication = lisimbaApplication;
            this.applicationStatus = applicationStatus;
            this.addressBookShell = addressBookShell;

            ContactListViewModel = contactListViewModel;
            ContactEditorViewModel = contactEditorViewModel;
            CreateNewAddressBookOperation = commandPool.CreateNewAddressBookOperation;
            OpenAddressBookOperation = commandPool.OpenAddressBookOperation;

            addressBookShell.AddressBookChanged += HandleCurrentAddressBookChanged;
            addressBookShell.StatusChanged += HandleAddressBookStatusChanged;
            addressBookShell.ContactChanged += HandleContactChanged;

            if (addressBookShell.AddressBook != null)
                addressBookShell.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            applicationStatus.StatusTextChanged += HandleStatusTextChanged;

            IsContactEditVisible = addressBookShell.Contact != null;
            IsAddressBookViewVisible = addressBookShell.AddressBook != null;

            StatusText = applicationStatus.StatusText;
            Title = BuildFormTitle();
        }

        private void HandleContactChanged(object sender, EventArgs eventArgs)
        {
            IsContactEditVisible = addressBookShell.Contact != null;
            ContactEditorViewModel.Contact = addressBookShell.Contact;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
                e.OldAddressBook.Changed -= HandleCurrentAddressBookContentChanged;

            if (e.NewAddressBook != null)
                e.NewAddressBook.Changed += HandleCurrentAddressBookContentChanged;

            Title = BuildFormTitle();
            IsAddressBookViewVisible = addressBookShell.AddressBook != null;
        }

        private void HandleAddressBookStatusChanged(object sender, EventArgs eventArgs)
        {
            Title = BuildFormTitle();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs eventArgs)
        {
            Title = BuildFormTitle();
        }

        private void HandleStatusTextChanged(object sender, EventArgs e)
        {
            StatusText = applicationStatus.StatusText;
        }

        private string BuildFormTitle()
        {
            if (addressBookShell.AddressBook == null)
                return lisimbaApplication.ProgramName;

            string addressBookName = addressBookShell.GetFriendlyName();
            string unsavedSign = addressBookShell.IsModified ? " *" : string.Empty;
            string programName = lisimbaApplication.ProgramName;

            return string.Format("{0}{1} - {2}", addressBookName, unsavedSign, programName);
        }

        public bool WindowIsClosing()
        {
            return addressBookShell.EnsureIsSaved();
        }
    }
}