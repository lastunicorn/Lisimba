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

        public ContactEditor()
        {
            InitializeComponent();

            Model = new ContactEditorViewModel(new Zodiac()) { View = this };
            CreateBindings();
        }

        private void CreateBindings()
        {
            nameEditor1.Bind(x => x.PersonName, Model, x => x.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            labelBirthday.Bind(x => x.Text, Model, x => x.Birthday, false, DataSourceUpdateMode.OnPropertyChanged);

            pictureBoxZodiacSign.Bind(x => x.Image, Model, x => x.ZodiacSignImage, true, DataSourceUpdateMode.Never);
            pictureBoxZodiacSign.Bind(x => x.Text, Model, x => x.ZodiacSignText, false, DataSourceUpdateMode.Never);
            labelZodiacSign.Bind(x => x.Text, Model, x => x.ZodiacSignText, false, DataSourceUpdateMode.Never);

            textBoxNotes.Bind(x => x.Text, Model, x => x.Notes, false, DataSourceUpdateMode.OnPropertyChanged);

            customTreeView1.Bind(x => x.Phones, Model, x => x.Phones, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Emails, Model, x => x.Emails, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.WebSites, Model, x => x.WebSites, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.PostalAddresses, Model, x => x.PostalAddresses, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Dates, Model, x => x.Dates, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.SocialProfileIds, Model, x => x.SocialProfileIds, true, DataSourceUpdateMode.Never);

            this.Bind(x => x.Enabled, Model, x => x.Enabled, false);
        }

        private void label7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Model.BirthdayEditWasRequested();
        }

        private void labelBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Model.BirthdayEditWasRequested();
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

        private void buttonAddSocialProfileId_Click(object sender, System.EventArgs e)
        {
            Model.AddSocialProfileIdWasClicked();
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
            BirthDateEditForm form = new BirthDateEditForm
            {
                Location = GetBottomLeftCorner(labelBirthday),
                Date = birthday
            };

            form.Show();
            form.Focus();
        }

        public void AddAddress(PostalAddressCollection postalAddresses)
        {
            PostalAddressEditForm form = new PostalAddressEditForm
            {
                AddMode = true,
                PostalAddresses = postalAddresses,
                Location = GetBottomLeftCorner(buttonAddAddress),
                PostalAddress = new PostalAddress()
            };

            form.Show();
            form.Focus();
        }

        public void AddDate(DateCollection dates)
        {
            DateEditForm form = new DateEditForm
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
            EmailEditForm form = new EmailEditForm
            {
                AddMode = true,
                Emails = emails,
                Location = GetBottomLeftCorner(buttonAddEmail),
                Email = new Email()
            };

            form.Show();
            form.Focus();
        }

        public void AddSocialProfileId(SocialProfileIdCollection socialProfileIds)
        {
            SocialProfileEditForm form = new SocialProfileEditForm
            {
                AddMode = true,
                SocialProfiles = socialProfileIds,
                Location = GetBottomLeftCorner(buttonAddSocialProfileId),
                SocialProfile = new SocialProfile()
            };

            form.Show();
            form.Focus();
        }

        public void AddPhone(PhoneCollection phones)
        {
            PhoneEditForm form = new PhoneEditForm
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
            WebSiteEditForm form = new WebSiteEditForm
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
