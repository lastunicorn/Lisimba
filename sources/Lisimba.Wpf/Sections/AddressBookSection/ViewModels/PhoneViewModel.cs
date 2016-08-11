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
using System.Windows;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.ViewModels
{
    internal class PhoneViewModel : ViewModelBase
    {
        private readonly Phone phone;

        private string number;
        private string description;

        public string Number
        {
            get { return number; }
            set
            {
                if (number == value)
                    return;

                number = value;
                phone.Number = value;

                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (description == value)
                    return;

                description = value;
                phone.Description = value;

                OnPropertyChanged();
            }
        }

        public Visibility DescriptionVisibility { get; private set; }

        public Visibility DescriptionButtonVisibility { get; private set; }

        public PhoneViewModel(Phone phone)
        {
            if (phone == null) throw new ArgumentNullException("phone");

            this.phone = phone;

            number = phone.Number;
            description = phone.Description;

            phone.Changed += HandlePhoneChanged;

            DescriptionVisibility = string.IsNullOrEmpty(phone.Description) ? Visibility.Collapsed : Visibility.Visible;
            DescriptionButtonVisibility = string.IsNullOrEmpty(phone.Description) ? Visibility.Visible : Visibility.Hidden;
        }

        private void HandlePhoneChanged(object sender, EventArgs eventArgs)
        {
            Number = phone.Number;
            Description = phone.Description;
        }
    }
}
