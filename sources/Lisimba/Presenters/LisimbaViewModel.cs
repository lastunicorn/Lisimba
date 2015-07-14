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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Presenters
{
    class LisimbaViewModel : INotifyPropertyChanged
    {
        private readonly AddressBookShell addressBookShell;
        private readonly ApplicationService applicationService;
        private readonly ApplicationStatus applicationStatus;
        private readonly LisimbaApplication lisimbaApplication;

        private string title;
        private string statusText;

        private bool allowToClose;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public LisimbaViewModel(AddressBookShell addressBookShell, ApplicationService applicationService,
            ApplicationStatus applicationStatus, LisimbaApplication lisimbaApplication)
        {
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");
            if (applicationService == null) throw new ArgumentNullException("applicationService");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");

            this.addressBookShell = addressBookShell;
            this.applicationService = applicationService;
            this.applicationStatus = applicationStatus;
            this.lisimbaApplication = lisimbaApplication;

            addressBookShell.AddressBookChanged += HandleCurrentAddressBookChanged;
            addressBookShell.StatusChanged += HandleAddressBookStatusChanged;

            if (addressBookShell.AddressBook != null)
                addressBookShell.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            applicationService.Exiting += HandleApplicationExiting;
            applicationService.ExitCanceled += HandleApplicationExitCanceled;

            applicationStatus.StatusTextChanged += HandleStatusTextChanged;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
                e.OldAddressBook.Changed -= HandleCurrentAddressBookContentChanged;

            if (e.NewAddressBook != null)
                e.NewAddressBook.Changed += HandleCurrentAddressBookContentChanged;

            Title = BuildFormTitle();
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

        private void HandleApplicationExitCanceled(object sender, EventArgs e)
        {
            allowToClose = false;
        }

        private void HandleApplicationExiting(object sender, CancelEventArgs e)
        {
            allowToClose = true;
        }

        private string BuildFormTitle()
        {
            if (addressBookShell.AddressBook == null)
                return lisimbaApplication.ProgramName;

            StringBuilder sb = new StringBuilder();

            string addressBookName = addressBookShell.GetFriendlyName() ?? "< Unnamed >";
            sb.Append(addressBookName);

            if (!addressBookShell.IsSaved)
                sb.Append(" *");

            sb.Append(" - ");

            sb.Append(lisimbaApplication.ProgramName);

            return sb.ToString();
        }

        public void WindowWasShown()
        {
            StatusText = applicationStatus.StatusText;
            Title = BuildFormTitle();
        }

        public bool WindowIsClosing()
        {
            if (!allowToClose)
                applicationService.Exit();

            return allowToClose;
        }
    }
}