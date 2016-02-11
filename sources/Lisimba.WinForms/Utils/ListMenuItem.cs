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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Utils
{
    internal partial class ListMenuItem : ToolStripMenuItem
    {
        private ListMenuItemViewModel viewModel;

        public ListMenuItemViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != null)
                    viewModel.Items.CollectionChanged -= HandleRecentFileNameAdded;

                viewModel = value;

                if (viewModel != null)
                    viewModel.Items.CollectionChanged += HandleRecentFileNameAdded;

                RefreshRecentFilesMenu();
            }
        }

        public ListMenuItem()
        {
            InitializeComponent();
        }

        public ListMenuItem(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void HandleRecentFileNameAdded(object sender, EventArgs e)
        {
            RefreshRecentFilesMenu();
        }

        private void RefreshRecentFilesMenu()
        {
            if (viewModel == null)
                return;

            ObservableCollection<CustomButtonViewModel> items = viewModel.Items;

            while (DropDownItems.Count < items.Count)
                AddNewMenuItem();

            while (DropDownItems.Count > items.Count)
                RemoveLastMenuItem();

            for (int i = 0; i < items.Count; i++)
                UpdateMenuItem(i, items[i]);

            Enabled = DropDownItems.Count != 0;
        }

        private void AddNewMenuItem()
        {
            CustomMenuItem menuItem = new CustomMenuItem();
            DropDownItems.Add(menuItem);
        }

        private void RemoveLastMenuItem()
        {
            DropDownItems.RemoveAt(DropDownItems.Count - 1);
        }

        private void UpdateMenuItem(int i, CustomButtonViewModel item)
        {
            CustomMenuItem menuItem = (CustomMenuItem)DropDownItems[i];
            menuItem.ViewModel = item;
        }

        private void HandleClick(object sender, EventArgs e)
        {
            if (ViewModel == null)
                return;

            ViewModel.Execute();
        }

        private void HandleMouseEnter(object sender, EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.MouseEnter();
        }

        private void HandleMouseLeave(object sender, EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.MouseLeave();
        }
    }
}