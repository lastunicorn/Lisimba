using System;
using System.Drawing;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormEditBase : Form
    {
        private bool allowClose = false;
        public bool AllowClose
        {
            get { return allowClose; }
            set { allowClose = value; }
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
            DialogResult = DialogResult.OK;
            UpdateData();
            Hide();
        }

        private void FormEditBase_Activated(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            // Correct the position on the screen.

            int margin = 10;

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
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.OK;
                UpdateData();
                Hide();
            }
        }

        public void FormEditBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AcceptChanges();
            }

            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void FormEditBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }

            if (!allowClose)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
