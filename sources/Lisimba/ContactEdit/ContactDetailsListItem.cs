using System;
using System.Drawing;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class ContactDetailsListItem : UserControl
    {
        public Image Image
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }
        public Image AddButtonImage
        {
            get { return buttonAdd.Image; }
            set { buttonAdd.Image = value; }
        }

        public string Title
        {
            get { return labelTitle.Text; }
            set { labelTitle.Text = value; }
        }

        public event EventHandler AddButtonClicked
        {
            add { buttonAdd.Click += value; }
            remove { buttonAdd.Click -= value; }
        }

        public ContactDetailsListItem()
        {
            InitializeComponent();
        }

        public void AddItem()
        {
            Label label1 = new Label
            {
                Text = "some text here",
                Dock = DockStyle.Top,
                BackColor = Color.DarkSalmon,
                Size = new Size(1, 1),
                AutoSize = true
            };

            Controls.Add(label1);
            Controls.SetChildIndex(label1, 0);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }
    }
}