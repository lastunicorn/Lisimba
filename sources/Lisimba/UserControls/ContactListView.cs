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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Comparers;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    partial class ContactListView : UserControl
    {
        private ContactCollection contacts;
        private readonly Dictionary<Contact, bool> modifiedContacts = new Dictionary<Contact, bool>();
        private readonly Dictionary<Contact, TreeNode> treeNodesByContact = new Dictionary<Contact, TreeNode>();

        private bool ignoreCurrentContactChange;

        private bool allowSort;

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Indicates if the Contacts will be shown sorted or not.")]
        public bool AllowSort
        {
            get { return allowSort; }
            set
            {
                allowSort = value;
                treeView1.Sort();
            }
        }

        private ContactsSortingType sortField = ContactsSortingType.NicknameOrName;
        private CurrentData currentData;
        private ConfigurationService configurationService;

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Specifies the field of the Contact used to sort them. Available only if AllowSort is true.")]
        public ContactsSortingType SortField
        {
            get { return sortField; }
            set
            {
                if (!allowSort) return;

                sortField = value;
                switch (value)
                {
                    case ContactsSortingType.Birthday:
                        comboBoxSortBy.SelectedIndex = 0;
                        break;

                    case ContactsSortingType.BirthDate:
                        comboBoxSortBy.SelectedIndex = 1;
                        break;

                    case ContactsSortingType.FirstName:
                        comboBoxSortBy.SelectedIndex = 2;
                        break;

                    case ContactsSortingType.LastName:
                        comboBoxSortBy.SelectedIndex = 3;
                        break;

                    case ContactsSortingType.Nickname:
                        comboBoxSortBy.SelectedIndex = 4;
                        break;

                    case ContactsSortingType.NicknameOrName:
                        comboBoxSortBy.SelectedIndex = 5;
                        break;
                }
            }
        }

        [Browsable(false)]
        public Contact SelectedContact
        {
            get
            {
                TreeNode selectedNode = treeView1.SelectedNode;
                if (selectedNode != null)
                    return (Contact)selectedNode.Tag;
                else
                    return null;
            }
        }

        [Browsable(true)]
        [Category("Data")]
        [Description("Specifies the initial text of the search filter.")]
        public string SearchText
        {
            get { return textBoxSearch.Text; }
            set { textBoxSearch.Text = value; }
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

                PopulateFromCurrentAddressBook();
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
            SetContactChangedFlag(e.Contact, true);
        }

        private void HandleCurrentAddressBookSaved(object sender, EventArgs eventArgs)
        {
            ResetModifiedFlags();
        }

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

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs eventArgs)
        {
            PopulateFromCurrentAddressBook();
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

            SearchText = string.Empty;
            PopulateFromCurrentAddressBook();
        }

        private void PopulateFromCurrentAddressBook()
        {
            Clear();

            if (currentData == null || currentData.AddressBook == null || currentData.AddressBook.Contacts == null)
            {
                contacts = null;
            }
            else
            {
                contacts = currentData.AddressBook.Contacts;

                foreach (Contact contact in contacts)
                {
                    modifiedContacts.Add(contact, false);

                    TreeNode treeNode = new TreeNode(contact.ToString()) { Tag = contact };
                    treeNodesByContact.Add(contact, treeNode);
                }
            }

            PopulateTreeView();
        }

        public StatusService StatusService
        {
            set
            {
                toolStripMenuItem_List_Add.StatusService = value;
                toolStripMenuItem_List_Delete.StatusService = value;
                toolStripMenuItem_List_ViewBiorythm.StatusService = value;
            }
        }

        public ContactListView()
        {
            InitializeComponent();
            treeView1.TreeViewNodeSorter = new TreeNodeByNicknameComparer();

            toolStripMenuItem_List_ViewBiorythm.ShortDescription = "Display the biorhythm of the selected person.";
        }

        //public bool IsModified(Contact c)
        //{
        //    return this.modifiedContacts[c];
        //}

        //public TreeNode GetTreeNode(Contact c)
        //{
        //    return this.treeNodes[c];
        //}

        //#region Event ContactListChanged

        //public event EventHandler ContactListChanged;

        //protected virtual void OnContactListChanged(EventArgs e)
        //{
        //    if (ContactListChanged != null)
        //        ContactListChanged(this, e);
        //}

        //#endregion

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            if (ignoreCurrentContactChange)
                return;

            Contact contactToSelect = currentData.Contact;

            TreeNode treeNodeToSelect = (contactToSelect == null || !treeNodesByContact.ContainsKey(contactToSelect))
                ? null
                : treeNodesByContact[contactToSelect];

            treeView1.SelectedNode = treeNodeToSelect;
        }

        public void Clear()
        {
            contacts = new ContactCollection();

            modifiedContacts.Clear();
            treeNodesByContact.Clear();
        }

        private void ResetModifiedFlags()
        {
            IEnumerable<Contact> modified = contacts.Where(contact => modifiedContacts[contact]);

            foreach (Contact contact in modified)
            {
                SetContactChangedFlag(contact, false);
            }
        }

        private void PopulateTreeView()
        {
            //TreeNode node = null;
            //Contact selectedContact = null;
            TreeNode selectedNode = null;
            List<TreeNode> nodeList = new List<TreeNode>();

            IComparer comparer = treeView1.TreeViewNodeSorter;

            //if (this.treeView1.SelectedNode != null)
            selectedNode = treeView1.SelectedNode;

            treeView1.Nodes.Clear();

            if (contacts == null)
                return;

            for (int i = 0; i < contacts.Count; i++)
            {
                Contact contact = contacts[i];

                if (textBoxSearch.Text.Length == 0 ||
                    contact.Name.FirstName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.MiddleName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.LastName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.Nickname.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    TreeNode treeNode = treeNodesByContact[contact];

                    if (allowSort)
                    {
                        bool inserted = false;

                        //for (int j = 0; j < this.treeView1.Nodes.Count; j++)
                        //{
                        //    if (comparer.Compare(treeNode, this.treeView1.Nodes[j]) < 0)
                        //    {
                        //        this.treeView1.Nodes.Insert(j, treeNode);
                        //        inserted = true;
                        //        break;
                        //    }
                        //}

                        //if (!inserted)
                        //    this.treeView1.Nodes.Add(treeNode);

                        for (int j = 0; j < nodeList.Count; j++)
                        {
                            if (comparer.Compare(treeNode, nodeList[j]) < 0)
                            {
                                nodeList.Insert(j, treeNode);
                                inserted = true;
                                break;
                            }
                        }

                        if (!inserted)
                            nodeList.Add(treeNode);
                    }
                    else
                    {
                        nodeList.Add(treeNode);
                    }

                    //this.HighlightTreeNode(treeNode, this.modifiedContacts[contact]);
                }
            }

            treeView1.Nodes.AddRange(nodeList.ToArray());

            //this.treeView1.SelectedNode = selectedNode;
            //if (this.treeView1.SelectedNode != selectedNode)
            //{
            //    TreeNode node = this.treeView1.SelectedNode;
            //    this.OnSelectedContactChanged(new SelectedContactChangedEventArgs(node != null ? (Contact)node.Tag : null));
            //}

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            PopulateTreeView();
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

        private void SetContactChangedFlag(Contact c, bool value)
        {
            if (c == null) return;

            TreeNode node = treeNodesByContact[c];
            if (node == null) return;

            if (value != modifiedContacts[c])
            {
                modifiedContacts[c] = value;
                HighlightTreeNode(node, value);
            }

            node.Text = node.Tag.ToString();
        }

        private void Add(Contact contact)
        {
            // todo: when contact is added, add only the corresponding tree node instead of refreshing the whole tree.

            TreeNode newTreeNode = new TreeNode(contact.ToString()) { Tag = contact };

            if (allowSort)
            {
                IComparer comparer = treeView1.TreeViewNodeSorter;
                bool isInserted = false;

                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    if (comparer.Compare(newTreeNode, treeView1.Nodes[i]) >= 0)
                        continue;

                    treeView1.Nodes.Insert(i, newTreeNode);
                    isInserted = true;
                    break;
                }

                if (!isInserted)
                    treeView1.Nodes.Add(newTreeNode);
            }
            else
            {
                treeView1.Nodes.Add(newTreeNode);
            }

            treeNodesByContact.Add(contact, newTreeNode);
            modifiedContacts.Add(contact, true);
            HighlightTreeNode(newTreeNode, true);

            treeView1.SelectedNode = newTreeNode;
        }

        private void RemoveContact(Contact contact)
        {
            // todo: when contact is deleted, remove only the corresponding tree node instead of refreshing the whole tree.

            if (contact == null || !treeNodesByContact.ContainsKey(contact))
                return;

            TreeNode nodeToRemove = treeNodesByContact[contact];
            TreeNode nodeToSelect = nodeToRemove.NextNode;

            modifiedContacts.Remove(contact);
            treeNodesByContact.Remove(contact);

            treeView1.Nodes.Remove(nodeToRemove);

            if (treeView1.Nodes.Count > 0)
                treeView1.SelectedNode = nodeToSelect ?? treeView1.Nodes[0].LastNode;
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
            if (!allowSort) return;

            switch (comboBoxSortBy.SelectedIndex)
            {
                case 0:
                    SortField = ContactsSortingType.Birthday;
                    treeView1.TreeViewNodeSorter = new TreeNodeByBirthdayComparer();
                    treeView1.Sort();
                    break;

                case 1:
                    SortField = ContactsSortingType.BirthDate;
                    treeView1.TreeViewNodeSorter = new TreeNodeByBirthDateComparer();
                    treeView1.Sort();
                    break;

                case 2:
                    SortField = ContactsSortingType.FirstName;
                    treeView1.TreeViewNodeSorter = new TreeNodeByFirstNameComparer();
                    treeView1.Sort();
                    break;

                case 3:
                    SortField = ContactsSortingType.LastName;
                    treeView1.TreeViewNodeSorter = new TreeNodeByLastNameComparer();
                    treeView1.Sort();
                    break;

                case 4:
                    SortField = ContactsSortingType.Nickname;
                    treeView1.TreeViewNodeSorter = new TreeNodeByNicknameComparer();
                    treeView1.Sort();
                    break;

                case 5:
                    SortField = ContactsSortingType.NicknameOrName;
                    treeView1.TreeViewNodeSorter = new TreeNodeByNicknameOrNameComparer();
                    treeView1.Sort();
                    break;

            }
        }

        private void ContactListView_Load(object sender, EventArgs e)
        {
            RefreshSortMethod();
            PopulateFromCurrentAddressBook();
        }

        private void RefreshSortMethod()
        {
            if (ConfigurationService == null)
                return;

            switch (ConfigurationService.LisimbaConfigSection.SortBy.Value)
            {
                case "Birthday":
                    SortField = ContactsSortingType.Birthday;
                    break;

                case "BirthDate":
                    SortField = ContactsSortingType.BirthDate;
                    break;

                case "FirstName":
                    SortField = ContactsSortingType.FirstName;
                    break;

                case "LastName":
                    SortField = ContactsSortingType.LastName;
                    break;

                case "Nickname":
                    SortField = ContactsSortingType.Nickname;
                    break;

                case "NicknameOrName":
                    SortField = ContactsSortingType.NicknameOrName;
                    break;
            }
        }
    }
}
