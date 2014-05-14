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
using DustInTheWind.Lisimba.Commands;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.UserControls;

namespace DustInTheWind.Lisimba.Forms
{
    internal partial class FormLisimba : Form
    {
        private string programTitle = string.Empty;
        private string fileNameToOpenAtLoad = string.Empty;

        private readonly StatusService statusService;
        private readonly CurrentData currentData;
        private readonly UIService uiService;

        private readonly CreateNewAddressBookCommand createNewAddressBookCommand;
        private readonly SaveAddressBookCommand saveAddressBookCommand;
        private readonly SaveAsAddressBookCommand saveAsAddressBookCommand;
        private readonly OpenAddressBookCommand openAddressBookCommand;
        private readonly ImportYahooCsvCommand importYahooCsvCommand;
        private readonly ExportYahooCsvCommand exportYahooCsvCommand;
        private readonly ShowAboutCommand showAboutCommand;
        private readonly ShowAddressBookPropertiesCommand showAddressBookPropertiesCommand;

        // Lisimba - male name meaning "lion" in Zulu language.

        public FormLisimba(ProgramArguments programArguments, ConfigurationService configurationService, StatusService statusService,
            RecentFilesService recentFilesService, CurrentData currentData)
        {
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (recentFilesService == null) throw new ArgumentNullException("recentFilesService");
            if (currentData == null) throw new ArgumentNullException("currentData");

            this.statusService = statusService;
            statusService.DefaultStatusText = "Ready";
            statusService.StatusTextChanged += HandleStatusTextChanged;

            recentFilesService.FileNameAdded += HandleRecentFilesServiceOnFileNameAdded;

            this.currentData = currentData;
            currentData.AddressBookContentChanged += HandleCurrentAddressBookContentChanged;
            currentData.AskToOpenLsbFile = AskToOpenLsbFile;
            currentData.AskToSaveLsbFile = AskToSaveLsbFile;
            currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
            currentData.AddressBookSaved += HandleCurrentAddressBookSaved;
            currentData.AskToOpenYahooCsvFile = AskToOpenYahooCsvFile;
            currentData.AskToSaveYahooCsvFile = AskToSaveYahooCsvFile;

            uiService = new UIService(this);

            createNewAddressBookCommand = new CreateNewAddressBookCommand(currentData, uiService);
            openAddressBookCommand = new OpenAddressBookCommand(currentData, uiService);
            saveAddressBookCommand = new SaveAddressBookCommand(currentData, uiService);
            saveAsAddressBookCommand = new SaveAsAddressBookCommand(currentData, uiService);
            importYahooCsvCommand = new ImportYahooCsvCommand(currentData, uiService);
            exportYahooCsvCommand = new ExportYahooCsvCommand(currentData, uiService);
            showAboutCommand = new ShowAboutCommand(currentData, uiService);
            showAddressBookPropertiesCommand = new ShowAddressBookPropertiesCommand(currentData, uiService);

            InitializeComponent();

            contactView1.Presenter.ContactChanged += contactView1_ContactChanged;

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            programTitle = string.Format("{0} {1}", Application.ProductName, version.ToString(2));

            toolStripMenuItem_File_New.StatusService = statusService;
            toolStripMenuItem_File_New.Command = createNewAddressBookCommand;

            toolStripMenuItem_File_Open.StatusService = statusService;
            toolStripMenuItem_File_Open.ShortDescription = "Open address book from file.";

            toolStripMenuItem_File_Save.StatusService = statusService;
            toolStripMenuItem_File_Save.Command = saveAddressBookCommand;

            toolStripMenuItem_File_SaveAs.StatusService = statusService;
            toolStripMenuItem_File_SaveAs.Command = saveAsAddressBookCommand;

            toolStripMenuItem_File_Export.StatusService = statusService;
            toolStripMenuItem_File_Export.ShortDescription = "Export current opened address book in another format.";

            toolStripMenuItem_ExportToYahooCSV.Command = exportYahooCsvCommand;

            toolStripMenuItem_File_Import.StatusService = statusService;
            toolStripMenuItem_File_Import.ShortDescription = "Import address book from another format.";

            toolStripMenuItem_File_Exit.StatusService = statusService;
            toolStripMenuItem_File_Exit.ShortDescription = "Exit the program.";

            toolStripMenuItem_Agenda_AddContact.StatusService = statusService;
            toolStripMenuItem_Agenda_AddContact.ShortDescription = "Add a new contact.";

            toolStripMenuItem_Agenda_DeleteContact.StatusService = statusService;
            toolStripMenuItem_Agenda_DeleteContact.ShortDescription = "Delete the selected contact.";

            toolStripMenuItem_Agenda_Properties.StatusService = statusService;
            toolStripMenuItem_Agenda_Properties.Command = showAddressBookPropertiesCommand;

            toolStripMenuItem_Help_About.StatusService = statusService;
            toolStripMenuItem_Help_About.Command = showAboutCommand;

            toolStripMenuItem_File_RecentFiles.SubItemClicked += HandleRecentMenuItemClick;
            toolStripMenuItem_File_RecentFiles.RecentFilesService = recentFilesService;
            toolStripMenuItem_File_RecentFiles.RefreshRecentFilesMenu();

            currentData.New();

            CalculateFileNameToInitiallyOpen(programArguments, configurationService, recentFilesService);

            RefreshSortMethod(configurationService);
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs eventArgs)
        {
            RefreshFormTitle();
        }

