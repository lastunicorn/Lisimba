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

using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class AddressEditForm : EditBaseForm
    {
        private Address address;
        private bool addMode;

        public Address Address
        {
            get { return address; }
            set
            {
                address = value;
                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? "Add Address" : "Edit Address";
            }
        }

        public AddressCollection Addresses { get; set; }

        public AddressEditForm()
        {
            InitializeComponent();

            AddMode = false;

            textBoxAddress.KeyDown += FormEditBase_KeyDown;
            textBoxCity.KeyDown += FormEditBase_KeyDown;
            textBoxState.KeyDown += FormEditBase_KeyDown;
            textBoxCountry.KeyDown += FormEditBase_KeyDown;
            textBoxZip.KeyDown += FormEditBase_KeyDown;
        }

        protected override void UpdateData()
        {
            bool dataWasChanged = UserChangedData();

            if (!dataWasChanged)
                return;

            ReadDataFromView();

            if (AddMode && Address != null)
                Addresses.Add(address);
        }

        private bool UserChangedData()
        {
            return !address.Street.Equals(textBoxAddress.Text) ||
                   !address.City.Equals(textBoxCity.Text) ||
                   !address.PostalCode.Equals(textBoxZip.Text) ||
                   !address.State.Equals(textBoxState.Text) ||
                   !address.Country.Equals(textBoxCountry.Text);
        }

        private void DisplayDataInView()
        {
            textBoxAddress.Text = address.Street;
            textBoxCity.Text = address.City;
            textBoxZip.Text = address.PostalCode;
            textBoxState.Text = address.State;
            textBoxCountry.Text = address.Country;
        }

        private void ReadDataFromView()
        {
            address.Street = textBoxAddress.Text;
            address.City = textBoxCity.Text;
            address.PostalCode = textBoxZip.Text;
            address.State = textBoxState.Text;
            address.Country = textBoxCountry.Text;
        }
    }
}