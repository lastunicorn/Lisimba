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
    public partial class MessengerIdListView : UserControl
    {
        private MessengerIdCollection messengerIds = null;
 
        public MessengerIdListView()
        {
            InitializeComponent();
        }

        #region Event MessengerIdAdded

        public event MessengerIdAddedHandler MessengerIdAdded;
        public delegate void MessengerIdAddedHandler(object sender, MessengerIdAddedEventArgs e);

        public class MessengerIdAddedEventArgs : EventArgs
        {
            private MessengerId messengerId;
            public MessengerId MessengerId
            {
                get { return messengerId; }
            }


            public MessengerIdAddedEventArgs(MessengerId messengerId)
            {
                this.messengerId = messengerId;
            }
        }

        protected void OnMessengerIdAdded(MessengerIdAddedEventArgs e)
        {
            if (MessengerIdAdded != null)
            {
                MessengerIdAdded(this, e);
            }
        }

        #endregion Event MessengerIdAdded

        #region Event MessengerIdChanged

        public event MessengerIdChangedHandler MessengerIdChanged;
        public delegate void MessengerIdChangedHandler(object sender, MessengerIdChangedEventArgs e);

        public class MessengerIdChangedEventArgs : EventArgs
        {
            private MessengerId messengerId;
            public MessengerId MessengerId
            {
                get { return messengerId; }
            }


            public MessengerIdChangedEventArgs(MessengerId messengerId)
            {
                this.messengerId = messengerId;
            }
        }

        protected virtual void OnMessengerIdChanged(MessengerIdChangedEventArgs e)
        {
            if (MessengerIdChanged != null)
            {
                MessengerIdChanged(this, e);
            }
        }

        #endregion Event MessengerIdChanged

        #region Event MessengerIdDeleted

        public event MessengerIdDeletedHandler MessengerIdDeleted;
        public delegate void MessengerIdDeletedHandler(object sender, MessengerIdDeletedEventArgs e);

        public class MessengerIdDeletedEventArgs : EventArgs
        {
            private MessengerId messengerId;
            public MessengerId MessengerId
            {
                get { return messengerId; }
            }


            public MessengerIdDeletedEventArgs(MessengerId messengerId)
            {
                this.messengerId = messengerId;
            }
        }

        protected virtual void OnMessengerIdDeleted(MessengerIdDeletedEventArgs e)
        {
            if (MessengerIdDeleted != null)
            {
                MessengerIdDeleted(this, e);
            }
        }

        #endregion Event MessengerIdDeleted

        public void Clear()
        {
            this.dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            this.dataGridView1.DataSource = this.messengerIds.ToDataTable();

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(MessengerIdCollection messengerIds)
        {
            this.messengerIds = messengerIds;
            this.RefreshData();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MessengerId messengerId = this.messengerIds[e.RowIndex];

            if (messengerId != null)
            {
                if (e.ColumnIndex == 0)
                {
                    string newId = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!messengerId.Id.Equals(newId))
                    {
                        messengerId.Id = newId;
                        this.OnMessengerIdChanged(new MessengerIdChangedEventArgs(messengerId));
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!messengerId.Description.Equals(newDescription))
                    {
                        messengerId.Description = newDescription;
                        this.OnMessengerIdChanged(new MessengerIdChangedEventArgs(messengerId));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                this.messengerIds.Add(new MessengerId());
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
                this.deleteMessengerIdToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void addMessengerIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessengerId messengerId = new MessengerId();
            this.messengerIds.Add(messengerId);
            this.RefreshData();
            this.OnMessengerIdAdded(new MessengerIdAddedEventArgs(messengerId));
        }

        private void deleteMessengerIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    int index = this.dataGridView1.Rows.IndexOf(r);
                    MessengerId messengerId = this.messengerIds[index];
                    this.messengerIds.RemoveAt(index);
                    this.OnMessengerIdDeleted(new MessengerIdDeletedEventArgs(messengerId));
                }

                this.RefreshData();
            }
        }
    }
}
