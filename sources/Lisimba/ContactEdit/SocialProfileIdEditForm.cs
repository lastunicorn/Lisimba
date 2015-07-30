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
    public partial class SocialProfileIdEditForm : EditBaseForm
    {
        private SocialProfileId socialProfileId;
        private bool addMode;

        public SocialProfileId SocialProfileId
        {
            get { return socialProfileId; }
            set
            {
                socialProfileId = value;
                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? "Add Social Profile Id" : "Edit Social Profile Id";
            }
        }

        public SocialProfileIdCollection SocialProfileIds { get; set; }

        public SocialProfileIdEditForm()
        {
            InitializeComponent();

            AddMode = false;

            textBoxEmail.KeyDown += FormEditBase_KeyDown;
            textBoxComments.KeyDown += FormEditBase_KeyDown;
        }

        protected override void UpdateData()
        {
            bool isAnyDataChanged = UserChangedData();

            if (!isAnyDataChanged)
                return;

            ReadDataFromView();

            if (AddMode && SocialProfileIds != null)
                SocialProfileIds.Add(SocialProfileId);
        }

        private bool UserChangedData()
        {
            return !socialProfileId.Id.Equals(textBoxEmail.Text) ||
                   !socialProfileId.Description.Equals(textBoxComments.Text);
        }

        private void DisplayDataInView()
        {
            textBoxEmail.Text = socialProfileId.Id;
            textBoxComments.Text = socialProfileId.Description;
        }

        private void ReadDataFromView()
        {
            socialProfileId.Id = textBoxEmail.Text;
            socialProfileId.Description = textBoxComments.Text;
        }
    }
}
