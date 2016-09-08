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
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Business.WorkerModel;
using DustInTheWind.Lisimba.WinForms.Services;

namespace DustInTheWind.Lisimba.WinForms
{
    internal class UserInterface : IUserInterface
    {
        private readonly WindowSystem windowSystem;
        private readonly Workers workers;
        private readonly ApplicationConfiguration config;
        private bool runAsTray;

        public UserInterface(WindowSystem windowSystem, Workers workers, ApplicationConfiguration config)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");
            if (workers == null) throw new ArgumentNullException("workers");
            if (config == null) throw new ArgumentNullException("config");

            this.windowSystem = windowSystem;
            this.workers = workers;
            this.config = config;
        }

        public void Initialize()
        {
            if (config.StartInTray)
                RunAsTrayApp();
            else
                RunAsWindowApp();
        }

        private void RunAsWindowApp()
        {
            windowSystem.CreateMainWindow();
            workers.Start();

            runAsTray = false;
        }

        private void RunAsTrayApp()
        {
            windowSystem.ShowTrayIcon();
            windowSystem.CreateMainWindow();
            windowSystem.DisplayMainWindow();
            workers.Start();

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