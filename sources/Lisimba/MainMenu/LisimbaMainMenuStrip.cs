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
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.MainMenu
{
    partial class LisimbaMainMenuStrip : MenuStrip
    {
        public LisimbaMainMenuStrip()
        {
            InitializeComponent();
        }

        public void Initialize(CommandPool commandPool, ApplicationStatus applicationStatus, RecentFiles recentFiles)
        {
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            recentFiles.FileNameAdded += HandleRecentFileNameAdded;

            toolStripMenuItem_File_New.ViewModel = commandPool.CreateNewAddressBookOperation;
            toolStripMenuItem_File_Open.ViewModel = commandPool.OpenAddressBookOperation;
            toolStripMenuItem_File_Save.ViewModel = commandPool.SaveAddressBookOperation;
            toolStripMenuItem_File_SaveAs.ViewModel = commandPool.SaveAsAddressBookOperation;
            toolStripMenuItem_File_Close.ViewModel = commandPool.CloseAddressBookOperation;
            toolStripMenuItem_File_Export.ShortDescription = "Export current opened address book in another format.";
            //toolStripMenuItem_ExportToYahooCSV.ViewModel = commandPool.ExportYahooCsvOperation;
            toolStripMenuItem_File_Import.ShortDescription = "Import address book from another format.";
            //toolStripMenuItem_ImportFromYahooCSV.ViewModel = commandPool.ImportYahooCsvOperation;
            toolStripMenuItem_File_Exit.ViewModel = commandPool.ApplicationExitOperation;
            toolStripMenuItem_AddressBook_AddContact.ViewModel = commandPool.CreateNewContactOperation;
            toolStripMenuItem_AddressBook_DeleteContact.ViewModel = commandPool.DeleteCurrentContactOperation;
            toolStripMenuItem_AddressBook_Properties.ViewModel = commandPool.ShowAddressBookPropertiesOperation;
            toolStripMenuItem_Help_About.ViewModel = commandPool.ShowAboutOperation;

            toolStripMenuItem_File_RecentFiles.ChildrenOpertion = commandPool.OpenAddressBookOperation;
            toolStripMenuItem_File_RecentFiles.RecentFiles = recentFiles;
            toolStripMenuItem_File_RecentFiles.RefreshRecentFilesMenu();
        }

        private void HandleRecentFileNameAdded(object sender, EventArgs e)
        {
            toolStripMenuItem_File_RecentFiles.RefreshRecentFilesMenu();
        }
    }
}