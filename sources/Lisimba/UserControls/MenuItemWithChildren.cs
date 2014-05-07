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
using DustInTheWind.Lisimba.Config;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    class MenuItemWithChildren : ToolStripMenuItem
    {
        public RecentFilesService RecentFilesService { get; set; }

        #region Event SubItemClicked

        public event EventHandler<SubItemClickedEventArgs> SubItemClicked;

        protected virtual void OnSubItemClicked(SubItemClickedEventArgs eventArgs)
        {
            EventHandler<SubItemClickedEventArgs> handler = SubItemClicked;

            if (handler != null)
                handler(this, eventArgs);
        }

        #endregion

        public void RefreshRecentFilesMenu()
        {
            if (RecentFilesService == null)
                return;

            int j = 0; // index for the list of menu items (this.recentFilesMenuItems)

            //RecentFilesConfigElementCollection recentFiles = configurationService.LisimbaConfigSection.RecentFilesList;
            RecentFilesConfigElementCollection recentFiles = RecentFilesService.GetAllFiles();

            for (int i = 0; i < recentFiles.Count; i++)
            {
                ToolStripMenuItem menuItem;

                if (j < DropDownItems.Count)
                {
                    // If already exists some menu items, reuse them.
                    menuItem = (ToolStripMenuItem)DropDownItems[j];
                }
                else
                {
                    // Create new menu items if necessary.
                    menuItem = new ToolStripMenuItem();
                    menuItem.Click += HandleSubMenuItemClick;

                    DropDownItems.Add(menuItem);
                }

                // Set the values of the menu item.
                menuItem.Tag = recentFiles[i].FileName;
                menuItem.Text = string.Format("{0} {1}", i, recentFiles[i].FileName);
                j++;
            }

            // Remove the unused menu items, if any.
            for (int i = j; i < DropDownItems.Count; i++)
            {
                DropDownItems.RemoveAt(i);
            }

            // Enable/Disable the recent files menu.
            Enabled = (DropDownItems.Count != 0);
        }

        private void HandleSubMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            OnSubItemClicked(new SubItemClickedEventArgs(menuItem));
        }
    }
}
