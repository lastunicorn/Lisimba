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

namespace Lisimba.Cmd.Business
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
