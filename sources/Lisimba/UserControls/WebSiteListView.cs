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
            dataGridView1.DataSource = null;
        }

        public void RefreshData()
        {
            dataGridView1.DataSource = webSites.ToDataTable();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Populate(WebSiteCollection webSites)
        {
            this.webSites = webSites;
            RefreshData();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            WebSite webSite = webSites[e.RowIndex];

            if (webSite != null)
            {
                if (e.ColumnIndex == 0)
                {
                    string newAddress = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!webSite.Address.Equals(newAddress))
                    {
                        webSite.Address = newAddress;
                        OnWebSiteChanged(new WebSiteChangedEventArgs(webSite));
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    string newDescription = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    if (!webSite.Description.Equals(newDescription))
                    {
                        webSite.Description = newDescription;
                        OnWebSiteChanged(new WebSiteChangedEventArgs(webSite));
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                webSites.Add(new WebSite());
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
                deleteWebSiteToolStripMenuItem.Enabled = (info.RowIndex >= 0 && info.ColumnIndex >= 0);

                // Display the context menu.
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void addWebSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebSite webSite = new WebSite();
            webSites.Add(webSite);
            RefreshData();
            OnWebSiteAdded(new WebSiteAddedEventArgs(webSite));
        }

        private void deleteWebSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    int index = dataGridView1.Rows.IndexOf(r);
                    WebSite webSite = webSites[index];
                    webSites.RemoveAt(index);
                    OnWebSiteDeleted(new WebSiteDeletedEventArgs(webSite));
                }

                RefreshData();
            }
        }
    }
}
