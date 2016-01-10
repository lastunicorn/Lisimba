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
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class ShowAddressBookPropertiesOperation : ExecutableViewModelBase<object>
    {
        private readonly AddressBooks addressBooks;
        private readonly UserInterface userInterface;

        public override string ShortDescription
        {
            get { return LocalizedResources.ShowAddressBookPropertiesOperationDescription; }
        }

        public ShowAddressBookPropertiesOperation(AddressBooks addressBooks, ApplicationStatus applicationStatus, UserInterface userInterface)
            : base(applicationStatus)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.addressBooks = addressBooks;
            this.userInterface = userInterface;
            this.addressBooks.AddressBookChanged += HandleAddressBookChanged;

            IsEnabled = addressBooks.Current != null;
        }

        private void HandleAddressBookChanged(object sender, AddressBookChangedEventArgs addressBookChangedEventArgs)
        {
            IsEnabled = addressBooks.Current != null;
        }

        protected override void DoExecute(object parameter)
        {
            DisplayAddressBookPropertiesWindow();
        }

        private void DisplayAddressBookPropertiesWindow()
        {
            AddressBookPropertiesViewModel viewModel = new AddressBookPropertiesViewModel(addressBooks);
            userInterface.DisplayAddressBookProperties(viewModel);
        }
    }
}