using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using System.IO;

namespace DustInTheWind.Lisimba
{
    public partial class FormBookProperties : Form
    {
        private AddressBook book;
        public bool IsModified = false;

        public AddressBook Book
        {
            get { return this.book; }
            set
            {
                this.book = value;
                this.IsModified = false;

                if (value != null)
                {
                    this.textBoxBookName.Text = value.Name;
                    this.textBoxFileLocation.Text = value.FileName.Length == 0 ? "<Address book is not saved yet.>" : Path.GetFullPath(value.FileName);
                    this.textBoxContactsCount.Text = value.Count.ToString();
                }
            }
        }

        public FormBookProperties()
        {
            InitializeComponent();
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            if (this.book != null)
            {
                if (!this.book.Name.Equals(this.textBoxBookName.Text))
                {
                    this.book.Name = this.textBoxBookName.Text;
                    this.IsModified = true;
                }
            }
        }

        private void FormBookProperties_Shown(object sender, EventArgs e)
        {
            this.textBoxBookName.Focus();
        }
    }
}