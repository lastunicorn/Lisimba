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
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    /// <summary>
    /// Control to display and edit a contact.
    /// </summary>
    partial class ContactView : UserControl, IContactView
    {
        private readonly ContactViewPresenter presenter;

        public ContactViewPresenter Presenter
        {
            get { return presenter; }
        }

        readonly FormDateEdit formBirthdayEdit;

        readonly FormPhoneEdit formPhoneEdit;
        readonly FormEmailEdit formEmailEdit;
        readonly FormWebSiteEdit formWebSiteEdit;
        readonly FormAddressEdit formAddressEdit;
        readonly FormDateEdit formDateEdit;

        public ContactView()
        {
            InitializeComponent();

            presenter = new ContactViewPresenter(new ZodiacService()) { View = this };

            formBirthdayEdit = new FormDateEdit();
            formPhoneEdit = new FormPhoneEdit();
            formEmailEdit = new FormEmailEdit();
            formWebSiteEdit = new FormWebSiteEdit();
            formAddressEdit = new FormAddressEdit();
            formDateEdit = new FormDateEdit();
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;

            if (selectedNode == null || selectedNode.Tag == null)
                return;

            if (selectedNode != treeView1.GetNodeAt(e.Location))
                return;

            if (selectedNode.Tag is string)
            {
                // todo: display the corresponding edit form to add a new item.
            }

            if (selectedNode.Tag is Phone)
            {
                formPhoneEdit.Phone = (Phone)selectedNode.Tag;
                formPhoneEdit.Location = treeView1.PointToScreen(e.Location);
                formPhoneEdit.Show();
                formPhoneEdit.Focus();
                return;
            }

            if (selectedNode.Tag is Email)
            {
                formEmailEdit.Email = (Email)selectedNode.Tag;
                formEmailEdit.Location = treeView1.PointToScreen(e.Location);
                formEmailEdit.Show();
                formEmailEdit.Focus();
                return;
            }

            if (selectedNode.Tag is WebSite)
            {
                formWebSiteEdit.WebSite = (WebSite)selectedNode.Tag;
                formWebSiteEdit.Location = treeView1.PointToScreen(e.Location);
                formWebSiteEdit.Show();
                formWebSiteEdit.Focus();
                return;
            }

            if (selectedNode.Tag is Address)
            {
                formAddressEdit.Address = (Address)selectedNode.Tag;
                formAddressEdit.Location = treeView1.PointToScreen(e.Location);
                formAddressEdit.Show();
                formAddressEdit.Focus();
                return;
            }

            if (selectedNode.Tag is Date)
            {
                formDateEdit.Date = (Date)selectedNode.Tag;
                formDateEdit.Location = treeView1.PointToScreen(e.Location);
                formDateEdit.Show();
                formDateEdit.Focus();
                return;
            }

            if (selectedNode.Tag is MessengerId)
                return;
        }

        // -------------------------------------------------------------------

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            presenter.FirstNameWasChanged();
        }

        private void textBoxMiddleName_TextChanged(object sender, EventArgs e)
        {
            presenter.MiddleNameWasChanged();
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            presenter.LastNameWasChanged();
        }

        private void textBoxNickname_TextChanged(object sender, EventArgs e)
        {
            presenter.NicknameWasChanged();
        }

        private void labelBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            presenter.BirthdayEditWasRequested();
        }

        private void textBoxNotes_TextChanged(object sender, EventArgs e)
        {
            presenter.NotesWasChanged();
        }

        public string FirstName
        {
            get { return textBoxFirstName.Text; }
            set { textBoxFirstName.Text = value; }
        }

        public string MiddleName
        {
            get { return textBoxMiddleName.Text; }
            set { textBoxMiddleName.Text = value; }
        }

        public string LastName
        {
            get { return textBoxLastName.Text; }
            set { textBoxLastName.Text = value; }
        }

        public string Nickname
        {
            get { return textBoxNickname.Text; }
            set { textBoxNickname.Text = value; }
        }

        public string Birthday
        {
            get { return labelBirthday.Text; }
            set { labelBirthday.Text = value; }
        }

        public Image ZodiacSignImage
        {
            set { pictureBoxZodiacSign.Image = value; }
        }

        public string ZodiacSignText
        {
            set { labelZodiacSign.Text = value; }
        }

        public string Notes
        {
            get { return textBoxNotes.Text; }
            set { textBoxNotes.Text = value; }
        }

        public PhoneCollection Phones
        {
            set
            {
                TreeNode phonesNode = treeView1.Nodes["Phones"];
                phonesNode.Nodes.Clear();

                if (value == null)
                    return;

                foreach (Phone phone in value)
                {
                    TreeNode phoneNode = new TreeNode(phone.ToString(), -2, -2);
                    phoneNode.Tag = phone;
                    phonesNode.Nodes.Add(phoneNode);
                    phoneNode.ImageIndex = -2;
                    phoneNode.SelectedImageIndex = -2;
                }

                phonesNode.Expand();
            }
        }

        public EmailCollection Emails
        {
            set
            {
                TreeNode emailsNode = treeView1.Nodes["Emails"];
                emailsNode.Nodes.Clear();

                if (value == null)
                    return;

                foreach (Email email in value)
                {
                    TreeNode emailNode = new TreeNode(email.ToString(), -2, -2);
                    emailNode.Tag = email;
                    emailsNode.Nodes.Add(emailNode);
                    emailNode.ImageIndex = -2;
                    emailNode.SelectedImageIndex = -2;
                }

                emailsNode.Expand();
            }
        }

        public WebSiteCollection WebSites
        {
            set
            {
                TreeNode webSitesNode = treeView1.Nodes["WebSites"];
                webSitesNode.Nodes.Clear();

                if (value == null)
                    return;

                foreach (WebSite webSite in value)
                {
                    TreeNode webSiteNode = new TreeNode(webSite.ToString(), -2, -2);
                    webSiteNode.Tag = webSite;
                    webSitesNode.Nodes.Add(webSiteNode);
                    webSiteNode.ImageIndex = -2;
                    webSiteNode.SelectedImageIndex = -2;
                }

                webSitesNode.Expand();
            }
        }

        public AddressCollection Addresses
        {
            set
            {
                TreeNode addressesNode = treeView1.Nodes["Addresses"];
                addressesNode.Nodes.Clear();

                if (value == null)
                    return;

                foreach (Address address in value)
                {
                    TreeNode addressNode = new TreeNode(address.ToString(), -2, -2);
                    addressNode.Tag = address;
                    addressesNode.Nodes.Add(addressNode);
                    addressNode.ImageIndex = -2;
                    addressNode.SelectedImageIndex = -2;
                }

                addressesNode.Expand();
            }
        }

        public DateCollection Dates
        {
            set
            {
                TreeNode datesNode = treeView1.Nodes["Dates"];
                datesNode.Nodes.Clear();

                if (value == null)
                    return;

                foreach (Date date in value)
                {
                    TreeNode dateNode = new TreeNode(date.ToString(), -2, -2);
                    dateNode.Tag = date;
                    datesNode.Nodes.Add(dateNode);
                    dateNode.ImageIndex = -2;
                    dateNode.SelectedImageIndex = -2;
                }

                datesNode.Expand();
            }
        }

        public MessengerIdCollection MessengerIds
        {
            set
            {
                TreeNode messengerIdsNode = treeView1.Nodes["MessengerIds"];
                messengerIdsNode.Nodes.Clear();

                if (value == null)
                    return;

                foreach (MessengerId messengerId in value)
                {
                    TreeNode messengerIdNode = new TreeNode(messengerId.ToString(), -2, -2);
                    messengerIdNode.Tag = messengerId;
                    messengerIdsNode.Nodes.Add(messengerIdNode);
                    messengerIdNode.ImageIndex = -2;
                    messengerIdNode.SelectedImageIndex = -2;
                }

                messengerIdsNode.Expand();
            }
        }

        public void EditBirthday(Date birthday)
        {
            // client position 
            int clientX = labelBirthday.Location.X;
            int clientY = labelBirthday.Location.Y + labelBirthday.Height;
            Point clientPoint = new Point(clientX, clientY);

            // screen position
            Point screenPoint = PointToScreen(clientPoint);

            // initialize form
            formBirthdayEdit.Location = screenPoint;
            formBirthdayEdit.Date = birthday;

            // show form
            formBirthdayEdit.Show();
            formBirthdayEdit.Focus();
        }
    }
}
