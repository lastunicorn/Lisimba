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

using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.ContactEdit
{
    internal class NameEditorLabelViewModel : ViewModelBase
    {
        private bool errorVisible = true;
        private string firstName;
        private string middleName;
        private string lastName;
        private string nickname;
        private bool firstNameVisible;
        private bool middleNameVisible;
        private bool lastNameVisible;
        private bool nicknameVisible;

        public bool ErrorVisible
        {
            get { return errorVisible; }
            set
            {
                errorVisible = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();

                FirstNameVisible = !string.IsNullOrEmpty(firstName);
            }
        }

        private bool IsAnyNameVisible()
        {
            return FirstNameVisible || MiddleNameVisible || LastNameVisible || NicknameVisible;
        }

        public bool FirstNameVisible
        {
            get { return firstNameVisible; }
            private set
            {
                firstNameVisible = value;
                OnPropertyChanged();

                ErrorVisible = !IsAnyNameVisible();
            }
        }

        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                OnPropertyChanged();

                MiddleNameVisible = !string.IsNullOrEmpty(middleName);
            }
        }

        public bool MiddleNameVisible
        {
            get { return middleNameVisible; }
            set
            {
                middleNameVisible = value;
                OnPropertyChanged();

                ErrorVisible = !IsAnyNameVisible();
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();

                LastNameVisible = !string.IsNullOrEmpty(lastName);
            }
        }

        public bool LastNameVisible
        {
            get { return lastNameVisible; }
            set
            {
                lastNameVisible = value;
                OnPropertyChanged();

                ErrorVisible = !IsAnyNameVisible();
            }
        }

        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                OnPropertyChanged();

                NicknameVisible = !string.IsNullOrEmpty(nickname);
            }
        }

        public bool NicknameVisible
        {
            get { return nicknameVisible; }
            set
            {
                nicknameVisible = value;
                OnPropertyChanged();

                ErrorVisible = !IsAnyNameVisible();
            }
        }
    }
}