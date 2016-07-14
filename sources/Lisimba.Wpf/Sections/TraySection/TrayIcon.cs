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

namespace DustInTheWind.Lisimba.Wpf.Sections.TraySection
{
    internal partial class TrayIcon : Component
    {
        private readonly TrayIconViewModel viewModel;

        public bool Visible
        {
            get { return notifyIcon1.Visible; }
            set { notifyIcon1.Visible = value; }
        }

        public TrayIcon(TrayIconViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            InitializeComponent();

            this.viewModel = viewModel;
            viewModel.TrayIcon = this;

            //toolStripMenuItem_Exit.ViewModel = viewModel.ApplicationExitViewModel;
            //toolStripMenuItem_About.ViewModel = viewModel.AboutViewModel;
            //toolStripMenuItem_Show.ViewModel = viewModel.ShowMainViewModel;
        }

        private void HandleMouseDoubleClick(object sender, MouseEventArgs e)
        {
            viewModel.IconWasDoubleClicked();
        }
    }
}