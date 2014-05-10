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
    public partial class FormBookProperties : Form
    {
        private AddressBook book;
        public bool IsModified = false;

        public AddressBook Book
        {
            get { return book; }
            set
            {
                book = value;
                IsModified = false;

                if (value != null)
                {
                    textBoxBookName.Text = value.Name;
                    textBoxFileLocation.Text = value.FileName.Length == 0 ? "<Address book is not saved yet.>" : Path.GetFullPath(value.FileName);
                    textBoxContactsCount.Text = value.Count.ToString();
                }
            }
        }

        public FormBookProperties()
        {
            InitializeComponent();
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            if (book != null)
            {
                if (!book.Name.Equals(textBoxBookName.Text))
                {
                    book.Name = textBoxBookName.Text;
                    IsModified = true;
                }
            }
        }

        private void FormBookProperties_Shown(object sender, EventArgs e)
        {
            textBoxBookName.Focus();
        }
    }
}