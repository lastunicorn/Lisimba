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
using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Forms
{
    partial class FormAddressBookProperties : Form, IAddressBookPropertiesView
    {
        public AddressBookPropertiesPresenter Presenter { private get; set; }

        public FormAddressBookProperties()
        {
            InitializeComponent();
        }

        public void CreateBindings(AddressBookPropertiesViewModel viewModel)
        {
            textBoxBookName.DataBindings.Add("Text", viewModel, "BookName", false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxBookName.DataBindings.Add("Enabled", viewModel, "BookNameEnabled", false, DataSourceUpdateMode.Never);
            textBoxFileLocation.DataBindings.Add("Text", viewModel, "FileLocation", false, DataSourceUpdateMode.Never);
            textBoxContactsCount.DataBindings.Add("Text", viewModel, "ContactsCount", false, DataSourceUpdateMode.Never);
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            if (Presenter == null)
                return;

            Presenter.OkButtonWasClicked();
        }

        private void FormBookProperties_Shown(object sender, EventArgs e)
        {
            textBoxBookName.Focus();
        }

        public void ShowModalView()
        {
            ShowDialog();
        }
    }
}