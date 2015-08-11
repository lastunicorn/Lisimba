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

namespace DustInTheWind.Lisimba.Forms
{
    partial class AddContactForm : Form, IAddContactView
    {
        private AddContactPresenter viewModel;

        public AddContactPresenter ViewModel
        {
            private get { return viewModel; }
            set
            {
                RemoveBindings();

                if (contactEditor1.ViewModel != null)
                {
                    contactEditor1.ViewModel.View = null;
                    contactEditor1.ViewModel = null;
                }

                viewModel = value;

                if (viewModel != null)
                {
                    contactEditor1.ViewModel = viewModel.ContactEditorViewModel;
                    contactEditor1.ViewModel.View = contactEditor1;

                    CreateBindings();
                }
            }
        }

        public AddContactForm()
        {
            InitializeComponent();
        }

        private void RemoveBindings()
        {
        }

        private void CreateBindings()
        {
            //contactView1.Bind(x => x.Model, Presenter, x => x.EditedContact, true);
        }

        private void HandleButtonOkayClick(object sender, EventArgs e)
        {
            ViewModel.OkButtonWasClicked();
        }

        private void HandleButtonCancelClick(object sender, EventArgs e)
        {
            ViewModel.CloseButtonWasClicked();
        }

        private void HandleFormLoad(object sender, EventArgs e)
        {
            ViewModel.ViewWasLoaded();
        }
    }
}