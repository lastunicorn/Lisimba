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

namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class CustomTreeView : TreeView
    {
        private readonly TreeNode treeNodePhones;
        private readonly TreeNode treeNodeEmails;
        private readonly TreeNode treeNodeWebSites;
        private readonly TreeNode treeNodeAddresses;
        private readonly TreeNode treeNodeDates;
        private readonly TreeNode treeNodeMessengerIds;

        private readonly FormPhoneEdit formPhoneEdit;
        private readonly FormEmailEdit formEmailEdit;
        private readonly FormWebSiteEdit formWebSiteEdit;
        private readonly FormAddressEdit formAddressEdit;
        private readonly FormDateEdit formDateEdit;

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
                formPhoneEdit.Phone = phoneTag;
                formPhoneEdit.Location = PointToScreen(e.Location);
                formPhoneEdit.Show();
                formPhoneEdit.Focus();
                return;
            }

            Email emailTag = selectedNode.Tag as Email;

            if (emailTag != null)
            {
                formEmailEdit.Email = emailTag;
                formEmailEdit.Location = PointToScreen(e.Location);
                formEmailEdit.Show();
                formEmailEdit.Focus();
                return;
            }

            WebSite webSiteTag = selectedNode.Tag as WebSite;

            if (webSiteTag != null)
            {
                formWebSiteEdit.WebSite = webSiteTag;
                formWebSiteEdit.Location = PointToScreen(e.Location);
                formWebSiteEdit.Show();
                formWebSiteEdit.Focus();
                return;
            }

            Address addressTag = selectedNode.Tag as Address;

            if (addressTag != null)
            {
                formAddressEdit.Address = addressTag;
                formAddressEdit.Location = PointToScreen(e.Location);
                formAddressEdit.Show();
                formAddressEdit.Focus();
                return;
            }

            Date dateTag = selectedNode.Tag as Date;

            if (dateTag != null)
            {
                formDateEdit.Date = dateTag;
                formDateEdit.Location = PointToScreen(e.Location);
                formDateEdit.Show();
                formDateEdit.Focus();
                return;
            }

            if (selectedNode.Tag is MessengerId)
                return;
        }

        private void DisplayPhones()
        {
            treeNodePhones.Nodes.Clear();

            if (phones == null)
                return;

            foreach (Phone phone in phones)
            {
                TreeNode phoneNode = new TreeNode(phone.ToString(), -2, -2);
                phoneNode.Tag = phone;
                treeNodePhones.Nodes.Add(phoneNode);
                phoneNode.ImageIndex = -2;
                phoneNode.SelectedImageIndex = -2;
            }

            treeNodePhones.Expand();
        }

        private void DisplayEmails()
        {
            treeNodeEmails.Nodes.Clear();

            if (emails == null)
                return;

            foreach (Email email in emails)
            {
                TreeNode emailNode = new TreeNode(email.ToString(), -2, -2);
                emailNode.Tag = email;
                treeNodeEmails.Nodes.Add(emailNode);
                emailNode.ImageIndex = -2;
                emailNode.SelectedImageIndex = -2;
            }

            treeNodeEmails.Expand();
        }

        private void DisplayWebSites()
        {
            treeNodeWebSites.Nodes.Clear();

            if (webSites == null)
                return;

            foreach (WebSite webSite in webSites)
            {
                TreeNode webSiteNode = new TreeNode(webSite.ToString(), -2, -2);
                webSiteNode.Tag = webSite;
                treeNodeWebSites.Nodes.Add(webSiteNode);
                webSiteNode.ImageIndex = -2;
                webSiteNode.SelectedImageIndex = -2;
            }

            treeNodeWebSites.Expand();
        }

        private void DisplayAddresses()
        {
            treeNodeAddresses.Nodes.Clear();

            if (addresses == null)
                return;

            foreach (Address address in addresses)
            {
                TreeNode addressNode = new TreeNode(address.ToString(), -2, -2);
                addressNode.Tag = address;
                treeNodeAddresses.Nodes.Add(addressNode);
                addressNode.ImageIndex = -2;
                addressNode.SelectedImageIndex = -2;
            }

            treeNodeAddresses.Expand();
        }

        private void DisplayDates()
        {
            treeNodeDates.Nodes.Clear();

            if (dates == null)
                return;

            foreach (Date date in dates)
            {
                TreeNode dateNode = new TreeNode(date.ToString(), -2, -2);
                dateNode.Tag = date;
                treeNodeDates.Nodes.Add(dateNode);
                dateNode.ImageIndex = -2;
                dateNode.SelectedImageIndex = -2;
            }

            treeNodeDates.Expand();
        }

        private void DisplayMessengerIds()
        {
            treeNodeMessengerIds.Nodes.Clear();

            if (messengerIds == null)
                return;

            foreach (MessengerId messengerId in messengerIds)
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
