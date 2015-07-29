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

using System.Linq;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class CustomTreeView : TreeView
    {
        private TreeNode TreeNodePhones
        {
            get { return GetOrCreateCategoryNode("phones", "Phones", "phone"); }
        }

        private TreeNode TreeNodeEmails
        {
            get { return GetOrCreateCategoryNode("emails", "Emails", "e-mail"); }
        }

        private TreeNode TreeNodeWebSites
        {
            get { return GetOrCreateCategoryNode("websites", "Web Sites", "website"); }
        }

        private TreeNode TreeNodeAddresses
        {
            get { return GetOrCreateCategoryNode("addresses", "Addresses", "address"); }
        }

        private TreeNode TreeNodeDates
        {
            get { return GetOrCreateCategoryNode("dates", "Dates", "date"); }
        }

        private TreeNode TreeNodeMessengerIds
        {
            get { return GetOrCreateCategoryNode("mesengerids", "Mesenger Ids", "mesengerid"); }
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
                    Nodes.Add(node);
            }

            return node;
        }

        private PhoneCollection phones;
        private EmailCollection emails;
        private WebSiteCollection webSites;
        private AddressCollection addresses;
        private DateCollection dates;
        private MessengerIdCollection messengerIds;

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

        public AddressCollection Addresses
        {
            get { return addresses; }
            set
            {
                addresses = value;
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

        public MessengerIdCollection MessengerIds
        {
            get { return messengerIds; }
            set
            {
                messengerIds = value;
                DisplayMessengerIds();
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

            Address addressTag = selectedNode.Tag as Address;

            if (addressTag != null)
            {
                AddressEditForm form = new AddressEditForm
                {
                    Address = addressTag,
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

            MessengerId messengerIdTag = selectedNode.Tag as MessengerId;

            if (messengerIdTag != null)
            {
                MessengerIdEditForm form = new MessengerIdEditForm
                {
                    MessengerId = messengerIdTag,
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

            if (addresses == null)
                return;

            foreach (Address address in addresses)
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

        private void DisplayMessengerIds()
        {
            TreeNodeMessengerIds.Nodes.Clear();

            if (messengerIds == null)
                return;

            foreach (MessengerId messengerId in messengerIds)
            {
                TreeNode messengerIdNode = new TreeNode(messengerId.ToString(), -2, -2)
                {
                    Tag = messengerId
                };

                TreeNodeMessengerIds.Nodes.Add(messengerIdNode);
                messengerIdNode.ImageIndex = -2;
                messengerIdNode.SelectedImageIndex = -2;
            }

            TreeNodeMessengerIds.Expand();
        }
    }
}
