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
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    /// <summary>
    /// Control to display and edit a contact.
    /// </summary>
    public partial class ContactView : System.Windows.Forms.UserControl
    {
        private ContactViewPresenter presenter;

        private Contact contact;
        private bool isInitializationMode;

        readonly FormDateEdit formBirthdayEdit;

        readonly FormPhoneEdit formPhoneEdit;
        readonly FormEmailEdit formEmailEdit;
        readonly FormWebSiteEdit formWebSiteEdit;
        readonly FormAddressEdit formAddressEdit;
        readonly FormDateEdit formDateEdit;

        public bool CheckMandatoryFields { get; set; }

        private readonly TreeNode phonesNode;
        private readonly TreeNode emailsNode;
        private readonly TreeNode webSitesNode;
        private readonly TreeNode addressesNode;
        private readonly TreeNode datesNode;
        private readonly TreeNode messengerIdsNode;

        public ContactView()
        {
            InitializeComponent();

            CurrentData currentData = new CurrentData();
            presenter = new ContactViewPresenter(currentData);

            CheckMandatoryFields = true;

            phonesNode = treeView1.Nodes["Phones"];
            emailsNode = treeView1.Nodes["Emails"];
            webSitesNode = treeView1.Nodes["WebSites"];
            addressesNode = treeView1.Nodes["Addresses"];
            datesNode = treeView1.Nodes["Dates"];
            messengerIdsNode = treeView1.Nodes["MessengerIds"];

            formBirthdayEdit = new FormDateEdit();
            formBirthdayEdit.DateUpdated += formBirthdayEdit_DateUpdated;

            formPhoneEdit = new FormPhoneEdit();
            formPhoneEdit.PhoneUpdated += formPhoneEdit_PhoneUpdated;

            formEmailEdit = new FormEmailEdit();
            formEmailEdit.EmailUpdated += formEmailEdit_EmailUpdated;

            formWebSiteEdit = new FormWebSiteEdit();
            formWebSiteEdit.WebSiteUpdated += formWebSiteEdit_WebSiteUpdated;

            formAddressEdit = new FormAddressEdit();
            formAddressEdit.AddressUpdated += formAddressEdit_AddressUpdated;

            formDateEdit = new FormDateEdit();
            formDateEdit.DateUpdated += formDateEdit_DateUpdated;
        }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                RefreshData();
            }
        }

        #region private void ClearData()

        private void ClearData()
        {
            textBoxFirstName.Text = string.Empty;
            textBoxMiddleName.Text = string.Empty;
            textBoxLastName.Text = string.Empty;
            textBoxNickname.Text = string.Empty;

            labelBirthday.Text = string.Empty;
            pictureBoxZodiacSign.Image = null;
            labelZodiacSign.Text = string.Empty;

            textBoxNotes.Text = string.Empty;

            phonesNode.Nodes.Clear();
            emailsNode.Nodes.Clear();
            webSitesNode.Nodes.Clear();
            addressesNode.Nodes.Clear();
            datesNode.Nodes.Clear();
            messengerIdsNode.Nodes.Clear();
        }

        #endregion

        private void RefreshData()
        {
            isInitializationMode = true;

            ClearData();

            if (contact != null)
            {
                textBoxFirstName.Text = contact.Name.FirstName;
                textBoxMiddleName.Text = contact.Name.MiddleName;
                textBoxLastName.Text = contact.Name.LastName;
                textBoxNickname.Text = contact.Name.Nickname;

                labelBirthday.Text = contact.Birthday.ToString();

                pictureBoxZodiacSign.Image = GetZodiacImage(contact.ZogiacSign);
                toolTip1.SetToolTip(pictureBoxZodiacSign, contact.ZogiacSign.ToString());
                labelZodiacSign.Text = contact.ZogiacSign.ToString();

                textBoxNotes.Text = contact.Notes;


                // Phones
                foreach (Phone phone in contact.Phones)
                {
                    TreeNode phoneNode = new TreeNode(phone.ToString(), -2, -2);
                    phoneNode.Tag = phone;
                    phonesNode.Nodes.Add(phoneNode);
                    phoneNode.ImageIndex = -2;
                    phoneNode.SelectedImageIndex = -2;
                }
                phonesNode.Expand();

                // Emails
                foreach (Email email in contact.Emails)
                {
                    TreeNode emailNode = new TreeNode(email.ToString(), -2, -2);
                    emailNode.Tag = email;
                    emailsNode.Nodes.Add(emailNode);
                    emailNode.ImageIndex = -2;
                    emailNode.SelectedImageIndex = -2;
                }
                emailsNode.Expand();

                // WebSites
                foreach (WebSite webSite in contact.WebSites)
                {
                    TreeNode webSiteNode = new TreeNode(webSite.ToString(), -2, -2);
                    webSiteNode.Tag = webSite;
                    webSitesNode.Nodes.Add(webSiteNode);
                    webSiteNode.ImageIndex = -2;
                    webSiteNode.SelectedImageIndex = -2;
                }
                webSitesNode.Expand();

                // Addresses
                foreach (Address address in contact.Addresses)
                {
                    TreeNode addressNode = new TreeNode(address.ToString(), -2, -2);
                    addressNode.Tag = address;
                    addressesNode.Nodes.Add(addressNode);
                    addressNode.ImageIndex = -2;
                    addressNode.SelectedImageIndex = -2;
                }
                addressesNode.Expand();

                // Dates
                foreach (Date date in contact.Dates)
                {
                    TreeNode dateNode = new TreeNode(date.ToString(), -2, -2);
                    dateNode.Tag = date;
                    datesNode.Nodes.Add(dateNode);
                    dateNode.ImageIndex = -2;
                    dateNode.SelectedImageIndex = -2;
                }
                datesNode.Expand();

                // Messenger Ids
                foreach (MessengerId messengerId in contact.MessengerIds)
                {
                    TreeNode messengerIdNode = new TreeNode(messengerId.ToString(), -2, -2);
                    messengerIdNode.Tag = messengerId;
                    messengerIdsNode.Nodes.Add(messengerIdNode);
                    messengerIdNode.ImageIndex = -2;
                    messengerIdNode.SelectedImageIndex = -2;
                }
                messengerIdsNode.Expand();
            }

            isInitializationMode = false;
        }

        private Image GetZodiacImage(ZodiacSign sign)
        {
            Image img = null;

            switch (sign)
            {
                case ZodiacSign.Aquarius:
                    img = Resources.Aquarius;
                    break;

                case ZodiacSign.Pisces:
                    img = Resources.Pisces;
                    break;

                case ZodiacSign.Aries:
                    img = Resources.Aries;
                    break;

                case ZodiacSign.Taurus:
                    img = Resources.Taurus;
                    break;

                case ZodiacSign.Gemini:
                    img = Resources.Gemini;
                    break;

                case ZodiacSign.Cancer:
                    img = Resources.Cancer;
                    break;

                case ZodiacSign.Leo:
                    img = Resources.Leo;
                    break;

                case ZodiacSign.Virgo:
                    img = Resources.Virgo;
                    break;

                case ZodiacSign.Libra:
                    img = Resources.Libra;
                    break;

                case ZodiacSign.Scorpio:
                    img = Resources.Scorpio;
                    break;

                case ZodiacSign.Sagittarius:
                    img = Resources.Sagittarius;
                    break;

                case ZodiacSign.Capricorn:
                    img = Resources.Capricorn;
                    break;
            }

            return img;
        }

        #region Event NameChanged

        public event EventHandler<NameChangedEventArgs> NameChanged;

        protected virtual void OnNameChanged(NameChangedEventArgs e)
        {
            EventHandler<NameChangedEventArgs> handler = NameChanged;

            if (handler != null)
                handler(this, e);
        }

        #endregion Event NameChanged

        #region Event ContactChanged

        public event EventHandler ContactChanged;

        protected virtual void OnContactChanged()
        {
            EventHandler handler = ContactChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion Event ContactChanged

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            if (isInitializationMode)
                return;

            contact.Name.FirstName = textBoxFirstName.Text;
            OnNameChanged(new NameChangedEventArgs(NameSection.FirstName));
            OnContactChanged();
        }

        private void textBoxMiddleName_TextChanged(object sender, EventArgs e)
        {
            if (isInitializationMode)
                return;

            contact.Name.MiddleName = textBoxMiddleName.Text;
            OnNameChanged(new NameChangedEventArgs(NameSection.MiddleName));
            OnContactChanged();
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            if (isInitializationMode)
                return;

            contact.Name.LastName = textBoxLastName.Text;
            OnNameChanged(new NameChangedEventArgs(NameSection.LastName));
            OnContactChanged();
        }

        private void textBoxNickname_TextChanged(object sender, EventArgs e)
        {
            if (isInitializationMode)
                return;

            contact.Name.Nickname = textBoxNickname.Text;
            OnNameChanged(new NameChangedEventArgs(NameSection.Nickname));
            OnContactChanged();
        }

        private void textBoxNotes_TextChanged(object sender, EventArgs e)
        {
            if (isInitializationMode)
                return;

            contact.Notes = textBoxNotes.Text;
            OnContactChanged();
        }

        private void textBoxBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditBirthday();
        }

        private void EditBirthday()
        {
            // client position 
            int cx = labelBirthday.Location.X;
            int cy = labelBirthday.Location.Y + labelBirthday.Height;
            Point cp = new Point(cx, cy);

            // screen position
            Point sp = PointToScreen(cp);

            // initialize form
            formBirthdayEdit.Location = sp;
            formBirthdayEdit.Date = contact.Birthday;

            // show form
            formBirthdayEdit.Show();
            formBirthdayEdit.Focus();
        }

        void formBirthdayEdit_DateUpdated(object sender, FormDateEdit.DateUpdatedEventArgs e)
        {
            labelBirthday.Text = e.Date.ToString();
            pictureBoxZodiacSign.Image = GetZodiacImage(contact.ZogiacSign);
            labelZodiacSign.Text = contact.ZogiacSign.ToString();
            OnContactChanged();
        }

        private void textBoxBirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                EditBirthday();
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            if (CheckMandatoryFields)
            {
                if (textBoxFirstName.Text.Length == 0 &&
                    textBoxMiddleName.Text.Length == 0 &&
                    textBoxLastName.Text.Length == 0 &&
                    textBoxNickname.Text.Length == 0)
                {
                    MessageBox.Show("At least one of the fields marked with \"*\" must be filled.", "Insufficient data.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    ((TextBox)sender).Focus();
                }
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;

            if (selectedNode == null || selectedNode.Tag == null)
            {
                contact.Addresses.Add(new Address());
                return;
            }

            if (selectedNode != treeView1.GetNodeAt(e.Location))
                return;

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

        void formPhoneEdit_PhoneUpdated(object sender, FormPhoneEdit.PhoneUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged();
        }

        void formEmailEdit_EmailUpdated(object sender, FormEmailEdit.EmailUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged();
        }

        void formWebSiteEdit_WebSiteUpdated(object sender, FormWebSiteEdit.WebSiteUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged();
        }

        void formAddressEdit_AddressUpdated(object sender, FormAddressEdit.AddressUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged();
        }

        void formDateEdit_DateUpdated(object sender, FormDateEdit.DateUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged();
        }

        private void labelBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditBirthday();
        }

        private void labelBirthday_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                EditBirthday();
            }
        }
    }
}
