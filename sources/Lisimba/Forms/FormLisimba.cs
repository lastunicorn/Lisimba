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
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Forms
{
    internal partial class FormLisimba : Form
    {
        private readonly string fileNameToOpenAtLoad = string.Empty;

        private readonly StatusService statusService;
        private readonly CurrentData currentData;
        private readonly CommandPool commandPool;
        private readonly ProgramArguments programArguments;
        private readonly ConfigurationService configurationService;
        private readonly RecentFilesService recentFilesService;
        private readonly ApplicationService applicationService;

        // Lisimba - male name meaning "lion" in Zulu language.

        public FormLisimba(ProgramArguments programArguments, ConfigurationService configurationService, StatusService statusService,
            RecentFilesService recentFilesService, CurrentData currentData, CommandPool commandPool, ApplicationService applicationService)
        {
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (recentFilesService == null) throw new ArgumentNullException("recentFilesService");
            if (currentData == null) throw new ArgumentNullException("currentData");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (applicationService == null) throw new ArgumentNullException("applicationService");

            this.programArguments = programArguments;
            this.configurationService = configurationService;

            this.statusService = statusService;
            statusService.StatusTextChanged += HandleStatusTextChanged;

            this.recentFilesService = recentFilesService;

            this.currentData = currentData;
            currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
            currentData.ContactChanged += HandleCurrentContactChanged;

            if (currentData.AddressBook != null)
                HookToAddressBook(currentData.AddressBook);

            currentData.AskToOpenLsbFile = AskToOpenLsbFile;
            currentData.AskToSaveLsbFile = AskToSaveLsbFile;

            this.commandPool = commandPool;
            commandPool.OpenAddressBookCommand.AskIfAllowToContinue = AskToSave;
            commandPool.ImportYahooCsvCommand.AskIfAllowToContinue = AskToSave;
            commandPool.ImportYahooCsvCommand.AskToOpenYahooCsvFile = AskToOpenYahooCsvFile;
            commandPool.ExportYahooCsvCommand.AskToSaveYahooCsvFile = AskToSaveYahooCsvFile;

            this.applicationService = applicationService;
            applicationService.Exiting += HandleApplicationExiting;

            InitializeComponent();

            contactListView1.CurrentData = currentData;
            contactListView1.CommandPool = commandPool;
            contactListView1.StatusService = statusService;
            contactListView1.ConfigurationService = configurationService;

            menuStripMain.Initialize(commandPool, statusService, recentFilesService);

            fileNameToOpenAtLoad = CalculateFileNameToInitiallyOpen();
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
                UnhookFromAddressBook(e.OldAddressBook);

            if (e.NewAddressBook != null)
                HookToAddressBook(e.NewAddressBook);

            Text = BuildFormTitle();
        }

        private void HookToAddressBook(AddressBook addressBook)
        {
            addressBook.Changed += HandleCurrentAddressBookContentChanged;
            addressBook.AddressBookSaved += HandleCurrentAddressBookSaved;
        }

        private void UnhookFromAddressBook(AddressBook addressBook)
        {
            addressBook.Changed -= HandleCurrentAddressBookContentChanged;
            addressBook.AddressBookSaved -= HandleCurrentAddressBookSaved;
        }

        private void HandleCurrentAddressBookSaved(object sender, EventArgs e)
        {
            Text = BuildFormTitle();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs eventArgs)
        {
            Text = BuildFormTitle();
        }

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            contactView1.Presenter.Contact = currentData.Contact;
        }

        private void HandleStatusTextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusService.StatusText;
        }

        private void HandleApplicationExiting(object sender, CancelEventArgs e)
        {
            bool allowToContinue = AskToSave();

            if (!allowToContinue)
                e.Cancel = true;
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
            saveFileDialog1.Filter = "Lisimba Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
            saveFileDialog1.DefaultExt = "lsb";

            DialogResult dialogResult = saveFileDialog1.ShowDialog();

            return dialogResult == DialogResult.OK ? saveFileDialog1.FileName : null;
        }

        private string AskToOpenLsbFile()
        {
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "Lisimba Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "lsb";

            DialogResult dialogResult = openFileDialog1.ShowDialog();

            return dialogResult == DialogResult.OK ? openFileDialog1.FileName : null;
        }

        private bool AskToSave()
        {
            if (currentData.AddressBook == null || currentData.AddressBook.Status == AddressBookStatus.Saved)
                return true;

            DialogResult dialogResult = MessageBox.Show("Current address book is not saved.\nDo you wanna save it before proceedeing?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return false;

            if (dialogResult == DialogResult.Yes)
                commandPool.SaveAddressBookCommand.Execute();

            return true;
        }

        private string BuildFormTitle()
        {
            if (currentData.AddressBook == null)
                return applicationService.ProgramName;

            StringBuilder sb = new StringBuilder();

            string addressBookName = currentData.AddressBook.GetFriendlyName() ?? "< Unnamed >";
            sb.Append(addressBookName);

            if (currentData.AddressBook.Status != AddressBookStatus.Saved)
                sb.Append(" *");

            sb.Append(" - ");

            sb.Append(applicationService.ProgramName);

            return sb.ToString();
        }

        private void FormLisimba_Shown(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = statusService.StatusText;

            if (string.IsNullOrWhiteSpace(fileNameToOpenAtLoad))
                commandPool.CreateNewAddressBookCommand.Execute();
            else
                commandPool.OpenAddressBookCommand.Execute(fileNameToOpenAtLoad);
        }
    }
}