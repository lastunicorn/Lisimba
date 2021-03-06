﻿// Lisimba
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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.WinFormsCommon;
using DustInTheWind.WinFormsCommon.Controls;
using DustInTheWind.WinFormsCommon.Operations;

namespace DustInTheWind.Lisimba.WinForms.MainMenu
{
    internal class RecentFilesMenuItemViewModel : ListMenuItemViewModel
    {
        private readonly RecentFiles recentFiles;

        public RecentFilesMenuItemViewModel(ApplicationStatus applicationStatus, IOperation operation, RecentFiles recentFiles)
            : base(applicationStatus, operation)
        {
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.recentFiles = recentFiles;

            recentFiles.FileNameAdded += HandleRecentFilesFileNameAdded;
        }

        protected override IEnumerable<CustomButtonViewModel> GetItems()
        {
            return recentFiles.GetAllFiles()
                .Select((x, i) => new RecentFileMenuItemViewModel(applicationStatus, ChildrenOpertion)
                {
                    File = x,
                    Index = i + 1
                });
        }

        private void HandleRecentFilesFileNameAdded(object sender, EventArgs e)
        {
            RepopulateItems();
        }
    }
}