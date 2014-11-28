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
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class ShowAddressBookPropertiesCommand : CommandBase<object>
    {
        private readonly CurrentData currentData;

        public override string ShortDescription
        {
            get { return "Display the address book properties."; }
        }

        public ShowAddressBookPropertiesCommand(CurrentData currentData)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            this.currentData = currentData;
        }

        protected override void DoExecute(object parameter)
        {
            DisplayAddressBookPropertiesWindow();
        }

        private void DisplayAddressBookPropertiesWindow()
        {
            AddressBookPropertiesPresenter presenter = new AddressBookPropertiesPresenter
            {
                AddressBook = currentData.AddressBook,
                View = new FormAddressBookProperties()
            };

            presenter.ShowWindow();
        }
    }
}
