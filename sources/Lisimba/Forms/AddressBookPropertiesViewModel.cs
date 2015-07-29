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
using System.IO;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Forms
{
    class AddressBookPropertiesViewModel : ViewModelBase
    {
        private readonly AddressBookShell addressBookShell;

        private string bookName;
        private bool bookNameEnabled;
        private string fileLocation;
        private int contactsCount;

        public string BookName
        {
            get { return bookName; }
            set
            {
                bookName = value;
                OnPropertyChanged();
            }
        }

        public bool BookNameEnabled
        {
            get { return bookNameEnabled; }
            set
            {
                bookNameEnabled = value;
                OnPropertyChanged();
            }
        }

        public string FileLocation
        {
            get { return fileLocation; }
            set
            {
                fileLocation = value;
                OnPropertyChanged();
            }
        }

        public int ContactsCount
        {
            get { return contactsCount; }
            set
            {
                contactsCount = value;
                OnPropertyChanged();
            }
        }

        public AddressBookPropertiesViewModel(AddressBookShell addressBookShell)
        {
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");
            
            this.addressBookShell = addressBookShell;

            this.addressBookShell.AddressBookChanged += HandleAddressBookChanged;
            PopulateModel();
        }

        private void HandleAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            PopulateModel();
        }

        private void PopulateModel()
        {
            if (addressBookShell.AddressBook == null)
            {
                BookName = string.Empty;
                BookNameEnabled = false;
                FileLocation = string.Empty;
                ContactsCount = 0;
            }
            else
            {
                BookName = addressBookShell.AddressBook.Name;
                BookNameEnabled = true;
                FileLocation = GetFullFileLocationForDisplay(addressBookShell.FileName);
                ContactsCount = addressBookShell.AddressBook.Contacts.Count;
            }
        }

        private static string GetFullFileLocationForDisplay(string fileName)
        {
            return fileName == null
                ? "<Address book is not saved yet.>"
                : Path.GetFullPath(fileName);
        }

        public void OkButtonWasClicked()
        {
            if (addressBookShell.AddressBook == null)
                return;

            bool nameIsChanged = addressBookShell.AddressBook.Name != BookName;

            if (nameIsChanged)
                addressBookShell.AddressBook.Name = BookName;
        }
    }
}