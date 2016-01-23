// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.MainMenu
{
    internal class MenuItemWithChildren : ToolStripMenuItem
    {
        public RecentFiles RecentFiles { get; set; }

        public IExecutableViewModel ChildrenOpertion { get; set; }

        public event EventHandler<SubItemClickedEventArgs> SubItemClicked;

        protected virtual void OnSubItemClicked(SubItemClickedEventArgs eventArgs)
        {
            EventHandler<SubItemClickedEventArgs> handler = SubItemClicked;

            if (handler != null)
                handler(this, eventArgs);
        }

        public void RefreshRecentFilesMenu()
        {
            if (RecentFiles == null)
                return;

            int j = 0; // index for the list of menu items (this.recentFilesMenuItems)

            AddressBookLocationInfo[] recentFiles = RecentFiles.GetAllFiles();

            for (int i = 0; i < recentFiles.Length; i++)
            {
                CommandedMenuItem menuItem;

                if (j < DropDownItems.Count)
                {
                    // If already exists some menu items, reuse them.
                    menuItem = (CommandedMenuItem) DropDownItems[j];
                }
                else
                {
                    // Create new menu items if necessary.
                    menuItem = new CommandedMenuItem();
                    menuItem.Click += HandleSubMenuItemClick;
                    menuItem.ViewModel = ChildrenOpertion;
                    menuItem.CommandParameterProvider = () => menuItem.Tag.ToString();


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
            ToolStripMenuItem menuItem = (ToolStripMenuItem) sender;
            OnSubItemClicked(new SubItemClickedEventArgs(menuItem));
        }
    }
}