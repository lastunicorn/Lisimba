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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Business.Sorting;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.ContactList
{
    internal class ContactListViewModel : ViewModelBase
    {
        private readonly ApplicationConfiguration applicationConfiguration;
        private readonly AddressBooks addressBooks;
        private ContactsSortingType selectedSortingMethod;
        private string searchedText;

        private bool ignoreCurrentContactChange;

        public List<SortingComboBoxItem> SortingMethods { get; private set; }
        public ContactMenuViewModels ContactMenuViewModels { get; private set; }

        public ContactListView View { get; set; }

        public ContactsSortingType SelectedSortingMethod
        {
            get { return selectedSortingMethod; }
            set
            {
                selectedSortingMethod = value;
                OnPropertyChanged();
            }
        }

        public string SearchedText
        {
            get { return searchedText; }
            set
            {
                searchedText = value;

                ContactsToTreeViewBinder.RefreshDisplayedNodes();

                OnPropertyChanged();
            }
        }

        public ContactsToTreeViewBinder ContactsToTreeViewBinder { get; set; }

        public ContactListViewModel(ApplicationConfiguration applicationConfiguration, AddressBooks addressBooks, ContactMenuViewModels contactMenuViewModels)
        {
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (contactMenuViewModels == null) throw new ArgumentNullException("contactMenuViewModels");

            this.applicationConfiguration = applicationConfiguration;
            this.addressBooks = addressBooks;

            ContactMenuViewModels = contactMenuViewModels;

            SortingMethods = new List<SortingComboBoxItem>
            {
                new SortingComboBoxItem {Text = "Birthday (without year)", SortingType = ContactsSortingType.Birthday},
                new SortingComboBoxItem {Text = "Birth Date (age)", SortingType = ContactsSortingType.BirthDate},
                new SortingComboBoxItem {Text = "First Name", SortingType = ContactsSortingType.FirstName},
                new SortingComboBoxItem {Text = "Last Name", SortingType = ContactsSortingType.LastName},
                new SortingComboBoxItem {Text = "Nickname", SortingType = ContactsSortingType.Nickname},
                new SortingComboBoxItem {Text = "Nickname or Name", SortingType = ContactsSortingType.NicknameOrName}
            };

            SelectedSortingMethod = GetSortingType();

            addressBooks.ContactChanged += HandleCurrentContactChanged;
            addressBooks.AddressBookChanged += HandleOpenedAddressBookChanged;

            if (addressBooks.Current != null)
            {
                addressBooks.Current.AddressBook.Changed += HandleCurrentAddressBookContentChanged;
                addressBooks.Current.AddressBook.ContactContentChanged += HandleContactContentChanged;
                addressBooks.Current.Saved += HandleCurrentAddressBookSaved;
            }
        }

        private void HandleOpenedAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
            {
                e.OldAddressBook.AddressBook.Changed -= HandleCurrentAddressBookContentChanged;
                e.OldAddressBook.AddressBook.ContactContentChanged -= HandleContactContentChanged;
                e.OldAddressBook.Saved -= HandleCurrentAddressBookSaved;
            }

            if (e.NewAddressBook != null)
            {
                e.NewAddressBook.AddressBook.Changed += HandleCurrentAddressBookContentChanged;
                e.NewAddressBook.AddressBook.ContactContentChanged += HandleContactContentChanged;
                e.NewAddressBook.Saved += HandleCurrentAddressBookSaved;
            }

            searchedText = string.Empty;
            RepopulateFromCurrentAddressBook();
        }

        private void HandleCurrentAddressBookSaved(object sender, EventArgs e)
        {
            ResetModifiedFlags();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs e)
        {
            RepopulateFromCurrentAddressBook();
        }

        private void HandleContactContentChanged(object sender, ContactContentChangedEventArgs e)
        {
            Contact c = e.Contact;

            if (c == null)
                return;

            SetContactChangedFlag(c);

            TreeNode treeNode = ContactsToTreeViewBinder.treeNodesByContact[c];

            if (treeNode == null)
                return;

            treeNode.Text = treeNode.Tag.ToString();

            View.SortContacts();
        }

        private void HandleCurrentContactChanged(object sender, EventArgs e)
        {
            if (ignoreCurrentContactChange || View == null)
                return;

            Contact contactToSelect = addressBooks.CurrentContact;

            TreeNode treeNodeToSelect = (contactToSelect == null || !ContactsToTreeViewBinder.treeNodesByContact.ContainsKey(contactToSelect))
                ? null
                : ContactsToTreeViewBinder.treeNodesByContact[contactToSelect];

            View.SelectTreeNode(treeNodeToSelect);
        }

        private void SetContactChangedFlag(Contact c)
        {
            if (c == null)
                return;

            TreeNode treeNode = ContactsToTreeViewBinder.treeNodesByContact[c];

            if (treeNode == null)
                return;

            if (!ContactsToTreeViewBinder.modifiedContacts[c])
            {
                ContactsToTreeViewBinder.modifiedContacts[c] = true;
                HighlightTreeNode(treeNode, true);
            }
        }

        private void RepopulateFromCurrentAddressBook()
        {
            bool currentDataContainsContacts =
                addressBooks != null &&
                addressBooks.Current != null &&
                addressBooks.Current.AddressBook.Contacts != null;

            CustomObservableCollection<Contact> contacts = currentDataContainsContacts
                ? addressBooks.Current.AddressBook.Contacts
                : null;

            ContactsToTreeViewBinder.Contacts = contacts;
        }

        private ContactsSortingType GetSortingType()
        {
            if (applicationConfiguration == null)
                return ContactsSortingType.Birthday;

            return applicationConfiguration.DefaultContactSort;
        }

        public void ContactWasSelected()
        {
            ignoreCurrentContactChange = true;

            try
            {
                TreeNode selectedNode = View.GetSelecteContact();
                Contact selectedContact = selectedNode != null ? (Contact)selectedNode.Tag : null;

                addressBooks.CurrentContact = selectedContact;
            }
            finally
            {
                ignoreCurrentContactChange = false;
            }
        }

        private void ResetModifiedFlags()
        {
            foreach (TreeNode treeNode in ContactsToTreeViewBinder.treeNodesByContact.Values)
            {
                Contact c = treeNode.Tag as Contact;

                if (c == null)
                    continue;

                if (ContactsToTreeViewBinder.modifiedContacts[c])
                {
                    ContactsToTreeViewBinder.modifiedContacts[c] = false;
                    HighlightTreeNode(treeNode, false);
                }
            }
        }

        private static void HighlightTreeNode(TreeNode treeNode, bool value)
        {
            if (value)
            {
                treeNode.ForeColor = Color.Blue;
                treeNode.BackColor = Color.FromArgb(0xf0, 0xf0, 0xf0);
            }
            else
            {
                treeNode.ForeColor = Color.Black;
                treeNode.BackColor = Color.White;
            }
        }

        public void ViewWasLoaded()
        {
            RepopulateFromCurrentAddressBook();
        }
    }
}