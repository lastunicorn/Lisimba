using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba
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
            private Phone phone = null;
            public Phone Phone
            {
                get { return this.phone; }
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
            private Phone phone = null;
            public Phone Phone
            {
                get { return this.phone; }
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
            private Phone phone = null;
            public Phone Phone
            {
                get { return this.phone; }
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
            this.dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            this.dataGridView1.DataSource = this.phones.ToDataTable();

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(PhoneCollection phones)
        {
            this.phones = phones;
            this.RefreshData();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Phone phone = this.phones[e.RowIndex];

            if (phone != null)
            {
                if (e.ColumnIndex == 0)
                {
                    string newNumber = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!phone.Number.Equals(newNumber))
                    {
                        phone.Number = newNumber;
                        this.OnPhoneChanged(new PhoneChangedEventArgs(phone));
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!phone.Description.Equals(newDescription))
                    {
                        phone.Description = newDescription;
                        this.OnPhoneChanged(new PhoneChangedEventArgs(phone));
                    }
                }

            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                this.phones.Add(new Phone());
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
                this.deletePhoneToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void addPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Phone p = new Phone();
            this.phones.Add(p);
            this.RefreshData();
            this.OnPhoneAdded(new PhoneAddedEventArgs(p));
        }

        private void deletePhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    int index = this.dataGridView1.Rows.IndexOf(r);
                    Phone p = this.phones[index];
                    this.phones.RemoveAt(index);
                    this.OnPhoneDeleted(new PhoneDeletedEventArgs(p));
                }

                this.RefreshData();
            }
        }
    }
}