        private void CalculateFileNameToInitiallyOpen(ProgramArguments programArguments,
            ConfigurationService configurationService, RecentFilesService recentFilesService)
        {
            if (!string.IsNullOrEmpty(programArguments.FileName))
            {
                fileNameToOpenAtLoad = programArguments.FileName;
            }
            else
            {
                switch (configurationService.LisimbaConfigSection.LoadFileAtStart.Type)
                {
                    case "new":
                        break;

                    case "last":
                        fileNameToOpenAtLoad = recentFilesService.GetMostRecentFileName();
                        break;

                    case "specified":
                        fileNameToOpenAtLoad = configurationService.LisimbaConfigSection.LoadFileAtStart.FileName;
                        break;
                }
            }
        }

        private void RefreshSortMethod(ConfigurationService configurationService)
        {
            switch (configurationService.LisimbaConfigSection.SortBy.Value)
            {
                case "Birthday":
                    contactListView1.SortField = ContactsSortingType.Birthday;
                    break;

                case "BirthDate":
                    contactListView1.SortField = ContactsSortingType.BirthDate;
                    break;

                case "FirstName":
                    contactListView1.SortField = ContactsSortingType.FirstName;
                    break;

                case "LastName":
                    contactListView1.SortField = ContactsSortingType.LastName;
                    break;

                case "Nickname":
                    contactListView1.SortField = ContactsSortingType.Nickname;
                    break;

                case "NicknameOrName":
                    contactListView1.SortField = ContactsSortingType.NicknameOrName;
                    break;
            }
        }

        private void HandleRecentMenuItemClick(object sender, SubItemClickedEventArgs e)
        {
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                return;

            string fileName = e.MenuItem.Tag.ToString();

            openAddressBookCommand.Execute(fileName);
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
            contactListView1.ResetModifiedFlags();

            RefreshFormTitle();
        }

        private void HandleCurrentAddressBookChanged(object sender, EventArgs e)
        {
            // Populate the list control
            contactListView1.Contacts = currentData.AddressBook.Contacts;

            // Disable the contact view control
            contactView1.Contact = null;
            contactView1.Enabled = false;

            // Clear the "Find" textbox.
            contactListView1.SearchText = string.Empty;

            // Refresh the form title
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
                saveAddressBookCommand.Execute();

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

        #region ContactListView

        void contactListView1_SelectedContactChanged(object sender, ContactListView.SelectedContactChangedEventArgs e)
        {
            contactView1.Contact = e.SelectedContact;
            contactView1.Enabled = (e.SelectedContact != null);
        }

        #endregion

        #region ContactView

        void contactView1_ContactChanged(object sender, EventArgs e)
        {
            if (!currentData.IsModified)
            {
                currentData.IsModified = true;
                RefreshFormTitle();
            }

            contactListView1.SetContactChangedFlag(contactView1.Contact, true);

        }

        #endregion

        #region Menu OnClick - File Menu

        private void toolStripMenuItem_File_Open_Click(object sender, EventArgs e)
        {
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                return;

            openAddressBookCommand.Execute(string.Empty);
        }

        private void toolStripMenuItem_ImportFromYahooCSV_Click(object sender, EventArgs e)
        {
            // Ask to save because temporarlly the import is done only in a new address book.
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                return;

            importYahooCsvCommand.Execute();
        }

        private void toolStripMenuItem_File_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Menu OnClick - Address Book Menu

        private void toolStripMenuItem_Agenda_AddContact_Click(object sender, EventArgs e)
        {
            FormAddContact formAddContact = new FormAddContact(currentData.AddressBook.Contacts);

            if (formAddContact.ShowDialog() == DialogResult.OK)
            {
                contactListView1.Add(formAddContact.Contact);

                currentData.IsModified = true;
                RefreshFormTitle();
            }
        }

        private void toolStripMenuItem_Agenda_DeleteContact_Click(object sender, EventArgs e)
        {
            Contact contact = contactListView1.SelectedContact;

            if (contact == null)
                return;

            string text = string.Format("Are you sure you wanna delete the contact {0} ?", contact);
            DialogResult dialogResult = MessageBox.Show(text, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Yes)
                contactListView1.RemoveContact(contact);
        }

        private void toolStripMenuItem_Agenda_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItem_Agenda_DeleteContact.Enabled = (contactListView1.SelectedContact != null);
        }

        #endregion

        #region Form

        private void FormLisimba_Shown(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusService.StatusText;

            if (!string.IsNullOrWhiteSpace(fileNameToOpenAtLoad))
                openAddressBookCommand.Execute(fileNameToOpenAtLoad);
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