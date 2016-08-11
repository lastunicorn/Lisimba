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

        public string Number
        {
            get { return phone.Number; }
            set { phone.Number = value; }
        }

        public string Description
        {
            get { return phone.Description; }
            set { phone.Description = value; }
        }

        public Visibility DescriptionVisibility { get; private set; }

        public Visibility DescriptionButtonVisibility { get; private set; }

        public PhoneViewModel(Phone phone)
        {
            if (phone == null) throw new ArgumentNullException("phone");

            this.phone = phone;
            phone.Changed += HandlePhoneChanged;

            DescriptionVisibility = string.IsNullOrEmpty(phone.Description) ? Visibility.Collapsed : Visibility.Visible;
            DescriptionButtonVisibility = string.IsNullOrEmpty(phone.Description) ? Visibility.Visible : Visibility.Hidden;
        }

        private void HandlePhoneChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("Number");
            OnPropertyChanged("Description");
        }
    }
}
