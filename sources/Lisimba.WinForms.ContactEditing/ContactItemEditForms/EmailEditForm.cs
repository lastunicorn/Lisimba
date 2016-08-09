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

using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Business.Actions;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.WinForms.ContactEditing.ContactItemEditForms
{
    public partial class EmailEditForm : EditBaseForm
    {
        private Email email;

        public Email Email
        {
            get { return email; }
            set
            {
                email = value;
                DisplayDataInView();
            }
        }

        public CustomObservableCollection<ContactItem> ContactItems { get; set; }

        public EmailEditForm()
        {
            InitializeComponent();

            EditMode = EditMode.Edit;

            textBoxEmail.KeyDown += HandleFormKeyDown;
            textBoxComments.KeyDown += HandleFormKeyDown;
        }

        protected override void OnEditModeChanged()
        {
            Text = EditMode == EditMode.Create ? "Add Email" : "Edit Email";
            base.OnEditModeChanged();
        }

        private void DisplayDataInView()
        {
            textBoxEmail.Text = email.Address;
            textBoxComments.Text = email.Description;
        }

        protected override bool IsDataChanged()
        {
            if (Email == null)
                return textBoxEmail.Text.Length > 0 || textBoxComments.Text.Length > 0;

            return !email.Address.Equals(textBoxEmail.Text) ||
                   !email.Description.Equals(textBoxComments.Text);
        }

        protected override IAction GetCreateAction()
        {
            Email newEmail = ReadEmailFromView();
            return new CreateContactItemAction(ContactItems, newEmail);
        }

        protected override IAction GetUpdateAction()
        {
            Email newEmail = ReadEmailFromView();
            return new UpdateContactItemAction(email, newEmail);
        }

        private Email ReadEmailFromView()
        {
            string newAddress = textBoxEmail.Text;
            string newDescription = textBoxComments.Text;

            return new Email(newAddress, newDescription);
        }
    }
}