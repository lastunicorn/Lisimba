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

using System;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Comparers;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    partial class ContactListView : UserControl
    {
        private readonly ContactsToTreeViewBinder contactsToTreeViewBinder;

        private bool ignoreCurrentContactChange;

        private AddressBookShell currentData;
        private ConfigurationService configurationService;
        private readonly ContactListViewModel viewModel;

        public CommandPool CommandPool
        {
            set
            {
                toolStripMenuItem_List_Add.Opertion = value.CreateNewContactOperation;
                toolStripMenuItem_List_Delete.Opertion = value.DeleteCurrentContactOperation;
            }
        }

        public ConfigurationService ConfigurationService
        {
            get { return configurationService; }
            set
            {
                configurationService = value;
                viewModel.SelectedSortingMethod = GetSortingType();
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

        public AddressBookShell CurrentData
        {
            get { return currentData; }
            set
            {
                if (currentData != null)
                {
                    currentData.AddressBookChanged -= HandleCurrentAddressBookChanged;
                    currentData.AddressBookSaved -= HandleCurrentAddressBookSaved;
                    currentData.ContactChanged -= HandleCurrentContactChanged;

                    if (currentData.AddressBook != null)
                    {
                        currentData.AddressBook.Changed -= HandleCurrentAddressBookContentChanged;
                        currentData.AddressBook.ContactContentChanged -= HandleContactContentChanged;
                    }
                }

                currentData = value;

                if (currentData != null)
                {
                    currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
                    currentData.AddressBookSaved += HandleCurrentAddressBookSaved;
                    currentData.ContactChanged += HandleCurrentContactChanged;

                    if (currentData.AddressBook != null)
                    {
                        currentData.AddressBook.Changed += HandleCurrentAddressBookContentChanged;
                        currentData.AddressBook.ContactContentChanged += HandleContactContentChanged;
                    }
                }

                RepopulateFromCurrentAddressBook();
            }
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

            treeViewContacts.Sort();
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
            bool currentDataContainsContacts =
                currentData != null &&
                currentData.AddressBook != null &&
                currentData.AddressBook.Contacts != null;

            contactsToTreeViewBinder.Contacts = currentDataContainsContacts
                ? currentData.AddressBook.Contacts
                : null;
        }

        public ContactListView()
        {
            InitializeComponent();

            viewModel = new ContactListViewModel();

            comboBoxSortBy.DataSource = viewModel.SortingMethods;
            comboBoxSortBy.DisplayMember = "Text";
            comboBoxSortBy.ValueMember = "SortingType";

            comboBoxSortBy.Bind(x => x.SelectedValue, viewModel, x => x.SelectedSortingMethod, false, DataSourceUpdateMode.OnPropertyChanged);

            //comboBoxSortBy.Items.AddRange(new[]
            //{
            //    new SortingComboBoxItem{ Text="Birthday (without year)", SortingType=ContactsSortingType.Birthday },
            //    new SortingComboBoxItem{ Text="Birth Date (age)", SortingType=ContactsSortingType.BirthDate },
            //    new SortingComboBoxItem{ Text="First Name", SortingType=ContactsSortingType.FirstName },
            //    new SortingComboBoxItem{ Text="Last Name", SortingType=ContactsSortingType.LastName },
            //    new SortingComboBoxItem{ Text="Nickname", SortingType=ContactsSortingType.Nickname },
            //    new SortingComboBoxItem{ Text="Nickname or Name", SortingType=ContactsSortingType.NicknameOrName }
            //});
            //comboBoxSortBy.DisplayMember = "Text";
            //comboBoxSortBy.ValueMember = "SortingType";

            treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Nickname);

            toolStripMenuItem_List_ViewBiorythm.ShortDescription = "Display the biorhythm of the selected person.";

            contactsToTreeViewBinder = new ContactsToTreeViewBinder(treeViewContacts);

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

            treeViewContacts.SelectedNode = treeNodeToSelect;
        }

        private void ResetModifiedFlags()
        {
            foreach (TreeNode treeNode in contactsToTreeViewBinder.treeNodesByContact.Values)
            {
                Contact c = treeNode.Tag as Contact;

                if (c == null)
                    continue;

                if (contactsToTreeViewBinder.modifiedContacts[c])
                {
                    contactsToTreeViewBinder.modifiedContacts[c] = false;
                    HighlightTreeNode(treeNode, false);
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

            TreeNode node = treeViewContacts.GetNodeAt(e.Location);

            if (node != null)
                treeViewContacts.SelectedNode = node;

            // Display the menu
            Contact selectedContact = currentData.Contact;
            toolStripMenuItem_List_ViewBiorythm.Enabled = (selectedContact != null && selectedContact.Birthday.IsCompleteDate);
            contextMenuStripListBox.Show(treeViewContacts, e.Location);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ignoreCurrentContactChange = true;

            try
            {
                TreeNode selectedNode = treeViewContacts.SelectedNode;
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
            TreeNode node = treeViewContacts.SelectedNode;

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
            switch (viewModel.SelectedSortingMethod)
            {
                case ContactsSortingType.Birthday:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Birthday);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.BirthDate:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.BirthDate);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.FirstName:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.FirstName);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.LastName:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.LastName);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.Nickname:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Nickname);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.NicknameOrName:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeByNicknameOrNameComparer();
                    treeViewContacts.Sort();
                    break;
            }
        }

        private void ContactListView_Load(object sender, EventArgs e)
        {
            viewModel.SelectedSortingMethod = GetSortingType();
            RepopulateFromCurrentAddressBook();
        }

        private ContactsSortingType GetSortingType()
        {
            if (configurationService == null)
                return ContactsSortingType.Birthday;

            switch (configurationService.LisimbaConfigSection.SortBy.Value)
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
    }
}
