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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Sorting;

namespace DustInTheWind.Lisimba.Wpf.Sections.OtherWindows.ViewModels
{
    internal class ContactListViewModel : ViewModelBase
    {
        private readonly ApplicationConfiguration applicationConfiguration;
        private readonly OpenedAddressBooks openedAddressBooks;

        private ListCollectionView contacts;
        private Contact selectedContact;
        private string searchText;
        private SortingComboBoxItem selectedSortingMethod;

        public ListCollectionView Contacts
        {
            get { return contacts; }
            private set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                openedAddressBooks.CurrentContact = value;
                OnPropertyChanged();
            }
        }

        public string SearchedText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();

                if (contacts != null)
                    contacts.Filter = ShouldContactBeVisible;
            }
        }

        public IReadOnlyList<SortingComboBoxItem> SortingMethods { get; private set; }

        public SortingComboBoxItem SelectedSortingMethod
        {
            get { return selectedSortingMethod; }
            set
            {
                selectedSortingMethod = value;
                OnPropertyChanged();

                if (Contacts != null)
                    Contacts.CustomSort = GetContactComparer();
            }
        }

        public ContactListViewModel(ApplicationConfiguration applicationConfiguration, OpenedAddressBooks openedAddressBooks)
        {
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.applicationConfiguration = applicationConfiguration;
            this.openedAddressBooks = openedAddressBooks;

            SortingMethods = new List<SortingComboBoxItem>
            {
                new SortingComboBoxItem { Text = "Birthday (without year)", SortingType = ContactsSortingType.Birthday },
                new SortingComboBoxItem { Text = "Birth Date (age)", SortingType = ContactsSortingType.BirthDate },
                new SortingComboBoxItem { Text = "First Name", SortingType = ContactsSortingType.FirstName },
                new SortingComboBoxItem { Text = "Last Name", SortingType = ContactsSortingType.LastName },
                new SortingComboBoxItem { Text = "Nickname", SortingType = ContactsSortingType.Nickname },
                new SortingComboBoxItem { Text = "Nickname or Name", SortingType = ContactsSortingType.NicknameOrName }
            };

            SelectedSortingMethod = GetSortingItem();

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            openedAddressBooks.ContactChanged += HandleContactChanged;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            Contacts = null;

            if (e.NewAddressBook != null)
            {
                Contacts = (ListCollectionView)CollectionViewSource.GetDefaultView(e.NewAddressBook.AddressBook.Contacts);
                Contacts.CustomSort = GetContactComparer();
                contacts.Filter = ShouldContactBeVisible;
            }
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            SelectedContact = openedAddressBooks.CurrentContact;
        }

        private bool ShouldContactBeVisible(object item)
        {
            Contact contact = item as Contact;

            if (contact == null)
                return false;

            if (searchText == null)
                return true;

            return searchText.Length == 0
                || contact.Name.ContainsText(searchText)
                || (contact.Notes != null && contact.Notes.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                || contact.Items.OfType<Phone>().Any(x => x.Number.Replace(" ", string.Empty).Contains(searchText));
        }

        private SortingComboBoxItem GetSortingItem()
        {
            ContactsSortingType contactsSortingType = GetSortingType();
            return SortingMethods.FirstOrDefault(x => x.SortingType == contactsSortingType);
        }

        private ContactsSortingType GetSortingType()
        {
            return applicationConfiguration == null
                ? ContactsSortingType.Birthday
                : applicationConfiguration.DefaultContactSort;
        }

        private IComparer GetContactComparer()
        {
            return SelectedSortingMethod == null
                ? null
                : ComparerFactory.GetComparer(SelectedSortingMethod.SortingType);
        }
    }
}