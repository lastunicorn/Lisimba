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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba
{
    public partial class AddressListView : UserControl
    {
        private AddressCollection addresses = null;

        public AddressListView()
        {
            InitializeComponent();
        }

        #region Event AddressAdded

        public event AddressAddedHandler AddressAdded;
        public delegate void AddressAddedHandler(object sender, AddressAddedEventArgs e);

        public class AddressAddedEventArgs : EventArgs
        {
            private Address address;
            public Address Address
            {
                get { return address; }
            }
	
            public AddressAddedEventArgs(Address address)
            {
                this.address = address;
            }
        }

        protected virtual void OnAddressAdded(AddressAddedEventArgs e)
        {
            if (AddressAdded != null)
            {
                AddressAdded(this, e);
            }
        }

        #endregion Event AddressAdded

        #region Event AddressChanged

        public event AddressChangedHandler AddressChanged;
        public delegate void AddressChangedHandler(object sender, AddressChangedEventArgs e);

        public class AddressChangedEventArgs : EventArgs
        {
            private Address address;
            public Address Address
            {
                get { return address; }
            }

            public AddressChangedEventArgs(Address address)
            {
                this.address = address;
            }
        }

        protected virtual void OnAddressChanged(AddressChangedEventArgs e)
        {
            if (AddressChanged != null)
            {
                AddressChanged(this, e);
            }
        }

        #endregion Event AddressChanged

        #region Event AddressDeleted

        public event AddressDeletedHandler AddressDeleted;
        public delegate void AddressDeletedHandler(object sender, AddressDeletedEventArgs e);

        public class AddressDeletedEventArgs : EventArgs
        {
            private Address address;
            public Address Address
            {
                get { return address; }
            }
	
            public AddressDeletedEventArgs(Address address)
            {
                this.address = address;
            }
        }

        protected virtual void OnAddressDeleted(AddressDeletedEventArgs e)
        {
            if (AddressDeleted != null)
            {
                AddressDeleted(this, e);
            }
        }

        #endregion Event AddressDeleted

        public void Clear()
        {
            this.dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            this.dataGridView1.DataSource = this.addresses.ToDataTable();

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(AddressCollection addresses)
        {
            this.addresses = addresses;
            this.RefreshData();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                FormAddressEdit formAddressEdit = new FormAddressEdit();
                Rectangle rect;

                if (e.RowIndex >= 0 && e.RowIndex < this.addresses.Count)
                    formAddressEdit.Address = this.addresses[e.RowIndex];

                rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                formAddressEdit.Location = dataGridView1.PointToScreen(new Point(rect.Left, rect.Top + rect.Height - 1));
                formAddressEdit.AddressUpdated += new FormAddressEdit.AddressUpdatedHandler(formAddressEdit_AddressUpdated);
                formAddressEdit.Show();
                formAddressEdit.Focus();

                e.Cancel = true;
            }
        }

        void formAddressEdit_AddressUpdated(object sender, FormAddressEdit.AddressUpdatedEventArgs e)
        {
            this.RefreshData();
            this.OnAddressChanged(new AddressChangedEventArgs(e.Address));
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Address address = this.addresses[e.RowIndex];

            if (address != null)
            {
                if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!address.Description.Equals(newDescription))
                    {
                        address.Description = newDescription;
                        this.OnAddressChanged(new AddressChangedEventArgs(address));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                this.addresses.Add(new Address());
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
                this.deleteAddressToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void addAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Address a = new Address();
            this.addresses.Add(a);
            this.RefreshData();
            this.OnAddressAdded(new AddressAddedEventArgs(a));
        }

        private void deleteAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    int index = this.dataGridView1.Rows.IndexOf(r);
                    Address a = this.addresses[index];
                    this.addresses.RemoveAt(index);
                    this.OnAddressDeleted(new AddressDeletedEventArgs(a));
                }

                this.RefreshData();
            }
        }
    }
}
