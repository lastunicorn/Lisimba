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
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.Commands
{
    internal class RedoCommand : CommandBase
    {
        private readonly AddressBooks addressBooks;

        public override string ShortDescription
        {
            get { return LocalizedResources.RedoOperationDescription; }
        }

        public RedoCommand(AddressBooks addressBooks, WindowSystem windowSystem)
            : base(windowSystem)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.addressBooks = addressBooks;

            addressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;

            if (addressBooks.Current != null)
                addressBooks.Current.ActionQueue.RedoStackChanged += HandleRedoStackChanged;

            IsEnabled = CalculateEnableState();
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
                e.OldAddressBook.ActionQueue.RedoStackChanged -= HandleRedoStackChanged;

            if (e.NewAddressBook != null)
                e.NewAddressBook.ActionQueue.RedoStackChanged += HandleRedoStackChanged;

            IsEnabled = CalculateEnableState();
        }

        private void HandleRedoStackChanged(object sender, EventArgs e)
        {
            IsEnabled = CalculateEnableState();
        }

        private bool CalculateEnableState()
        {
            return addressBooks.Current != null &&
                   addressBooks.Current.ActionQueue.CanRedo;
        }

        protected override void DoExecute(object parameter)
        {
            addressBooks.Current.ActionQueue.Redo();
        }
    }
}