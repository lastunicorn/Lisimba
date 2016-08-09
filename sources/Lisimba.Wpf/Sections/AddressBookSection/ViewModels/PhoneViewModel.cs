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
