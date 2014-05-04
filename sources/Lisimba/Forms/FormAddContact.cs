using System;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormAddContact : Form
    {
        private Contact contact = null;
        public Contact Contact
        {
            get
            {
                return contact;
            }
        }

        private ContactCollection contacts = null;
        public ContactCollection Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        public FormAddContact()
            : this(null)
        {
        }

        public FormAddContact(ContactCollection contacts)
        {
            InitializeComponent();

            this.contacts = contacts;
            contactView1.Contact = new Contact();
            contactView1.CheckMandatoryFields = false;
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            Contact p = contactView1.Contact;
            if (ValidateContact(p))
            {
                contact = p;
            }
            else
            {
                MessageBox.Show("Please enter at least one of the fields marked with \"*\".", "Insufficient data.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                DialogResult = DialogResult.None;
            }
        }

        private bool ValidateContact(Contact contact)
        {
            if (contact.Name.FirstName.Length == 0 &&
                contact.Name.MiddleName.Length == 0 &&
                contact.Name.LastName.Length == 0 &&
                contact.Name.Nickname.Length == 0)
            {
                return false;
            }

            if (contacts != null)
            {
                Contact p = null;
                for (int i = 0; i < contacts.Count; i++)
                {
                    p = contacts[i];
                    if (p.Name.FirstName.Equals(contact.Name.FirstName) &&
                        p.Name.MiddleName.Equals(contact.Name.MiddleName) &&
                        p.Name.LastName.Equals(contact.Name.LastName) &&
                        p.Name.Nickname.Equals(contact.Name.Nickname))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}