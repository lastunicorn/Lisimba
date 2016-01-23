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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Forms
{
    partial class AddressBookPropertiesForm : Form
    {
        private AddressBookPropertiesViewModel viewModel;

        public AddressBookPropertiesViewModel ViewModel
        {
            set
            {
                ClearAllBindings();

                viewModel = value;

                if (viewModel != null)
                    CreateBindings();
            }
        }

        private void ClearAllBindings()
        {
            textBoxBookName.DataBindings.Clear();
            textBoxFileLocation.DataBindings.Clear();
            textBoxContactsCount.DataBindings.Clear();
        }

        private void CreateBindings()
        {
            textBoxBookName.Bind(x => x.Text, viewModel, x => x.BookName, false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxBookName.Bind(x => x.Enabled, viewModel, x => x.BookNameEnabled, false, DataSourceUpdateMode.Never);
            textBoxFileLocation.Bind(x => x.Text, viewModel, x => x.FileLocation, false, DataSourceUpdateMode.Never);
            textBoxContactsCount.Bind(x => x.Text, viewModel, x => x.ContactsCount, false, DataSourceUpdateMode.Never);
        }

        public AddressBookPropertiesForm()
        {
            InitializeComponent();
        }

        private void HandleButtonOkayClick(object sender, EventArgs e)
        {
            if (viewModel == null)
                return;

            viewModel.OkButtonWasClicked();
        }

        private void HandleFormShown(object sender, EventArgs e)
        {
            textBoxBookName.Focus();
        }
    }
}