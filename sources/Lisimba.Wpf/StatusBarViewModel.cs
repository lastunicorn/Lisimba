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
using DustInTheWind.Lisimba.Business.GateManagement;

namespace DustInTheWind.Lisimba.Wpf
{
    internal class StatusBarViewModel : ViewModelBase
    {
        private readonly ApplicationStatus applicationStatus;
        private readonly AvailableGates availableGates;
        private readonly WindowSystem windowSystem;

        private string statusText;
        private string defaultGate;

        public string StatusText
        {
            get { return statusText; }
            set
            {
                statusText = value;
                OnPropertyChanged();
            }
        }

        public string DefaultGate
        {
            get { return defaultGate; }
            set
            {
                defaultGate = value;
                OnPropertyChanged();
            }
        }

        public StatusBarViewModel(ApplicationStatus applicationStatus, AvailableGates availableGates, WindowSystem windowSystem)
        {
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.applicationStatus = applicationStatus;
            this.availableGates = availableGates;
            this.windowSystem = windowSystem;

            applicationStatus.StatusTextChanged += HandleStatusTextChanged;
            availableGates.GateChanged += HandleDefaultGateChanged;

            DefaultGate = availableGates.DefaultGate == null
                ? string.Empty
                : availableGates.DefaultGate.Name;

            StatusText = applicationStatus.StatusText;
        }

        private void HandleStatusTextChanged(object sender, EventArgs e)
        {
            StatusText = applicationStatus.StatusText;
        }

        private void HandleDefaultGateChanged(object sender, EventArgs e)
        {
            DefaultGate = availableGates.DefaultGate == null
                ? string.Empty
                : availableGates.DefaultGate.Name;
        }

        public void DefaultGateWasClicked(Point point)
        {
            windowSystem.ShowGateSelector(point);
        }
    }
}