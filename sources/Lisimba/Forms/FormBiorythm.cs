// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
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
                Text = "The biorythm";
                biorythm1.Enabled = false;
            }
            else
            {
                biorythm1.Birthday = contact.Birthday.ToDateTime();
                Text = "The biorythm for " + contact.Name.ToString();
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
            Close();
        }
    }
}