using System;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.WinForms.ContactDetailsEditing
{
    public partial class ContactItemView : UserControl
    {
        private ContactItem contactItem;

        public ContactItem ContactItem
        {
            get { return contactItem; }
            set
            {
                if (contactItem != null)
                    contactItem.Changed -= HandleContactItemChanged;

                contactItem = value;

                if (contactItem != null)
                    contactItem.Changed += HandleContactItemChanged;

                RefreshDisplayedData();
            }
        }

        public ContactItemView()
        {
            InitializeComponent();
        }

        private void HandleContactItemChanged(object sender, EventArgs eventArgs)
        {
            RefreshDisplayedData();
        }

        private void RefreshDisplayedData()
        {
            label1.Text = contactItem.ToString();
        }

        private void HandleMouseEnter(object sender, EventArgs e)
        {
            HighlightOn();
        }

        private void HandleMouseLeave(object sender, EventArgs e)
        {
            HighlightOff();
        }

        private void HandleTableLayoutPanelMouseEnter(object sender, EventArgs e)
        {
            HighlightOn();
        }

        private void HandleTableLayoutPanelMouseLeave(object sender, EventArgs e)
        {
            HighlightOff();
        }

        private void HandleLabelMouseEnter(object sender, EventArgs e)
        {
            HighlightOn();
        }

        private void HandleLabelMouseLeave(object sender, EventArgs e)
        {
            HighlightOff();
        }

        private void HighlightOn()
        {
            BackColor = SystemColors.Highlight;
            ForeColor = SystemColors.HighlightText;
        }

        private void HighlightOff()
        {
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
        }
    }
}
