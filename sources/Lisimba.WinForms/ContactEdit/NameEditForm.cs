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

using System;
using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Business.Actions;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.WinForms.ContactEdit
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

        protected override bool IsDataChanged()
        {
            if (personName == null)
                return textBoxFirstName.Text.Length > 0 ||
                       textBoxMiddleName.Text.Length > 0 ||
                       textBoxLastName.Text.Length > 0 ||
                       textBoxNickname.Text.Length > 0;

            return !textBoxFirstName.Text.Equals(personName.FirstName) ||
                !textBoxMiddleName.Text.Equals(personName.MiddleName) ||
                !textBoxLastName.Text.Equals(personName.LastName) ||
                !textBoxNickname.Text.Equals(personName.Nickname);
        }

        protected override IAction GetCreateAction()
        {
            throw new NotSupportedException();
        }

        protected override IAction GetUpdateAction()
        {
            PersonName newPersonName = ReadPersonNameFromView();
            return new UpdateContactItemAction(personName, newPersonName);
        }

        private void DisplayDataInView()
        {
            textBoxFirstName.Text = personName.FirstName;
            textBoxMiddleName.Text = personName.MiddleName;
            textBoxLastName.Text = personName.LastName;
            textBoxNickname.Text = personName.Nickname;
        }

        private PersonName ReadPersonNameFromView()
        {
            return new PersonName
            {
                FirstName = textBoxFirstName.Text,
                MiddleName = textBoxMiddleName.Text,
                LastName = textBoxLastName.Text,
                Nickname = textBoxNickname.Text
            };
        }
    }
}