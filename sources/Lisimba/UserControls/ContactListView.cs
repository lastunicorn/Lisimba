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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Comparers;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Forms;

namespace DustInTheWind.Lisimba.UserControls
{
    public partial class ContactListView : UserControl
    {
        private ContactCollection contacts;
        private Dictionary<Contact, bool> modifiedContacts = new Dictionary<Contact, bool>();
        private Dictionary<Contact, TreeNode> treeNodes = new Dictionary<Contact, TreeNode>();

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

                    default:
                        break;
                }
            }
        }


        [Browsable(false)]
        public ContactCollection Contacts
        {
            get { return contacts; }
            set
            {
                contacts = value;

                modifiedContacts.Clear();
                treeNodes.Clear();

                //this.textBoxSearch.Text = string.Empty;

                if (contacts == null)
                    return;

                Contact c = null;
                TreeNode treeNode = null;

                for (int i = 0; i < contacts.Count; i++)
                {
                    c = contacts[i];
                    modifiedContacts.Add(c, false);
                    treeNode = new TreeNode(c.ToString());
                    treeNode.Tag = c;
                    treeNodes.Add(c, treeNode);
                }

                PopulateTreeView();
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

        public ContactListView()
        {
            InitializeComponent();
            treeView1.TreeViewNodeSorter = new TreeNodeByNicknameComparer();
        }

        //public bool IsModified(Contact c)
        //{
        //    return this.modifiedContacts[c];
        //}

        //public TreeNode GetTreeNode(Contact c)
        //{
        //    return this.treeNodes[c];
        //}

        #region Event SelectedContactChanged

        public event SelectedContactChangedHandler SelectedContactChanged;
        public delegate void SelectedContactChangedHandler(object sender, SelectedContactChangedEventArgs e);

        public class SelectedContactChangedEventArgs : EventArgs
        {
            private Contact selectedContact;

            public Contact SelectedContact
            {
                get { return selectedContact; }
            }

            public SelectedContactChangedEventArgs(Contact selectedContact)
            {
                this.selectedContact = selectedContact;
            }
        }

        protected virtual void OnSelectedContactChanged(SelectedContactChangedEventArgs e)
        {
            if (SelectedContactChanged != null)
            {
                SelectedContactChanged(this, e);
            }
        }

        #endregion

        #region Event ContactListChanged

        public event EventHandler ContactListChanged;

        protected virtual void OnContactListChanged(EventArgs e)
        {
            if (ContactListChanged != null)
                ContactListChanged(this, e);
        }

        #endregion

        public void Clear()
        {
            contacts = new ContactCollection();

            modifiedContacts.Clear();
            treeNodes.Clear();
        }

        public void AddRange(ContactCollection contacts)
        {
            if (this.contacts == null)
                return;

            Contact c = null;
            TreeNode treeNode = null;

            for (int i = 0; i < this.contacts.Count; i++)
            {
                c = this.contacts[i];
                modifiedContacts.Add(c, false);
                treeNode = new TreeNode(c.ToString());
                treeNode.Tag = c;
                treeNodes.Add(c, treeNode);
            }

            PopulateTreeView();
        }

        public void ResetModifiedFlags()
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                if (modifiedContacts[contacts[i]] == true)
                    SetContactChangedFlag(contacts[i], false);
            }
        }

        private void PopulateTreeView()
        {
            Contact contact = null;
            //TreeNode node = null;
            //Contact selectedContact = null;
            TreeNode selectedNode = null;
            TreeNode treeNode = null;
            List<TreeNode> nodeList = new List<TreeNode>();
            bool inserted = false;

            IComparer comparer = treeView1.TreeViewNodeSorter;

            //if (this.treeView1.SelectedNode != null)
            selectedNode = treeView1.SelectedNode;

            treeView1.Nodes.Clear();

            for (int i = 0; i < contacts.Count; i++)
            {
                contact = contacts[i];

                if (textBoxSearch.Text.Length == 0 ||
                    contact.Name.FirstName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.MiddleName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.LastName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.Nickname.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    treeNode = treeNodes[contact];

                    if (allowSort)
                    {
                        inserted = false;

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
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeView1.GetNodeAt(e.Location);
                Contact contact = null;

                if (node != null)
                {
                    // Select the item
                    treeView1.SelectedNode = node;
                    //this.OnSelectedContactChanged(new SelectedContactChangedEventArgs((Contact)node.Tag));
                    contact = (Contact)node.Tag;
                }

                // Display the menu
                toolStripMenuItem_List_Delete.Enabled = (node != null);
                toolStripMenuItem_List_ViewBiorythm.Enabled = (contact != null && contact.Birthday.IsCompleteDate);
                contextMenuStripListBox.Show(treeView1, e.Location);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            OnSelectedContactChanged(new SelectedContactChangedEventArgs(selectedNode != null ? (Contact)selectedNode.Tag : null));
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

        public void SetContactChangedFlag(Contact c, bool value)
        {
            if (c == null) return;

            TreeNode node = treeNodes[c];
            if (node == null) return;

            if (value != modifiedContacts[c])
            {
                modifiedContacts[c] = value;
                HighlightTreeNode(node, value);
            }

            node.Text = node.Tag.ToString();
        }

        public void Add(Contact c)
        {
            contacts.Add(c);
            TreeNode newTreeNode = new TreeNode(c.ToString());
            newTreeNode.Tag = c;
            bool inserted = false;

            if (allowSort)
            {
                IComparer comparer = treeView1.TreeViewNodeSorter;

                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    if (comparer.Compare(newTreeNode, treeView1.Nodes[i]) < 0)
                    {
                        treeView1.Nodes.Insert(i, newTreeNode);
                        inserted = true;
                        break;
                    }
                }

                if (!inserted)
                    treeView1.Nodes.Add(newTreeNode);
            }
            else
            {
                treeView1.Nodes.Add(newTreeNode);
            }

            treeNodes.Add(c, newTreeNode);
            modifiedContacts.Add(c, true);
            HighlightTreeNode(newTreeNode, true);

            treeView1.SelectedNode = newTreeNode;

            OnContactListChanged(EventArgs.Empty);
        }

        private void toolStripMenuItem_List_Add_Click(object sender, EventArgs e)
        {
            FormAddContact formAddContact = new FormAddContact(contacts);

            if (formAddContact.ShowDialog() == DialogResult.OK)
            {
                Add(formAddContact.Contact);
            }
        }

        public void RemoveContact(Contact contact)
        {
            TreeNode node;
            TreeNode nodeToSelect;

            if (contact != null && contacts.Contains(contact))
            {
                node = treeNodes[contact];
                nodeToSelect = node.NextNode;

                contacts.Remove(contact);
                modifiedContacts.Remove(contact);
                treeNodes.Remove(contact);

                treeView1.Nodes.Remove(node);
                if (treeView1.Nodes.Count > 0)
                {
                    if (nodeToSelect != null)
                        treeView1.SelectedNode = nodeToSelect;
                    else
                        treeView1.SelectedNode = treeView1.Nodes[0].LastNode;
                }

                OnContactListChanged(EventArgs.Empty);
            }
        }

        private void toolStripMenuItem_List_Delete_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            Contact contact = null;

            if (node != null)
            {
                contact = (Contact)node.Tag;
                if (MessageBox.Show("Are you sure you wanna delete the contact " + contact.ToString() + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    RemoveContact(contact);
                }
            }
        }

        private void toolStripMenuItem_List_ViewBiorythm_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            Contact contact = null;

            if (node != null)
            {
                contact = (Contact)node.Tag;

                if (contact.Birthday.IsCompleteDate)
                {
                    FormBiorythm formBiorythm = new FormBiorythm();
                    formBiorythm.Contact = contact;
                    formBiorythm.ShowDialog(this);
                }
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
    }
}
