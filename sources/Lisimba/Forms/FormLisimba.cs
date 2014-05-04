using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Config;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Egg.Exceptions;
using DustInTheWind.Lisimba.UserControls;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormLisimba : Form
    {
        private AddressBook addressBook = null;
        private bool isNew = true;
        private bool isModified = false;
        private string statusText = string.Empty;
        private string programTitle = string.Empty;
        private Configuration config;
        private LisimbaConfigSection lisimbaConfigSection;

        private string fileNameToOpenAtLoad = string.Empty;
        private AddressBookManager addressBookLoader = new AddressBookManager();

        // Lisimba - male name meaning "lion" in Zulu language.

        #region Constructors

        public FormLisimba()
            : this(string.Empty)
        {
        }

        public FormLisimba(string fileName)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            programTitle = Application.ProductName + " " + version.Major + "." + version.Minor;

            InitializeComponent();

            addressBookLoader.IncorrectXmlVersion += new AddressBookManager.IncorrectXmlVersionHandler(addressBookLoader_IncorrectXmlVersion);

            ReadLisimbaConfigSection();

            RefreshRecentFilesMenu();

            BookNew();

            if (!string.IsNullOrEmpty(fileName))
            {
                fileNameToOpenAtLoad = fileName;
            }
            else
            {
                switch (lisimbaConfigSection.LoadFileAtStart.Type)
                {
                    case "new":
                        break;

                    case "last":
                        if (lisimbaConfigSection.RecentFilesList.Count > 0)
                        {
                            fileNameToOpenAtLoad = lisimbaConfigSection.RecentFilesList[0].FileName;
                        }
                        break;

                    case "specified":
                        fileNameToOpenAtLoad = lisimbaConfigSection.LoadFileAtStart.FileName;
                        break;

                    default:
                        break;
                }

            }

            toolStripStatusLabel1.Text = statusText;

            // Set the initial sort method
            switch (lisimbaConfigSection.SortBy.Value)
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

                default:
                    break;
            }

            //// todo: for test
            //Contact c = new Contact();
            //c.Name = new PersonName("Alex", "Nicolae", "Iuga", "Alez");
            //c.Phones.Add(new Phone("123456789", "Test"));

            //this.ctrl1.Value = c;
        }

        #endregion

        void addressBookLoader_IncorrectXmlVersion(object sender, AddressBookManager.IncorrectXmlVersionEventArgs e)
        {
            if (e.XmlVersion == null)
            {
                DialogResult r = MessageBox.Show(this, "The version of the file \"" + e.FileName + "\" could not be determined.\n\nDo you still wanna try to open the file?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {
                    e.ContinueParsing = true;
                }
                return;
            }
            else
            {
                if (MessageBox.Show(this, "The file \"" + e.FileName + "\" is created with another version of the Egg.\n\nCurrent Egg version = " + e.EggVersion.Major + "." + e.EggVersion.Minor + "\nFile created by Egg version = " + e.XmlVersion.Major + "." + e.XmlVersion.Minor + "\n\nDo you still wanna try to open the file?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    e.ContinueParsing = true;
                    return;
                }
            }
        }

        #region Recent Files

        private void ReadLisimbaConfigSection()
        {
            try
            {
                // Read the config file
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Read lisimba section
                lisimbaConfigSection = config.GetSection("lisimba") as LisimbaConfigSection;
                if (lisimbaConfigSection == null)
                {
                    lisimbaConfigSection = new LisimbaConfigSection();
                    config.Sections.Add("lisimba", lisimbaConfigSection);
                    lisimbaConfigSection.SectionInformation.ForceSave = true;
                    config.Save(ConfigurationSaveMode.Full);
                }
            }
            catch
            {
                lisimbaConfigSection = new LisimbaConfigSection();
            }
        }

        private void RefreshRecentFilesMenu()
        {
            ToolStripMenuItem menuItem = null;
            ToolStripItemCollection menuItems = toolStripMenuItem_File_RecentFiles.DropDownItems;
            int j = 0; // index for the list of menu items (this.recentFilesMenuItems)

            RecentFilesConfigElementCollection recentFiles = lisimbaConfigSection.RecentFilesList;

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
            if (AskToSave())
            {
                string fileName = ((ToolStripMenuItem)sender).Tag.ToString();
                BookOpen(fileName);
            }
        }

        private void AddRecentFile(string fileName)
        {
            lisimbaConfigSection.RecentFilesList.AddNewRecentFile(Path.GetFullPath(fileName));
            RefreshRecentFilesMenu();
            config.Save(ConfigurationSaveMode.Full);
        }

        #endregion

        #region Other Methods

        private bool AskToSave()
        {
            if (isModified)
            {
                DialogResult r = MessageBox.Show("Current address book is not saved.\nDo you wanna save it before proceedeing?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (r == DialogResult.Cancel)
                {
                    return false;
                }
                else if (r == DialogResult.Yes)
                {
                    BookSave();
                }
            }

            return true;
        }

        private void RefreshFormTitle()
        {
            string text = string.Empty;

            // Book name or file path
            if (!isNew || isModified)
            {
                if (addressBook.Name.Length == 0)
                {
                    if (addressBook.FileName.Length == 0)
                        text += "< Unnamed >";
                    else
                        text += addressBook.FileName;
                }
                else
                {
                    text += addressBook.Name;
                }
            }

            // Is modified (*)
            if (isModified)
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
            isModified = true;

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
            if (!isModified)
            {
                isModified = true;

                // Refresh form title
                RefreshFormTitle();
            }

            contactListView1.SetContactChangedFlag(contactView1.Contact, true);

        }

        #endregion

        #region New Open Save Close

        private void BookNew()
        {
            addressBook = new AddressBook();
            isNew = true;
            isModified = false;

            // Populate the list control
            contactListView1.Contacts = addressBook.Contacts;

            // Disable the contact view control
            contactView1.Contact = null;
            contactView1.Enabled = false;

            // Display a status text
            toolStripStatusLabel1.Text = "A new address book was created.";

            // Clear the "Find" textbox.
            contactListView1.SearchText = string.Empty;

            // Refresh the form title
            RefreshFormTitle();
        }

        private void BookOpen()
        {
            BookOpen(string.Empty);
        }

        private void BookOpen(string fileName)
        {
            // If no file name is specified, display an OpenFileDialog window.
            if (string.IsNullOrEmpty(fileName))
            {
                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog1.Filter = "Lis Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
                openFileDialog1.DefaultExt = "lsb";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
                else
                {
                    return;
                }
            }

            try
            {
                // Check the version of the file that will be open.
                //if (!this.CheckLsbVersion(fileName))
                //    return;

                // Open the file
                addressBook = addressBookLoader.LoadFromFile(fileName);

                isNew = false;
                isModified = false;

                // Populate the list control
                contactListView1.Contacts = addressBook.Contacts;

                // Disable the contact view control
                contactView1.Contact = null;
                contactView1.Enabled = false;

                // Display a status text
                toolStripStatusLabel1.Text = addressBook.Count + " contacts oppened.";

                // Clear the "Find" textbox.
                contactListView1.SearchText = string.Empty;

                // Update the RecentFiles list.
                AddRecentFile(fileName);
            }
            catch (EggIncorrectVersionException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Refresh the form title
            RefreshFormTitle();
        }

        private void BookSave()
        {
            if (addressBook.FileName.Length == 0)
            {
                BookSaveAs();
            }
            else
            {
                try
                {
                    addressBookLoader.SaveToFile(addressBook);
                    isNew = false;
                    isModified = false;
                    contactListView1.ResetModifiedFlags();

                    // Display a status text
                    toolStripStatusLabel1.Text = "Address book saved. (" + addressBook.Count + " contacts)";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            RefreshFormTitle();
        }

        private void BookSaveAs()
        {
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "Lis Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
            saveFileDialog1.DefaultExt = "lsb";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = saveFileDialog1.FileName;
                    addressBookLoader.SaveToFile(addressBook, fileName);
                    isNew = false;
                    isModified = false;
                    contactListView1.ResetModifiedFlags();

                    // Display a status text
                    toolStripStatusLabel1.Text = "Address book saved. (" + addressBook.Count + " contacts)";

                    // Update the RecentFiles list.
                    AddRecentFile(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            RefreshFormTitle();
        }

        #endregion

        #region Import Export

        private void BookImportFromYahooCsv()
        {
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "Csv Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ContactCollection newContacts = addressBookLoader.ImportFromFile(openFileDialog1.FileName, FileFormat.CsvYahoo);

                    ImportRuleCollection importRules = addressBookLoader.CreateImportRules(newContacts);

                    int countImport = addressBook.AddRange(newContacts, importRules);
                    //this.contactListView1.addr

                    //this.isNew = false;
                    isModified = true;

                    // Populate the list control
                    contactListView1.Contacts = addressBook.Contacts;

                    // Disable the contact view control
                    contactView1.Contact = null;
                    contactView1.Enabled = false;

                    // Refresh the form title
                    RefreshFormTitle();

                    // Display a status text
                    toolStripStatusLabel1.Text = countImport + " contacts imported from " + newContacts.Count + " contacts in .csv file.";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BookExportToYahooCsv()
        {
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "Csv Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "csv";
            saveFileDialog1.FileName = string.Empty;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    addressBookLoader.ExportToFile(addressBook, saveFileDialog1.FileName, FileFormat.CsvYahoo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        #region Menu OnClick

        #region File Menu

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

        #region Address Book Menu

        private void toolStripMenuItem_Agenda_AddContact_Click(object sender, EventArgs e)
        {
            FormAddContact formAddContact = new FormAddContact(addressBook.Contacts);

            if (formAddContact.ShowDialog() == DialogResult.OK)
            {
                contactListView1.Add(formAddContact.Contact);

                isModified = true;
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
            formBookProperties.Book = addressBook;
            formBookProperties.ShowDialog();
            if (formBookProperties.IsModified)
            {
                isModified = true;
                RefreshFormTitle();
            }
        }

        #endregion

        #region Help Menu

        private void toolStripMenuItem_Help_About_Click(object sender, EventArgs e)
        {
            FormAbout oFormAbout = new FormAbout();
            oFormAbout.ShowDialog();
            oFormAbout.Dispose();
        }

        #endregion

        #endregion Menu OnClick

        #region Mouse Over Meniu

        #region File Menu

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

        #region Address Book Menu

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

        #region Help

        private void toolStripMenuItem_Help_About_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Info about the program.";
        }

        private void toolStripMenuItem_Help_About_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusText;
        }

        #endregion

        #endregion Mouse Over Meniu

        #region Form

        private void FormLisimba_Shown(object sender, EventArgs e)
        {
            if (fileNameToOpenAtLoad.Length > 0)
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