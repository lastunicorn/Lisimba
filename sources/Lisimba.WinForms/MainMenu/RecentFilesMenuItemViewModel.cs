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
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.MainMenu
{
    class RecentFilesMenuItemViewModel : CustomButtonViewModel
    {
        private readonly RecentFiles recentFiles;
        private IOperation childrenOpertion;
        public ObservableCollection<CustomButtonViewModel> Items { get; private set; }

        public IOperation ChildrenOpertion
        {
            get { return childrenOpertion; }
            set
            {
                childrenOpertion = value;
                RepopulateItems();
            }
        }

        public RecentFilesMenuItemViewModel(ApplicationStatus applicationStatus, UserInterface userInterface, RecentFiles recentFiles, IOperation operation)
            : base(applicationStatus, userInterface, operation)
        {
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.recentFiles = recentFiles;

            Items = new ObservableCollection<CustomButtonViewModel>();

            recentFiles.FileNameAdded += HandleRecentFilesFileNameAdded;
        }

        private void RepopulateItems()
        {
            Items.Clear();

            if (ChildrenOpertion == null)
                return;

            AddressBookLocationInfo[] files = recentFiles.GetAllFiles();

            for (int i = 0; i < files.Length; i++)
            {
                RecentFileMenuItemViewModel viewModel = new RecentFileMenuItemViewModel(applicationStatus, userInterface, ChildrenOpertion)
                {
                    File = files[i],
                    Index = i + 1
                };

                Items.Add(viewModel);
            }
        }

        private void HandleRecentFilesFileNameAdded(object sender, EventArgs e)
        {
            RepopulateItems();
        }
    }
}