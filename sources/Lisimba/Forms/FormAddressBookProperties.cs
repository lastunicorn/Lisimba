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
using System.IO;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormAddressBookProperties : Form
    {
        private AddressBook addressBook;

        public AddressBook AddressBook
        {
            get { return addressBook; }
            set
            {
                addressBook = value;
                PopulateView();
            }
        }

        private void PopulateView()
        {
            if (addressBook == null)
            {
                textBoxBookName.Text = string.Empty;
                textBoxFileLocation.Text = string.Empty;
                textBoxContactsCount.Text = "0";
            }
            else
            {
                textBoxBookName.Text = addressBook.Name;

                textBoxFileLocation.Text = addressBook.FileName == null
                    ? "<Address book is not saved yet.>"
                    : Path.GetFullPath(addressBook.FileName);

                textBoxContactsCount.Text = addressBook.Contacts.Count.ToString();
            }
        }

        public FormAddressBookProperties()
        {
            InitializeComponent();
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            if (addressBook == null)
                return;

            bool nameIsChanged = !addressBook.Name.Equals(textBoxBookName.Text);

            if (nameIsChanged)
                addressBook.Name = textBoxBookName.Text;
        }

        private void FormBookProperties_Shown(object sender, EventArgs e)
        {
            textBoxBookName.Focus();
        }
    }
}