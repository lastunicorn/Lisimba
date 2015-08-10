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
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Forms
{
    // Create the LisimbaStatusBar control.
    // Refactor ContactListView to take advantage of the services.

    internal partial class LisimbaForm : Form
    {
        // Lisimba - male name meaning "lion" in Zulu language.

        private readonly RecentFiles recentFiles;
        private readonly CommandPool commandPool;

        private LisimbaViewModel viewModel;

        public LisimbaViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                if (contactListView1.ViewModel != null)
                {
                    contactListView1.ViewModel.View = null;
                    contactListView1.ViewModel = null;
                }

                if (contactView1.ViewModel != null)
                {
                    contactView1.ViewModel.View = null;
                    contactView1.ViewModel = null;
                }

                viewModel = value;

                if (viewModel != null)
                {
                    contactListView1.ViewModel = viewModel.ContactListViewModel;
                    contactListView1.ViewModel.View = contactListView1;

                    contactView1.ViewModel = viewModel.ContactEditorViewModel;
                    contactView1.ViewModel.View = contactView1;
                    
                    contactListView1.CommandPool = commandPool;
                    menuStripMain.Initialize(commandPool, recentFiles);
                }
            }
        }

        public LisimbaForm(RecentFiles recentFiles, CommandPool commandPool)
        {
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (commandPool == null) throw new ArgumentNullException("commandPool");

            InitializeComponent();

            this.recentFiles = recentFiles;
            this.commandPool = commandPool;
        }

        private void HandleFormShown(object sender, EventArgs e)
        {
            this.Bind(x => x.Text, viewModel, x => x.Title, false, DataSourceUpdateMode.Never);
            toolStripStatusLabel1.Bind(x => x.Text, viewModel, x => x.StatusText, false, DataSourceUpdateMode.Never);

            contactView1.Bind(x => x.Visible, viewModel, x => x.IsContactEditVisible, false, DataSourceUpdateMode.Never);
            panelAddressBookView.Bind(x => x.Visible, viewModel, x => x.IsAddressBookViewVisible, false, DataSourceUpdateMode.Never);

            buttonNewAddressBook.ViewModel = commandPool.CreateNewAddressBookOperation;
            buttonOpenAddressBook.ViewModel = commandPool.OpenAddressBookOperation;

            viewModel.WindowWasShown();
        }
    }
}