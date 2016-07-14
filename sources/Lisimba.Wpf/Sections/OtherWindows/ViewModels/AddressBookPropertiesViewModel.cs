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
using System.IO;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Wpf.Commands;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.Sections.OtherWindows.ViewModels
{
    internal class AddressBookPropertiesViewModel : ViewModelBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;

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

        public DelegateCommand OkCommand { get; private set; }

        public AddressBookPropertiesViewModel(OpenedAddressBooks openedAddressBooks)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.openedAddressBooks = openedAddressBooks;
            OkCommand = new DelegateCommand(OkButtonWasClicked);

            this.openedAddressBooks.AddressBookChanged += HandleAddressBookChanged;
            RefreshModel();
        }

        private void HandleAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            RefreshModel();
        }

        private void RefreshModel()
        {
            if (openedAddressBooks.Current == null)
                ClearModel();
            else
                PopulateModel();
        }

        private void ClearModel()
        {
            BookName = string.Empty;
            BookNameEnabled = false;
            FileLocation = string.Empty;
            ContactsCount = 0;
        }

        private void PopulateModel()
        {
            BookName = openedAddressBooks.Current.AddressBook.Name;
            BookNameEnabled = true;
            FileLocation = GetFullFileLocationForDisplay(openedAddressBooks.Current.Location);
            ContactsCount = openedAddressBooks.Current.AddressBook.Contacts.Count;
        }

        private static string GetFullFileLocationForDisplay(string fileName)
        {
            return fileName == null
                ? LocalizedResources.AddressBookNotSavedYet
                : Path.GetFullPath(fileName);
        }

        private void OkButtonWasClicked(object parameter)
        {
            if (openedAddressBooks.Current == null)
                return;

            bool nameIsChanged = openedAddressBooks.Current.AddressBook.Name != bookName;

            if (nameIsChanged)
                openedAddressBooks.Current.ChangeAddressBookName(bookName);
        }
    }
}
