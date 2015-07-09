using System.IO;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Presenters
{
    public class AddressBookPropertiesPresenter
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