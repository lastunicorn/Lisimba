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
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.UserControls;

namespace DustInTheWind.Lisimba.Forms
{
    internal partial class FormLisimba : Form
    {
        private readonly string programTitle = string.Empty;
        private readonly string fileNameToOpenAtLoad = string.Empty;

        private readonly StatusService statusService;
        private readonly CurrentData currentData;
        private readonly CommandPool commandPool;
        private readonly ProgramArguments programArguments;
        private readonly ConfigurationService configurationService;
        private readonly RecentFilesService recentFilesService;

        // Lisimba - male name meaning "lion" in Zulu language.

        public FormLisimba(ProgramArguments programArguments, ConfigurationService configurationService, StatusService statusService,
            RecentFilesService recentFilesService, CurrentData currentData, CommandPool commandPool)
        {
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (recentFilesService == null) throw new ArgumentNullException("recentFilesService");
            if (currentData == null) throw new ArgumentNullException("currentData");
            if (commandPool == null) throw new ArgumentNullException("commandPool");

            this.programArguments = programArguments;
            this.configurationService = configurationService;

            this.statusService = statusService;
            statusService.StatusTextChanged += HandleStatusTextChanged;

            this.recentFilesService = recentFilesService;
            recentFilesService.FileNameAdded += HandleRecentFilesServiceOnFileNameAdded;

            this.currentData = currentData;
            currentData.AddressBookContentChanged += HandleCurrentAddressBookContentChanged;
            currentData.ContactChanged += HandleCurrentContactChanged;
            currentData.AskToOpenLsbFile = AskToOpenLsbFile;
            currentData.AskToSaveLsbFile = AskToSaveLsbFile;
            currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
            currentData.AddressBookSaved += HandleCurrentAddressBookSaved;
            currentData.AskToOpenYahooCsvFile = AskToOpenYahooCsvFile;
            currentData.AskToSaveYahooCsvFile = AskToSaveYahooCsvFile;

            this.commandPool = commandPool;

            InitializeComponent();

            contactView1.Presenter.ContactChanged += contactView1_ContactChanged;

            contactListView1.CurrentData = currentData;
            contactListView1.CommandPool = commandPool;
            contactListView1.StatusService = statusService;
            contactListView1.ConfigurationService = configurationService;

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            programTitle = string.Format("{0} {1}", Application.ProductName, version.ToString(2));

            toolStripMenuItem_File_New.StatusService = statusService;
            toolStripMenuItem_File_New.Command = commandPool.CreateNewAddressBookCommand;

            toolStripMenuItem_File_Open.StatusService = statusService;
            toolStripMenuItem_File_Open.ShortDescription = "Open address book from file.";

            toolStripMenuItem_File_Save.StatusService = statusService;
            toolStripMenuItem_File_Save.Command = commandPool.SaveAddressBookCommand;

            toolStripMenuItem_File_SaveAs.StatusService = statusService;
            toolStripMenuItem_File_SaveAs.Command = commandPool.SaveAsAddressBookCommand;

            toolStripMenuItem_File_Export.StatusService = statusService;
            toolStripMenuItem_File_Export.ShortDescription = "Export current opened address book in another format.";

            toolStripMenuItem_ExportToYahooCSV.Command = commandPool.ExportYahooCsvCommand;

            toolStripMenuItem_File_Import.StatusService = statusService;
            toolStripMenuItem_File_Import.ShortDescription = "Import address book from another format.";

            toolStripMenuItem_File_Exit.StatusService = statusService;
            toolStripMenuItem_File_Exit.ShortDescription = "Exit the program.";

            toolStripMenuItem_Agenda_AddContact.StatusService = statusService;
            toolStripMenuItem_Agenda_AddContact.Command = commandPool.CreateNewContactCommand;

            toolStripMenuItem_Agenda_DeleteContact.StatusService = statusService;
            toolStripMenuItem_Agenda_DeleteContact.Command = commandPool.DeleteCurrentContactCommand;

            toolStripMenuItem_Agenda_Properties.StatusService = statusService;
            toolStripMenuItem_Agenda_Properties.Command = commandPool.ShowAddressBookPropertiesCommand;

            toolStripMenuItem_Help_About.StatusService = statusService;
            toolStripMenuItem_Help_About.Command = commandPool.ShowAboutCommand;

            toolStripMenuItem_File_RecentFiles.SubItemClicked += HandleRecentMenuItemClick;
            toolStripMenuItem_File_RecentFiles.RecentFilesService = recentFilesService;
            toolStripMenuItem_File_RecentFiles.RefreshRecentFilesMenu();

            commandPool.CreateNewAddressBookCommand.Execute();

            fileNameToOpenAtLoad = CalculateFileNameToInitiallyOpen();
        }

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            contactView1.Presenter.Contact = currentData.Contact;
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs eventArgs)
        {
            RefreshFormTitle();
        }

