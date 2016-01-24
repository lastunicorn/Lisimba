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

using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class PhoneEditForm : EditBaseForm
    {
        private Phone phone;
        private bool addMode;

        public Phone Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? "Add Phone" : "Edit Phone";
            }
        }

        public PhoneCollection Phones { get; set; }

        public PhoneEditForm()
        {
            InitializeComponent();

            AddMode = false;

            textBoxPhone.KeyDown += HandleFormKeyDown;
            textBoxComments.KeyDown += HandleFormKeyDown;
        }

        protected override void UpdateData()
        {
            bool dataWasChanged = UserChangedData();

            if (!dataWasChanged)
                return;

            ReadDataFromView();

            if (AddMode && Phones != null)
                Phones.Add(phone);
        }

        private bool UserChangedData()
        {
            return !phone.Number.Equals(textBoxPhone.Text) ||
                   !phone.Description.Equals(textBoxComments.Text);
        }

        private void DisplayDataInView()
        {
            textBoxPhone.Text = phone.Number;
            textBoxComments.Text = phone.Description;
        }

        private void ReadDataFromView()
        {
            phone.Number = textBoxPhone.Text;
            phone.Description = textBoxComments.Text;
        }
    }
}