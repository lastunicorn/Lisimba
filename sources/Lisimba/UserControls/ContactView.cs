using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba
{
    /// <summary>
    /// Control to display and edit a contact.
    /// </summary>
    public partial class ContactView : System.Windows.Forms.UserControl
    {
        private Contact contact = null;
        private bool isFillMode = false;

        FormDateEdit formBirthdayEdit;

        FormPhoneEdit formPhoneEdit;
        FormEmailEdit formEmailEdit;
        FormWebSiteEdit formWebSiteEdit;
        FormAddressEdit formAddressEdit;
        FormDateEdit formDateEdit;

        private bool checkMandatoryFields = true;
        public bool CheckMandatoryFields
        {
            get { return checkMandatoryFields; }
            set { checkMandatoryFields = value; }
        }


        private TreeNode phonesNode;
        private TreeNode emailsNode;
        private TreeNode webSitesNode;
        private TreeNode addressesNode;
        private TreeNode datesNode;
        private TreeNode messengerIdsNode;

        public ContactView()
        {
            InitializeComponent();

            this.phonesNode = this.treeView1.Nodes["Phones"];
            this.emailsNode = this.treeView1.Nodes["Emails"];
            this.webSitesNode = this.treeView1.Nodes["WebSites"];
            this.addressesNode = this.treeView1.Nodes["Addresses"];
            this.datesNode = this.treeView1.Nodes["Dates"];
            this.messengerIdsNode = this.treeView1.Nodes["MessengerIds"];

            this.formBirthdayEdit = new FormDateEdit();
            this.formBirthdayEdit.DateUpdated += new FormDateEdit.DateUpdatedHandler(formBirthdayEdit_DateUpdated);

            this.formPhoneEdit = new FormPhoneEdit();
            this.formPhoneEdit.PhoneUpdated += new FormPhoneEdit.PhoneUpdatedHandler(formPhoneEdit_PhoneUpdated);

            this.formEmailEdit = new FormEmailEdit();
            this.formEmailEdit.EmailUpdated += new FormEmailEdit.EmailUpdatedHandler(formEmailEdit_EmailUpdated);

            this.formWebSiteEdit = new FormWebSiteEdit();
            this.formWebSiteEdit.WebSiteUpdated += new FormWebSiteEdit.WebSiteUpdatedHandler(formWebSiteEdit_WebSiteUpdated);

            this.formAddressEdit = new FormAddressEdit();
            this.formAddressEdit.AddressUpdated += new FormAddressEdit.AddressUpdatedHandler(formAddressEdit_AddressUpdated);

            this.formDateEdit = new FormDateEdit();
            this.formDateEdit.DateUpdated += new FormDateEdit.DateUpdatedHandler(formDateEdit_DateUpdated);
        }

        public Contact Contact
        {
            get
            {
                return this.contact;
            }
            set
            {
                this.contact = value;
                this.RefreshData();
            }
        }

        #region private void ClearData()

        private void ClearData()
        {
            this.textBoxFirstName.Text = string.Empty;
            this.textBoxMiddleName.Text = string.Empty;
            this.textBoxLastName.Text = string.Empty;
            this.textBoxNickname.Text = string.Empty;

            this.labelBirthday.Text = string.Empty;
            this.pictureBoxZodiacSign.Image = null;
            this.labelZodiacSign.Text = string.Empty;

            this.textBoxNotes.Text = string.Empty;

            this.phonesNode.Nodes.Clear();
            this.emailsNode.Nodes.Clear();
            this.webSitesNode.Nodes.Clear();
            this.addressesNode.Nodes.Clear();
            this.datesNode.Nodes.Clear();
            this.messengerIdsNode.Nodes.Clear();
        }

        #endregion

        private void RefreshData()
        {
            this.isFillMode = true;

            this.ClearData();

            if (this.contact != null)
            {
                this.textBoxFirstName.Text = this.contact.Name.FirstName;
                this.textBoxMiddleName.Text = this.contact.Name.MiddleName;
                this.textBoxLastName.Text = this.contact.Name.LastName;
                this.textBoxNickname.Text = this.contact.Name.Nickname;

                this.labelBirthday.Text = this.contact.Birthday.ToString();

                this.pictureBoxZodiacSign.Image = GetZodiacImage(this.contact.ZogiacSign);
                this.toolTip1.SetToolTip(this.pictureBoxZodiacSign, this.contact.ZogiacSign.ToString());
                this.labelZodiacSign.Text = this.contact.ZogiacSign.ToString();

                this.textBoxNotes.Text = this.contact.Notes;


                // Phones
                foreach (Phone phone in this.contact.Phones)
                {
                    TreeNode phoneNode = new TreeNode(phone.ToString(), -2, -2);
                    phoneNode.Tag = phone;
                    this.phonesNode.Nodes.Add(phoneNode);
                    phoneNode.ImageIndex = -2;
                    phoneNode.SelectedImageIndex = -2;
                }
                phonesNode.Expand();

                // Emails
                foreach (Email email in this.contact.Emails)
                {
                    TreeNode emailNode = new TreeNode(email.ToString(), -2, -2);
                    emailNode.Tag = email;
                    this.emailsNode.Nodes.Add(emailNode);
                    emailNode.ImageIndex = -2;
                    emailNode.SelectedImageIndex = -2;
                }
                emailsNode.Expand();

                // WebSites
                foreach (WebSite webSite in this.contact.WebSites)
                {
                    TreeNode webSiteNode = new TreeNode(webSite.ToString(), -2, -2);
                    webSiteNode.Tag = webSite;
                    webSitesNode.Nodes.Add(webSiteNode);
                    webSiteNode.ImageIndex = -2;
                    webSiteNode.SelectedImageIndex = -2;
                }
                webSitesNode.Expand();

                // Addresses
                foreach (Address address in this.contact.Addresses)
                {
                    TreeNode addressNode = new TreeNode(address.ToString(), -2, -2);
                    addressNode.Tag = address;
                    addressesNode.Nodes.Add(addressNode);
                    addressNode.ImageIndex = -2;
                    addressNode.SelectedImageIndex = -2;
                }
                addressesNode.Expand();

                // Dates
                foreach (Date date in this.contact.Dates)
                {
                    TreeNode dateNode = new TreeNode(date.ToString(), -2, -2);
                    dateNode.Tag = date;
                    datesNode.Nodes.Add(dateNode);
                    dateNode.ImageIndex = -2;
                    dateNode.SelectedImageIndex = -2;
                }
                datesNode.Expand();

                // Messenger Ids
                foreach (MessengerId messengerId in this.contact.MessengerIds)
                {
                    TreeNode messengerIdNode = new TreeNode(messengerId.ToString(), -2, -2);
                    messengerIdNode.Tag = messengerId;
                    messengerIdsNode.Nodes.Add(messengerIdNode);
                    messengerIdNode.ImageIndex = -2;
                    messengerIdNode.SelectedImageIndex = -2;
                }
                messengerIdsNode.Expand();
            }

            this.isFillMode = false;
        }

        #region private Image GetZodiacImage(ZodiacSign sign)

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

        #endregion

        #region Events

        #region Event NameChanged

        public event NameChangedHandler NameChanged;
        public delegate void NameChangedHandler(object sender, NameChangedEventArgs e);

        public class NameChangedEventArgs : EventArgs
        {
            private NameSection nameSection;

            public NameSection NameSection
            {
                get { return nameSection; }
            }

            public NameChangedEventArgs(NameSection nameSection)
            {
                this.nameSection = nameSection;
            }
        }

        protected virtual void OnNameChanged(NameChangedEventArgs e)
        {
            if (NameChanged != null)
            {
                NameChanged(this, e);
            }
        }

        #endregion Event NameChanged

        #region Event ContactChanged

        public event ContactChangedHandler ContactChanged;
        public delegate void ContactChangedHandler(object sender, ContactChangedEventArgs e);

        public class ContactChangedEventArgs : EventArgs
        {
            public ContactChangedEventArgs()
            {
            }
        }

        protected virtual void OnContactChanged(ContactChangedEventArgs e)
        {
            if (ContactChanged != null)
            {
                ContactChanged(this, e);
            }
        }

        #endregion Event ContactChanged

        #endregion

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            if (!this.isFillMode)
            {
                this.contact.Name.FirstName = this.textBoxFirstName.Text;
                this.OnNameChanged(new NameChangedEventArgs(NameSection.FirstName));
                this.OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxMiddleName_TextChanged(object sender, EventArgs e)
        {
            if (!this.isFillMode)
            {
                this.contact.Name.MiddleName = this.textBoxMiddleName.Text;
                this.OnNameChanged(new NameChangedEventArgs(NameSection.MiddleName));
                this.OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            if (!this.isFillMode)
            {
                this.contact.Name.LastName = this.textBoxLastName.Text;
                this.OnNameChanged(new NameChangedEventArgs(NameSection.LastName));
                this.OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxNickname_TextChanged(object sender, EventArgs e)
        {
            if (!this.isFillMode)
            {
                this.contact.Name.Nickname = this.textBoxNickname.Text;
                this.OnNameChanged(new NameChangedEventArgs(NameSection.Nickname));
                this.OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxNotes_TextChanged(object sender, EventArgs e)
        {
            if (!this.isFillMode)
            {
                this.contact.Notes = this.textBoxNotes.Text;
                this.OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.EditBirthday();
        }

        private void EditBirthday()
        {
            // client position 
            int cx = this.labelBirthday.Location.X;
            int cy = this.labelBirthday.Location.Y + this.labelBirthday.Height;
            Point cp = new Point(cx, cy);

            // screen position
            Point sp = this.PointToScreen(cp);

            // initialize form
            formBirthdayEdit.Location = sp;
            formBirthdayEdit.Date = this.contact.Birthday;

            // show form
            formBirthdayEdit.Show();
            formBirthdayEdit.Focus();
        }

        void formBirthdayEdit_DateUpdated(object sender, FormDateEdit.DateUpdatedEventArgs e)
        {
            this.labelBirthday.Text = e.Date.ToString();
            this.pictureBoxZodiacSign.Image = GetZodiacImage(this.contact.ZogiacSign);
            this.labelZodiacSign.Text = this.contact.ZogiacSign.ToString();
            this.OnContactChanged(new ContactChangedEventArgs());
        }

        private void textBoxBirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.EditBirthday();
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            if (this.checkMandatoryFields)
            {
                if (this.textBoxFirstName.Text.Length == 0 &&
                    this.textBoxMiddleName.Text.Length == 0 &&
                    this.textBoxLastName.Text.Length == 0 &&
                    this.textBoxNickname.Text.Length == 0)
                {
                    MessageBox.Show("At least one of the fields marked with \"*\" must be filled.", "Insufficient data.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    ((TextBox)sender).Focus();
                }
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;

            if (selectedNode == null || selectedNode.Tag == null)
                return;

            if (selectedNode != this.treeView1.GetNodeAt(e.Location))
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
                formDateEdit.Location = this.treeView1.PointToScreen(e.Location);
                formDateEdit.Show();
                formDateEdit.Focus();
                return;
            }

            if (selectedNode.Tag is MessengerId)
            {
                return;
            }
        }

        void formPhoneEdit_PhoneUpdated(object sender, FormPhoneEdit.PhoneUpdatedEventArgs e)
        {
            this.RefreshData();
            this.OnContactChanged(new ContactChangedEventArgs());
        }

        void formEmailEdit_EmailUpdated(object sender, FormEmailEdit.EmailUpdatedEventArgs e)
        {
            this.RefreshData();
            this.OnContactChanged(new ContactChangedEventArgs());
        }

        void formWebSiteEdit_WebSiteUpdated(object sender, FormWebSiteEdit.WebSiteUpdatedEventArgs e)
        {
            this.RefreshData();
            this.OnContactChanged(new ContactChangedEventArgs());
        }

        void formAddressEdit_AddressUpdated(object sender, FormAddressEdit.AddressUpdatedEventArgs e)
        {
            this.RefreshData();
            this.OnContactChanged(new ContactChangedEventArgs());
        }

        void formDateEdit_DateUpdated(object sender, FormDateEdit.DateUpdatedEventArgs e)
        {
            this.RefreshData();
            this.OnContactChanged(new ContactChangedEventArgs());
        }

        private void labelBirthday_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.EditBirthday();
        }

        private void labelBirthday_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.EditBirthday();
            }
        }
    }
}
