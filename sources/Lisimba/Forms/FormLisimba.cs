// Lisimba
// Copyright (C) 2014 Dust in the Wind
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

using DustInTheWind.Lisimba.Commands;
using DustInTheWind.Lisimba.Config;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Egg.Exceptions;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.UserControls;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormLisimba : Form
    {
        //private AddressBook addressBook = null;
        private bool isModified = false;
        private string statusText = string.Empty;
        private string programTitle = string.Empty;

        private string fileNameToOpenAtLoad = string.Empty;

        private readonly StatusService statusService;
        private readonly ConfigurationService configurationService;
        private readonly CurrentAddressBook currentAddressBook;
        private readonly RecentFilesService recentFilesService;

        // Lisimba - male name meaning "lion" in Zulu language.

        public FormLisimba()
            : this(string.Empty)
        {
        }

        public FormLisimba(string fileName)
        {
            configurationService = new ConfigurationService();

            statusService = new StatusService();
            statusService.StatusTextChanged += HandleStatusServiceOnStatusTextChanged;

            recentFilesService = new RecentFilesService(configurationService);
            recentFilesService.FileNameAdded += HandleRecentFilesServiceOnFileNameAdded;

            currentAddressBook = new CurrentAddressBook(statusService, recentFilesService);
            currentAddressBook.AskToOpenLsbFile = AskToOpenLsbFile;
            currentAddressBook.AskToSaveLsbFile = AskToSaveLsbFile;
            currentAddressBook.AddressBookChanged += HandleCurrentAddressBookOnAddressBookChanged;
            currentAddressBook.AddressBookSaved += HandleCurrentAddressBookOnAddressBookSaved;
            currentAddressBook.AskToOpenYahooCsvFile = AskToOpenYahooCsvFile;
            currentAddressBook.AskToSaveYahooCsvFile = AskToSaveYahooCsvFile;
            currentAddressBook.HandleIncorrectXmlVersion = addressBookLoader_IncorrectXmlVersion;

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            programTitle = Application.ProductName + " " + version.Major + "." + version.Minor;

            InitializeComponent();

            RefreshRecentFilesMenu();

            BookNew();

            if (!string.IsNullOrEmpty(fileName))
            {
                fileNameToOpenAtLoad = fileName;
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

            toolStripStatusLabel1.Text = statusText;

            // Set the initial sort method
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

            //// todo: for test
            //Contact c = new Contact();
            //c.Name = new PersonName("Alex", "Nicolae", "Iuga", "Alez");
            //c.Phones.Add(new Phone("123456789", "Test"));

            //this.ctrl1.Value = c;
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

        private void HandleCurrentAddressBookOnAddressBookSaved(object sender, EventArgs e)
        {
            contactListView1.ResetModifiedFlags();

            RefreshFormTitle();
        }

        private void HandleCurrentAddressBookOnAddressBookChanged(object sender, EventArgs e)
        {
            // Populate the list control
            contactListView1.Contacts = currentAddressBook.AddressBook.Contacts;

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
            RefreshRecentFilesMenu();
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

        private void HandleStatusServiceOnStatusTextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusService.StatusText;
        }

        void addressBookLoader_IncorrectXmlVersion(object sender, IncorrectXmlVersionEventArgs e)
        {
            string text = e.XmlVersion == null
                ? string.Format("The version of the file \"{0}\" could not be determined.\n\nDo you still wanna try to open the file?", e.FileName)
                : string.Format("The file \"{0}\" is created with another version of the Egg.\n\nCurrent Egg version = {1}.{2}\nFile created by Egg version = {3}.{4}\n\nDo you still wanna try to open the file?", e.FileName, e.EggVersion.Major, e.EggVersion.Minor, e.XmlVersion.Major, e.XmlVersion.Minor);

            DialogResult dialogResult = MessageBox.Show(this, text, "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Yes)
                e.ContinueParsing = true;
        }

        #region Recent Files

        private void RefreshRecentFilesMenu()
        {
            ToolStripMenuItem menuItem = null;
            ToolStripItemCollection menuItems = toolStripMenuItem_File_RecentFiles.DropDownItems;
            int j = 0; // index for the list of menu items (this.recentFilesMenuItems)

            //RecentFilesConfigElementCollection recentFiles = configurationService.LisimbaConfigSection.RecentFilesList;
            RecentFilesConfigElementCollection recentFiles = recentFilesService.GetAllFiles();

            for (int i = 0; i < recentFiles.Count; i++)
            {
                if (j < menuItems.Count)
                {
                    // If already exists some menu items, reuse them.
                    menuItem = (ToolStripMenuItem)menuItems[j];
                }
                else
                {
                    // Create new menu items if necessary.
                    menuItem = new ToolStripMenuItem();
                    menuItem.Click += new EventHandler(toolStripMenuItem_RecentFile_Click);
                    menuItems.Add(menuItem);
                }

                // Set the values of the menu item.
                menuItem.Tag = recentFiles[i].FileName;
                menuItem.Text = i + " " + recentFiles[i].FileName;
                j++;
            }

            // Remove the unused menu items, if any.
            for (int i = j; i < menuItems.Count; i++)
            {
                menuItems.RemoveAt(i);
            }

            // Enable/Desable the recent files menu.
            toolStripMenuItem_File_RecentFiles.Enabled = (menuItems.Count != 0);
        }

        private void toolStripMenuItem_RecentFile_Click(object sender, EventArgs e)
        {
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                return;

            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            string fileName = menuItem.Tag.ToString();

            BookOpen(fileName);
        }

        #endregion

        #region Other Methods

        private bool AskToSave()
        {
            if (!currentAddressBook.IsModified)
                return true;

            DialogResult dialogResult = MessageBox.Show("Current address book is not saved.\nDo you wanna save it before proceedeing?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return false;

            if (dialogResult == DialogResult.Yes)
                BookSave();

            return true;
        }

        private void RefreshFormTitle()
        {
            string text = string.Empty;

            // Book name or file path
            if (!currentAddressBook.IsNew || currentAddressBook.IsModified)
            {
                if (currentAddressBook.AddressBook.Name.Length == 0)
                {
                    if (currentAddressBook.AddressBook.FileName.Length == 0)
                        text += "< Unnamed >";
                    else
                        text += currentAddressBook.AddressBook.FileName;
                }
                else
                {
                    text += currentAddressBook.AddressBook.Name;
                }
            }

            // Is modified (*)
            if (currentAddressBook.IsModified)
                text += " *";

            // -
            if (text.Length > 0)
                text += " - ";

            // Progeam title
            text += programTitle;

            // Display the title
            Text = text;
        }

        //private bool CheckLsbVersion(string fileName)
        //{
        //    Version eggVersion = Assembly.GetExecutingAssembly().GetName().Version;
        //    Version xmlVersion = AddressBook.ReadLsbVersion(fileName);

        //    if (xmlVersion == null)
        //    {
        //        DialogResult r = MessageBox.Show(this, "The version of the file \"" + fileName + "\" could not be determined.\n\nDo you still wanna try to open the file?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //        if (r == DialogResult.No)
        //        {
        //            return false;
        //        }
        //        else if (r == DialogResult.Yes)
        //        {
        //            return true;
        //        }
        //    }

        //    int verCmp = 0;
        //    verCmp = eggVersion.Major.CompareTo(xmlVersion.Major);
        //    if (verCmp == 0)
        //        verCmp = eggVersion.Minor.CompareTo(xmlVersion.Minor);
        //    if (verCmp != 0)
        //    {
        //        if (MessageBox.Show(this, "The file \"" + fileName + "\" is created with another version of the Egg.\n\nCurrent Egg version = " + eggVersion.Major + "." + eggVersion.Minor + "\nFile created by Egg version = " + xmlVersion.Major + "." + xmlVersion.Minor + "\n\nDo you still wanna try to open the file?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        #endregion

        #region ContactListView

        void contactListView1_ContactListChanged(object sender, ContactListView.ContactListChangedEventArgs e)
        {
            currentAddressBook.IsModified = true;

            // Refresh form title
            RefreshFormTitle();
        }

        void contactListView1_SelectedContactChanged(object sender, ContactListView.SelectedContactChangedEventArgs e)
        {
            contactView1.Contact = e.SelectedContact;
            contactView1.Enabled = (e.SelectedContact != null);
        }

        #endregion

        #region ContactView

        void contactView1_ContactChanged(object sender, ContactView.ContactChangedEventArgs e)
        {
            if (!currentAddressBook.IsModified)
            {
                currentAddressBook.IsModified = true;

                // Refresh form title
                RefreshFormTitle();
            }

            contactListView1.SetContactChangedFlag(contactView1.Contact, true);

        }

        #endregion

        #region New Open Save Close

        private void BookNew()
        {
            currentAddressBook.New();
        }

        private void BookOpen()
        {
            BookOpen(string.Empty);
        }

        private void BookOpen(string fileName)
        {
            try
            {
                currentAddressBook.Open(fileName);
            }
            catch (EggIncorrectVersionException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BookSave()
        {
            try
            {
                currentAddressBook.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BookSaveAs()
        {
            try
            {
                currentAddressBook.SaveAs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Import Export

        private void BookImportFromYahooCsv()
        {
            try
            {
                currentAddressBook.ImportFromYahooCsv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BookExportToYahooCsv()
        {
            try
            {
                currentAddressBook.ExportToYahooCsv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Menu OnClick - File Menu

        private void toolStripMenuItem_File_New_Click(object sender, EventArgs e)
        {
            if (AskToSave())
            {
                BookNew();
            }
        }

        private void toolStripMenuItem_File_Open_Click(object sender, EventArgs e)
        {
            if (AskToSave())
            {
                BookOpen();
            }
        }

        private void toolStripMenuItem_File_Save_Click(object sender, EventArgs e)
        {
            BookSave();
        }

        private void toolStripMenuItem_File_SaveAs_Click(object sender, EventArgs e)
        {
            BookSaveAs();
        }

        private void toolStripMenuItem_ImportFromYahooCSV_Click(object sender, EventArgs e)
        {
            // Ask to save because temporarlly the import is done only in a new address book.
            if (AskToSave())
            {
                BookNew();
                BookImportFromYahooCsv();
            }
        }

        private void toolStripMenuItem_ExportToYahooCSV_Click(object sender, EventArgs e)
        {
            BookExportToYahooCsv();
        }

        private void toolStripMenuItem_File_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Menu OnClick - Address Book Menu

        private void toolStripMenuItem_Agenda_AddContact_Click(object sender, EventArgs e)
        {
            FormAddContact formAddContact = new FormAddContact(currentAddressBook.AddressBook.Contacts);

            if (formAddContact.ShowDialog() == DialogResult.OK)
            {
                contactListView1.Add(formAddContact.Contact);

                currentAddressBook.IsModified = true;
                RefreshFormTitle();
            }
        }

        private void toolStripMenuItem_Agenda_DeleteContact_Click(object sender, EventArgs e)
        {
            Contact contact = contactListView1.SelectedContact;

            if (contact != null)
            {
                if (MessageBox.Show("Are you sure you wanna delete the contact " + contact.ToString() + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    contactListView1.RemoveContact(contact);
                }
            }
        }

        private void toolStripMenuItem_Agenda_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItem_Agenda_DeleteContact.Enabled = (contactListView1.SelectedContact != null);
        }

        private void toolStripMenuItem_Agenda_Properties_Click(object sender, EventArgs e)
        {
            FormBookProperties formBookProperties = new FormBookProperties();
            formBookProperties.Book = currentAddressBook.AddressBook;
            formBookProperties.ShowDialog();
            if (formBookProperties.IsModified)
            {
                currentAddressBook.IsModified = true;
                RefreshFormTitle();
            }
        }

        #endregion

        #region Menu OnClick - Help Menu

        private void toolStripMenuItem_Help_About_Click(object sender, EventArgs e)
        {
            FormAbout oFormAbout = new FormAbout();
            oFormAbout.ShowDialog();
            oFormAbout.Dispose();
        }

        #endregion

        #region Mouse Over Meniu - File Menu

        private void toolStripMenuItem_File_New_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Create a new address book.";
        }

        private void toolStripMenuItem_File_New_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        private void toolStripMenuItem_File_Open_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Open address book from file.";
        }

        private void toolStripMenuItem_File_Open_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        private void toolStripMenuItem_File_Save_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Save current opened address book.";
        }

        private void toolStripMenuItem_File_Save_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        private void toolStripMenuItem_File_SaveAs_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Save current opened address book with another name.";
        }

        private void toolStripMenuItem_File_SaveAs_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        private void toolStripMenuItem_File_Export_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Export current opened address book in another format.";
        }

        private void toolStripMenuItem_File_Export_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        private void toolStripMenuItem_File_Import_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Import address book from another format.";
        }

        private void toolStripMenuItem_File_Import_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        private void toolStripMenuItem_File_Exit_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Exit the program.";
        }

        private void toolStripMenuItem_File_Exit_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        #endregion

        #region Mouse Over Meniu - Address Book Menu

        private void toolStripMenuItem_Agenda_AddContact_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Add a new contact.";
        }

        private void toolStripMenuItem_Agenda_AddContact_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        private void toolStripMenuItem_Agenda_DeleteContact_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Delete the selected contact.";
        }

        private void toolStripMenuItem_Agenda_DeleteContact_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        private void toolStripMenuItem_Agenda_Properties_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Display the address book properties.";
        }

        private void toolStripMenuItem_Agenda_Properties_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        #endregion

        #region Mouse Over Meniu - Help

        private void toolStripMenuItem_Help_About_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Info about the program.";
        }

        private void toolStripMenuItem_Help_About_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        #endregion

        #region Form

        private void FormLisimba_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(fileNameToOpenAtLoad))
                BookOpen(fileNameToOpenAtLoad);
        }

        private void FormLisimba_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AskToSave())
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}