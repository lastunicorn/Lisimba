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
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Exceptions;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Presenters
{
    class AddContactPresenter
    {
        private readonly CurrentData currentData;
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

        public AddContactPresenter(CurrentData currentData, UiService uiService)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            this.currentData = currentData;
            this.uiService = uiService;
        }

        public void ViewWasLoaded()
        {
            if (currentData.AddressBookShell == null)
                throw new LisimbaException("There is no opened address book to add contacts to.");

            addressBook = currentData.AddressBookShell.AddressBook;
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
                ValidateContact(editedContact);

                addressBook.Contacts.Add(editedContact);

                view.Close();
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }

        private void ValidateContact(Contact contactToValidate)
        {
            bool isNameEmpty = contactToValidate.Name.IsEmpty();

            if (isNameEmpty)
                throw new LisimbaException("Please enter at least one of the fields marked with '*'.");

            bool isAnotherContactWithSameName = addressBook.Contacts.Any(x => x.Name.Equals(contactToValidate.Name));

            if (isAnotherContactWithSameName)
                throw new LisimbaException("Another contact having the same name already exists.");
        }

        public void CloseButtonWasClicked()
        {
            view.Close();
        }
    }
}
