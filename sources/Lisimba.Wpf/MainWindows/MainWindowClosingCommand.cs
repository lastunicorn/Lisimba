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
using System.Windows.Input;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.Config;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class MainWindowClosingCommand : ICommand
    {
        private readonly LisimbaApplication lisimbaApplication;
        private readonly ApplicationConfiguration config;
        private readonly WindowSystem windowSystem;

        public event EventHandler CanExecuteChanged;

        public MainWindowClosingCommand(LisimbaApplication lisimbaApplication, ApplicationConfiguration config, WindowSystem windowSystem)
        {
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (config == null) throw new ArgumentNullException("config");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.lisimbaApplication = lisimbaApplication;
            this.config = config;
            this.windowSystem = windowSystem;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            bool closeToTray = true;

            if(closeToTray)
                windowSystem.MainWindow.Close();
            else
                lisimbaApplication.Exit();
        }
    }
}