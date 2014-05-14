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
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Forms
{
    partial class FormAddContact : Form
    {
        private readonly CurrentData currentData;

        public Contact Contact { get; private set; }

        public FormAddContact(CurrentData currentData)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            this.currentData = currentData;

            InitializeComponent();

            contactView1.Presenter.Contact = new Contact();
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            Contact editedContact = contactView1.Presenter.Contact;

            bool isContactValid = ValidateContact(editedContact);

            if (!isContactValid)
            {
                MessageBox.Show("Please enter at least one of the fields marked with \"*\".", "Insufficient data.",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                DialogResult = DialogResult.None;
                return;
            }

            Contact = editedContact;
        }

        private bool ValidateContact(Contact contactToValidate)
        {
            bool isNameFilled =
                contactToValidate.Name.FirstName.Length != 0 ||
                contactToValidate.Name.MiddleName.Length != 0 ||
                contactToValidate.Name.LastName.Length != 0 ||
                contactToValidate.Name.Nickname.Length != 0;

            if (!isNameFilled)
                return false;

            if (currentData.AddressBook == null)
                return true;

            foreach (Contact c in currentData.AddressBook.Contacts)
            {
                bool contactAlreadyExists =
                         c.Name.FirstName.Equals(contactToValidate.Name.FirstName) &&
                         c.Name.MiddleName.Equals(contactToValidate.Name.MiddleName) &&
                         c.Name.LastName.Equals(contactToValidate.Name.LastName) &&
                         c.Name.Nickname.Equals(contactToValidate.Name.Nickname);

                if (contactAlreadyExists)
                    return false;
            }

            return true;
        }
    }
}