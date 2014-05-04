using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba
{
    public partial class FormAddContact : Form
    {
        private Contact contact = null;
        public Contact Contact
        {
            get
            {
                return this.contact;
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
            this.contactView1.Contact = new Contact();
            this.contactView1.CheckMandatoryFields = false;
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            Contact p = this.contactView1.Contact;
            if (this.ValidateContact(p))
            {
                this.contact = p;
            }
            else
            {
                MessageBox.Show("Please enter at least one of the fields marked with \"*\".", "Insufficient data.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.DialogResult = DialogResult.None;
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

            if (this.contacts != null)
            {
                Contact p = null;
                for (int i = 0; i < this.contacts.Count; i++)
                {
                    p = this.contacts[i];
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