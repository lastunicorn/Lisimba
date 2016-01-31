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
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.Main;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.MainMenu
{
    partial class LisimbaMainMenuStrip : MenuStrip
    {
        public LisimbaMainMenuStrip()
        {
            InitializeComponent();
        }

        public void Initialize(AvailableOperations availableOperations, RecentFiles recentFiles, MainMenusViewModels mainMenusViewModels)
        {
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            toolStripMenuItem_File_New.ViewModel = mainMenusViewModels.NewAddressBookViewModel;
            toolStripMenuItem_File_Open.ViewModel = mainMenusViewModels.OpenAddressBookViewModel;
            toolStripMenuItem_File_Save.ViewModel = mainMenusViewModels.SaveAddressBookViewModel;
            toolStripMenuItem_File_SaveAs.ViewModel = mainMenusViewModels.SaveAsAddressBookViewModel;
            toolStripMenuItem_File_Close.ViewModel = mainMenusViewModels.CloseAddressBookViewModel;
            toolStripMenuItem_File_Export.ViewModel = mainMenusViewModels.ExportViewModel;
            toolStripMenuItem_File_Import.ViewModel = mainMenusViewModels.ImportViewModel;
            toolStripMenuItem_File_Exit.ViewModel = mainMenusViewModels.ApplicationExitViewModel;
            
            toolStripMenuItem_AddressBook_AddContact.ViewModel = mainMenusViewModels.NewContactViewModel;
            toolStripMenuItem_AddressBook_DeleteContact.ViewModel = mainMenusViewModels.DeleteContactViewModel;
            toolStripMenuItem_AddressBook_Properties.ViewModel = mainMenusViewModels.AddressBookPropertiesViewModel;
            
            toolStripMenuItem_Help_About.ViewModel = mainMenusViewModels.AboutViewModel;

            toolStripRecentFilesMenuItem_File_RecentFiles.ViewModel = mainMenusViewModels.RecentFilesViewModel;

            //toolStripRecentFilesMenuItem_File_RecentFiles.ChildrenOpertion = availableOperations.GetOperation<OpenAddressBookOperation>();
            //toolStripRecentFilesMenuItem_File_RecentFiles.RecentFiles = recentFiles;
            //toolStripRecentFilesMenuItem_File_RecentFiles.Items = recentFileItems;
        }
    }
}