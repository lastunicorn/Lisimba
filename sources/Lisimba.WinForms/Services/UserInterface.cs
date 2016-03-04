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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.WinFormsCommon.ObservingModel;

namespace DustInTheWind.Lisimba.WinForms.Services
{
    internal class UserInterface : IUserInterface
    {
        private readonly WindowSystem windowSystem;
        private readonly ActiveObservers activeObservers;
        private bool runAsTray;

        public UserInterface(WindowSystem windowSystem, ActiveObservers activeObservers)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");
            if (activeObservers == null) throw new ArgumentNullException("activeObservers");

            this.windowSystem = windowSystem;
            this.activeObservers = activeObservers;
        }

        public void Initialize()
        {
            RunAsTrayApp();
            //RunAsWindowApp();
        }

        private void RunAsWindowApp()
        {
            windowSystem.CreateMainWindow();
            activeObservers.Start();

            runAsTray = false;
        }

        private void RunAsTrayApp()
        {
            windowSystem.ShowTrayIcon();
            windowSystem.CreateMainWindow();
            windowSystem.DisplayMainWindow();
            activeObservers.Start();

            runAsTray = true;
        }

        public void Start()
        {
            if (runAsTray)
                Application.Run();
            else
                Application.Run(windowSystem.MainWindow);
        }

        public void Exit()
        {
            windowSystem.HideTrayIcon();

            Application.Exit();
        }
    }
}