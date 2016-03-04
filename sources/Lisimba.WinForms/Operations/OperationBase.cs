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
using DustInTheWind.Lisimba.WinForms.Properties;
using DustInTheWind.Lisimba.WinForms.Services;
using DustInTheWind.WinFormsCommon.Operations;

namespace DustInTheWind.Lisimba.WinForms.Operations
{
    internal abstract class OperationBase<T> : IOperation<T>
    {
        protected readonly WindowSystem windowSystem;

        private bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            protected set
            {
                if (isEnabled == value)
                    return;

                isEnabled = value;
                OnEnableChanged();
            }
        }

        public abstract string ShortDescription { get; }

        public event EventHandler EnableChanged;

        protected OperationBase(WindowSystem windowSystem)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.windowSystem = windowSystem;

            isEnabled = true;
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
                string message = string.Format(LocalizedResources.ExecutableViewModel_IncorectParameterType, typeof(T).Name);
                throw new ArgumentException(message);
            }

            if (!isEnabled)
                return;

            try
            {
                DoExecute((T)parameter);
            }
            catch (Exception ex)
            {
                windowSystem.DisplayError(ex.Message);
            }
        }

        public void Execute(T parameter)
        {
            if (!isEnabled)
                return;

            try
            {
                DoExecute(parameter);
            }
            catch (Exception ex)
            {
                windowSystem.DisplayError(ex.Message);
            }
        }

        protected abstract void DoExecute(T parameter);

        protected virtual void OnEnableChanged()
        {
            EventHandler handler = EnableChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}