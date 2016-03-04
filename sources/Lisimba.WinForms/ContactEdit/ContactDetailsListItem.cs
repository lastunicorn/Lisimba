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
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.ContactEdit
{
    public partial class ContactDetailsListItem : UserControl
    {
        private ContactDetailsItem item;

        public ContactDetailsItem Item
        {
            get { return item; }
            set
            {
                pictureBoxIcon.DataBindings.Clear();

                item = value;

                if (item != null)
                {
                    pictureBoxIcon.Bind(x => x.Image, item, x => x.Image, false, DataSourceUpdateMode.OnPropertyChanged);
                    buttonAdd.Bind(x => x.Image, item, x => x.AddButtonImage, false, DataSourceUpdateMode.OnPropertyChanged);
                    labelTitle.Bind(x => x.Text, item, x => x.Title, false, DataSourceUpdateMode.OnPropertyChanged);
                }
            }
        }

        public Image Image
        {
            get { return pictureBoxIcon.Image; }
            set { pictureBoxIcon.Image = value; }
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

            panelItems.Controls.Add(label1);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void ContactDetailsListItem_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void labelTitle_Resize(object sender, EventArgs e)
        {
            labelTitle.Invalidate();
        }
    }
}