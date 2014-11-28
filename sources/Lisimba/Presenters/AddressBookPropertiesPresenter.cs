using System.IO;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Presenters
{
    public class AddressBookPropertiesPresenter
    {
        private readonly AddressBookPropertiesViewModel viewModel;
        private AddressBook addressBook;
        private IAddressBookPropertiesView view;

        public AddressBook AddressBook
        {
            get { return addressBook; }
            set
            {
                addressBook = value;
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
            if (addressBook == null)
            {
                viewModel.BookName = string.Empty;
                viewModel.BookNameEnabled = false;
                viewModel.FileLocation = string.Empty;
                viewModel.ContactsCount = 0;
            }
            else
            {
                viewModel.BookName = addressBook.Name;
                viewModel.BookNameEnabled = true;
                viewModel.FileLocation = GetFullFileLocationForDisplay(addressBook.FileName);
                viewModel.ContactsCount = addressBook.Contacts.Count;
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
            if (addressBook == null)
                return;

            bool nameIsChanged = addressBook.Name != viewModel.BookName;

            if (nameIsChanged)
                addressBook.Name = viewModel.BookName;
        }

        public void ShowWindow()
        {
            view.ShowModalView();
        }
    }
}