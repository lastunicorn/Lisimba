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
            this.dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            this.dataGridView1.DataSource = this.dates.ToDataTable();

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(DateCollection dates)
        {
            this.dates = dates;
            this.RefreshData();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                FormDateEdit formDateEdit = new FormDateEdit();
                Rectangle rect;

                if (e.RowIndex >= 0 && e.RowIndex < this.dates.Count)
                    formDateEdit.Date = this.dates[e.RowIndex];

                rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                formDateEdit.Location = dataGridView1.PointToScreen(new Point(rect.Left, rect.Top + rect.Height - 1));
                formDateEdit.DateUpdated += new FormDateEdit.DateUpdatedHandler(formDataEdit_DateUpdated);
                formDateEdit.Show();
                formDateEdit.Focus();

                e.Cancel = true;
            }
        }

        void formDataEdit_DateUpdated(object sender, FormDateEdit.DateUpdatedEventArgs e)
        {
            this.RefreshData();
            this.OnDateChanged(new DateChangedEventArgs(e.Date));
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Date date = this.dates[e.RowIndex];

            if (date != null)
            {
                if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!date.Description.Equals(newDescription))
                    {
                        date.Description = newDescription;
                        this.OnDateChanged(new DateChangedEventArgs(date));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                this.dates.Add(new Date());
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
                this.deleteDateToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void addDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Date date = new Date();
            this.dates.Add(date);
            this.RefreshData();
            this.OnDateAdded(new DateAddedEventArgs(date));
        }

        private void deleteDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    int index = this.dataGridView1.Rows.IndexOf(r);
                    Date date = this.dates[index];
                    this.dates.RemoveAt(index);
                    this.OnDateDeleted(new DateDeletedEventArgs(date));
                }

                this.RefreshData();
            }
        }
    }
}
