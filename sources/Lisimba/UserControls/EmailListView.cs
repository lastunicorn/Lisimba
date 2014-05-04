using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba
{
    public partial class EmailListView : UserControl
    {
        private EmailCollection emails = null;

        public EmailListView()
        {
            InitializeComponent();
        }

        #region Event EmailAdded

        public event EmailAddedHandler EmailAdded;
        public delegate void EmailAddedHandler(object sender, EmailAddedEventArgs e);

        public class EmailAddedEventArgs : EventArgs
        {
            private Email email;
            public Email Email
            {
                get { return email; }
            }
	
            public EmailAddedEventArgs(Email email)
            {
                this.email = email;
            }
        }

        protected virtual void OnEmailAdded(EmailAddedEventArgs e)
        {
            if (EmailAdded != null)
            {
                EmailAdded(this, e);
            }
        }

        #endregion Event EmailAdded

        #region Event EmailChanged

        public event EmailChangedHandler EmailChanged;
        public delegate void EmailChangedHandler(object sender, EmailChangedEventArgs e);

        public class EmailChangedEventArgs : EventArgs
        {
            private Email email;
            public Email Email
            {
                get { return email; }
            }

            public EmailChangedEventArgs(Email email)
            {
                this.email = email;
            }
        }

        protected virtual void OnEmailChanged(EmailChangedEventArgs e)
        {
            if (EmailChanged != null)
            {
                EmailChanged(this, e);
            }
        }

        #endregion Event EmailChanged

        #region Event EmailDeleted

        public event EmailDeletedHandler EmailDeleted;
        public delegate void EmailDeletedHandler(object sender, EmailDeletedEventArgs e);

        public class EmailDeletedEventArgs : EventArgs
        {
            private Email email;
            public Email Email
            {
                get { return email; }
            }
	
            public EmailDeletedEventArgs(Email email)
            {
                this.email = email;
            }
        }

        protected virtual void OnEmailDeleted(EmailDeletedEventArgs e)
        {
            if (EmailDeleted != null)
            {
                EmailDeleted(this, e);
            }
        }

        #endregion Event EmailDeleted

        public void Clear()
        {
            this.dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            this.dataGridView1.DataSource = this.emails.ToDataTable();

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(EmailCollection emails)
        {
            this.emails = emails;
            this.RefreshData();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Email email = this.emails[e.RowIndex];

            if (email != null)
            {
                if (e.ColumnIndex == 0)
                {
                    string newAddress = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!email.Address.Equals(newAddress))
                    {
                        email.Address = newAddress;
                        this.OnEmailChanged(new EmailChangedEventArgs(email));
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!email.Description.Equals(newDescription))
                    {
                        email.Description = newDescription;
                        this.OnEmailChanged(new EmailChangedEventArgs(email));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                this.emails.Add(new Email());
                this.RefreshData();
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Get info about the location where the user clicked.
            System.Windows.Forms.DataGridView.HitTestInfo info = this.dataGridView1.HitTest(e.X, e.Y);

            // Select the row that the user clicked.
            if (info.RowIndex >= 0)
                this.dataGridView1.Rows[info.RowIndex].Selected = true;
            else
                if (this.dataGridView1.SelectedRows.Count > 0)
                    this.dataGridView1.SelectedRows[0].Selected = false;

            if (e.Button == MouseButtons.Right)
            {
                // Refresh the context menu.
                this.deleteEmailToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void addEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Email email = new Email();
            this.emails.Add(email);
            this.RefreshData();
            this.OnEmailAdded(new EmailAddedEventArgs(email));
        }

        private void deleteEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    int index = this.dataGridView1.Rows.IndexOf(r);
                    Email email = this.emails[index];
                    this.emails.RemoveAt(index);
                    this.OnEmailDeleted(new EmailDeletedEventArgs(email));
                }

                this.RefreshData();
            }
        }
    }
}
