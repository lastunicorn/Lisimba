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
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Utils
{
    internal class CustomButtonViewModel : ViewModelBase
    {
        protected readonly ApplicationStatus applicationStatus;
        protected readonly UserInterface userInterface;
        protected readonly IOperation operation;
        private bool isEnabled;
        private string text;

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

        public CustomButtonViewModel(ApplicationStatus applicationStatus, UserInterface userInterface, IOperation operation)
        {
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (operation == null) throw new ArgumentNullException("operation");

            this.applicationStatus = applicationStatus;
            this.userInterface = userInterface;
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

        public virtual void Execute()
        {
            try
            {
                operation.Execute();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex.Message);
            }
        }
    }
}