// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Main
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
                RemoveBindings();

                viewModel = value;

                if (viewModel != null)
                    CreateBindings();
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

        private void RemoveBindings()
        {
            DataBindings.Clear();
            toolStripStatusLabel1.DataBindings.Clear();
            contactEditor1.DataBindings.Clear();
            panelAddressBookView.DataBindings.Clear();

            buttonNewAddressBook.ViewModel = null;
            buttonOpenAddressBook.ViewModel = null;

            if (contactListView1.ViewModel != null)
            {
                contactListView1.ViewModel.View = null;
                contactListView1.ViewModel = null;
            }

            if (contactEditor1.ViewModel != null)
            {
                contactEditor1.ViewModel.View = null;
                contactEditor1.ViewModel = null;
            }
        }

        private void CreateBindings()
        {
            contactListView1.ViewModel = viewModel.ContactListViewModel;
            contactListView1.ViewModel.View = contactListView1;

            contactEditor1.ViewModel = viewModel.ContactEditorViewModel;
            contactEditor1.ViewModel.View = contactEditor1;

            menuStripMain.Initialize(commandPool, recentFiles);

            this.Bind(x => x.Text, viewModel, x => x.Title, false, DataSourceUpdateMode.Never);
            toolStripStatusLabel1.Bind(x => x.Text, viewModel, x => x.StatusText, false, DataSourceUpdateMode.Never);

            contactEditor1.Bind(x => x.Visible, viewModel, x => x.IsContactEditVisible, false, DataSourceUpdateMode.Never);
            panelAddressBookView.Bind(x => x.Visible, viewModel, x => x.IsAddressBookViewVisible, false, DataSourceUpdateMode.Never);

            buttonNewAddressBook.ViewModel = viewModel.CreateNewAddressBookOperation;
            buttonOpenAddressBook.ViewModel = viewModel.OpenAddressBookOperation;
        }

        private void LisimbaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ViewModel = null;
        }

        private void LisimbaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool allowToContinue = ViewModel.WindowIsClosing();
            e.Cancel = !allowToContinue;
        }
    }
}