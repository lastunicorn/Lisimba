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

using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.ContactEdit
{
    /// <summary>
    /// Control to display and edit a contact.
    /// </summary>
    partial class ContactEditor : UserControl, IContactEditorView
    {
        public ContactEditorViewModel Model { get; private set; }

        readonly FormDateEdit formBirthdayEdit;
        readonly FormNameEdit formNameEdit;

        public ContactEditor()
        {
            InitializeComponent();

            Model = new ContactEditorViewModel(new Zodiac()) { View = this };
            formBirthdayEdit = new FormDateEdit();
            formNameEdit = new FormNameEdit();
            CreateBindings();
        }

        private void CreateBindings()
        {
            labelFullName.Bind(x => x.Text, Model, x => x.FullName, false, DataSourceUpdateMode.Never);
            labelBirthday.Bind(x => x.Text, Model, x => x.Birthday, false, DataSourceUpdateMode.OnPropertyChanged);

            pictureBoxZodiacSign.Bind(x => x.Image, Model, x => x.ZodiacSignImage, true, DataSourceUpdateMode.Never);
            pictureBoxZodiacSign.Bind(x => x.Text, Model, x => x.ZodiacSignText, false, DataSourceUpdateMode.Never);
            labelZodiacSign.Bind(x => x.Text, Model, x => x.ZodiacSignText, false, DataSourceUpdateMode.Never);

            textBoxNotes.Bind(x => x.Text, Model, x => x.Notes, false, DataSourceUpdateMode.OnPropertyChanged);

            customTreeView1.Bind(x => x.Phones, Model, x => x.Phones, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Emails, Model, x => x.Emails, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.WebSites, Model, x => x.WebSites, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Addresses, Model, x => x.Addresses, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Dates, Model, x => x.Dates, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.MessengerIds, Model, x => x.MessengerIds, true, DataSourceUpdateMode.Never);

            this.Bind(x => x.Enabled, Model, x => x.Enabled, false);
        }

        private void labelBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Model.BirthdayEditWasRequested();
        }

        private void labelFullName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Model.NameEditWasRequested();
        }

        private void buttonAddAddress_Click(object sender, System.EventArgs e)
        {
            Model.AddAddressWasClicked();
        }

        private void buttonAddDate_Click(object sender, System.EventArgs e)
        {
            Model.AddDateWasClicked();
        }

        private void buttonAddEmail_Click(object sender, System.EventArgs e)
        {
            Model.AddEmailWasClicked();
        }

        private void buttonAddMessengerId_Click(object sender, System.EventArgs e)
        {
            Model.AddMessengerIdWasClicked();
        }

        private void buttonAddPhone_Click(object sender, System.EventArgs e)
        {
            Model.AddPhoneWasClicked();
        }

        private void buttonAddWebSite_Click(object sender, System.EventArgs e)
        {
            Model.AddWebSiteClicked();
        }

        public void EditBirthday(Date birthday)
        {
            formBirthdayEdit.Location = GetBottomLeftCorner(labelBirthday);
            formBirthdayEdit.Date = birthday;

            formBirthdayEdit.Show();
            formBirthdayEdit.Focus();
        }

        public void EditName(PersonName name)
        {
            formNameEdit.Location = GetBottomLeftCorner(labelFullName);
            formNameEdit.PersonName = name;

            formNameEdit.Show();
            formNameEdit.Focus();
        }

        public void AddAddress(AddressCollection addresses)
        {
            FormAddressEdit form = new FormAddressEdit
            {
                AddMode = true,
                Addresses = addresses,
                Location = GetBottomLeftCorner(buttonAddAddress),
                Address = new Address()
            };

            form.Show();
            form.Focus();
        }

        public void AddDate(DateCollection dates)
        {
            FormDateEdit form = new FormDateEdit
            {
                AddMode = true,
                Dates = dates,
                Location = GetBottomLeftCorner(buttonAddDate),
                Date = new Date()
            };

            form.Show();
            form.Focus();
        }

        public void AddEmail(EmailCollection emails)
        {
            FormEmailEdit form = new FormEmailEdit
            {
                AddMode = true,
                Emails = emails,
                Location = GetBottomLeftCorner(buttonAddEmail),
                Email = new Email()
            };

            form.Show();
            form.Focus();
        }

        public void AddMessengerId(MessengerIdCollection messengerIds)
        {
            FormMessengerIdEdit form = new FormMessengerIdEdit
            {
                AddMode = true,
                MessengerIds = messengerIds,
                Location = GetBottomLeftCorner(buttonAddMessengerId),
                MessengerId = new MessengerId()
            };

            form.Show();
            form.Focus();
        }

        public void AddPhone(PhoneCollection phones)
        {
            FormPhoneEdit form = new FormPhoneEdit
            {
                AddMode = true,
                Phones = phones,
                Location = GetBottomLeftCorner(buttonAddPhone),
                Phone = new Phone()
            };

            form.Show();
            form.Focus();
        }

        public void AddWebSite(WebSiteCollection webSites)
        {
            FormWebSiteEdit form = new FormWebSiteEdit
            {
                AddMode = true,
                WebSites = webSites,
                Location = GetBottomLeftCorner(buttonAddWebSite),
                WebSite = new WebSite()
            };

            form.Show();
            form.Focus();
        }

        private Point GetBottomLeftCorner(Control control)
        {
            int clientX = control.Location.X;
            int clientY = control.Location.Y + control.Height;

            Point clientPoint = new Point(clientX, clientY);

            return control.Parent.PointToScreen(clientPoint);
        }
    }
}
