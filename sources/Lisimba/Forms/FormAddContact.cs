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

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormAddContact : Form
    {
        private readonly ContactCollection contacts;

        public Contact Contact { get; private set; }

        public FormAddContact(ContactCollection contacts)
        {
            InitializeComponent();

            this.contacts = contacts;

            contactView1.Contact = new Contact();
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            Contact editedContact = contactView1.Contact;

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
            bool doesContactContainsName =
                contactToValidate.Name.FirstName.Length != 0 ||
                contactToValidate.Name.MiddleName.Length != 0 ||
                contactToValidate.Name.LastName.Length != 0 ||
                contactToValidate.Name.Nickname.Length != 0;

            if (!doesContactContainsName)
                return false;

            if (contacts == null)
                return true;

            foreach (Contact c in contacts)
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