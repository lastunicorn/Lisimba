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
        private readonly ContactEditorViewModel model;

        public ContactEditorViewModel Model
        {
            get { return model; }
        }

        readonly FormDateEdit formBirthdayEdit;
        readonly FormNameEdit formNameEdit;

        public ContactEditor()
        {
            InitializeComponent();

            model = new ContactEditorViewModel(new Zodiac()) { View = this };
            formBirthdayEdit = new FormDateEdit();
            formNameEdit = new FormNameEdit();
            CreateBindings();
        }

        private void CreateBindings()
        {
            labelFullName.Bind(x => x.Text, Model, x => x.FullName, false, DataSourceUpdateMode.Never);
            labelBirthday.Bind(x => x.Text, model, x => x.Birthday, false, DataSourceUpdateMode.OnPropertyChanged);

            pictureBoxZodiacSign.Bind(x => x.Image, model, x => x.ZodiacSignImage, true, DataSourceUpdateMode.Never);
            pictureBoxZodiacSign.Bind(x => x.Text, model, x => x.ZodiacSignText, false, DataSourceUpdateMode.Never);
            labelZodiacSign.Bind(x => x.Text, model, x => x.ZodiacSignText, false, DataSourceUpdateMode.Never);

            textBoxNotes.Bind(x => x.Text, model, x => x.Notes, false, DataSourceUpdateMode.OnPropertyChanged);

            customTreeView1.Bind(x => x.Phones, model, x => x.Phones, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Emails, model, x => x.Emails, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.WebSites, model, x => x.WebSites, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Addresses, model, x => x.Addresses, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Dates, model, x => x.Dates, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.MessengerIds, model, x => x.MessengerIds, true, DataSourceUpdateMode.Never);

            this.Bind(x => x.Enabled, model, x => x.Enabled, false);
        }

        private void labelBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            model.BirthdayEditWasRequested();
        }

        //public Image ZodiacSignImage
        //{
        //    set { pictureBoxZodiacSign.Image = value; }
        //}

        private void labelFullName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Model.NameEditWasRequested();
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

        public void EditName(PersonName name)
        {
            // client position 
            int clientX = labelFullName.Location.X;
            int clientY = labelFullName.Location.Y + labelFullName.Height;
            Point clientPoint = new Point(clientX, clientY);

            // screen position
            Point screenPoint = PointToScreen(clientPoint);

            // initialize form
            formNameEdit.Location = screenPoint;
            formNameEdit.PersonName = name;

            // show form
            formNameEdit.Show();
            formNameEdit.Focus();
        }
    }
}
