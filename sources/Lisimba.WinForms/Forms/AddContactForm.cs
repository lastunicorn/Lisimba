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

namespace DustInTheWind.Lisimba.WinForms.Forms
{
    partial class AddContactForm : Form, IAddContactView
    {
        private readonly AddContactPresenter viewModel;

        public AddContactForm(AddContactPresenter viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            
            InitializeComponent();

            this.viewModel = viewModel;
            viewModel.View = this;

            CreateBindings();
        }

        private void CreateBindings()
        {
            contactEditor1.ViewModel = viewModel.ContactEditorViewModel;
            contactEditor1.ViewModel.View = contactEditor1;
        }

        private void HandleButtonOkayClick(object sender, EventArgs e)
        {
            viewModel.OkButtonWasClicked();
        }

        private void HandleButtonCancelClick(object sender, EventArgs e)
        {
            viewModel.CloseButtonWasClicked();
        }

        private void HandleFormLoad(object sender, EventArgs e)
        {
            viewModel.ViewWasLoaded();
        }
    }
}