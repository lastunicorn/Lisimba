using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba
{
    public partial class FormBiorythm : Form
    {
        private Contact contact = null;
        public Contact Contact
        {
            get { return contact; }
            set { contact = value; RefreshData(); }
        }

        private void RefreshData()
        {
            if (contact == null)
            {
                biorythm1.Birthday = new DateTime(0);
                this.Text = "The biorythm";
                biorythm1.Enabled = false;
            }
            else
            {
                biorythm1.Birthday = contact.Birthday.ToDateTime();
                this.Text = "The biorythm for " + contact.Name.ToString();
                biorythm1.Enabled = true;
            }
        }

        public FormBiorythm()
        {
            InitializeComponent();
            biorythm1.CurrentDay = DateTime.Today;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}