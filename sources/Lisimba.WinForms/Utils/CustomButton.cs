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
using System.ComponentModel;
using System.Windows.Forms;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.Utils
{
    partial class CustomButton : Button
    {
        private CustomButtonViewModel viewModel;

        public CustomButtonViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                DataBindings.Clear();
                Enabled = false;

                viewModel = value;

                if (viewModel != null)
                    this.Bind(x => x.Enabled, viewModel, x => x.IsEnabled, false, DataSourceUpdateMode.Never);
            }
        }

        public CustomButton()
        {
            InitializeComponent();
        }

        public CustomButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void HandleClick(object sender, EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.Execute();
        }

        private void HandleMouseEnter(object sender, EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.MouseEnter();
        }

        private void HandleMouseLeave(object sender, EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.MouseLeave();
        }
    }
}