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
using System.Windows.Media.Imaging;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class ContactEditorViewModel : ViewModelBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private string name;
        private string notes;
        private BitmapSource picture;
        private ZodiacSignViewModel zodiacSignViewModel;
        private ContactItemCollection contactItems;
        private Date birthday;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public BitmapSource Picture
        {
            get { return picture; }
            set
            {
                picture = value;
                OnPropertyChanged();
            }
        }

        public ZodiacSignViewModel ZodiacSignViewModel
        {
            get { return zodiacSignViewModel; }
            set
            {
                zodiacSignViewModel = value;
                OnPropertyChanged();
            }
        }

        public ContactItemCollection ContactItems
        {
            get { return contactItems; }
            set
            {
                contactItems = value;
                OnPropertyChanged();
            }
        }

        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged();
            }
        }

        public Date Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged();
            }
        }

        public ImageClickCommand ImageClickCommand { get; set; }

        public ContactEditorViewModel(OpenedAddressBooks openedAddressBooks, ZodiacSignViewModel zodiacSignViewModel)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (zodiacSignViewModel == null) throw new ArgumentNullException("zodiacSignViewModel");

            this.openedAddressBooks = openedAddressBooks;
            this.zodiacSignViewModel = zodiacSignViewModel;

            ImageClickCommand = new ImageClickCommand();

            openedAddressBooks.ContactChanging += HandleCurrentContactChanging;
            openedAddressBooks.ContactChanged += HandleCurrentContactChanged;

            RefreshDisplayedData();
        }

        private void HandleCurrentContactChanging(object sender, EventArgs e)
        {
            Contact currentContact = openedAddressBooks.CurrentContact;

            if (currentContact != null)
                currentContact.Changed -= HandleContactChanged;
        }

        private void HandleCurrentContactChanged(object sender, EventArgs e)
        {
            Contact currentContact = openedAddressBooks.CurrentContact;

            if (currentContact != null)
                currentContact.Changed += HandleContactChanged;

            RefreshDisplayedData();
        }

        private void RefreshDisplayedData()
        {
            if (openedAddressBooks.CurrentContact == null)
            {
                Name = string.Empty;
                Picture = Resources.no_user_128.ToBitmapSource();
                Birthday = null;
                zodiacSignViewModel.ZodiacSign = ZodiacSign.NotSpecified;
                ContactItems = null;
                Notes = string.Empty;

                ImageClickCommand.Contact = null;
            }
            else
            {
                Contact currentContact = openedAddressBooks.CurrentContact;

                Name = currentContact.Name.ToString();
                Picture = currentContact.Picture.ToBitmapSource() ?? Resources.no_user_128.ToBitmapSource();
                Birthday = currentContact.Birthday;
                zodiacSignViewModel.ZodiacSign = currentContact.ZodiacSign;
                ContactItems = currentContact.Items;
                Notes = currentContact.Notes;

                ImageClickCommand.Contact = currentContact;
            }
        }

        private void HandleContactChanged(object sender, EventArgs eventArgs)
        {
            RefreshDisplayedData();
        }
    }
}