using System;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.UserControls
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

            phonesNode = treeView1.Nodes["Phones"];
            emailsNode = treeView1.Nodes["Emails"];
            webSitesNode = treeView1.Nodes["WebSites"];
            addressesNode = treeView1.Nodes["Addresses"];
            datesNode = treeView1.Nodes["Dates"];
            messengerIdsNode = treeView1.Nodes["MessengerIds"];

            formBirthdayEdit = new FormDateEdit();
            formBirthdayEdit.DateUpdated += new FormDateEdit.DateUpdatedHandler(formBirthdayEdit_DateUpdated);

            formPhoneEdit = new FormPhoneEdit();
            formPhoneEdit.PhoneUpdated += new FormPhoneEdit.PhoneUpdatedHandler(formPhoneEdit_PhoneUpdated);

            formEmailEdit = new FormEmailEdit();
            formEmailEdit.EmailUpdated += new FormEmailEdit.EmailUpdatedHandler(formEmailEdit_EmailUpdated);

            formWebSiteEdit = new FormWebSiteEdit();
            formWebSiteEdit.WebSiteUpdated += new FormWebSiteEdit.WebSiteUpdatedHandler(formWebSiteEdit_WebSiteUpdated);

            formAddressEdit = new FormAddressEdit();
            formAddressEdit.AddressUpdated += new FormAddressEdit.AddressUpdatedHandler(formAddressEdit_AddressUpdated);

            formDateEdit = new FormDateEdit();
            formDateEdit.DateUpdated += new FormDateEdit.DateUpdatedHandler(formDateEdit_DateUpdated);
        }

        public Contact Contact
        {
            get
            {
                return contact;
            }
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
            isFillMode = true;

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

            isFillMode = false;
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
            if (!isFillMode)
            {
                contact.Name.FirstName = textBoxFirstName.Text;
                OnNameChanged(new NameChangedEventArgs(NameSection.FirstName));
                OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxMiddleName_TextChanged(object sender, EventArgs e)
        {
            if (!isFillMode)
            {
                contact.Name.MiddleName = textBoxMiddleName.Text;
                OnNameChanged(new NameChangedEventArgs(NameSection.MiddleName));
                OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            if (!isFillMode)
            {
                contact.Name.LastName = textBoxLastName.Text;
                OnNameChanged(new NameChangedEventArgs(NameSection.LastName));
                OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxNickname_TextChanged(object sender, EventArgs e)
        {
            if (!isFillMode)
            {
                contact.Name.Nickname = textBoxNickname.Text;
                OnNameChanged(new NameChangedEventArgs(NameSection.Nickname));
                OnContactChanged(new ContactChangedEventArgs());
            }
        }

        private void textBoxNotes_TextChanged(object sender, EventArgs e)
        {
            if (!isFillMode)
            {
                contact.Notes = textBoxNotes.Text;
                OnContactChanged(new ContactChangedEventArgs());
            }
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
            OnContactChanged(new ContactChangedEventArgs());
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
            if (checkMandatoryFields)
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
                return;

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
            {
                return;
            }
        }

        void formPhoneEdit_PhoneUpdated(object sender, FormPhoneEdit.PhoneUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged(new ContactChangedEventArgs());
        }

        void formEmailEdit_EmailUpdated(object sender, FormEmailEdit.EmailUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged(new ContactChangedEventArgs());
        }

        void formWebSiteEdit_WebSiteUpdated(object sender, FormWebSiteEdit.WebSiteUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged(new ContactChangedEventArgs());
        }

        void formAddressEdit_AddressUpdated(object sender, FormAddressEdit.AddressUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged(new ContactChangedEventArgs());
        }

        void formDateEdit_DateUpdated(object sender, FormDateEdit.DateUpdatedEventArgs e)
        {
            RefreshData();
            OnContactChanged(new ContactChangedEventArgs());
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
