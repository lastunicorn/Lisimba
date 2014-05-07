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
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.UserControls
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
            dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            dataGridView1.DataSource = messengerIds.ToDataTable();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(MessengerIdCollection messengerIds)
        {
            this.messengerIds = messengerIds;
            RefreshData();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MessengerId messengerId = messengerIds[e.RowIndex];

            if (messengerId != null)
            {
                if (e.ColumnIndex == 0)
                {
                    string newId = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!messengerId.Id.Equals(newId))
                    {
                        messengerId.Id = newId;
                        OnMessengerIdChanged(new MessengerIdChangedEventArgs(messengerId));
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!messengerId.Description.Equals(newDescription))
                    {
                        messengerId.Description = newDescription;
                        OnMessengerIdChanged(new MessengerIdChangedEventArgs(messengerId));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                messengerIds.Add(new MessengerId());
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
                deleteMessengerIdToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void addMessengerIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessengerId messengerId = new MessengerId();
            messengerIds.Add(messengerId);
            RefreshData();
            OnMessengerIdAdded(new MessengerIdAddedEventArgs(messengerId));
        }

        private void deleteMessengerIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    int index = dataGridView1.Rows.IndexOf(r);
                    MessengerId messengerId = messengerIds[index];
                    messengerIds.RemoveAt(index);
                    OnMessengerIdDeleted(new MessengerIdDeletedEventArgs(messengerId));
                }

                RefreshData();
            }
        }
    }
}
