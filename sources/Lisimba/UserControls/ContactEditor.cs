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
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    /// <summary>
    /// Control to display and edit a contact.
    /// </summary>
    partial class ContactEditor : UserControl, IContactEditorView
    {
        private readonly ContactViewPresenter presenter;

        public ContactViewPresenter Presenter
        {
            get { return presenter; }
        }

        readonly FormDateEdit formBirthdayEdit;

        public ContactEditor()
        {
            InitializeComponent();

            presenter = new ContactViewPresenter(new Zodiac()) { View = this };

            formBirthdayEdit = new FormDateEdit();
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            presenter.FirstNameWasChanged();
        }

        private void textBoxMiddleName_TextChanged(object sender, EventArgs e)
        {
            presenter.MiddleNameWasChanged();
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            presenter.LastNameWasChanged();
        }

        private void textBoxNickname_TextChanged(object sender, EventArgs e)
        {
            presenter.NicknameWasChanged();
        }

        private void labelBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            presenter.BirthdayEditWasRequested();
        }

        private void textBoxNotes_TextChanged(object sender, EventArgs e)
        {
            presenter.NotesWasChanged();
        }

        public string FirstName
        {
            get { return textBoxFirstName.Text; }
            set { textBoxFirstName.Text = value; }
        }

        public string MiddleName
        {
            get { return textBoxMiddleName.Text; }
            set { textBoxMiddleName.Text = value; }
        }

        public string LastName
        {
            get { return textBoxLastName.Text; }
            set { textBoxLastName.Text = value; }
        }

        public string Nickname
        {
            get { return textBoxNickname.Text; }
            set { textBoxNickname.Text = value; }
        }

        public string Birthday
        {
            get { return labelBirthday.Text; }
            set { labelBirthday.Text = value; }
        }

        public Image ZodiacSignImage
        {
            set { pictureBoxZodiacSign.Image = value; }
        }

        public string ZodiacSignText
        {
            set { labelZodiacSign.Text = value; }
        }

        public string Notes
        {
            get { return textBoxNotes.Text; }
            set { textBoxNotes.Text = value; }
        }

        public PhoneCollection Phones
        {
            set { customTreeView1.Phones = value; }
        }

        public EmailCollection Emails
        {
            set { customTreeView1.Emails = value; }
        }

        public WebSiteCollection WebSites
        {
            set { customTreeView1.WebSites = value; }
        }

        public AddressCollection Addresses
        {
            set { customTreeView1.Addresses = value; }
        }

        public DateCollection Dates
        {
            set { customTreeView1.Dates = value; }
        }

        public MessengerIdCollection MessengerIds
        {
            set { customTreeView1.MessengerIds = value; }
        }

        public void EditBirthday(Date birthday)
        {
            // client position 
            int clientX = labelBirthday.Location.X;
            int clientY = labelBirthday.Location.Y + labelBirthday.Height;
            Point clientPoint = new Point(clientX, clientY);

            // screen position
            Point screenPoint = PointToScreen(clientPoint);

            // initialize form
            formBirthdayEdit.Location = screenPoint;
            formBirthdayEdit.Date = birthday;

            // show form
            formBirthdayEdit.Show();
            formBirthdayEdit.Focus();
        }
    }
}
