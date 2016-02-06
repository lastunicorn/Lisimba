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
using System.Collections.Generic;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.ActionManagement
{
    public class ActionQueue
    {
        private readonly AddressBook addressBook;
        private readonly Stack<IAction> undoList = new Stack<IAction>();
        private readonly Stack<IAction> redoList = new Stack<IAction>();
        private readonly object synchronizationToken = new object();

        public event EventHandler UndoStackChanged;
        public event EventHandler RedoStackChanged;

        public ActionQueue(AddressBook addressBook)
        {
            if (addressBook == null) throw new ArgumentNullException("addressBook");
            this.addressBook = addressBook;
        }

        public bool CanUndo
        {
            get { return undoList.Count > 0; }
        }

        public bool CanRedo
        {
            get { return redoList.Count > 0; }
        }

        public void Do(IAction action)
        {
            lock (synchronizationToken)
            {
                action.Do();
                undoList.Push(action);

                OnUndoStackChanged();
            }
        }

        public void Undo()
        {
            lock (synchronizationToken)
            {
                IAction action = undoList.Pop();
                action.Undo();
                redoList.Push(action);

                OnUndoStackChanged();
                OnRedoStackChanged();
            }
        }

        public void Redo()
        {
            lock (synchronizationToken)
            {
                IAction action = redoList.Pop();
                action.Do();
                undoList.Push(action);

                OnRedoStackChanged();
                OnUndoStackChanged();
            }
        }

        public void ChangeAddressBookName(string newName)
        {
            IAction action = new RenameAddressBookAction(addressBook, newName);
            Do(action);
        }

        protected virtual void OnUndoStackChanged()
        {
            EventHandler handler = UndoStackChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnRedoStackChanged()
        {
            EventHandler handler = RedoStackChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
