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
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.ContactEdit
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

        private PhoneCollection phones;
        private EmailCollection emails;
        private WebSiteCollection webSites;
        private PostalAddressCollection postalAddresses;
        private DateCollection dates;
        private SocialProfileIdCollection socialProfileIds;

        public PhoneCollection Phones
        {
            get { return phones; }
            set
            {
                phones = value;
                DisplayPhones();
            }
        }

        public EmailCollection Emails
        {
            get { return emails; }
            set
            {
                emails = value;
                DisplayEmails();
            }
        }

        public WebSiteCollection WebSites
        {
            get { return webSites; }
            set
            {
                webSites = value;
                DisplayWebSites();
            }
        }

        public PostalAddressCollection PostalAddresses
        {
            get { return postalAddresses; }
            set
            {
                postalAddresses = value;
                DisplayAddresses();
            }
        }

        public DateCollection Dates
        {
            get { return dates; }
            set
            {
                dates = value;
                DisplayDates();
            }
        }

        public SocialProfileIdCollection SocialProfileIds
        {
            get { return socialProfileIds; }
            set
            {
                socialProfileIds = value;
                DisplaySocialProfileIds();
            }
        }

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

            //if (selectedNode == treeNodePhones)
            //{
            //    Phone phone = new Phone("<number>", "<description>");
            //    TreeNode phoneNode = new TreeNode(phone.ToString(), -2, -2);
            //    phoneNode.Tag = phone;
            //    treeNodePhones.Nodes.Add(phoneNode);
            //    phoneNode.ImageIndex = -2;
            //    phoneNode.SelectedImageIndex = -2;
            //    selectedNode = phoneNode;
            //}

            if (selectedNode.Tag is string)
            {
                // todo: display the corresponding edit form to add a new item.
            }

            Phone phoneTag = selectedNode.Tag as Phone;

            if (phoneTag != null)
            {
                PhoneEditForm form = new PhoneEditForm
                {
                    Phone = phoneTag,
                    Location = PointToScreen(e.Location),
                    AddMode = false
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
                    Email = emailTag,
                    Location = PointToScreen(e.Location),
                    AddMode = false
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
                    WebSite = webSiteTag,
                    Location = PointToScreen(e.Location),
                    AddMode = false
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
                    PostalAddress = postalAddressTag,
                    Location = PointToScreen(e.Location),
                    AddMode = false
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
                    Date = dateTag,
                    Location = PointToScreen(e.Location),
                    AddMode = false
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
                    SocialProfile = socialProfileTag,
                    Location = PointToScreen(e.Location),
                    AddMode = false
                };

                form.Show();
                form.Focus();

                return;
            }
        }

        private void DisplayPhones()
        {
            TreeNodePhones.Nodes.Clear();

            if (phones == null)
                return;

            foreach (Phone phone in phones)
            {
                TreeNode phoneNode = new TreeNode(phone.ToString(), -2, -2)
                {
                    Tag = phone
                };

                TreeNodePhones.Nodes.Add(phoneNode);
                phoneNode.ImageIndex = -2;
                phoneNode.SelectedImageIndex = -2;
            }

            TreeNodePhones.Expand();
        }

        private void DisplayEmails()
        {
            TreeNodeEmails.Nodes.Clear();

            if (emails == null)
                return;

            foreach (Email email in emails)
            {
                TreeNode emailNode = new TreeNode(email.ToString(), -2, -2)
                {
                    Tag = email
                };

                TreeNodeEmails.Nodes.Add(emailNode);
                emailNode.ImageIndex = -2;
                emailNode.SelectedImageIndex = -2;
            }

            TreeNodeEmails.Expand();
        }

        private void DisplayWebSites()
        {
            TreeNodeWebSites.Nodes.Clear();

            if (webSites == null)
                return;

            foreach (WebSite webSite in webSites)
            {
                TreeNode webSiteNode = new TreeNode(webSite.ToString(), -2, -2)
                {
                    Tag = webSite
                };

                TreeNodeWebSites.Nodes.Add(webSiteNode);
                webSiteNode.ImageIndex = -2;
                webSiteNode.SelectedImageIndex = -2;
            }

            TreeNodeWebSites.Expand();
        }

        private void DisplayAddresses()
        {
            TreeNodeAddresses.Nodes.Clear();

            if (postalAddresses == null)
                return;

            foreach (PostalAddress address in postalAddresses)
            {
                TreeNode addressNode = new TreeNode(address.ToString(), -2, -2)
                {
                    Tag = address
                };

                TreeNodeAddresses.Nodes.Add(addressNode);
                addressNode.ImageIndex = -2;
                addressNode.SelectedImageIndex = -2;
            }

            TreeNodeAddresses.Expand();
        }

        private void DisplayDates()
        {
            TreeNodeDates.Nodes.Clear();

            if (dates == null)
                return;

            foreach (Date date in dates)
            {
                TreeNode dateNode = new TreeNode(date.ToString(), -2, -2)
                {
                    Tag = date
                };

                TreeNodeDates.Nodes.Add(dateNode);
                dateNode.ImageIndex = -2;
                dateNode.SelectedImageIndex = -2;
            }

            TreeNodeDates.Expand();
        }

        private void DisplaySocialProfileIds()
        {
            TreeNodeSocialProfileIds.Nodes.Clear();

            if (socialProfileIds == null)
                return;

            foreach (SocialProfile socialProfileId in socialProfileIds)
            {
                TreeNode socialProfileIdNode = new TreeNode(socialProfileId.ToString(), -2, -2)
                {
                    Tag = socialProfileId
                };

                TreeNodeSocialProfileIds.Nodes.Add(socialProfileIdNode);
                socialProfileIdNode.ImageIndex = -2;
                socialProfileIdNode.SelectedImageIndex = -2;
            }

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
                Phone phone = SelectedNode.Tag as Phone;

                if (phone != null)
                    Phones.Remove(phone);

                Email email = SelectedNode.Tag as Email;

                if (email != null)
                    Emails.Remove(email);

                WebSite webSite = SelectedNode.Tag as WebSite;

                if (webSite != null)
                    WebSites.Remove(webSite);
                
                PostalAddress postalAddress = SelectedNode.Tag as PostalAddress;

                if (postalAddress != null)
                    PostalAddresses.Remove(postalAddress);

                Date date = SelectedNode.Tag as Date;

                if (date != null)
                    Dates.Remove(date);

                SocialProfile socialProfile = SelectedNode.Tag as SocialProfile;

                if (socialProfile != null)
                    SocialProfileIds.Remove(socialProfile);
            }
        }
    }
}