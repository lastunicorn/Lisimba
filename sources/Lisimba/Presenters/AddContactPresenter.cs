using System;
using System.Linq;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Exceptions;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Presenters
{
    class AddContactPresenter
    {
        private IAddContactView view;
        public IAddContactView View
        {
            private get { return view; }
            set
            {
                view = value;
                view.Presenter = this;
            }
        }

        private readonly CurrentData currentData;
        private UIService uiService;

        private Contact editedContact;
        private AddressBook addressBook;

        public AddContactPresenter(CurrentData currentData, UIService uiService)
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
            if (currentData.AddressBook == null)
                throw new LisimbaException("There is no opened address book to add contacts to.");

            addressBook = currentData.AddressBook;
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
