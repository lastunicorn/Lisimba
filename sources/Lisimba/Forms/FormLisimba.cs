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
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Forms
{
    // Create the LisimbaStatusBar control.
    // Refactor ContactListView to take advantage of the services.
    // Create a StatusChanged event in AddressBook.

    internal partial class FormLisimba : Form
    {
        private readonly StatusService statusService;
        private readonly CurrentData currentData;
        private readonly ApplicationService applicationService;

        private bool allowToClose;

        // Lisimba - male name meaning "lion" in Zulu language.

        public FormLisimba(ConfigurationService configurationService, StatusService statusService,
            RecentFiles recentFiles, CurrentData currentData, CommandPool commandPool, ApplicationService applicationService,
            UiService uiService)
        {
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (currentData == null) throw new ArgumentNullException("currentData");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (applicationService == null) throw new ArgumentNullException("applicationService");
            if (uiService == null) throw new ArgumentNullException("uiService");

            InitializeComponent();

            this.statusService = statusService;
            statusService.StatusTextChanged += HandleStatusTextChanged;

            this.currentData = currentData;
            currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
            currentData.ContactChanged += HandleCurrentContactChanged;

            if (currentData.AddressBook != null)
                HookToAddressBook(currentData.AddressBook);

            this.applicationService = applicationService;
            applicationService.Exiting += HandleApplicationExiting;
            applicationService.ExitCanceled += HandleApplicationExitCanceled;

            contactListView1.CurrentData = currentData;
            contactListView1.CommandPool = commandPool;
            contactListView1.StatusService = statusService;
            contactListView1.ConfigurationService = configurationService;

            menuStripMain.Initialize(commandPool, statusService, recentFiles);
        }

        private void HandleApplicationExitCanceled(object sender, EventArgs e)
        {
            allowToClose = false;
        }

        private void HandleApplicationExiting(object sender, CancelEventArgs e)
        {
            allowToClose = true;
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
            addressBook.StatusChanged += HandleAddressBookStatusChanged;
        }

        private void UnhookFromAddressBook(AddressBook addressBook)
        {
            addressBook.Changed -= HandleCurrentAddressBookContentChanged;
            addressBook.StatusChanged -= HandleAddressBookStatusChanged;
        }

        private void HandleAddressBookStatusChanged(object sender, EventArgs eventArgs)
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

            Text = BuildFormTitle();
        }

        private void FormLisimba_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (allowToClose)
                return;

            e.Cancel = !allowToClose;
            applicationService.Exit();
        }
    }
}