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

using System.Collections.ObjectModel;
using System.Drawing;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.ContactEditing.ContactDetailsEditing
{
    public class ContactItemSetViewModel : ViewModelBase
    {
        private Image image;
        private Image addButtonImage;
        private string title;
        private ObservableCollection<ContactItem> contactItems;

        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        public Image AddButtonImage
        {
            get { return addButtonImage; }
            set
            {
                addButtonImage = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ContactItem> ContactItems
        {
            get { return contactItems; }
            set
            {
                contactItems = value;
                OnPropertyChanged();
            }
        }

        public ContactItemSetViewModel()
        {
            ContactItems = new ContactItemCollection();
        }
    }
}