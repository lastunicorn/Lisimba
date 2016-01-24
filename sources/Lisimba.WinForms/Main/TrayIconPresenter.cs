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
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Main
{
    internal class TrayIconPresenter : ViewModelBase
    {
        private readonly ApplicationBackEnd applicationBackEnd;
        private readonly UserInterface userInterface;
        private TrayIcon trayIcon;

        public ApplicationExitOperation ApplicationExitOperation { get; private set; }

        public TrayIcon TrayIcon
        {
            get { return trayIcon; }
            set
            {
                trayIcon = value;

                applicationBackEnd.Ending += HandleApplicationBackEndEnding;
                TrayIcon.Visible = true;
            }
        }

        public TrayIconPresenter(ApplicationBackEnd applicationBackEnd, UserInterface userInterface, CommandPool commandPool)
        {
            if (applicationBackEnd == null) throw new ArgumentNullException("applicationBackEnd");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.applicationBackEnd = applicationBackEnd;
            this.userInterface = userInterface;

            ApplicationExitOperation = commandPool.ApplicationExitOperation;
        }

        private void HandleApplicationBackEndEnding(object sender, CancelEventArgs cancelEventArgs)
        {
            TrayIcon.Visible = false;
        }

        public void IconWasDoubleClicked()
        {
            userInterface.DisplayMainWindow();
        }
    }
}