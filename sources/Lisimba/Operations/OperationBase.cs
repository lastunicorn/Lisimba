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

namespace DustInTheWind.Lisimba.Operations
{
    abstract class OperationBase<T> : IOpertion<T>
    {
        private bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            protected set
            {
                bool isChanged = isEnabled != value;

                isEnabled = value;

                if (isChanged)
                    OnIsEnabledChanged();
            }
        }

        public abstract string ShortDescription { get; }

        public event EventHandler IsEnabledChanged;

        protected virtual void OnIsEnabledChanged()
        {
            EventHandler handler = IsEnabledChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected OperationBase()
        {
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