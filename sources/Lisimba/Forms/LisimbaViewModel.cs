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
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Forms
{
    class LisimbaViewModel : ViewModelBase
    {
        private readonly AddressBookShell addressBookShell;
        private readonly ApplicationStatus applicationStatus;
        private readonly LisimbaApplication lisimbaApplication;

        private string title;
        private string statusText;

        private bool allowToClose;
        private bool isContactEditVisible;
        private bool isAddressBookViewVisible;

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

        public LisimbaViewModel(LisimbaApplication lisimbaApplication, ApplicationStatus applicationStatus, AddressBookShell addressBookShell)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");

            this.lisimbaApplication = lisimbaApplication;
            this.applicationStatus = applicationStatus;
            this.addressBookShell = addressBookShell;

            addressBookShell.AddressBookChanged += HandleCurrentAddressBookChanged;
            addressBookShell.StatusChanged += HandleAddressBookStatusChanged;
            addressBookShell.ContactChanged += HandleContactChanged;

            if (addressBookShell.AddressBook != null)
                addressBookShell.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            lisimbaApplication.BeforeExiting += HandleApplicationBeforeExiting;
            lisimbaApplication.ExitCanceled += HandleApplicationExitCanceled;

            applicationStatus.StatusTextChanged += HandleStatusTextChanged;

            IsContactEditVisible = addressBookShell.Contact != null;
            IsAddressBookViewVisible = addressBookShell.AddressBook != null;
        }

        private void HandleContactChanged(object sender, EventArgs eventArgs)
        {
            IsContactEditVisible = addressBookShell.Contact != null;
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

        private void HandleApplicationBeforeExiting(object sender, EventArgs e)
        {
            allowToClose = true;
        }

        private void HandleApplicationExitCanceled(object sender, EventArgs e)
        {
            allowToClose = false;
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

        public void WindowWasShown()
        {
            StatusText = applicationStatus.StatusText;
            Title = BuildFormTitle();
        }

        public bool WindowIsClosing()
        {
            return allowToClose || lisimbaApplication.Exit();
        }
    }
}