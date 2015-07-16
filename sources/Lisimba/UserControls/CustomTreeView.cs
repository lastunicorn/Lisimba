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

using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Forms;

namespace DustInTheWind.Lisimba.UserControls
{
    partial class CustomTreeView : TreeView
    {
        private readonly TreeNode treeNodePhones;
        private readonly TreeNode treeNodeEmails;
        private readonly TreeNode treeNodeWebSites;
        private readonly TreeNode treeNodeAddresses;
        private readonly TreeNode treeNodeDates;
        private readonly TreeNode treeNodeMessengerIds;

        readonly FormPhoneEdit formPhoneEdit;
        readonly FormEmailEdit formEmailEdit;
        readonly FormWebSiteEdit formWebSiteEdit;
        readonly FormAddressEdit formAddressEdit;
        readonly FormDateEdit formDateEdit;

        public CustomTreeView()
        {
            InitializeComponent();

            formPhoneEdit = new FormPhoneEdit();
            formEmailEdit = new FormEmailEdit();
            formWebSiteEdit = new FormWebSiteEdit();
            formAddressEdit = new FormAddressEdit();
            formDateEdit = new FormDateEdit();

            treeNodePhones = Nodes["Phones"];
            treeNodeEmails = Nodes["Emails"];
            treeNodeWebSites = Nodes["Web Sites"];
            treeNodeAddresses = Nodes["Addresses"];
            treeNodeDates = Nodes["Dates"];
            treeNodeMessengerIds = Nodes["Messenger Ids"];
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
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

            if (selectedNode.Tag is Phone)
            {
                formPhoneEdit.Phone = (Phone)selectedNode.Tag;
                formPhoneEdit.Location = PointToScreen(e.Location);
                formPhoneEdit.Show();
                formPhoneEdit.Focus();
                return;
            }

            if (selectedNode.Tag is Email)
            {
                formEmailEdit.Email = (Email)selectedNode.Tag;
                formEmailEdit.Location = PointToScreen(e.Location);
                formEmailEdit.Show();
                formEmailEdit.Focus();
                return;
            }

            if (selectedNode.Tag is WebSite)
            {
                formWebSiteEdit.WebSite = (WebSite)selectedNode.Tag;
                formWebSiteEdit.Location = PointToScreen(e.Location);
                formWebSiteEdit.Show();
                formWebSiteEdit.Focus();
                return;
            }

            if (selectedNode.Tag is Address)
            {
                formAddressEdit.Address = (Address)selectedNode.Tag;
                formAddressEdit.Location = PointToScreen(e.Location);
                formAddressEdit.Show();
                formAddressEdit.Focus();
                return;
            }

            if (selectedNode.Tag is Date)
            {
                formDateEdit.Date = (Date)selectedNode.Tag;
                formDateEdit.Location = PointToScreen(e.Location);
                formDateEdit.Show();
                formDateEdit.Focus();
                return;
            }

            if (selectedNode.Tag is MessengerId)
                return;
        }

        private PhoneCollection phones;

        public PhoneCollection Phones
        {
            get { return phones; }
            set
            {
                treeNodePhones.Nodes.Clear();

                if (value == null)
                    return;

                phones = value;

                foreach (Phone phone in value)
                {
                    TreeNode phoneNode = new TreeNode(phone.ToString(), -2, -2);
                    phoneNode.Tag = phone;
                    treeNodePhones.Nodes.Add(phoneNode);
                    phoneNode.ImageIndex = -2;
                    phoneNode.SelectedImageIndex = -2;
                }

                treeNodePhones.Expand();
            }
        }

        public EmailCollection Emails
        {
            set
            {
                treeNodeEmails.Nodes.Clear();

                if (value == null)
                    return;

                foreach (Email email in value)
                {
                    TreeNode emailNode = new TreeNode(email.ToString(), -2, -2);
                    emailNode.Tag = email;
                    treeNodeEmails.Nodes.Add(emailNode);
                    emailNode.ImageIndex = -2;
                    emailNode.SelectedImageIndex = -2;
                }

                treeNodeEmails.Expand();
            }
        }

        public WebSiteCollection WebSites
        {
            set
            {
                treeNodeWebSites.Nodes.Clear();

                if (value == null)
                    return;

                foreach (WebSite webSite in value)
                {
                    TreeNode webSiteNode = new TreeNode(webSite.ToString(), -2, -2);
                    webSiteNode.Tag = webSite;
                    treeNodeWebSites.Nodes.Add(webSiteNode);
                    webSiteNode.ImageIndex = -2;
                    webSiteNode.SelectedImageIndex = -2;
                }

                treeNodeWebSites.Expand();
            }
        }

        public AddressCollection Addresses
        {
            set
            {
                treeNodeAddresses.Nodes.Clear();

                if (value == null)
                    return;

                foreach (Address address in value)
                {
                    TreeNode addressNode = new TreeNode(address.ToString(), -2, -2);
                    addressNode.Tag = address;
                    treeNodeAddresses.Nodes.Add(addressNode);
                    addressNode.ImageIndex = -2;
                    addressNode.SelectedImageIndex = -2;
                }

                treeNodeAddresses.Expand();
            }
        }

        public DateCollection Dates
        {
            set
            {
                treeNodeDates.Nodes.Clear();

                if (value == null)
                    return;

                foreach (Date date in value)
                {
                    TreeNode dateNode = new TreeNode(date.ToString(), -2, -2);
                    dateNode.Tag = date;
                    treeNodeDates.Nodes.Add(dateNode);
                    dateNode.ImageIndex = -2;
                    dateNode.SelectedImageIndex = -2;
                }

                treeNodeDates.Expand();
            }
        }

        public MessengerIdCollection MessengerIds
        {
            set
            {
                treeNodeMessengerIds.Nodes.Clear();

                if (value == null)
                    return;

                foreach (MessengerId messengerId in value)
                {
                    TreeNode messengerIdNode = new TreeNode(messengerId.ToString(), -2, -2);
                    messengerIdNode.Tag = messengerId;
                    treeNodeMessengerIds.Nodes.Add(messengerIdNode);
                    messengerIdNode.ImageIndex = -2;
                    messengerIdNode.SelectedImageIndex = -2;
                }

                treeNodeMessengerIds.Expand();
            }
        }
    }
}
