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
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class PostalAddressEditForm : EditBaseForm
    {
        private PostalAddress postalAddress;
        private bool addMode;

        public PostalAddress PostalAddress
        {
            get { return postalAddress; }
            set
            {
                postalAddress = value;
                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? Resources.AddPostalAddress_WindowTitle : Resources.EditPostalAddress_WindowTitle;
            }
        }

        public PostalAddressCollection PostalAddresses { get; set; }

        public PostalAddressEditForm()
        {
            InitializeComponent();

            AddMode = false;

            textBoxAddress.KeyDown += HandleFormKeyDown;
            textBoxCity.KeyDown += HandleFormKeyDown;
            textBoxState.KeyDown += HandleFormKeyDown;
            textBoxCountry.KeyDown += HandleFormKeyDown;
            textBoxZip.KeyDown += HandleFormKeyDown;
            textBoxComments.KeyDown += HandleFormKeyDown;
        }

        protected override void UpdateData()
        {
            bool dataWasChanged = UserChangedData();

            if (!dataWasChanged)
                return;

            ReadDataFromView();

            if (AddMode && PostalAddress != null)
                PostalAddresses.Add(postalAddress);
        }

        private bool UserChangedData()
        {
            return !postalAddress.Street.Equals(textBoxAddress.Text) ||
                   !postalAddress.City.Equals(textBoxCity.Text) ||
                   !postalAddress.PostalCode.Equals(textBoxZip.Text) ||
                   !postalAddress.State.Equals(textBoxState.Text) ||
                   !postalAddress.Country.Equals(textBoxCountry.Text) ||
                   !postalAddress.Description.Equals(textBoxComments.Text);
        }

        private void DisplayDataInView()
        {
            textBoxAddress.Text = postalAddress.Street;
            textBoxCity.Text = postalAddress.City;
            textBoxZip.Text = postalAddress.PostalCode;
            textBoxState.Text = postalAddress.State;
            textBoxCountry.Text = postalAddress.Country;
            textBoxComments.Text = postalAddress.Description;
        }

        private void ReadDataFromView()
        {
            postalAddress.Street = textBoxAddress.Text;
            postalAddress.City = textBoxCity.Text;
            postalAddress.PostalCode = textBoxZip.Text;
            postalAddress.State = textBoxState.Text;
            postalAddress.Country = textBoxCountry.Text;
            postalAddress.Description = textBoxComments.Text;
        }
    }
}