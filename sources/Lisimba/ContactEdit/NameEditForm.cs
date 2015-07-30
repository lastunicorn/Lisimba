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

using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class NameEditForm : EditBaseForm
    {
        private PersonName personName;
        public PersonName PersonName
        {
            get { return personName; }
            set
            {
                personName = value;
                DisplayDataInView();
            }
        }

        public NameEditForm()
        {
            InitializeComponent();

            textBoxFirstName.KeyDown += HandleFormKeyDown;
            textBoxMiddleName.KeyDown += HandleFormKeyDown;
            textBoxLastName.KeyDown += HandleFormKeyDown;
            textBoxNickname.KeyDown += HandleFormKeyDown;
        }

        protected override void UpdateData()
        {
            bool isAnyDataChanged = !textBoxFirstName.Text.Equals(personName.FirstName) ||
                     !textBoxMiddleName.Text.Equals(personName.MiddleName) ||
                     !textBoxLastName.Text.Equals(personName.LastName) ||
                     !textBoxNickname.Text.Equals(personName.Nickname);

            if (!isAnyDataChanged)
                return;

            ReadDataFromView();
        }

        private void DisplayDataInView()
        {
            textBoxFirstName.Text = personName.FirstName;
            textBoxMiddleName.Text = personName.MiddleName;
            textBoxLastName.Text = personName.LastName;
            textBoxNickname.Text = personName.Nickname;
        }

        private void ReadDataFromView()
        {
            personName.FirstName = textBoxFirstName.Text;
            personName.MiddleName = textBoxMiddleName.Text;
            personName.LastName = textBoxLastName.Text;
            personName.Nickname = textBoxNickname.Text;
        }
    }
}