using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisimba.Cmd.Data
{
    class AddressBookGuarder
    {
        private readonly AddressBooks addressBooks;
        private readonly AddressBookGuarderConsole consoleView;

        public AddressBookGuarder(AddressBooks addressBooks, AddressBookGuarderConsole consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public bool EnsureSave()
        {
            if (addressBooks.IsAddressBookSaved)
                return true;

            bool? needSave = consoleView.AskToSaveAddressBook();

            if (needSave == null)
                return false;

            if (!needSave.Value)
                return true;

            if (addressBooks.AddressBookLocation == null)
            {
                string newLocation = consoleView.AskForLocation();

                if (newLocation == null)
                    return false;

                addressBooks.SaveAddressBookAs(newLocation);
            }
            else
            {
                addressBooks.SaveAddressBook();
            }

            return true;
        }
    }
}
