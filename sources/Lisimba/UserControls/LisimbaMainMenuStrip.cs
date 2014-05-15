﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    partial class LisimbaMainMenuStrip : MenuStrip
    {
        public LisimbaMainMenuStrip()
        {
            InitializeComponent();
        }

        public void Initialize(CommandPool commandPool, StatusService statusService, RecentFilesService recentFilesService)
        {
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (recentFilesService == null) throw new ArgumentNullException("recentFilesService");

            recentFilesService.FileNameAdded += HandleRecentFileNameAdded;

            toolStripMenuItem_File_New.StatusService = statusService;
            toolStripMenuItem_File_New.Command = commandPool.CreateNewAddressBookCommand;

            toolStripMenuItem_File_Open.StatusService = statusService;
            toolStripMenuItem_File_Open.Command = commandPool.OpenAddressBookCommand;

            toolStripMenuItem_File_Save.StatusService = statusService;
            toolStripMenuItem_File_Save.Command = commandPool.SaveAddressBookCommand;

            toolStripMenuItem_File_SaveAs.StatusService = statusService;
            toolStripMenuItem_File_SaveAs.Command = commandPool.SaveAsAddressBookCommand;

            toolStripMenuItem_File_Export.StatusService = statusService;
            toolStripMenuItem_File_Export.ShortDescription = "Export current opened address book in another format.";

            toolStripMenuItem_ExportToYahooCSV.Command = commandPool.ExportYahooCsvCommand;

            toolStripMenuItem_File_Import.StatusService = statusService;
            toolStripMenuItem_File_Import.ShortDescription = "Import address book from another format.";

            toolStripMenuItem_ImportFromYahooCSV.Command = commandPool.ImportYahooCsvCommand;

            toolStripMenuItem_File_Exit.StatusService = statusService;
            toolStripMenuItem_File_Exit.Command = commandPool.ApplicationExitCommand;

            toolStripMenuItem_AddressBook_AddContact.StatusService = statusService;
            toolStripMenuItem_AddressBook_AddContact.Command = commandPool.CreateNewContactCommand;

            toolStripMenuItem_AddressBook_DeleteContact.StatusService = statusService;
            toolStripMenuItem_AddressBook_DeleteContact.Command = commandPool.DeleteCurrentContactCommand;

            toolStripMenuItem_AddressBook_Properties.StatusService = statusService;
            toolStripMenuItem_AddressBook_Properties.Command = commandPool.ShowAddressBookPropertiesCommand;

            toolStripMenuItem_Help_About.StatusService = statusService;
            toolStripMenuItem_Help_About.Command = commandPool.ShowAboutCommand;

            toolStripMenuItem_File_RecentFiles.ChildrenCommand = commandPool.OpenAddressBookCommand;
            toolStripMenuItem_File_RecentFiles.RecentFilesService = recentFilesService;
            toolStripMenuItem_File_RecentFiles.RefreshRecentFilesMenu();
        }

        private void HandleRecentFileNameAdded(object sender, EventArgs e)
        {
            toolStripMenuItem_File_RecentFiles.RefreshRecentFilesMenu();
        }
    }
}
