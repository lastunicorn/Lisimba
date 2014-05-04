using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using System.Collections;

namespace DustInTheWind.Lisimba
{
    public partial class ContactListView : UserControl
    {
        private ContactCollection contacts;
        private Dictionary<Contact, bool> modifiedContacts = new Dictionary<Contact, bool>();
        private Dictionary<Contact, TreeNode> treeNodes = new Dictionary<Contact, TreeNode>();

        private bool allowSort = false;

        [
        Browsable(true),
        Category("Behavior"),
        Description("Indicates if the Contacts will be shown sorted or not.")
        ]
        public bool AllowSort
        {
            get { return allowSort; }
            set
            {
                allowSort = value;
                this.treeView1.Sort();
            }
        }

        private ContactsSortingType sortField = ContactsSortingType.NicknameOrName;

        [
        Browsable(true),
        Category("Behavior"),
        Description("Specifies the field of the Contact used to sort them. Available only if AllowSort is true.")
        ]
        public ContactsSortingType SortField
        {
            get { return sortField; }
            set
            {
                if (!this.allowSort) return;

                sortField = value;
                switch (value)
                {
                    case ContactsSortingType.Birthday:
                        this.comboBoxSortBy.SelectedIndex = 0;
                        break;

                    case ContactsSortingType.BirthDate:
                        this.comboBoxSortBy.SelectedIndex = 1;
                        break;

                    case ContactsSortingType.FirstName:
                        this.comboBoxSortBy.SelectedIndex = 2;
                        break;

                    case ContactsSortingType.LastName:
                        this.comboBoxSortBy.SelectedIndex = 3;
                        break;

                    case ContactsSortingType.Nickname:
                        this.comboBoxSortBy.SelectedIndex = 4;
                        break;

                    case ContactsSortingType.NicknameOrName:
                        this.comboBoxSortBy.SelectedIndex = 5;
                        break;

                    default:
                        break;
                }
            }
        }


        [Browsable(false)]
        public ContactCollection Contacts
        {
            get { return this.contacts; }
            set
            {
                this.contacts = value;

                this.modifiedContacts.Clear();
                this.treeNodes.Clear();

                //this.textBoxSearch.Text = string.Empty;

                if (this.contacts == null)
                    return;

                Contact c = null;
                TreeNode treeNode = null;

                for (int i = 0; i < this.contacts.Count; i++)
                {
                    c = this.contacts[i];
                    this.modifiedContacts.Add(c, false);
                    treeNode = new TreeNode(c.ToString());
                    treeNode.Tag = c;
                    this.treeNodes.Add(c, treeNode);
                }

                this.PopulateTreeView();
            }
        }

        [Browsable(false)]
        public Contact SelectedContact
        {
            get
            {
                TreeNode selectedNode = this.treeView1.SelectedNode;
                if (selectedNode != null)
                    return (Contact)selectedNode.Tag;
                else
                    return null;
            }
        }

        [
        Browsable(true),
        Category("Data"),
        Description("Specifies the initial text of the search filter.")
        ]
        public string SearchText
        {
            get { return this.textBoxSearch.Text; }
            set { this.textBoxSearch.Text = value; }
        }

