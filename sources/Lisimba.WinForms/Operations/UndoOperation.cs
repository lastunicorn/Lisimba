﻿// Lisimba
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
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class UndoOperation : OperationBase<object>
    {
        private readonly OpenedAddressBooks openedAddressBooks;

        public override string ShortDescription
        {
            get { return LocalizedResources.UndoOperationDescription; }
        }

        public UndoOperation(OpenedAddressBooks openedAddressBooks, UserInterface userInterface)
            : base(userInterface)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.openedAddressBooks = openedAddressBooks;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;

            if (openedAddressBooks.Current != null)
                openedAddressBooks.Current.ActionQueue.UndoStackChanged += HandleUndoStackChanged;

            IsEnabled = CalculateEnableState();
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
                e.OldAddressBook.ActionQueue.UndoStackChanged -= HandleUndoStackChanged;

            if (e.NewAddressBook != null)
                e.NewAddressBook.ActionQueue.UndoStackChanged += HandleUndoStackChanged;

            IsEnabled = CalculateEnableState();
        }

        private void HandleUndoStackChanged(object sender, EventArgs eventArgs)
        {
            IsEnabled = CalculateEnableState();
        }

        private bool CalculateEnableState()
        {
            return openedAddressBooks.Current != null &&
                   openedAddressBooks.Current.ActionQueue.CanUndo;
        }

        protected override void DoExecute(object parameter)
        {
            openedAddressBooks.Current.ActionQueue.Undo();
        }
    }
}