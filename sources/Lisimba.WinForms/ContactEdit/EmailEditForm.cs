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

using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class EmailEditForm : EditBaseForm
    {
        private Email email;
        private bool addMode;

        public Email Email
        {
            get { return email; }
            set
            {
                email = value;
                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? "Add Email" : "Edit Email";
            }
        }

        public EmailCollection Emails { get; set; }

        public EmailEditForm()
        {
            InitializeComponent();

            AddMode = false;

            textBoxEmail.KeyDown += HandleFormKeyDown;
            textBoxComments.KeyDown += HandleFormKeyDown;
        }

        protected override void UpdateData()
        {
            bool isAnyDataChanged = UserChangedData();

            if (!isAnyDataChanged)
                return;

            ReadDataFromView();

            if (AddMode && Emails != null)
                Emails.Add(Email);
        }

        private bool UserChangedData()
        {
            return !email.Address.Equals(textBoxEmail.Text) ||
                   !email.Description.Equals(textBoxComments.Text);
        }

        private void DisplayDataInView()
        {
            textBoxEmail.Text = email.Address;
            textBoxComments.Text = email.Description;
        }

        private void ReadDataFromView()
        {
            email.Address = textBoxEmail.Text;
            email.Description = textBoxComments.Text;
        }
    }
}