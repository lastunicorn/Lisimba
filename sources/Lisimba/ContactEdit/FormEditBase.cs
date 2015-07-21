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
using System.Drawing;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class FormEditBase : Form
    {
        public bool AllowClose { get; set; }

        public FormEditBase()
        {
            InitializeComponent();
        }

        protected virtual void UpdateData()
        {
        }

        protected void AcceptChanges()
        {
            DialogResult = DialogResult.OK;
            UpdateData();
            Hide();
        }

        private void FormEditBase_Activated(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            // Correct the position on the screen.

            const int margin = 10;

            // the screen
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            screen.Width -= Width + margin;
            screen.Height -= Height + margin;

            // new position
            Point p = Location;
            
            int x = Math.Min(screen.Width, p.X);
            x = Math.Max(margin, x);
            
            int y = Math.Min(screen.Height, p.Y);
            y = Math.Max(margin, y);

            Location = new Point(x, y);
        }

        private void FormEditBase_Deactivate(object sender, EventArgs e)
        {
            if (DialogResult != DialogResult.None)
                return;

            DialogResult = DialogResult.OK;
            UpdateData();
            Hide();
        }

        public void FormEditBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                AcceptChanges();
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        private void FormEditBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                DialogResult = DialogResult.Cancel;

            if (!AllowClose)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