        public ContactListView()
        {
            InitializeComponent();
            this.treeView1.TreeViewNodeSorter = new TreeNodeByNicknameComparer();
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

        #endregion Event SelectedContactChanged

        #region Event ContactListChanged

        public event ContactListChangedHandler ContactListChanged;
        public delegate void ContactListChangedHandler(object sender, ContactListChangedEventArgs e);

        public class ContactListChangedEventArgs : EventArgs
        {
            public ContactListChangedEventArgs()
            {
            }
        }

        protected virtual void OnContactListChanged(ContactListChangedEventArgs e)
        {
            if (ContactListChanged != null)
            {
                ContactListChanged(this, e);
            }
        }

        #endregion Event ContactListChanged

        public void Clear()
        {
            this.contacts = new ContactCollection();

            this.modifiedContacts.Clear();
            this.treeNodes.Clear();
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
                this.modifiedContacts.Add(c, false);
                treeNode = new TreeNode(c.ToString());
                treeNode.Tag = c;
                this.treeNodes.Add(c, treeNode);
            }

            this.PopulateTreeView();
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

            IComparer comparer = this.treeView1.TreeViewNodeSorter;

            //if (this.treeView1.SelectedNode != null)
            selectedNode = this.treeView1.SelectedNode;

            this.treeView1.Nodes.Clear();

            for (int i = 0; i < this.contacts.Count; i++)
            {
                contact = this.contacts[i];

                if (this.textBoxSearch.Text.Length == 0 ||
                    contact.Name.FirstName.IndexOf(this.textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.MiddleName.IndexOf(this.textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.LastName.IndexOf(this.textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    contact.Name.Nickname.IndexOf(this.textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    treeNode = this.treeNodes[contact];

                    if (this.allowSort)
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

            this.treeView1.Nodes.AddRange(nodeList.ToArray());

            //this.treeView1.SelectedNode = selectedNode;
            //if (this.treeView1.SelectedNode != selectedNode)
            //{
            //    TreeNode node = this.treeView1.SelectedNode;
            //    this.OnSelectedContactChanged(new SelectedContactChangedEventArgs(node != null ? (Contact)node.Tag : null));
            //}

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            this.PopulateTreeView();
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = this.treeView1.GetNodeAt(e.Location);
                Contact contact = null;

                if (node != null)
                {
                    // Select the item
                    this.treeView1.SelectedNode = node;
                    //this.OnSelectedContactChanged(new SelectedContactChangedEventArgs((Contact)node.Tag));
                    contact = (Contact)node.Tag;
                }

                // Display the menu
                this.toolStripMenuItem_List_Delete.Enabled = (node != null);
                this.toolStripMenuItem_List_ViewBiorythm.Enabled = (contact != null && contact.Birthday.IsCompleteDate);
                this.contextMenuStripListBox.Show(this.treeView1, e.Location);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            this.OnSelectedContactChanged(new SelectedContactChangedEventArgs(selectedNode != null ? (Contact)selectedNode.Tag : null));
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

            TreeNode node = this.treeNodes[c];
            if (node == null) return;

            if (value != this.modifiedContacts[c])
            {
                this.modifiedContacts[c] = value;
                HighlightTreeNode(node, value);
            }

            node.Text = node.Tag.ToString();
        }

        public void Add(Contact c)
        {
            this.contacts.Add(c);
            TreeNode newTreeNode = new TreeNode(c.ToString());
            newTreeNode.Tag = c;
            bool inserted = false;

            if (this.allowSort)
            {
                IComparer comparer = this.treeView1.TreeViewNodeSorter;

                for (int i = 0; i < this.treeView1.Nodes.Count; i++)
                {
                    if (comparer.Compare(newTreeNode, this.treeView1.Nodes[i]) < 0)
                    {
                        this.treeView1.Nodes.Insert(i, newTreeNode);
                        inserted = true;
                        break;
                    }
                }

                if (!inserted)
                    this.treeView1.Nodes.Add(newTreeNode);
            }
            else
            {
                this.treeView1.Nodes.Add(newTreeNode);
            }

            this.treeNodes.Add(c, newTreeNode);
            this.modifiedContacts.Add(c, true);
            HighlightTreeNode(newTreeNode, true);

            this.treeView1.SelectedNode = newTreeNode;

            this.OnContactListChanged(new ContactListChangedEventArgs());
        }

        private void toolStripMenuItem_List_Add_Click(object sender, EventArgs e)
        {
            FormAddContact formAddContact = new FormAddContact(this.contacts);

            if (formAddContact.ShowDialog() == DialogResult.OK)
            {
                this.Add(formAddContact.Contact);
            }
        }

        public void RemoveContact(Contact contact)
        {
            TreeNode node = null;
            TreeNode nodeToSelect = null;

            if (contact != null && this.contacts.Contains(contact))
            {
                node = this.treeNodes[contact];
                nodeToSelect = node.NextNode;

                this.contacts.Remove(contact);
                this.modifiedContacts.Remove(contact);
                this.treeNodes.Remove(contact);

                this.treeView1.Nodes.Remove(node);
                if (this.treeView1.Nodes.Count > 0)
                {
                    if (nodeToSelect != null)
                        this.treeView1.SelectedNode = nodeToSelect;
                    else
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0].LastNode;
                }

                this.OnContactListChanged(new ContactListChangedEventArgs());
            }
        }

        private void toolStripMenuItem_List_Delete_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            Contact contact = null;

            if (node != null)
            {
                contact = (Contact)node.Tag;
                if (MessageBox.Show("Are you sure you wanna delete the contact " + contact.ToString() + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.RemoveContact(contact);
                }
            }
        }

        private void toolStripMenuItem_List_ViewBiorythm_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
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
            if (!this.allowSort) return;

            switch (this.comboBoxSortBy.SelectedIndex)
            {
                case 0:
                    this.SortField = ContactsSortingType.Birthday;
                    treeView1.TreeViewNodeSorter = new TreeNodeByBirthdayComparer();
                    this.treeView1.Sort();
                    break;

                case 1:
                    this.SortField = ContactsSortingType.BirthDate;
                    treeView1.TreeViewNodeSorter = new TreeNodeByBirthDateComparer();
                    this.treeView1.Sort();
                    break;

                case 2:
                    this.SortField = ContactsSortingType.FirstName;
                    treeView1.TreeViewNodeSorter = new TreeNodeByFirstNameComparer();
                    this.treeView1.Sort();
                    break;

                case 3:
                    this.SortField = ContactsSortingType.LastName;
                    treeView1.TreeViewNodeSorter = new TreeNodeByLastNameComparer();
                    this.treeView1.Sort();
                    break;

                case 4:
                    this.SortField = ContactsSortingType.Nickname;
                    treeView1.TreeViewNodeSorter = new TreeNodeByNicknameComparer();
                    this.treeView1.Sort();
                    break;

                case 5:
                    this.SortField = ContactsSortingType.NicknameOrName;
                    treeView1.TreeViewNodeSorter = new TreeNodeByNicknameOrNameComparer();
                    this.treeView1.Sort();
                    break;

            }
        }
    }
}
