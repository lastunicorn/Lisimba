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
    public partial class SocialProfileEditForm : EditBaseForm
    {
        private SocialProfile socialProfile;
        private bool addMode;

        public SocialProfile SocialProfile
        {
            get { return socialProfile; }
            set
            {
                socialProfile = value;
                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? "Add Social Profile" : "Edit Social Profile";
            }
        }

        public SocialProfileIdCollection SocialProfiles { get; set; }

        public SocialProfileEditForm()
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

            if (AddMode && SocialProfiles != null)
                SocialProfiles.Add(SocialProfile);
        }

        private bool UserChangedData()
        {
            return !socialProfile.Id.Equals(textBoxEmail.Text) ||
                   !socialProfile.Description.Equals(textBoxComments.Text);
        }

        private void DisplayDataInView()
        {
            textBoxEmail.Text = socialProfile.Id;
            textBoxComments.Text = socialProfile.Description;
        }

        private void ReadDataFromView()
        {
            socialProfile.Id = textBoxEmail.Text;
            socialProfile.Description = textBoxComments.Text;
        }
    }
}