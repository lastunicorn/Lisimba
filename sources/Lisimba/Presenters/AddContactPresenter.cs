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
using System.Linq;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.BookShell;
using DustInTheWind.Lisimba.Exceptions;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Presenters
{
    class AddContactPresenter
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UiService uiService;

        private Contact editedContact;
        private AddressBook addressBook;

        private IAddContactView view;
        public IAddContactView View
        {
            set
            {
                view = value;
                view.Presenter = this;
            }
        }

        public AddContactPresenter(AddressBookShell addressBookShell, UiService uiService)
        {
            if (addressBookShell == null)
                throw new ArgumentNullException("addressBookShell");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            this.addressBookShell = addressBookShell;
            this.uiService = uiService;
        }

        public void ViewWasLoaded()
        {
            if (addressBookShell.AddressBook == null)
                throw new LisimbaException("There is no opened address book to add contacts to.");

            addressBook = addressBookShell.AddressBook;
            editedContact = new Contact();

            view.Contact = editedContact;
        }

        public void Show()
        {
            view.Show();
        }

        public void OkButtonWasClicked()
        {
            try
            {
                ValidateContact();

                addressBook.Contacts.Add(editedContact);

                view.Close();
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }

        private void ValidateContact()
        {
            bool isNameEmpty = editedContact.Name.IsEmpty();

            if (isNameEmpty)
                throw new LisimbaException("Please enter at least one of the fields marked with '*'.");

            bool isAnotherContactWithSameName = addressBook.Contacts.Any(x => x.Name.Equals(editedContact.Name));

            if (isAnotherContactWithSameName)
                throw new LisimbaException("Another contact having the same name already exists.");
        }

        public void CloseButtonWasClicked()
        {
            view.Close();
        }
    }
}
