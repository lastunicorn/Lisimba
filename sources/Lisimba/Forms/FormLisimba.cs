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
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.UserControls;

namespace DustInTheWind.Lisimba.Forms
{
    // Create the LisimbaStatusBar control.
    // Refactor ContactListView to take advantage of the services.

    internal partial class FormLisimba : Form
    {
        // Lisimba - male name meaning "lion" in Zulu language.

        private readonly ApplicationStatus applicationStatus;
        private readonly AddressBookShell addressBookShell;
        private readonly CommandPool commandPool;

        private LisimbaViewModel viewModel;
        private ContactListViewModel contactListViewModel;

        public LisimbaViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
            }
        }

        public ContactListViewModel ContactListViewModel
        {
            get { return contactListViewModel; }
            set
            {
                if (contactListViewModel != null)
                {
                    contactListView1.ViewModel = null;
                    contactListViewModel.View = null;
                }

                contactListViewModel = value;

                if (contactListViewModel != null)
                {
                    contactListView1.ViewModel = contactListViewModel;
                    contactListViewModel.View = contactListView1;
                }
            }
        }

        public FormLisimba(ApplicationStatus applicationStatus, RecentFiles recentFiles,
            AddressBookShell addressBookShell, CommandPool commandPool)
        {
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");
            if (commandPool == null) throw new ArgumentNullException("commandPool");

            InitializeComponent();

            this.applicationStatus = applicationStatus;
            this.addressBookShell = addressBookShell;
            this.commandPool = commandPool;

            addressBookShell.ContactChanged += HandleCurrentContactChanged;

            contactListView1.CommandPool = commandPool;
            contactListView1.ApplicationStatus = applicationStatus;

            menuStripMain.Initialize(commandPool, applicationStatus, recentFiles);
        }

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            contactView1.Model.Contact = addressBookShell.Contact;
        }

        private void HandleFormShown(object sender, EventArgs e)
        {
            this.Bind(x => x.Text, viewModel, x => x.Title, false, DataSourceUpdateMode.Never);
            toolStripStatusLabel1.DataBindings.Add("Text", viewModel, "StatusText");

            contactView1.Bind(x => x.Visible, viewModel, x => x.IsContactEditVisible, false, DataSourceUpdateMode.Never);
            panelAddressBookView.Bind(x => x.Visible, viewModel, x => x.IsAddressBookViewVisible, false, DataSourceUpdateMode.Never);

            buttonNewAddressBook.ApplicationStatus = applicationStatus;
            buttonNewAddressBook.Opertion = commandPool.CreateNewAddressBookOperation;

            buttonOpenAddressBook.ApplicationStatus = applicationStatus;
            buttonOpenAddressBook.Opertion = commandPool.OpenAddressBookOperation;

            viewModel.WindowWasShown();
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            bool allowToClose = viewModel.WindowIsClosing();
            e.Cancel = !allowToClose;
        }
    }
}