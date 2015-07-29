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
using DustInTheWind.Lisimba.Presenters;

namespace DustInTheWind.Lisimba.Forms
{
    partial class AddContactForm : Form, IAddContactView
    {
        public AddContactPresenter Presenter { private get; set; }

        public AddContactForm()
        {
            InitializeComponent();
        }

        public Contact Contact
        {
            get { return contactView1.Model.Contact; }
            set { contactView1.Model.Contact = value; }
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            Presenter.OkButtonWasClicked();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Presenter.CloseButtonWasClicked();
        }

        private void FormAddContact_Load(object sender, EventArgs e)
        {
            Presenter.ViewWasLoaded();
        }
    }
}