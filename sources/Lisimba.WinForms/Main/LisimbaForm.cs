// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Main
{
    // Create the LisimbaStatusBar control.
    // Refactor ContactListView to take advantage of the services.

    internal partial class LisimbaForm : Form
    {
        // Lisimba - male name meaning "lion" in Zulu language.

        private LisimbaViewModel viewModel;

        private readonly RecentFiles recentFiles;
        private readonly AvailableOperations availableOperations;

        public LisimbaForm(LisimbaViewModel viewModel, RecentFiles recentFiles, AvailableOperations availableOperations)
        {
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            InitializeComponent();

            this.recentFiles = recentFiles;
            this.availableOperations = availableOperations;

            this.viewModel = viewModel;

            CreateBindings();
        }

        private void CreateBindings()
        {
            contactListView1.ViewModel = viewModel.ContactListViewModel;
            contactListView1.ViewModel.View = contactListView1;

            contactEditor1.ViewModel = viewModel.ContactEditorViewModel;
            contactEditor1.ViewModel.View = contactEditor1;

            menuStripMain.Initialize(availableOperations, recentFiles, viewModel.MenuItemViewModels);

            this.Bind(x => x.Text, viewModel, x => x.Title, false, DataSourceUpdateMode.Never);
            toolStripStatus.Bind(x => x.Text, viewModel, x => x.StatusText, false, DataSourceUpdateMode.Never);
            toolStripDefaultGate.Bind(x => x.Text, viewModel, x => x.DefaultGate, false, DataSourceUpdateMode.Never);

            contactEditor1.Bind(x => x.Visible, viewModel, x => x.IsContactEditVisible, false, DataSourceUpdateMode.Never);
            panelAddressBookView.Bind(x => x.Visible, viewModel, x => x.IsAddressBookViewVisible, false, DataSourceUpdateMode.Never);

            buttonNewAddressBook.ViewModel = viewModel.NewAddressBookOperation;
            buttonOpenAddressBook.ViewModel = viewModel.OpenAddressBookOperation;

            toolStripButtonNew.ViewModel = availableOperations.GetOperation<NewAddressBookOperation>();
            toolStripButtonOpen.ViewModel = availableOperations.GetOperation<OpenAddressBookOperation>();
            toolStripButtonSave.ViewModel = availableOperations.GetOperation<SaveAddressBookOperation>();
            toolStripButtonAbout.ViewModel = availableOperations.GetOperation<ShowAboutOperation>();
        }

        private void LisimbaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            viewModel = null;
        }

        private void LisimbaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool allowToContinue = viewModel.WindowIsClosing();
            e.Cancel = !allowToContinue;
        }

        private void toolStripDefaultGate_Click(object sender, EventArgs e)
        {
            int x = statusStripMain.Location.X + statusStripMain.Size.Width;
            int y = statusStripMain.Location.Y;
            Point point = new Point(x, y);
            Point screenPoint = PointToScreen(point);

            viewModel.DefaultGateWasClicked(screenPoint);
        }
    }
}