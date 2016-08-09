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
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection
{
    internal class AddContactItemClickCommand : ICommand
    {
        private readonly WindowSystem windowSystem;
        private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                OnCanExecuteChanged();
            }
        }

        public event EventHandler CanExecuteChanged;

        public AddContactItemClickCommand(WindowSystem windowSystem)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.windowSystem = windowSystem;
        }

        public bool CanExecute(object parameter)
        {
            return Contact != null;
        }

        public void Execute(object parameter)
        {
            try
            {
                Type type = parameter as Type;

                if(type == null)
                    return;

                ContactItem newContactItem = Activator.CreateInstance(type) as ContactItem;

                if (newContactItem == null)
                    return;

                Contact.Items.Add(newContactItem);
            }
            catch (Exception ex)
            {
                windowSystem.DisplayError(ex.Message);
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}