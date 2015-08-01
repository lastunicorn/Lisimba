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
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Presenters
{
    internal class AddContactPresenter
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UserInterface userInterface;

        public Contact EditedContact { get; private set; }
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

        public AddContactPresenter(AddressBookShell addressBookShell, UserInterface userInterface)
        {
            if (addressBookShell == null)
                throw new ArgumentNullException("addressBookShell");

            if (userInterface == null)
                throw new ArgumentNullException("userInterface");

            this.addressBookShell = addressBookShell;
            this.userInterface = userInterface;
        }

        public void ViewWasLoaded()
        {
            if (addressBookShell.AddressBook == null)
                throw new LisimbaException("There is no opened address book to add contacts to.");

            addressBook = addressBookShell.AddressBook;
            EditedContact = new Contact();

            view.Contact = EditedContact;
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

                addressBook.Contacts.Add(EditedContact);

                view.Close();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex.Message);
            }
        }

        private void ValidateContact()
        {
            bool isNameEmpty = EditedContact.Name.IsEmpty();

            if (isNameEmpty)
                throw new LisimbaException("Please enter a name.");

            bool isAnotherContactWithSameName = addressBook.Contacts.Any(x => x.Name.Equals(EditedContact.Name));

            if (isAnotherContactWithSameName)
                throw new LisimbaException("Another contact with the same name already exists.");
        }

        public void CloseButtonWasClicked()
        {
            view.Close();
        }
    }
}