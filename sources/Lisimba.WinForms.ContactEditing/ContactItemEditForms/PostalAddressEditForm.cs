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
using DustInTheWind.Lisimba.WinForms.ContactEditing.Properties;

namespace DustInTheWind.Lisimba.WinForms.ContactEditing.ContactItemEditForms
{
    public partial class PostalAddressEditForm : EditBaseForm
    {
        private PostalAddress postalAddress;

        public PostalAddress PostalAddress
        {
            get { return postalAddress; }
            set
            {
                postalAddress = value;
                DisplayDataInView();
            }
        }

        public CustomObservableCollection<ContactItem> ContactItems { get; set; }

        public PostalAddressEditForm()
        {
            InitializeComponent();

            EditMode = EditMode.Create;

            textBoxAddress.KeyDown += HandleFormKeyDown;
            textBoxCity.KeyDown += HandleFormKeyDown;
            textBoxState.KeyDown += HandleFormKeyDown;
            textBoxCountry.KeyDown += HandleFormKeyDown;
            textBoxZip.KeyDown += HandleFormKeyDown;
            textBoxComments.KeyDown += HandleFormKeyDown;
        }

        protected override void OnEditModeChanged()
        {
            Text = EditMode == EditMode.Create ? Resources.AddPostalAddress_WindowTitle : Resources.EditPostalAddress_WindowTitle;
            base.OnEditModeChanged();
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

        protected override bool IsDataChanged()
        {
            if (postalAddress == null)
                return textBoxAddress.Text.Length > 0 ||
                       textBoxCity.Text.Length > 0 ||
                       textBoxZip.Text.Length > 0 ||
                       textBoxState.Text.Length > 0 ||
                       textBoxCountry.Text.Length > 0 ||
                       textBoxComments.Text.Length > 0;

            return !postalAddress.Street.Equals(textBoxAddress.Text) ||
                !postalAddress.City.Equals(textBoxCity.Text) ||
                !postalAddress.PostalCode.Equals(textBoxZip.Text) ||
                !postalAddress.State.Equals(textBoxState.Text) ||
                !postalAddress.Country.Equals(textBoxCountry.Text) ||
                !postalAddress.Description.Equals(textBoxComments.Text);
        }

        protected override IAction GetCreateAction()
        {
            PostalAddress newPostalAddress = ReadPostalAddressFromView();
            return new CreateContactItemAction(ContactItems, newPostalAddress);
        }

        protected override IAction GetUpdateAction()
        {
            PostalAddress newPostalAddress = ReadPostalAddressFromView();
            return new UpdateContactItemAction(postalAddress, newPostalAddress);
        }

        private PostalAddress ReadPostalAddressFromView()
        {
            string newStreet = textBoxAddress.Text;
            string newCity = textBoxCity.Text;
            string newPostalCode = textBoxZip.Text;
            string newState = textBoxState.Text;
            string newCountry = textBoxCountry.Text;
            string newDescription = textBoxComments.Text;

            return new PostalAddress(newStreet, newCity, newState, newPostalCode, newCountry, newDescription);
        }
    }
}