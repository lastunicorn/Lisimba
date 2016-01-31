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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.Main;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.MainMenu
{
    partial class LisimbaMainMenuStrip : MenuStrip
    {
        public LisimbaMainMenuStrip()
        {
            InitializeComponent();
        }

        public void Initialize(AvailableOperations availableOperations, RecentFiles recentFiles, MenuItemViewModels menuItemViewModels)
        {
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            toolStripMenuItem_File_New.ViewModel = menuItemViewModels.NewAddressBookViewModel;
            toolStripMenuItem_File_Open.ViewModel = menuItemViewModels.OpenAddressBookViewModel;
            toolStripMenuItem_File_Save.ViewModel = menuItemViewModels.SaveAddressBookViewModel;
            toolStripMenuItem_File_SaveAs.ViewModel = menuItemViewModels.SaveAsAddressBookViewModel;
            toolStripMenuItem_File_Close.ViewModel = menuItemViewModels.CloseAddressBookViewModel;
            toolStripMenuItem_File_Export.ViewModel = menuItemViewModels.ExportViewModel;
            toolStripMenuItem_File_Import.ViewModel = menuItemViewModels.ImportViewModel;
            toolStripMenuItem_File_Exit.ViewModel = menuItemViewModels.ApplicationExitViewModel;
            
            toolStripMenuItem_AddressBook_AddContact.ViewModel = menuItemViewModels.NewContactViewModel;
            toolStripMenuItem_AddressBook_DeleteContact.ViewModel = menuItemViewModels.DeleteContactViewModel;
            toolStripMenuItem_AddressBook_Properties.ViewModel = menuItemViewModels.AddressBookPropertiesViewModel;
            
            toolStripMenuItem_Help_About.ViewModel = menuItemViewModels.AboutViewModel;

            toolStripRecentFilesMenuItem_File_RecentFiles.ViewModel = menuItemViewModels.RecentFilesViewModel;

            //toolStripRecentFilesMenuItem_File_RecentFiles.ChildrenOpertion = availableOperations.GetOperation<OpenAddressBookOperation>();
            //toolStripRecentFilesMenuItem_File_RecentFiles.RecentFiles = recentFiles;
            //toolStripRecentFilesMenuItem_File_RecentFiles.Items = recentFileItems;
        }
    }
}