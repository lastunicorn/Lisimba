// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.WinForms.ContactEditing;
using DustInTheWind.Lisimba.WinForms.ContactEditing.ContactItemEditForms;
using DustInTheWind.WinFormsCommon;
using DustInTheWind.WinFormsCommon.Utils;

namespace DustInTheWind.Lisimba.WinForms.ContactEdit
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
            nameEditor1.Bind(x => x.ActionQueue, ViewModel, x => x.ActionQueue, true, DataSourceUpdateMode.Never);
            
            birthdayView1.Bind(x => x.Birthday, ViewModel, x => x.Birthday, true, DataSourceUpdateMode.Never);
            birthdayView1.Bind(x => x.ActionQueue, ViewModel, x => x.ActionQueue, true, DataSourceUpdateMode.Never);
            birthdayView1.Bind(x => x.BiorhythmButtonViewModel, ViewModel, x => x.BiorhythmButtonViewModel, true, DataSourceUpdateMode.Never);
            
            zodiacSignView1.Bind(x => x.ZodiacSign, ViewModel, x => x.ZodiacSign, false, DataSourceUpdateMode.Never);
            
            customTreeView1.Bind(x => x.ContactItems, ViewModel, x => x.ContactItems, true, DataSourceUpdateMode.Never);
            customTreeView1.Bind(x => x.ActionQueue, ViewModel, x => x.ActionQueue, true, DataSourceUpdateMode.Never);
            
            textBoxNotes.Bind(x => x.Text, ViewModel, x => x.Notes, false, DataSourceUpdateMode.OnPropertyChanged);

            this.Bind(x => x.Enabled, ViewModel, x => x.Enabled, false);
        }

        private void buttonAddAddress_Click(object sender, EventArgs e)
        {
            ViewModel.AddAddressWasClicked();
        }

        private void buttonAddDate_Click(object sender, EventArgs e)
        {
            ViewModel.AddDateWasClicked();
        }

        private void buttonAddEmail_Click(object sender, EventArgs e)
        {
            ViewModel.AddEmailWasClicked();
        }

        private void buttonAddSocialProfileId_Click(object sender, EventArgs e)
        {
            ViewModel.AddSocialProfileIdWasClicked();
        }

        private void buttonAddPhone_Click(object sender, EventArgs e)
        {
            ViewModel.AddPhoneWasClicked();
        }

        private void buttonAddWebSite_Click(object sender, EventArgs e)
        {
            ViewModel.AddWebSiteClicked();
        }

        public void AddAddress(CustomObservableCollection<ContactItem> contactItems)
        {
            PostalAddressEditForm form = new PostalAddressEditForm
            {
                EditMode = EditMode.Create,
                ActionQueue = ViewModel.ActionQueue,
                ContactItems = contactItems,
                Location = buttonAddAddress.GetBottomLeftCorner()
            };

            form.Show();
            form.Focus();
        }

        public void AddDate(CustomObservableCollection<ContactItem> contactItems)
        {
            DateEditForm form = new DateEditForm
            {
                EditMode = EditMode.Create,
                ActionQueue = ViewModel.ActionQueue,
                ContactItems = contactItems,
                Location = buttonAddDate.GetBottomLeftCorner()
            };

            form.Show();
            form.Focus();
        }

        public void AddEmail(CustomObservableCollection<ContactItem> contactItems)
        {
            EmailEditForm form = new EmailEditForm
            {
                EditMode = EditMode.Create,
                ActionQueue = ViewModel.ActionQueue,
                ContactItems = contactItems,
                Location = buttonAddEmail.GetBottomLeftCorner()
            };

            form.Show();
            form.Focus();
        }

        public void AddSocialProfileId(CustomObservableCollection<ContactItem> contactItems)
        {
            SocialProfileEditForm form = new SocialProfileEditForm
            {
                EditMode = EditMode.Create,
                ActionQueue = ViewModel.ActionQueue,
                ContactItems = contactItems,
                Location = buttonAddSocialProfileId.GetBottomLeftCorner()
            };

            form.Show();
            form.Focus();
        }

        public void AddPhone(CustomObservableCollection<ContactItem> contactItems)
        {
            PhoneEditForm form = new PhoneEditForm
            {
                EditMode = EditMode.Create,
                ActionQueue = ViewModel.ActionQueue,
                ContactItems = contactItems,
                Location = buttonAddPhone.GetBottomLeftCorner()
            };

            form.Show();
            form.Focus();
        }

        public void AddWebSite(CustomObservableCollection<ContactItem> contactItems)
        {
            WebSiteEditForm form = new WebSiteEditForm
            {
                EditMode = EditMode.Create,
                ActionQueue = ViewModel.ActionQueue,
                ContactItems = contactItems,
                Location = buttonAddWebSite.GetBottomLeftCorner()
            };

            form.Show();
            form.Focus();
        }
    }
}