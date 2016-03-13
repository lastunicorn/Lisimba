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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.WinForms.ContactEditing;
using DustInTheWind.Lisimba.WinForms.ContactEditing.ContactItemEditForms;

namespace DustInTheWind.Lisimba.WinForms.ContactEdit
{
    partial class CustomTreeView : TreeView
    {
        private readonly List<TreeNode> categoryNodes = new List<TreeNode>();

        private TreeNode TreeNodePhones
        {
            get { return GetOrCreateCategoryNode("phones", "Phones", "phone"); }
        }

        private TreeNode TreeNodeEmails
        {
            get { return GetOrCreateCategoryNode("e-mails", "Emails", "e-mail"); }
        }

        private TreeNode TreeNodeWebSites
        {
            get { return GetOrCreateCategoryNode("web-sites", "Web Sites", "webSite"); }
        }

        private TreeNode TreeNodeAddresses
        {
            get { return GetOrCreateCategoryNode("postal-addresses", "Postal Addresses", "postalAddress"); }
        }

        private TreeNode TreeNodeDates
        {
            get { return GetOrCreateCategoryNode("dates", "Dates", "date"); }
        }

        private TreeNode TreeNodeSocialProfileIds
        {
            get { return GetOrCreateCategoryNode("social-profile-ids", "Social Profile Ids", "socialProfileid"); }
        }

        private TreeNode GetOrCreateCategoryNode(string categoryId, string label, string imageKey)
        {
            TreeNode node = Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(x => ReferenceEquals(x.Tag, categoryId));

            if (node == null)
            {
                node = new TreeNode(label)
                {
                    ImageKey = imageKey,
                    SelectedImageKey = imageKey,
                    Tag = categoryId
                };

                if (!DesignMode)
                {
                    categoryNodes.Add(node);
                    Nodes.Add(node);
                }
            }

            return node;
        }

        private CustomObservableCollection<ContactItem> contactItems;

        public CustomObservableCollection<ContactItem> ContactItems
        {
            get { return contactItems; }
            set
            {
                contactItems = value;
                DisplayContactItems();
            }
        }

        public ActionQueue ActionQueue { get; set; }

        public CustomTreeView()
        {
            InitializeComponent();
        }

        private void HandleMouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode selectedNode = SelectedNode;

            if (selectedNode == null)
                return;

            if (selectedNode != GetNodeAt(e.Location))
                return;

            if (selectedNode.Tag is string)
            {
                // todo: display the corresponding edit form to add a new item.
            }

            Phone phoneTag = selectedNode.Tag as Phone;

            if (phoneTag != null)
            {
                PhoneEditForm form = new PhoneEditForm
                {
                    ActionQueue = ActionQueue,
                    Phone = phoneTag,
                    Location = PointToScreen(e.Location),
                    EditMode = EditMode.Edit
                };

                form.Show();
                form.Focus();

                return;
            }

            Email emailTag = selectedNode.Tag as Email;

            if (emailTag != null)
            {
                EmailEditForm form = new EmailEditForm
                {
                    ActionQueue = ActionQueue,
                    Email = emailTag,
                    Location = PointToScreen(e.Location),
                    EditMode = EditMode.Edit
                };

                form.Show();
                form.Focus();

                return;
            }

            WebSite webSiteTag = selectedNode.Tag as WebSite;

            if (webSiteTag != null)
            {
                WebSiteEditForm form = new WebSiteEditForm
                {
                    ActionQueue = ActionQueue,
                    WebSite = webSiteTag,
                    Location = PointToScreen(e.Location),
                    EditMode = EditMode.Edit
                };

                form.Show();
                form.Focus();

                return;
            }

            PostalAddress postalAddressTag = selectedNode.Tag as PostalAddress;

            if (postalAddressTag != null)
            {
                PostalAddressEditForm form = new PostalAddressEditForm
                {
                    ActionQueue = ActionQueue,
                    PostalAddress = postalAddressTag,
                    Location = PointToScreen(e.Location),
                    EditMode = EditMode.Edit
                };

                form.Show();
                form.Focus();

                return;
            }

            Date dateTag = selectedNode.Tag as Date;

            if (dateTag != null)
            {
                DateEditForm form = new DateEditForm
                {
                    ActionQueue = ActionQueue,
                    Date = dateTag,
                    Location = PointToScreen(e.Location),
                    EditMode = EditMode.Edit
                };

                form.Show();
                form.Focus();

                return;
            }

            SocialProfile socialProfileTag = selectedNode.Tag as SocialProfile;

            if (socialProfileTag != null)
            {
                SocialProfileEditForm form = new SocialProfileEditForm
                {
                    ActionQueue = ActionQueue,
                    SocialProfile = socialProfileTag,
                    Location = PointToScreen(e.Location),
                    EditMode = EditMode.Edit
                };

                form.Show();
                form.Focus();

                return;
            }
        }

        private void DisplayContactItems()
        {
            TreeNodePhones.Nodes.Clear();
            TreeNodeEmails.Nodes.Clear();
            TreeNodeWebSites.Nodes.Clear();
            TreeNodeAddresses.Nodes.Clear();
            TreeNodeDates.Nodes.Clear();
            TreeNodeSocialProfileIds.Nodes.Clear();

            if (contactItems == null)
                return;

            foreach (ContactItem contactItem in contactItems)
            {
                TreeNode treeNode = new TreeNode(contactItem.ToString(), -2, -2) { Tag = contactItem };

                if (contactItem is Phone)
                    TreeNodePhones.Nodes.Add(treeNode);
                else if (contactItem is Email)
                    TreeNodeEmails.Nodes.Add(treeNode);
                else if (contactItem is WebSite)
                    TreeNodeWebSites.Nodes.Add(treeNode);
                else if (contactItem is PostalAddress)
                    TreeNodeAddresses.Nodes.Add(treeNode);
                else if (contactItem is Date)
                    TreeNodeDates.Nodes.Add(treeNode);
                else if (contactItem is SocialProfile)
                    TreeNodeSocialProfileIds.Nodes.Add(treeNode);

                treeNode.ImageIndex = -2;
                treeNode.SelectedImageIndex = -2;
            }

            TreeNodePhones.Expand();
            TreeNodeEmails.Expand();
            TreeNodeWebSites.Expand();
            TreeNodeAddresses.Expand();
            TreeNodeDates.Expand();
            TreeNodeSocialProfileIds.Expand();
        }

        private void CustomTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (categoryNodes.Contains(e.Node))
                e.Cancel = true;
        }

        private void CustomTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && SelectedNode != null && !categoryNodes.Contains(SelectedNode))
            {
                ContactItem contactItem = SelectedNode.Tag as ContactItem;

                if (contactItem != null)
                    contactItems.Remove(contactItem);
            }
        }
    }
}