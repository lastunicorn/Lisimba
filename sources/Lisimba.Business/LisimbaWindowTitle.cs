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
using DustInTheWind.Lisimba.Business.AddressBookManagement;

namespace DustInTheWind.Lisimba.Business
{
    public class LisimbaWindowTitle
    {
        private readonly LisimbaApplication lisimbaApplication;
        private readonly OpenedAddressBooks openedAddressBooks;
        private string value;

        public event EventHandler ValueChanged;

        public string Value
        {
            get { return value; }
            private set
            {
                this.value = value;
                OnValueChanged();
            }
        }
        
        public LisimbaWindowTitle(LisimbaApplication lisimbaApplication, OpenedAddressBooks openedAddressBooks)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.lisimbaApplication = lisimbaApplication;
            this.openedAddressBooks = openedAddressBooks;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            openedAddressBooks.AddressBookClosing += HandleAddressBooksClosing;
            openedAddressBooks.AddressBookOpened += HandleAddressBooksOpened;

            if (openedAddressBooks.Current != null)
            {
                openedAddressBooks.Current.AddressBook.Changed += HandleCurrentAddressBookContentChanged;
                openedAddressBooks.Current.StatusChanged += HandleAddressBookStatusChanged;
            }

            Value = BuildTitle();
        }

        private void HandleAddressBooksOpened(object sender, EventArgs e)
        {
            openedAddressBooks.Current.StatusChanged += HandleAddressBookStatusChanged;
        }

        private void HandleAddressBooksClosing(object sender, EventArgs e)
        {
            openedAddressBooks.Current.StatusChanged -= HandleAddressBookStatusChanged;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
                e.OldAddressBook.AddressBook.Changed -= HandleCurrentAddressBookContentChanged;

            if (e.NewAddressBook != null)
                e.NewAddressBook.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            Value = BuildTitle();
        }

        private void HandleAddressBookStatusChanged(object sender, EventArgs e)
        {
            Value = BuildTitle();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs e)
        {
            Value = BuildTitle();
        }

        private string BuildTitle()
        {
            if (openedAddressBooks.Current == null)
                return lisimbaApplication.ProgramName;

            string addressBookName = openedAddressBooks.Current.GetFriendlyName();
            bool isModified = openedAddressBooks.Current != null && openedAddressBooks.Current.Status == AddressBookStatus.Modified;
            string unsavedSign = isModified ? " *" : string.Empty;
            string programName = lisimbaApplication.ProgramName;

            return string.Format("{0}{1} - {2}", addressBookName, unsavedSign, programName);
        }

        public override string ToString()
        {
            return Value;
        }

        protected virtual void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}