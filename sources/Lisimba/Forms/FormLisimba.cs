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
using DustInTheWind.Lisimba.Egg.BookShell;
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Forms
{
    // Create the LisimbaStatusBar control.
    // Refactor ContactListView to take advantage of the services.

    internal partial class FormLisimba : Form
    {
        private readonly LisimbaViewModel viewModel;
        private readonly AddressBookShell addressBookShell;

        // Lisimba - male name meaning "lion" in Zulu language.

        public FormLisimba(ConfigurationService configurationService, ApplicationStatus applicationStatus,
            RecentFiles recentFiles, AddressBookShell addressBookShell, CommandPool commandPool, ApplicationService applicationService,
            UiService uiService, LisimbaApplication lisimbaApplication)
        {
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (applicationService == null) throw new ArgumentNullException("applicationService");
            if (uiService == null) throw new ArgumentNullException("uiService");
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");

            InitializeComponent();

            viewModel = new LisimbaViewModel(addressBookShell, applicationService, applicationStatus, lisimbaApplication);

            this.addressBookShell = addressBookShell;
            addressBookShell.ContactChanged += HandleCurrentContactChanged;

            contactListView1.CurrentData = addressBookShell;
            contactListView1.CommandPool = commandPool;
            contactListView1.ApplicationStatus = applicationStatus;
            contactListView1.ConfigurationService = configurationService;

            menuStripMain.Initialize(commandPool, applicationStatus, recentFiles);
        }

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            contactView1.Presenter.Contact = addressBookShell.Contact;
        }

        private void HandleFormShown(object sender, EventArgs e)
        {
            DataBindings.Add("Text", viewModel, "Title");
            toolStripStatusLabel1.DataBindings.Add("Text", viewModel, "StatusText");

            viewModel.WindowWasShown();
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            bool allowToClose = viewModel.WindowIsClosing();
            e.Cancel = !allowToClose;
        }
    }
}