        private string CalculateFileNameToInitiallyOpen()
        {
            if (!string.IsNullOrEmpty(programArguments.FileName))
                return programArguments.FileName;

            switch (configurationService.LisimbaConfigSection.LoadFileAtStart.Type)
            {
                case "new":
                    return null;

                case "last":
                    return recentFilesService.GetMostRecentFileName();

                case "specified":
                    return configurationService.LisimbaConfigSection.LoadFileAtStart.FileName;

                default:
                    return null;
            }
        }

        private void HandleRecentMenuItemClick(object sender, SubItemClickedEventArgs e)
        {
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                return;

            string fileName = e.MenuItem.Tag.ToString();

            commandPool.OpenAddressBookCommand.Execute(fileName);
        }

        private string AskToSaveYahooCsvFile()
        {
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "Csv Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "csv";
            saveFileDialog1.FileName = string.Empty;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return null;

            return saveFileDialog1.FileName;
        }

        private string AskToOpenYahooCsvFile()
        {
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "Csv Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.FileName = string.Empty;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return null;

            return openFileDialog1.FileName;
        }

        private string AskToSaveLsbFile()
        {
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "Lis Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
            saveFileDialog1.DefaultExt = "lsb";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return null;

            return saveFileDialog1.FileName;
        }

        private void HandleCurrentAddressBookSaved(object sender, EventArgs e)
        {
            RefreshFormTitle();
        }

        private void HandleCurrentAddressBookChanged(object sender, EventArgs e)
        {
            RefreshFormTitle();
        }

        private void HandleRecentFilesServiceOnFileNameAdded(object sender, EventArgs e)
        {
            toolStripMenuItem_File_RecentFiles.RefreshRecentFilesMenu();
        }

        private string AskToOpenLsbFile()
        {
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "Lis Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "lsb";

            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult != DialogResult.OK)
                return null;

            return openFileDialog1.FileName;
        }

        private void HandleStatusTextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusService.StatusText;
        }

        #region Other Methods

        private bool AskToSave()
        {
            if (!currentData.IsModified)
                return true;

            DialogResult dialogResult = MessageBox.Show("Current address book is not saved.\nDo you wanna save it before proceedeing?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return false;

            if (dialogResult == DialogResult.Yes)
                commandPool.SaveAddressBookCommand.Execute();

            return true;
        }

        private void RefreshFormTitle()
        {
            string text = string.Empty;

            // Book name or file path
            if (!currentData.IsNew || currentData.IsModified)
            {
                if (currentData.AddressBook.Name.Length == 0)
                {
                    if (currentData.AddressBook.FileName.Length == 0)
                        text += "< Unnamed >";
                    else
                        text += currentData.AddressBook.FileName;
                }
                else
                {
                    text += currentData.AddressBook.Name;
                }
            }

            // Is modified (*)
            if (currentData.IsModified)
                text += " *";

            // -
            if (text.Length > 0)
                text += " - ";

            // Progeam title
            text += programTitle;

            // Display the title
            Text = text;
        }

        #endregion

        void contactView1_ContactChanged(object sender, EventArgs e)
        {
            if (!currentData.IsModified)
            {
                currentData.IsModified = true;
                RefreshFormTitle();
            }

            contactListView1.SetContactChangedFlag(contactView1.Presenter.Contact, true);

        }

        #region Menu OnClick - File Menu

        private void toolStripMenuItem_File_Open_Click(object sender, EventArgs e)
        {
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                return;

            commandPool.OpenAddressBookCommand.Execute(string.Empty);
        }

        private void toolStripMenuItem_ImportFromYahooCSV_Click(object sender, EventArgs e)
        {
            // Ask to save because temporarlly the import is done only in a new address book.
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                return;

            commandPool.ImportYahooCsvCommand.Execute();
        }

        private void toolStripMenuItem_File_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Form

        private void FormLisimba_Shown(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusService.StatusText;

            if (!string.IsNullOrWhiteSpace(fileNameToOpenAtLoad))
                commandPool.OpenAddressBookCommand.Execute(fileNameToOpenAtLoad);
        }

        private void FormLisimba_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                e.Cancel = true;
        }

        #endregion
    }
}