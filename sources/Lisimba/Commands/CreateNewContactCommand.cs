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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class CreateNewContactCommand : CommandBase<object>
    {
        private readonly CurrentData currentData;

        public override string ShortDescription
        {
            get { return "Create a new contact."; }
        }

        public CreateNewContactCommand(CurrentData currentData)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            this.currentData = currentData;
            currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
        }

        private void HandleCurrentAddressBookChanged(object sender, EventArgs eventArgs)
        {
            IsEnabled = currentData.AddressBook != null;
        }

        protected override void DoExecute(object parameter)
        {
            FormAddContact formAddContact = new FormAddContact(currentData);

            if (formAddContact.ShowDialog() == DialogResult.OK)
                currentData.AddressBook.Contacts.Add(formAddContact.Contact);
        }
    }
}
