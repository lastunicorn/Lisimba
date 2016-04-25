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
using DustInTheWind.Lisimba.Business.AddressBookManagement;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class ContactEditorViewModel : ViewModelBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private string name;
        private string notes;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
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

        public ContactEditorViewModel(OpenedAddressBooks openedAddressBooks)
        {
            this.openedAddressBooks = openedAddressBooks;
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            openedAddressBooks.ContactChanged += HandleCurrentContactChanged;

            RefreshDisplayedData();
        }

        private void HandleCurrentContactChanged(object sender, EventArgs e)
        {
            RefreshDisplayedData();
        }

        private void RefreshDisplayedData()
        {
            if (openedAddressBooks.CurrentContact == null)
            {
                Name = string.Empty;
                Notes = string.Empty;
            }
            else
            {
                Name = openedAddressBooks.CurrentContact.Name.ToString();
                Notes = openedAddressBooks.CurrentContact.Notes;
            }
        }
    }
}