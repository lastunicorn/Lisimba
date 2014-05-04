using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba
{
    public partial class FormEditBase : Form
    {
        private bool allowClose = false;
        public bool AllowClose
        {
            get { return this.allowClose; }
            set { this.allowClose = value; }
        }

        public FormEditBase()
        {
            InitializeComponent();
        }

        protected virtual void UpdateData()
        {
        }

        protected void AcceptChanges()
        {
            this.DialogResult = DialogResult.OK;
            this.UpdateData();
            this.Hide();
        }

        private void FormEditBase_Activated(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            // Correct the position on the screen.

            int margin = 10;

            // the screen
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            screen.Width -= this.Width + margin;
            screen.Height -= this.Height + margin;

            // new position
            Point p = this.Location;
            int x = Math.Min(screen.Width, p.X);
            x = Math.Max(margin, x);
            int y = Math.Min(screen.Height, p.Y);
            y = Math.Max(margin, y);

            this.Location = new Point(x, y);
        }

        private void FormEditBase_Deactivate(object sender, EventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.OK;
                this.UpdateData();
                this.Hide();
            }
        }

        public void FormEditBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.AcceptChanges();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FormEditBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }

            if (!this.allowClose)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
