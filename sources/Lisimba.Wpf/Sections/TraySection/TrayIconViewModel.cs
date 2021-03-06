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

namespace DustInTheWind.Lisimba.Wpf.Sections.TraySection
{
    internal class TrayIconViewModel : ViewModelBase
    {
        private readonly WindowSystem windowSystem;
        private TrayIcon trayIcon;

        //public CustomButtonViewModel ApplicationExitViewModel { get; private set; }
        //public CustomButtonViewModel AboutViewModel { get; private set; }
        //public CustomButtonViewModel ShowMainViewModel { get; private set; }

        public TrayIcon TrayIcon
        {
            get { return trayIcon; }
            set
            {
                trayIcon = value;
                trayIcon.Visible = true;
            }
        }

        public TrayIconViewModel(WindowSystem windowSystem, TrayIconMenuViewModels trayIconMenuViewModels)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.windowSystem = windowSystem;

            //ApplicationExitViewModel = trayIconMenuViewModels.ApplicationExitViewModel;
            //AboutViewModel = trayIconMenuViewModels.AboutViewModel;
            //ShowMainViewModel = trayIconMenuViewModels.ShowMainViewModel;
        }

        public void IconWasDoubleClicked()
        {
            windowSystem.DisplayMainWindow();
        }
    }
}