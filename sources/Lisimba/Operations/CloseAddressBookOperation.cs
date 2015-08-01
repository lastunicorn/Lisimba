using System;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class CloseAddressBookOperation : ExecutableViewModelBase<object>
    {
        private readonly AddressBookShell addressBookShell;

        public override string ShortDescription
        {
            get { return LocalizedResources.CloseCurrentAddressBookOperationDescription; }
        }

        public CloseAddressBookOperation(AddressBookShell addressBookShell, ApplicationStatus applicationStatus)
            : base(applicationStatus)
        {
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");

            this.addressBookShell = addressBookShell;
            addressBookShell.AddressBookChanged += HandleAddressBookChanged;

            IsEnabled = addressBookShell.AddressBook != null;
        }

        private void HandleAddressBookChanged(object sender, EventArgs e)
        {
            IsEnabled = addressBookShell.AddressBook != null;
        }

        protected override void DoExecute(object parameter)
        {
            addressBookShell.CloseAddressBook();
        }
    }
}