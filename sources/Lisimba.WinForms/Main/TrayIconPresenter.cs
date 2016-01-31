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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Main
{
    internal class TrayIconPresenter : ViewModelBase
    {
        private readonly UserInterface userInterface;
        private TrayIcon trayIcon;

        public TrayIconMenuViewModels TrayIconMenuViewModels { get; private set; }

        public TrayIcon TrayIcon
        {
            get { return trayIcon; }
            set
            {
                trayIcon = value;
                trayIcon.Visible = true;
            }
        }

        public TrayIconPresenter(ApplicationBackEnd applicationBackEnd, UserInterface userInterface, TrayIconMenuViewModels trayIconMenuViewModels)
        {
            if (applicationBackEnd == null) throw new ArgumentNullException("applicationBackEnd");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;

            TrayIconMenuViewModels = trayIconMenuViewModels;

            applicationBackEnd.Ending += HandleApplicationBackEndEnding;
            applicationBackEnd.EndCanceled += HandleApplicationBackEndExitCanceled;
        }

        private void HandleApplicationBackEndEnding(object sender, CancelEventArgs cancelEventArgs)
        {
            if (TrayIcon != null)
                TrayIcon.Visible = false;
        }

        private void HandleApplicationBackEndExitCanceled(object sender, EventArgs eventArgs)
        {
            if (TrayIcon != null)
                TrayIcon.Visible = true;
        }

        public void IconWasDoubleClicked()
        {
            userInterface.DisplayMainWindow();
        }
    }
}