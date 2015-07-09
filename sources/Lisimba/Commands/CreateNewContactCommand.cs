// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using DustInTheWind.Lisimba.Egg.BookShell;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class CreateNewContactCommand : CommandBase<object>
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UiService uiService;

        public override string ShortDescription
        {
            get { return "Create a new contact."; }
        }

        public CreateNewContactCommand(AddressBookShell addressBookShell, UiService uiService)
        {
            if (addressBookShell == null)
                throw new ArgumentNullException("addressBookShell");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            this.addressBookShell = addressBookShell;
            addressBookShell.AddressBookChanged += HandleCurrentAddressBookChanged;

            this.uiService = uiService;
        }

        private void HandleCurrentAddressBookChanged(object sender, EventArgs eventArgs)
        {
            IsEnabled = addressBookShell.AddressBook != null;
        }

        protected override void DoExecute(object parameter)
        {
            AddContactPresenter addContactPresenter = new AddContactPresenter(addressBookShell, uiService);
            addContactPresenter.View = new FormAddContact();
            addContactPresenter.Show();
        }
    }
}
