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
using System.Drawing;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.WinFormsCommon;
using DustInTheWind.WinFormsCommon.Operations;

namespace DustInTheWind.Lisimba.Utils
{
    internal class CustomButtonViewModel : ViewModelBase
    {
        protected readonly ApplicationStatus applicationStatus;
        protected readonly WindowSystem windowSystem;
        protected readonly IOperation operation;

        private bool isEnabled;
        private string text;
        private Image image;

        public bool IsEnabled
        {
            get { return isEnabled; }
            protected set
            {
                if (isEnabled == value)
                    return;

                isEnabled = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        public CustomButtonViewModel(ApplicationStatus applicationStatus, WindowSystem windowSystem, IOperation operation)
        {
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");
            if (operation == null) throw new ArgumentNullException("operation");

            this.applicationStatus = applicationStatus;
            this.windowSystem = windowSystem;
            this.operation = operation;

            operation.EnableChanged += HandleOperationEnableChanged;

            isEnabled = operation.IsEnabled;
        }

        private void HandleOperationEnableChanged(object sender, EventArgs eventArgs)
        {
            IsEnabled = operation.IsEnabled;
        }

        public void MouseEnter()
        {
            if (applicationStatus != null)
                applicationStatus.SetPermanentStatusText(operation.ShortDescription);
        }

        public void MouseLeave()
        {
            if (applicationStatus != null)
                applicationStatus.Reset();
        }

        public void Execute()
        {
            try
            {
                object parameter = GetExecuteParameter();

                if (parameter == null)
                    operation.Execute();
                else
                    operation.Execute(parameter);
            }
            catch (Exception ex)
            {
                windowSystem.DisplayError(ex.Message);
            }
        }

        protected virtual object GetExecuteParameter()
        {
            return null;
        }
    }
}