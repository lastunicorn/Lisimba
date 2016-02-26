// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.ActionManagement;
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

        public ActionQueue ActionQueue { get; set; }

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
            flowLayoutPanel2.BackColor = SystemColors.Highlight;
            flowLayoutPanel2.ForeColor = SystemColors.HighlightText;
        }

        private void HighlightOff()
        {
            flowLayoutPanel2.BackColor = SystemColors.Control;
            flowLayoutPanel2.ForeColor = SystemColors.ControlText;
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
                ActionQueue = ActionQueue,
                Date = Birthday
            };

            form.Show();
            form.Focus();
        }
    }
}
