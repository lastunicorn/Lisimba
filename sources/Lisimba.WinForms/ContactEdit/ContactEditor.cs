// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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

using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.ContactEdit
{
    /// <summary>
    /// Control to display and edit a contact.
    /// </summary>
    partial class ContactEditor : UserControl, IContactEditorView
    {
        private ContactEditorViewModel viewModel;

        public ContactEditorViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != null)
                    return;

                viewModel = value;

                if (viewModel != null)
                    CreateBindings();
            }
        }

        public ContactEditor()
        {
            InitializeComponent();
        }

        private void CreateBindings()
        {
            nameEditor1.Bind(x => x.PersonName, ViewModel, x => x.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            labelBirthday.Bind(x => x.Text, ViewModel, x => x.Birthday, false, DataSourceUpdateMode.OnPropertyChanged);

            pictureBoxZodiacSign.Bind(x => x.Image, ViewModel, x => x.ZodiacSignImage, true, DataSourceUpdateMode.Never);
            pictureBoxZodiacSign.Bind(x => x.Text, ViewModel, x => x.ZodiacSignText, false, DataSourceUpdateMode.Never);
            labelZodiacSign.Bind(x => x.Text, ViewModel, x => x.ZodiacSignText, false, DataSourceUpdateMode.Never);

            textBoxNotes.Bind(x => x.Text, ViewModel, x => x.Notes, false, DataSourceUpdateMode.OnPropertyChanged);

            customTreeView1.Bind(x => x.Phones, ViewModel, x => x.Phones, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Emails, ViewModel, x => x.Emails, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.WebSites, ViewModel, x => x.WebSites, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.PostalAddresses, ViewModel, x => x.PostalAddresses, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.Dates, ViewModel, x => x.Dates, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.SocialProfileIds, ViewModel, x => x.SocialProfileIds, true, DataSourceUpdateMode.Never);

            this.Bind(x => x.Enabled, ViewModel, x => x.Enabled, false);
        }

        private void label7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ViewModel.BirthdayEditWasRequested();
        }

        private void labelBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ViewModel.BirthdayEditWasRequested();
        }

        private void buttonAddAddress_Click(object sender, System.EventArgs e)
        {
            ViewModel.AddAddressWasClicked();
        }

        private void buttonAddDate_Click(object sender, System.EventArgs e)
        {
            ViewModel.AddDateWasClicked();
        }

        private void buttonAddEmail_Click(object sender, System.EventArgs e)
        {
            ViewModel.AddEmailWasClicked();
        }

        private void buttonAddSocialProfileId_Click(object sender, System.EventArgs e)
        {
            ViewModel.AddSocialProfileIdWasClicked();
        }

        private void buttonAddPhone_Click(object sender, System.EventArgs e)
        {
            ViewModel.AddPhoneWasClicked();
        }

        private void buttonAddWebSite_Click(object sender, System.EventArgs e)
        {
            ViewModel.AddWebSiteClicked();
        }

        public void EditBirthday(Date birthday)
        {
            BirthDateEditForm form = new BirthDateEditForm
            {
                Location = labelBirthday.GetBottomLeftCorner(),
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
                Location = buttonAddAddress.GetBottomLeftCorner(),
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
                Location = buttonAddDate.GetBottomLeftCorner(),
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
                Location = buttonAddEmail.GetBottomLeftCorner(),
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
                Location = buttonAddSocialProfileId.GetBottomLeftCorner(),
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
                Location = buttonAddPhone.GetBottomLeftCorner(),
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
                Location = buttonAddWebSite.GetBottomLeftCorner(),
                WebSite = new WebSite()
            };

            form.Show();
            form.Focus();
        }
    }
}