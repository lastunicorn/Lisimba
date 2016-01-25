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

namespace DustInTheWind.Lisimba.Main
{
    partial class TrayIcon : Component
    {
        private readonly TrayIconPresenter presenter;

        public bool Visible
        {
            get { return notifyIcon1.Visible; }
            set { notifyIcon1.Visible = value; }
        }

        public TrayIcon(TrayIconPresenter presenter)
        {
            if (presenter == null) throw new ArgumentNullException("presenter");

            InitializeComponent();

            this.presenter = presenter;
            
            toolStripMenuItem_Exit.ViewModel = presenter.ApplicationExitOperation;
            toolStripMenuItem_About.ViewModel = presenter.ShowAboutOperation;
            toolStripMenuItem_Show.ViewModel = presenter.ShowMainOperation;
        }

        private void HandleMouseDoubleClick(object sender, MouseEventArgs e)
        {
            presenter.IconWasDoubleClicked();
        }
    }
}