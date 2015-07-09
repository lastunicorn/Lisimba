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

namespace DustInTheWind.Lisimba.UserControls
{
    public partial class PhoneListView : UserControl
    {
        private PhoneCollection phones = null;

        public PhoneListView()
        {
            InitializeComponent();
        }

        #region Event PhoneAdded

        public event PhoneAddedHandler PhoneAdded;
        public delegate void PhoneAddedHandler(object sender, PhoneAddedEventArgs e);

        public class PhoneAddedEventArgs : EventArgs
        {
            private readonly Phone phone = null;
            public Phone Phone
            {
                get { return phone; }
            }

            public PhoneAddedEventArgs(Phone phone)
            {
                this.phone = phone;
            }
        }

        protected virtual void OnPhoneAdded(PhoneAddedEventArgs e)
        {
            if (PhoneAdded != null)
            {
                PhoneAdded(this, e);
            }
        }

        #endregion Event PhoneAdded

        #region Event PhoneDeleted

        public event PhoneDeletedHandler PhoneDeleted;
        public delegate void PhoneDeletedHandler(object sender, PhoneDeletedEventArgs e);

        public class PhoneDeletedEventArgs : EventArgs
        {
            private readonly Phone phone = null;
            public Phone Phone
            {
                get { return phone; }
            }

            public PhoneDeletedEventArgs(Phone phone)
            {
                this.phone = phone;
            }
        }

        protected virtual void OnPhoneDeleted(PhoneDeletedEventArgs e)
        {
            if (PhoneDeleted != null)
            {
                PhoneDeleted(this, e);
            }
        }

        #endregion Event PhoneDeleted

        #region Event PhoneChanged

        public event PhoneChangedHandler PhoneChanged;
        public delegate void PhoneChangedHandler(object sender, PhoneChangedEventArgs e);

        public class PhoneChangedEventArgs : EventArgs
        {
            private readonly Phone phone = null;
            public Phone Phone
            {
                get { return phone; }
            }
	
            public PhoneChangedEventArgs(Phone phone)
            {
                this.phone = phone;
            }
        }

        protected virtual void OnPhoneChanged(PhoneChangedEventArgs e)
        {
            if (PhoneChanged != null)
            {
                PhoneChanged(this, e);
            }
        }

        #endregion Event PhoneChanged

        public void Clear()
        {
            dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            dataGridView1.DataSource = phones.ToDataTable();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(PhoneCollection phones)
        {
            this.phones = phones;
            RefreshData();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Phone phone = phones[e.RowIndex];

            if (phone != null)
            {
                if (e.ColumnIndex == 0)
                {
                    string newNumber = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!phone.Number.Equals(newNumber))
                    {
                        phone.Number = newNumber;
                        OnPhoneChanged(new PhoneChangedEventArgs(phone));
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!phone.Description.Equals(newDescription))
                    {
                        phone.Description = newDescription;
                        OnPhoneChanged(new PhoneChangedEventArgs(phone));
                    }
                }

            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                phones.Add(new Phone());
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
                deletePhoneToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void addPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Phone p = new Phone();
            phones.Add(p);
            RefreshData();
            OnPhoneAdded(new PhoneAddedEventArgs(p));
        }

        private void deletePhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    int index = dataGridView1.Rows.IndexOf(r);
                    Phone p = phones[index];
                    phones.RemoveAt(index);
                    OnPhoneDeleted(new PhoneDeletedEventArgs(p));
                }

                RefreshData();
            }
        }
    }
}
