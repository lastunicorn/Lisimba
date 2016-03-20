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
using System.Windows;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf
{
    internal class WindowSystem
    {
        private readonly UiFactory uiFactory;
        private Window mainWindow;
        private TrayIcon trayIcon;

        public Window MainWindow
        {
            get { return mainWindow; }
            private set
            {
                if (mainWindow != null)
                    mainWindow.Closed -= HandleMainWindowClosed;

                mainWindow = value;

                if (mainWindow != null)
                    mainWindow.Closed += HandleMainWindowClosed;
            }
        }

        private void HandleMainWindowClosed(object sender, EventArgs e)
        {
            MainWindow = null;
        }

        public WindowSystem(UiFactory uiFactory, ApplicationStatus applicationStatus)
        {
            if (uiFactory == null) throw new ArgumentNullException("uiFactory");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");

            this.uiFactory = uiFactory;

            applicationStatus.DefaultStatusText = LocalizedResources.DefaultStatusText;
        }

        public void CreateMainWindow()
        {
            MainWindow = uiFactory.CreateWindow<LisimbaWindow>();
        }

        public void ShowTrayIcon()
        {
            if (trayIcon == null)
                trayIcon = uiFactory.CreateComponent<TrayIcon>();

            trayIcon.Visible = true;
        }

        public void HideTrayIcon()
        {
            if (trayIcon != null)
                trayIcon.Visible = false;
        }

        public void DisplayMainWindow()
        {
            if (mainWindow == null)
                CreateMainWindow();

            MainWindow.Show();
            MainWindow.Activate();
        }

        public void ShowGateSelector(Point point)
        {
        }
    }
}