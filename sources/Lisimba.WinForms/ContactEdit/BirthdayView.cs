using System;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class BirthdayView : UserControl
    {
        private Date birthday;

        public Date Birthday
        {
            get { return birthday; }
            set
            {
                if (birthday != null)
                    birthday.Changed -= HandleBirthdayChanged;

                birthday = value;

                if (birthday != null)
                    birthday.Changed += HandleBirthdayChanged;

                UpdateDisplayedValue();
            }
        }

        public BirthdayView()
        {
            InitializeComponent();
        }

        private void HandleBirthdayChanged(object sender, EventArgs eventArgs)
        {
            UpdateDisplayedValue();
        }

        private void UpdateDisplayedValue()
        {
            labelBirthday.Text = birthday != null
                ? birthday.ToShortString()
                : string.Empty;
        }

        private void HandleFlowLayoutPanelMouseEnter(object sender, EventArgs e)
        {
            HighlightOn();
        }

        private void HandleFlowLayoutPanelMouseLeave(object sender, EventArgs e)
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

        private void HandleLabelBirthdayMouseEnter(object sender, EventArgs e)
        {
            HighlightOn();
        }

        private void HandleLabelBirthdayMouseLeave(object sender, EventArgs e)
        {
            HighlightOff();
        }

        private void HighlightOn()
        {
            flowLayoutPanel2.BackColor = SystemColors.Info;
        }

        private void HighlightOff()
        {
            flowLayoutPanel2.BackColor = SystemColors.Control;
        }

        private void HandleFlowLayoutPanelMouseClick(object sender, MouseEventArgs e)
        {
            EditBirthday();
        }

        private void HandleLabelMouseClick(object sender, MouseEventArgs e)
        {
            EditBirthday();
        }

        private void HandleLabelBirthdayMouseClick(object sender, MouseEventArgs e)
        {
            EditBirthday();
        }

        private void EditBirthday()
        {
            if (Birthday == null)
                return;

            BirthDateEditForm form = new BirthDateEditForm
            {
                Location = labelBirthday.GetBottomLeftCorner(),
                Date = Birthday
            };

            form.Show();
            form.Focus();
        }
    }
}
