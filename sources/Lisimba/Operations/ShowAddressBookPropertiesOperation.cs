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
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.Operations
{
    class ShowAddressBookPropertiesOperation : OperationBase<object>
    {
        private readonly AddressBookShell addressBookShell;

        public override string ShortDescription
        {
            get { return Resources.ShowAddressBookPropertiesOperationDescription; }
        }

        public ShowAddressBookPropertiesOperation(AddressBookShell addressBookShell)
        {
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");

            this.addressBookShell = addressBookShell;
            this.addressBookShell.AddressBookChanged += HandleAddressBookChanged;

            IsEnabled = addressBookShell.AddressBook != null;
        }

        private void HandleAddressBookChanged(object sender, AddressBookChangedEventArgs addressBookChangedEventArgs)
        {
            IsEnabled = addressBookShell.AddressBook != null;
        }

        protected override void DoExecute(object parameter)
        {
            DisplayAddressBookPropertiesWindow();
        }

        private void DisplayAddressBookPropertiesWindow()
        {
            AddressBookPropertiesPresenter presenter = new AddressBookPropertiesPresenter
            {
                AddressBookShell = addressBookShell,
                View = new FormAddressBookProperties()
            };

            presenter.ShowWindow();
        }
    }
}
