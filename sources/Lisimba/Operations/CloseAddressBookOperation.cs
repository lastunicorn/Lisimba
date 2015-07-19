using System;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.Operations
{
    internal class CloseAddressBookOperation : OperationBase<object>
    {
        private readonly AddressBookShell addressBookShell;

        public override string ShortDescription
        {
            get { return Resources.CloseCurrentAddressBookOperationDescription; }
        }

        public CloseAddressBookOperation(AddressBookShell addressBookShell)
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