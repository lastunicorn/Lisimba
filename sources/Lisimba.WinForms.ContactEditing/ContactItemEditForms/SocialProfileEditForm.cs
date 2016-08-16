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
    public partial class SocialProfileEditForm : EditBaseForm
    {
        private SocialProfileId socialProfileId;

        public SocialProfileId SocialProfileId
        {
            get { return socialProfileId; }
            set
            {
                socialProfileId = value;
                DisplayDataInView();
            }
        }

        public CustomObservableCollection<ContactItem> ContactItems { get; set; }

        public SocialProfileEditForm()
        {
            InitializeComponent();

            EditMode = EditMode.Create;

            textBoxEmail.KeyDown += HandleFormKeyDown;
            textBoxComments.KeyDown += HandleFormKeyDown;
        }

        protected override void OnEditModeChanged()
        {
            Text = EditMode == EditMode.Create ? "Add Social Profile" : "Edit Social Profile";
            base.OnEditModeChanged();
        }

        private void DisplayDataInView()
        {
            textBoxEmail.Text = socialProfileId.Id;
            textBoxComments.Text = socialProfileId.Description;
        }

        protected override bool IsDataChanged()
        {
            if (socialProfileId == null)
                return textBoxEmail.Text.Length > 0 ||
                       textBoxComments.Text.Length > 0;

            return !socialProfileId.Id.Equals(textBoxEmail.Text) ||
                !socialProfileId.Description.Equals(textBoxComments.Text);
        }

        protected override IAction GetCreateAction()
        {
            SocialProfileId newSocialProfileId = ReadSocialProfileFromView();
            return new CreateContactItemAction(ContactItems, newSocialProfileId);
        }

        protected override IAction GetUpdateAction()
        {
            SocialProfileId newSocialProfileId = ReadSocialProfileFromView();
            return new UpdateContactItemAction(socialProfileId, newSocialProfileId);
        }

        private SocialProfileId ReadSocialProfileFromView()
        {
            string newId = textBoxEmail.Text;
            string newDescription = textBoxComments.Text;

            return new SocialProfileId(newId, newDescription);
        }
    }
}