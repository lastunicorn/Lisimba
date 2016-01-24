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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.ContactList
{
    internal class ContactListViewModel : ViewModelBase
    {
        private readonly ApplicationConfiguration applicationConfiguration;
        private readonly OpenedAddressBooks openedAddressBooks;
        private ContactsSortingType selectedSortingMethod;
        private string searchedText;

        private bool ignoreCurrentContactChange;

        public List<SortingComboBoxItem> SortingMethods { get; private set; }
        public NewContactOperation NewContactOperation { get; private set; }
        public DeleteCurrentContactOperation DeleteCurrentContactOperation { get; private set; }

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

        public ContactListViewModel(ApplicationConfiguration applicationConfiguration, OpenedAddressBooks openedAddressBooks, CommandPool commandPool)
        {
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (commandPool == null) throw new ArgumentNullException("commandPool");

            this.applicationConfiguration = applicationConfiguration;
            this.openedAddressBooks = openedAddressBooks;

            NewContactOperation = commandPool.NewContactOperation;
            DeleteCurrentContactOperation = commandPool.DeleteCurrentContactOperation;

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

            openedAddressBooks.ContactChanged += HandleCurrentContactChanged;
            openedAddressBooks.AddressBookChanged += HandleOpenedAddressBookChanged;

            if (openedAddressBooks.Current != null)
            {
                openedAddressBooks.Current.AddressBook.Changed += HandleCurrentAddressBookContentChanged;
                openedAddressBooks.Current.AddressBook.ContactContentChanged += HandleContactContentChanged;
                openedAddressBooks.Current.Saved += HandleCurrentAddressBookSaved;
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

        private void HandleCurrentAddressBookSaved(object sender, EventArgs eventArgs)
        {
            ResetModifiedFlags();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs eventArgs)
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

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            if (ignoreCurrentContactChange || View == null)
                return;

            Contact contactToSelect = openedAddressBooks.Contact;

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
                openedAddressBooks != null &&
                openedAddressBooks.Current != null &&
                openedAddressBooks.Current.AddressBook.Contacts != null;

            ContactCollection contacts = currentDataContainsContacts
                ? openedAddressBooks.Current.AddressBook.Contacts
                : null;

            ContactsToTreeViewBinder.Contacts = contacts;
        }

        private ContactsSortingType GetSortingType()
        {
            if (applicationConfiguration == null)
                return ContactsSortingType.Birthday;

            switch (applicationConfiguration.DefaultContactSort)
            {
                default:
                case "Birthday":
                    return ContactsSortingType.Birthday;

                case "BirthDate":
                    return ContactsSortingType.BirthDate;

                case "FirstName":
                    return ContactsSortingType.FirstName;

                case "LastName":
                    return ContactsSortingType.LastName;

                case "Nickname":
                    return ContactsSortingType.Nickname;

                case "NicknameOrName":
                    return ContactsSortingType.NicknameOrName;
            }
        }

        public void ContactWasSelected()
        {
            ignoreCurrentContactChange = true;

            try
            {
                TreeNode selectedNode = View.GetSelecteContact();
                Contact selectedContact = selectedNode != null ? (Contact)selectedNode.Tag : null;

                openedAddressBooks.Contact = selectedContact;
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