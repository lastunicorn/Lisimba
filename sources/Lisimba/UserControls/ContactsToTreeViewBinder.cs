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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.UserControls
{
    class ContactsToTreeViewBinder
    {
        private readonly TreeView treeView1;

        private ContactCollection contacts;
        public readonly Dictionary<Contact, bool> modifiedContacts = new Dictionary<Contact, bool>();
        public readonly Dictionary<Contact, TreeNode> treeNodesByContact = new Dictionary<Contact, TreeNode>();

        public ContactsToTreeViewBinder(TreeView treeView1)
        {
            if (treeView1 == null) throw new ArgumentNullException("treeView1");

            this.treeView1 = treeView1;
        }

        public ContactCollection Contacts
        {
            set
            {
                if (contacts != null)
                {
                    contacts.CollectionChanged -= HandleContactsCollectionChanged;
                    contacts.ItemChanged -= HandleContactChanged;
                }

                if (contacts == value)
                    return;

                contacts = value;
                modifiedContacts.Clear();
                treeNodesByContact.Clear();

                if (contacts != null)
                {
                    foreach (Contact contact in contacts)
                    {
                        modifiedContacts.Add(contact, false);

                        if (treeNodesByContact.ContainsKey(contact))
                            continue;

                        TreeNode treeNode = new TreeNode(contact.ToString()) { Tag = contact };
                        treeNodesByContact.Add(contact, treeNode);
                    }

                    contacts.CollectionChanged += HandleContactsCollectionChanged;
                    contacts.ItemChanged += HandleContactChanged;
                }

                RefreshDisplayedNodes();
            }
        }

        public Func<Contact, bool> Filter { get; set; }

        private void HandleContactChanged(object sender, ItemChangedEventArgs<Contact> e)
        {
            //if (ignoreCurrentContactChange)
            //    return;

            //Contact contactToSelect = e.Item;

            //TreeNode treeNodeToSelect = (contactToSelect == null || !treeNodesByContact.ContainsKey(contactToSelect))
            //    ? null
            //    : treeNodesByContact[contactToSelect];

            //treeView1.SelectedNode = treeNodeToSelect;
        }

        private void HandleContactsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        public void RefreshDisplayedNodes()
        {
            Dictionary<TreeNode, TreeNode> treeNodesToRemove = treeView1.Nodes
                .Cast<TreeNode>()
                .ToDictionary(node => node);


            treeView1.SuspendLayout();
            try
            {
                foreach (TreeNode treeNode in treeNodesByContact.Values)
                {
                    bool isContactVisible = Filter == null || Filter(treeNode.Tag as Contact);

                    if (!isContactVisible)
                        continue;

                    if (treeNodesToRemove.ContainsKey(treeNode))
                        treeNodesToRemove.Remove(treeNode);
                    else
                        treeView1.Nodes.Add(treeNode);
                }

                foreach (TreeNode treeNode in treeNodesToRemove.Values)
                {
                    treeView1.Nodes.Remove(treeNode);
                }
            }
            finally
            {
                treeView1.ResumeLayout();
            }
        }
    }
}