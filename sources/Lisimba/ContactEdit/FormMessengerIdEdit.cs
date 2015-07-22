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
    public partial class FormMessengerIdEdit : FormEditBase
    {
        private MessengerId messengerId;
        private bool addMode;

        public MessengerId MessengerId
        {
            get { return messengerId; }
            set
            {
                messengerId = value;
                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? "Add messenger id" : "Edit messenger id";
            }
        }

        public MessengerIdCollection MessengerIds { get; set; }

        public FormMessengerIdEdit()
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

            if (AddMode && MessengerIds != null)
                MessengerIds.Add(MessengerId);
        }

        private bool UserChangedData()
        {
            return !messengerId.Id.Equals(textBoxEmail.Text) ||
                   !messengerId.Description.Equals(textBoxComments.Text);
        }

        private void DisplayDataInView()
        {
            textBoxEmail.Text = messengerId.Id;
            textBoxComments.Text = messengerId.Description;
        }

        private void ReadDataFromView()
        {
            messengerId.Id = textBoxEmail.Text;
            messengerId.Description = textBoxComments.Text;
        }
    }
}
