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
using System.Linq;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.WinForms.ContactEdit;
using DustInTheWind.Lisimba.WinForms.Services;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.Forms
{
    internal class AddContactPresenter : ViewModelBase
    {
        private readonly AddressBooks addressBooks;
        private readonly WindowSystem windowSystem;

        public Contact EditedContact { get; private set; }

        public IAddContactView View { get; set; }
        public ContactEditorViewModel ContactEditorViewModel { get; set; }

        public AddContactPresenter(ContactEditorViewModel contactEditorViewModel, AddressBooks addressBooks, WindowSystem windowSystem)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            ContactEditorViewModel = contactEditorViewModel;
            this.addressBooks = addressBooks;
            this.windowSystem = windowSystem;
        }

        public void ViewWasLoaded()
        {
            if (addressBooks.Current == null)
                throw new LisimbaException("There is no opened address book to add contacts to.");

            EditedContact = new Contact();

            ContactEditorViewModel.Contact = EditedContact;
        }

        public void OkButtonWasClicked()
        {
            try
            {
                bool allowToContinue = ValidateContact();

                if (!allowToContinue)
                    return;

                addressBooks.Current.AddContact(EditedContact);

                View.Close();
            }
            catch (Exception ex)
            {
                windowSystem.DisplayError(ex.Message);
            }
        }

        private bool ValidateContact()
        {
            bool isNameEmpty = EditedContact.Name.IsEmpty;

            if (isNameEmpty)
                throw new LisimbaException("Please enter a name.");

            bool isAnotherContactWithSameName = addressBooks.Current.AddressBook.Contacts.Any(x => x.Name.Equals(EditedContact.Name));

            return !isAnotherContactWithSameName ||
                windowSystem.DisplayYesNoExclamation("Another contact with the same name already exists.\nIt will NOT be overwritten.\n\nContinue?", "Another contact exists");
        }

        public void CloseButtonWasClicked()
        {
            View.Close();
        }
    }
}