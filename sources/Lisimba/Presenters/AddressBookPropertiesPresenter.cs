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

using System.IO;
using DustInTheWind.Lisimba.Egg.BookShell;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Presenters
{
    class AddressBookPropertiesPresenter
    {
        private readonly AddressBookPropertiesViewModel viewModel;
        private AddressBookShell addressBookShell;
        private IAddressBookPropertiesView view;

        public AddressBookShell AddressBookShell
        {
            get { return addressBookShell; }
            set
            {
                addressBookShell = value;
                PopulateModel();
            }
        }

        public IAddressBookPropertiesView View
        {
            get { return view; }
            set
            {
                view = value;
                view.Presenter = this;
                view.CreateBindings(viewModel);
            }
        }

        public AddressBookPropertiesPresenter()
        {
            viewModel = new AddressBookPropertiesViewModel();
        }

        private void PopulateModel()
        {
            if (addressBookShell == null)
            {
                viewModel.BookName = string.Empty;
                viewModel.BookNameEnabled = false;
                viewModel.FileLocation = string.Empty;
                viewModel.ContactsCount = 0;
            }
            else
            {
                viewModel.BookName = addressBookShell.AddressBook.Name;
                viewModel.BookNameEnabled = true;
                viewModel.FileLocation = GetFullFileLocationForDisplay(addressBookShell.FileName);
                viewModel.ContactsCount = addressBookShell.AddressBook.Contacts.Count;
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
            if (addressBookShell == null)
                return;

            bool nameIsChanged = addressBookShell.AddressBook.Name != viewModel.BookName;

            if (nameIsChanged)
                addressBookShell.AddressBook.Name = viewModel.BookName;
        }

        public void ShowWindow()
        {
            view.ShowModalView();
        }
    }
}