// Lisimba
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
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class DeleteCurrentContactOperation : ExecutableViewModelBase<object>
    {
        private readonly AddressBooks addressBooks;
        private readonly UserInterface userInterface;

        public override string ShortDescription
        {
            get { return LocalizedResources.DeleteCurrentContactOperationDescription; }
        }

        public DeleteCurrentContactOperation(AddressBooks addressBooks, ApplicationStatus applicationStatus, UserInterface userInterface)
            : base(applicationStatus)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.addressBooks = addressBooks;
            this.userInterface = userInterface;

            addressBooks.ContactChanged += HandleCurrentContactChanged;

            IsEnabled = addressBooks.Contact != null;
        }

        private void HandleCurrentContactChanged(object sender, EventArgs e)
        {
            IsEnabled = addressBooks.Contact != null;
        }

        protected override void DoExecute(object parameter)
        {
            DeleteCurrentContact();
        }

        public void DeleteCurrentContact()
        {
            Contact contactToDelete = addressBooks.Contact;

            if (contactToDelete == null)
                return;

            bool allowToContinue = ConfirmDeleteContact(contactToDelete);

            if (allowToContinue)
            {
                addressBooks.Current.AddressBook.Contacts.Remove(contactToDelete);

                if (ReferenceEquals(contactToDelete, addressBooks.Contact))
                    addressBooks.Contact = null;
            }
        }

        private bool ConfirmDeleteContact(Contact contactToDelete)
        {
            string text = string.Format(LocalizedResources.ContactDelete_ConfirametionQuestion, contactToDelete.Name);
            string title = LocalizedResources.ContactDelete_ConfirmationTitle;

            return userInterface.DisplayYesNoExclamation(text, title);
        }
    }
}