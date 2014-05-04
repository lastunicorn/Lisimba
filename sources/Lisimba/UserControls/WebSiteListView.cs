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
    public partial class WebSiteListView : UserControl
    {
        private WebSiteCollection webSites = null;

        public WebSiteListView()
        {
            InitializeComponent();
        }

        #region Event WebSiteAdded

        public event WebSiteAddedHandler WebSiteAdded;
        public delegate void WebSiteAddedHandler(object sender, WebSiteAddedEventArgs e);

        public class WebSiteAddedEventArgs : EventArgs
        {
            private WebSite webSite;
            public WebSite WebSite
            {
                get { return webSite; }
            }

            public WebSiteAddedEventArgs(WebSite webSite)
            {
                this.webSite = webSite;
            }
        }

        protected virtual void OnWebSiteAdded(WebSiteAddedEventArgs e)
        {
            if (WebSiteAdded != null)
            {
                WebSiteAdded(this, e);
            }
        }

        #endregion Event WebSiteAdded

        #region Event WebSiteChanged

        public event WebSiteChangedHandler WebSiteChanged;
        public delegate void WebSiteChangedHandler(object sender, WebSiteChangedEventArgs e);

        public class WebSiteChangedEventArgs : EventArgs
        {
            private WebSite webSite;
            public WebSite WebSite
            {
                get { return webSite; }
            }

            public WebSiteChangedEventArgs(WebSite webSite)
            {
                this.webSite = webSite;
            }
        }

        protected virtual void OnWebSiteChanged(WebSiteChangedEventArgs e)
        {
            if (WebSiteChanged != null)
            {
                WebSiteChanged(this, e);
            }
        }

        #endregion Event WebSiteChanged

        #region Event WebSiteDeleted

        public event WebSiteDeletedHandler WebSiteDeleted;
        public delegate void WebSiteDeletedHandler(object sender, WebSiteDeletedEventArgs e);

        public class WebSiteDeletedEventArgs : EventArgs
        {
            private WebSite webSite;
            public WebSite WebSite
            {
                get { return webSite; }
            }

            public WebSiteDeletedEventArgs(WebSite webSite)
            {
                this.webSite = webSite;
            }
        }

        protected virtual void OnWebSiteDeleted(WebSiteDeletedEventArgs e)
        {
            if (WebSiteDeleted != null)
            {
                WebSiteDeleted(this, e);
            }
        }

        #endregion Event WebSiteDeleted

        public void Clear()
        {
            this.dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            this.dataGridView1.DataSource = this.webSites.ToDataTable();

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(WebSiteCollection webSites)
        {
            this.webSites = webSites;
            this.RefreshData();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            WebSite webSite = this.webSites[e.RowIndex];

            if (webSite != null)
            {
                if (e.ColumnIndex == 0)
                {
                    string newAddress = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!webSite.Address.Equals(newAddress))
                    {
                        webSite.Address = newAddress;
                        this.OnWebSiteChanged(new WebSiteChangedEventArgs(webSite));
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!webSite.Description.Equals(newDescription))
                    {
                        webSite.Description = newDescription;
                        this.OnWebSiteChanged(new WebSiteChangedEventArgs(webSite));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                this.webSites.Add(new WebSite());
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
                this.deleteWebSiteToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void addWebSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebSite webSite = new WebSite();
            this.webSites.Add(webSite);
            this.RefreshData();
            this.OnWebSiteAdded(new WebSiteAddedEventArgs(webSite));
        }

        private void deleteWebSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    int index = this.dataGridView1.Rows.IndexOf(r);
                    WebSite webSite = this.webSites[index];
                    this.webSites.RemoveAt(index);
                    this.OnWebSiteDeleted(new WebSiteDeletedEventArgs(webSite));
                }

                this.RefreshData();
            }
        }
    }
}
