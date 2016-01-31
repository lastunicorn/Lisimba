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
using DustInTheWind.Lisimba.Utils;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.MainMenu
{
    internal partial class RecentFilesMenuItem : ToolStripMenuItem
    {
        private RecentFilesMenuItemViewModel viewModel;

        public RecentFilesMenuItemViewModel ViewModel
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

        public RecentFilesMenuItem()
        {
            InitializeComponent();
        }

        public RecentFilesMenuItem(IContainer container)
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

//// Lisimba
//// Copyright (C) 2007-2016 Dust in the Wind
//// 
//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

//using System;
//using System.Collections.ObjectModel;
//using System.Collections.Specialized;
//using System.Windows.Forms;
//using DustInTheWind.Lisimba.Business.RecentFilesManagement;
//using DustInTheWind.Lisimba.Operations;
//using DustInTheWind.Lisimba.Services;
//using DustInTheWind.Lisimba.Utils;

//namespace DustInTheWind.Lisimba.MainMenu
//{
//    class RecentFilesMenuItemViewModel : CustomMenuItemViewModel
//    {
//        public ObservableCollection<CustomMenuItemViewModel> Items { get; private set; }

//        public RecentFilesMenuItemViewModel(ApplicationStatus applicationStatus, UserInterface userInterface)
//            : base(applicationStatus, userInterface, new EmptyOperation())
//        {
//            Items = new ObservableCollection<CustomMenuItemViewModel>();
//        }
//    }

//    internal class RecentFilesMenuItem : ToolStripMenuItem
//    {
//        private RecentFiles recentFiles;

//        public RecentFiles RecentFiles
//        {
//            get { return recentFiles; }
//            set
//            {
//                if (recentFiles != null)
//                    recentFiles.FileNameAdded -= HandleRecentFileNameAdded;

//                recentFiles = value;

//                if (recentFiles != null)
//                    recentFiles.FileNameAdded += HandleRecentFileNameAdded;

//                RefreshRecentFilesMenu();
//            }
//        }

//        public IExecutableViewModel ChildrenOpertion { get; set; }

//        private void HandleRecentFileNameAdded(object sender, EventArgs e)
//        {
//            RefreshRecentFilesMenu();
//        }

//        private void RefreshRecentFilesMenu()
//        {
//            if (RecentFiles == null)
//                return;

//            AddressBookLocationInfo[] recentFiles = RecentFiles.GetAllFiles();

//            while (DropDownItems.Count < recentFiles.Length)
//                AddNewMenuItem();

//            while (DropDownItems.Count > recentFiles.Length)
//                RemoveLastMenuItem();

//            for (int i = 0; i < recentFiles.Length; i++)
//                UpdateMenuItem(i, recentFiles[i]);

//            Enabled = DropDownItems.Count != 0;
//        }

//        private void AddNewMenuItem()
//        {
//            CommandedMenuItem menuItem = new CommandedMenuItem();

//            menuItem.ViewModel = ChildrenOpertion;
//            menuItem.CommandParameterProvider = () => menuItem.Tag.ToString();

//            DropDownItems.Add(menuItem);
//        }

//        private void RemoveLastMenuItem()
//        {
//            DropDownItems.RemoveAt(DropDownItems.Count - 1);
//        }

//        private void UpdateMenuItem(int i, AddressBookLocationInfo recentFile)
//        {
//            CommandedMenuItem menuItem = (CommandedMenuItem)DropDownItems[i];

//            menuItem.Tag = recentFile.FileName;
//            menuItem.Text = string.Format("{0} {1}", i, recentFile.FileName);
//        }
//    }
//}