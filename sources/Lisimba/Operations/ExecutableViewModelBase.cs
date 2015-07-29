// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Operations
{
    abstract class ExecutableViewModelBase<T> : ViewModelBase, IExecutableViewModel<T>
    {
        private readonly ApplicationStatus applicationStatus;
        private bool isEnabled;

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

        public abstract string ShortDescription { get; }

        protected ExecutableViewModelBase(ApplicationStatus applicationStatus)
        {
            this.applicationStatus = applicationStatus;
            
            isEnabled = true;
        }

        public void MouseEnter()
        {
            if (applicationStatus != null)
                applicationStatus.SetPermanentStatusText(ShortDescription);
        }

        public void MouseLeave()
        {
            if (applicationStatus != null)
                applicationStatus.Reset();
        }

        public void Execute()
        {
            if (!isEnabled)
                return;

            DoExecute(default(T));
        }

        public void Execute(object parameter)
        {
            if (parameter != null && !(parameter is T))
            {
                string message = string.Format("The parameter is not of type {0}", typeof(T).Name);
                throw new ArgumentException(message);
            }

            Execute((T)parameter);
        }

        public void Execute(T parameter)
        {
            if (!isEnabled)
                return;

            DoExecute(parameter);
        }

        protected abstract void DoExecute(T parameter);
    }
}