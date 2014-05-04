using System;
using System.IO;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormBookProperties : Form
    {
        private AddressBook book;
        public bool IsModified = false;

        public AddressBook Book
        {
            get { return book; }
            set
            {
                book = value;
                IsModified = false;

                if (value != null)
                {
                    textBoxBookName.Text = value.Name;
                    textBoxFileLocation.Text = value.FileName.Length == 0 ? "<Address book is not saved yet.>" : Path.GetFullPath(value.FileName);
                    textBoxContactsCount.Text = value.Count.ToString();
                }
            }
        }

        public FormBookProperties()
        {
            InitializeComponent();
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            if (book != null)
            {
                if (!book.Name.Equals(textBoxBookName.Text))
                {
                    book.Name = textBoxBookName.Text;
                    IsModified = true;
                }
            }
        }

        private void FormBookProperties_Shown(object sender, EventArgs e)
        {
            textBoxBookName.Focus();
        }
    }
}