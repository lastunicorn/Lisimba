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
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.WinForms.ContactEdit
{
    public partial class PhoneEditForm : EditBaseForm
    {
        private Phone phone;

        public Phone Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                DisplayDataInView();
            }
        }

        public CustomObservableCollection<ContactItem> ContactItems { get; set; }

        public PhoneEditForm()
        {
            InitializeComponent();

            EditMode = EditMode.Create;

            textBoxPhone.KeyDown += HandleFormKeyDown;
            textBoxComments.KeyDown += HandleFormKeyDown;
        }

        protected override void OnEditModeChanged()
        {
            Text = EditMode == EditMode.Create ? "Add Phone" : "Edit Phone";
            base.OnEditModeChanged();
        }

        private void DisplayDataInView()
        {
            textBoxPhone.Text = phone.Number;
            textBoxComments.Text = phone.Description;
        }

        protected override bool IsDataChanged()
        {
            if (phone == null)
                return textBoxPhone.Text.Length > 0 ||
                       textBoxComments.Text.Length > 0;

            return !phone.Number.Equals(textBoxPhone.Text) ||
                !phone.Description.Equals(textBoxComments.Text);
        }

        protected override IAction GetCreateAction()
        {
            Phone newPhone = ReadPhoneFromView();
            return new CreateContactItemAction(ContactItems, newPhone);
        }

        protected override IAction GetUpdateAction()
        {
            Phone newPhone = ReadPhoneFromView();
            return new UpdateContactItemAction(phone, newPhone);
        }

        private Phone ReadPhoneFromView()
        {
            string newNumber = textBoxPhone.Text;
            string newDescription = textBoxComments.Text;

            return new Phone(newNumber, newDescription);
        }
    }
}