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

namespace DustInTheWind.Lisimba.UserControls
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
            dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            dataGridView1.DataSource = emails.ToDataTable();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(EmailCollection emails)
        {
            this.emails = emails;
            RefreshData();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Email email = emails[e.RowIndex];

            if (email != null)
            {
                if (e.ColumnIndex == 0)
                {
                    string newAddress = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!email.Address.Equals(newAddress))
                    {
                        email.Address = newAddress;
                        OnEmailChanged(new EmailChangedEventArgs(email));
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!email.Description.Equals(newDescription))
                    {
                        email.Description = newDescription;
                        OnEmailChanged(new EmailChangedEventArgs(email));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                emails.Add(new Email());
                RefreshData();
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Get info about the location where the user clicked.
            System.Windows.Forms.DataGridView.HitTestInfo info = dataGridView1.HitTest(e.X, e.Y);

            // Select the row that the user clicked.
            if (info.RowIndex >= 0)
                dataGridView1.Rows[info.RowIndex].Selected = true;
            else
                if (dataGridView1.SelectedRows.Count > 0)
                    dataGridView1.SelectedRows[0].Selected = false;

            if (e.Button == MouseButtons.Right)
            {
                // Refresh the context menu.
                deleteEmailToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void addEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Email email = new Email();
            emails.Add(email);
            RefreshData();
            OnEmailAdded(new EmailAddedEventArgs(email));
        }

        private void deleteEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    int index = dataGridView1.Rows.IndexOf(r);
                    Email email = emails[index];
                    emails.RemoveAt(index);
                    OnEmailDeleted(new EmailDeletedEventArgs(email));
                }

                RefreshData();
            }
        }
    }
}
