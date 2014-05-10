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
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Forms;

namespace DustInTheWind.Lisimba.UserControls
{
    public partial class DateListView : UserControl
    {
        private DateCollection dates = null;

        public DateListView()
        {
            InitializeComponent();
        }

        #region Event DateAdded

        public event DateAddedHandler DateAdded;
        public delegate void DateAddedHandler(object sender, DateAddedEventArgs e);

        public class DateAddedEventArgs : EventArgs
        {
            private Date date;
            public Date Date
            {
                get { return date; }
            }

            public DateAddedEventArgs(Date date)
            {
                this.date = date;
            }
        }

        protected virtual void OnDateAdded(DateAddedEventArgs e)
        {
            if (DateAdded != null)
            {
                DateAdded(this, e);
            }
        }

        #endregion Event DateAdded

        #region Event DateChanged

        public event DateChangedHandler DateChanged;
        public delegate void DateChangedHandler(object sender, DateChangedEventArgs e);

        public class DateChangedEventArgs : EventArgs
        {
            private Date date;
            public Date Date
            {
                get { return date; }
            }

            public DateChangedEventArgs(Date date)
            {
                this.date = date;
            }
        }

        protected virtual void OnDateChanged(DateChangedEventArgs e)
        {
            if (DateChanged != null)
            {
                DateChanged(this, e);
            }
        }

        #endregion Event DateChanged

        #region Event DateDeleted

        public event DateDeletedHandler DateDeleted;
        public delegate void DateDeletedHandler(object sender, DateDeletedEventArgs e);

        public class DateDeletedEventArgs : EventArgs
        {
            private Date date;
            public Date Date
            {
                get { return date; }
            }

            public DateDeletedEventArgs(Date date)
            {
                this.date = date;
            }
        }

        protected virtual void OnDateDeleted(DateDeletedEventArgs e)
        {
            if (DateDeleted != null)
            {
                DateDeleted(this, e);
            }
        }

        #endregion Event DateDeleted

        public void Clear()
        {
            dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            dataGridView1.DataSource = dates.ToDataTable();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(DateCollection dates)
        {
            this.dates = dates;
            RefreshData();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                FormDateEdit formDateEdit = new FormDateEdit();
                Rectangle rect;

                if (e.RowIndex >= 0 && e.RowIndex < dates.Count)
                    formDateEdit.Date = dates[e.RowIndex];

                rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                formDateEdit.Location = dataGridView1.PointToScreen(new Point(rect.Left, rect.Top + rect.Height - 1));
                formDateEdit.DateUpdated += formDataEdit_DateUpdated;
                formDateEdit.Show();
                formDateEdit.Focus();

                e.Cancel = true;
            }
        }

        void formDataEdit_DateUpdated(object sender, DateUpdatedEventArgs e)
        {
            RefreshData();
            OnDateChanged(new DateChangedEventArgs(e.Date));
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Date date = dates[e.RowIndex];

            if (date != null)
            {
                if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!date.Description.Equals(newDescription))
                    {
                        date.Description = newDescription;
                        OnDateChanged(new DateChangedEventArgs(date));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                dates.Add(new Date());
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
                deleteDateToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void addDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Date date = new Date();
            dates.Add(date);
            RefreshData();
            OnDateAdded(new DateAddedEventArgs(date));
        }

        private void deleteDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    int index = dataGridView1.Rows.IndexOf(r);
                    Date date = dates[index];
                    dates.RemoveAt(index);
                    OnDateDeleted(new DateDeletedEventArgs(date));
                }

                RefreshData();
            }
        }
    }
}
