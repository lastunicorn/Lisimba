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
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Forms
{
    // Create the LisimbaStatusBar control.
    // Refactor ContactListView to take advantage of the services.
    // Create a StatusChanged event in AddressBook.

    internal partial class FormLisimba : Form
    {
        private readonly ApplicationStatus applicationStatus;
        private readonly CurrentData currentData;
        private readonly ApplicationService applicationService;

        private bool allowToClose;

        // Lisimba - male name meaning "lion" in Zulu language.

        public FormLisimba(ConfigurationService configurationService, ApplicationStatus applicationStatus,
            RecentFiles recentFiles, CurrentData currentData, CommandPool commandPool, ApplicationService applicationService,
            UiService uiService)
        {
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (currentData == null) throw new ArgumentNullException("currentData");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (applicationService == null) throw new ArgumentNullException("applicationService");
            if (uiService == null) throw new ArgumentNullException("uiService");

            InitializeComponent();

            this.applicationStatus = applicationStatus;
            applicationStatus.StatusTextChanged += HandleStatusTextChanged;

            this.currentData = currentData;
            currentData.AddressBookShell.AddressBookChanged += HandleCurrentAddressBookChanged;
            currentData.AddressBookShell.StatusChanged += HandleAddressBookStatusChanged;
            currentData.ContactChanged += HandleCurrentContactChanged;

            if (currentData.AddressBookShell.AddressBook != null)
                currentData.AddressBookShell.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            this.applicationService = applicationService;
            applicationService.Exiting += HandleApplicationExiting;
            applicationService.ExitCanceled += HandleApplicationExitCanceled;

            contactListView1.CurrentData = currentData;
            contactListView1.CommandPool = commandPool;
            contactListView1.ApplicationStatus = applicationStatus;
            contactListView1.ConfigurationService = configurationService;

            menuStripMain.Initialize(commandPool, applicationStatus, recentFiles);
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
                e.OldAddressBook.Changed -= HandleCurrentAddressBookContentChanged;

            if (e.NewAddressBook != null)
                e.NewAddressBook.Changed += HandleCurrentAddressBookContentChanged;

            Text = BuildFormTitle();
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
            toolStripStatusLabel1.Text = applicationStatus.StatusText;
        }

        private string BuildFormTitle()
        {
            if (currentData.AddressBookShell == null)
                return applicationService.ProgramName;

            StringBuilder sb = new StringBuilder();

            string addressBookName = currentData.AddressBookShell.GetFriendlyName() ?? "< Unnamed >";
            sb.Append(addressBookName);

            if (currentData.AddressBookShell.Status != AddressBookStatus.Saved)
                sb.Append(" *");

            sb.Append(" - ");

            sb.Append(applicationService.ProgramName);

            return sb.ToString();
        }

        private void FormLisimba_Shown(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = applicationStatus.StatusText;

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