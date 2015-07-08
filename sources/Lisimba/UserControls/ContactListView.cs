// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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

using DustInTheWind.Lisimba.Comparers;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.UserControls
{
    partial class ContactListView : UserControl
    {
        private readonly ContactsToTreeViewBinder contactsToTreeViewBinder;

        private bool ignoreCurrentContactChange;

        private CurrentData currentData;
        private ConfigurationService configurationService;

        public CommandPool CommandPool
        {
            set
            {
                toolStripMenuItem_List_Add.Command = value.CreateNewContactCommand;
                toolStripMenuItem_List_Delete.Command = value.DeleteCurrentContactCommand;
            }
        }

        public ConfigurationService ConfigurationService
        {
            get { return configurationService; }
            set
            {
                configurationService = value;
                RefreshSortMethod();
            }
        }

        public ApplicationStatus ApplicationStatus
        {
            set
            {
                toolStripMenuItem_List_Add.ApplicationStatus = value;
                toolStripMenuItem_List_Delete.ApplicationStatus = value;
                toolStripMenuItem_List_ViewBiorythm.ApplicationStatus = value;
            }
        }

        public CurrentData CurrentData
        {
            get { return currentData; }
            set
            {
                if (currentData != null)
                {
                    currentData.AddressBookChanged -= HandleCurrentAddressBookChanged;
                    currentData.ContactChanged -= HandleCurrentContactChanged;

                    if (currentData.AddressBook != null)
                        UnhookFromAddressBook(currentData.AddressBook);
                }

                currentData = value;

                if (currentData != null)
                {
                    currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
                    currentData.ContactChanged += HandleCurrentContactChanged;

                    if (currentData.AddressBook != null)
                        HookToAddressBook(currentData.AddressBook);
                }

                RepopulateFromCurrentAddressBook();
            }
        }

        private void HookToAddressBook(AddressBook addressBook)
        {
            addressBook.Changed += HandleCurrentAddressBookContentChanged;
            addressBook.ContactContentChanged += HandleContactContentChanged;
            addressBook.AddressBookSaved += HandleCurrentAddressBookSaved;
        }

        private void UnhookFromAddressBook(AddressBook addressBook)
        {
            addressBook.Changed -= HandleCurrentAddressBookContentChanged;
            addressBook.ContactContentChanged -= HandleContactContentChanged;
            addressBook.AddressBookSaved -= HandleCurrentAddressBookSaved;
        }

        private void HandleContactContentChanged(object sender, ContactContentChangedEventArgs e)
        {
            Contact c = e.Contact;

            if (c == null)
                return;

            SetContactChangedFlag(c);

            TreeNode treeNode = contactsToTreeViewBinder.treeNodesByContact[c];

            if (treeNode == null)
                return;

            treeNode.Text = treeNode.Tag.ToString();

            treeView1.Sort();
        }

        private void HandleCurrentAddressBookSaved(object sender, EventArgs eventArgs)
        {
            ResetModifiedFlags();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs eventArgs)
        {
            RepopulateFromCurrentAddressBook();
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
            {
                e.OldAddressBook.Changed -= HandleCurrentAddressBookContentChanged;
                e.OldAddressBook.ContactContentChanged -= HandleContactContentChanged;
            }

            if (e.NewAddressBook != null)
            {
                e.NewAddressBook.Changed += HandleCurrentAddressBookContentChanged;
                e.NewAddressBook.ContactContentChanged += HandleContactContentChanged;
            }

            textBoxSearch.Text = string.Empty;
            RepopulateFromCurrentAddressBook();
        }

        private void RepopulateFromCurrentAddressBook()
        {
            bool currentDataContainsContacts = currentData != null && currentData.AddressBook != null
                     && currentData.AddressBook.Contacts != null;

            contactsToTreeViewBinder.Contacts = currentDataContainsContacts
                ? currentData.AddressBook.Contacts
                : null;
        }

        public ContactListView()
        {
            InitializeComponent();

            comboBoxSortBy.Items.AddRange(new[]
            {
                new SortingComboBoxItem{ Text="Birthday (without year)", SortingType=ContactsSortingType.Birthday },
                new SortingComboBoxItem{ Text="Birth Date (age)", SortingType=ContactsSortingType.BirthDate },
                new SortingComboBoxItem{ Text="First Name", SortingType=ContactsSortingType.FirstName },
                new SortingComboBoxItem{ Text="Last Name", SortingType=ContactsSortingType.LastName },
                new SortingComboBoxItem{ Text="Nickname", SortingType=ContactsSortingType.Nickname },
                new SortingComboBoxItem{ Text="Nickname or Name", SortingType=ContactsSortingType.NicknameOrName }
            });
            comboBoxSortBy.DisplayMember = "Text";
            comboBoxSortBy.ValueMember = "SortingType";

            treeView1.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Nickname);

            toolStripMenuItem_List_ViewBiorythm.ShortDescription = "Display the biorhythm of the selected person.";

            contactsToTreeViewBinder = new ContactsToTreeViewBinder(treeView1);

            contactsToTreeViewBinder.Filter = contact => textBoxSearch.Text.Length == 0 ||
                                                         contact.Name.FirstName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                         contact.Name.MiddleName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                         contact.Name.LastName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                         contact.Name.Nickname.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            if (ignoreCurrentContactChange)
                return;

            Contact contactToSelect = currentData.Contact;

            TreeNode treeNodeToSelect = (contactToSelect == null || !contactsToTreeViewBinder.treeNodesByContact.ContainsKey(contactToSelect))
                ? null
                : contactsToTreeViewBinder.treeNodesByContact[contactToSelect];

            treeView1.SelectedNode = treeNodeToSelect;
        }

        private void ResetModifiedFlags()
        {
            foreach (TreeNode treeNode in contactsToTreeViewBinder.treeNodesByContact.Values)
            {
                Contact c = treeNode.Tag as Contact;

                if (c == null)
                    continue;

                if (!contactsToTreeViewBinder.modifiedContacts[c])
                {
                    contactsToTreeViewBinder.modifiedContacts[c] = true;
                    HighlightTreeNode(treeNode, true);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            contactsToTreeViewBinder.RefreshDisplayedNodes();
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            TreeNode node = treeView1.GetNodeAt(e.Location);

            if (node != null)
                treeView1.SelectedNode = node;

            // Display the menu
            Contact selectedContact = currentData.Contact;
            toolStripMenuItem_List_ViewBiorythm.Enabled = (selectedContact != null && selectedContact.Birthday.IsCompleteDate);
            contextMenuStripListBox.Show(treeView1, e.Location);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ignoreCurrentContactChange = true;

            try
            {
                TreeNode selectedNode = treeView1.SelectedNode;
                Contact selectedContact = selectedNode != null ? (Contact)selectedNode.Tag : null;

                CurrentData.Contact = selectedContact;
            }
            finally
            {
                ignoreCurrentContactChange = false;
            }
        }

        private void HighlightTreeNode(TreeNode treeNode, bool value)
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

        private void SetContactChangedFlag(Contact c)
        {
            if (c == null)
                return;

            TreeNode treeNode = contactsToTreeViewBinder.treeNodesByContact[c];

            if (treeNode == null)
                return;

            if (!contactsToTreeViewBinder.modifiedContacts[c])
            {
                contactsToTreeViewBinder.modifiedContacts[c] = true;
                HighlightTreeNode(treeNode, true);
            }
        }

        private void toolStripMenuItem_List_ViewBiorythm_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;

            if (node == null)
                return;

            Contact contact = (Contact)node.Tag;

            if (contact.Birthday.IsCompleteDate)
            {
                FormBiorythm formBiorythm = new FormBiorythm();
                formBiorythm.Contact = contact;
                formBiorythm.ShowDialog(this);
            }
        }

        private void comboBoxSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortingComboBoxItem selectedItem = comboBoxSortBy.SelectedItem as SortingComboBoxItem;

            if (selectedItem == null)
                return;

            switch (selectedItem.SortingType)
            {
                case ContactsSortingType.Birthday:
                    treeView1.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Birthday);
                    treeView1.Sort();
                    break;

                case ContactsSortingType.BirthDate:
                    treeView1.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.BirthDate);
                    treeView1.Sort();
                    break;

                case ContactsSortingType.FirstName:
                    treeView1.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.FirstName);
                    treeView1.Sort();
                    break;

                case ContactsSortingType.LastName:
                    treeView1.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.LastName);
                    treeView1.Sort();
                    break;

                case ContactsSortingType.Nickname:
                    treeView1.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Nickname);
                    treeView1.Sort();
                    break;

                case ContactsSortingType.NicknameOrName:
                    treeView1.TreeViewNodeSorter = new TreeNodeByNicknameOrNameComparer();
                    treeView1.Sort();
                    break;

            }
        }

        private void ContactListView_Load(object sender, EventArgs e)
        {
            RefreshSortMethod();
            RepopulateFromCurrentAddressBook();
        }

        private void RefreshSortMethod()
        {
            if (ConfigurationService == null)
                return;

            switch (ConfigurationService.LisimbaConfigSection.SortBy.Value)
            {
                case "Birthday":
                    comboBoxSortBy.SelectedIndex = 0;
                    break;

                case "BirthDate":
                    comboBoxSortBy.SelectedIndex = 1;
                    break;

                case "FirstName":
                    comboBoxSortBy.SelectedIndex = 2;
                    break;

                case "LastName":
                    comboBoxSortBy.SelectedIndex = 3;
                    break;

                case "Nickname":
                    comboBoxSortBy.SelectedIndex = 4;
                    break;

                case "NicknameOrName":
                    comboBoxSortBy.SelectedIndex = 5;
                    break;
            }
        }
    }
}
