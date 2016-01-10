﻿// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.ContactEdit;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Forms
{
    internal class AddContactPresenter : ViewModelBase
    {
        private readonly AddressBooks addressBooks;
        private readonly UserInterface userInterface;

        public Contact EditedContact { get; private set; }
        private AddressBook addressBook;

        public IAddContactView View { get; set; }
        public ContactEditorViewModel ContactEditorViewModel { get; set; }

        public AddContactPresenter(ContactEditorViewModel contactEditorViewModel,
            AddressBooks addressBooks, UserInterface userInterface)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            ContactEditorViewModel = contactEditorViewModel;
            this.addressBooks = addressBooks;
            this.userInterface = userInterface;
        }

        public void ViewWasLoaded()
        {
            if (addressBooks.Current == null)
                throw new LisimbaException("There is no opened address book to add contacts to.");

            addressBook = addressBooks.Current.AddressBook;
            EditedContact = new Contact();

            ContactEditorViewModel.Contact = EditedContact;
        }

        public void OkButtonWasClicked()
        {
            try
            {
                ValidateContact();

                addressBook.Contacts.Add(EditedContact);

                View.Close();
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
            View.Close();
        }
    }
}