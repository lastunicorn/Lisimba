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

namespace DustInTheWind.Lisimba.Wpf.Commands
{
    internal abstract class CommandBase : ICommand
    {
        private bool isEnabled;

        protected WindowSystem WindowSystem { get; private set; }

        protected bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (isEnabled == value)
                    return;

                isEnabled = value;
                OnCanExecuteChanged();
            }
        }

        public abstract string ShortDescription { get; }

        public event EventHandler CanExecuteChanged;

        protected CommandBase(WindowSystem windowSystem)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            WindowSystem = windowSystem;

            isEnabled = true;
        }

        public bool CanExecute(object parameter)
        {
            return isEnabled;
        }

        public void Execute(object parameter)
        {
            if (!isEnabled)
                return;

            try
            {
                DoExecute(parameter);
            }
            catch (Exception ex)
            {
                WindowSystem.DisplayError(ex.Message);
            }
        }

        protected abstract void DoExecute(object parameter);

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}