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
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class DeleteCurrentContactCommand : CommandBase<object>
    {
        private readonly CurrentData currentData;

        public override string ShortDescription
        {
            get { return "Delete the currently selected contact."; }
        }

        public DeleteCurrentContactCommand(CurrentData currentData)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            this.currentData = currentData;
            currentData.ContactChanged += HandleCurrentContactChanged;
        }

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            IsEnabled = currentData.Contact != null;
        }

        protected override void DoExecute(object parameter)
        {
            Contact contactToDelete = currentData.Contact;

            if (contactToDelete == null)
                return;

            string text = string.Format("Are you sure you wanna delete the contact {0} ?", contactToDelete);
            DialogResult dialogResult = MessageBox.Show(text, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Yes)
            {
                currentData.AddressBook.Contacts.Remove(contactToDelete);

                if (contactToDelete == currentData.Contact)
                    currentData.Contact = null;
            }
        }
    }
}
