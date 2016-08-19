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
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Wpf.Sections.OtherWindows.ViewModels;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.ViewModels
{
    internal class AddressBookViewModel : ViewModelBase
    {
        private readonly AddressBooks addressBooks;
        private Visibility isContactEditVisible;
        private Visibility isNoContactVisible;

        public ContactListViewModel ContactListViewModel { get; private set; }
        public ContactEditorViewModel ContactEditorViewModel { get; private set; }

        public Visibility IsContactEditVisible
        {
            get { return isContactEditVisible; }
            set
            {
                isContactEditVisible = value;
                OnPropertyChanged();
            }
        }

        public Visibility IsNoContactVisible
        {
            get { return isNoContactVisible; }
            set
            {
                isNoContactVisible = value;
                OnPropertyChanged();
            }
        }

        public AddressBookViewModel(ContactListViewModel contactListViewModel, ContactEditorViewModel contactEditorViewModel,
            AddressBooks addressBooks)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.addressBooks = addressBooks;

            ContactListViewModel = contactListViewModel;
            ContactEditorViewModel = contactEditorViewModel;

            IsContactEditVisible = Visibility.Hidden;
            IsNoContactVisible = Visibility.Visible;

            addressBooks.ContactChanged += HandleContactChanged;
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            IsContactEditVisible = addressBooks.CurrentContact != null ? Visibility.Visible : Visibility.Hidden;
            IsNoContactVisible = addressBooks.CurrentContact != null ? Visibility.Hidden : Visibility.Visible;

            //todo: ContactEditorViewModel.ActionQueue = addressBooks.Current.ActionQueue;
        }
    }
}
