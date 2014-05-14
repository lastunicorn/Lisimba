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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class CustomObservableCollection<T> : ObservableCollection<T>
        where T : class, IObservableEntity
    {
        private bool suppressCollectionChangedEvent;

        #region Event ItemChanged

        public event EventHandler<ItemChangedEventArgs<T>> ItemChanged;

        protected virtual void OnItemChanged(ItemChangedEventArgs<T> e)
        {
            EventHandler<ItemChangedEventArgs<T>> handler = ItemChanged;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.Changed += HandleItemChanged;
        }

        protected override void RemoveItem(int index)
        {
            T itemToRemove = Items[index];
            base.RemoveItem(index);
            itemToRemove.Changed -= HandleItemChanged;
        }

        private void HandleItemChanged(object sender, EventArgs e)
        {
            OnItemChanged(new ItemChangedEventArgs<T>(sender as T));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!suppressCollectionChangedEvent)
                base.OnCollectionChanged(e);
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            suppressCollectionChangedEvent = true;

            try
            {
                foreach (T item in items)
                {
                    Add(item);
                }
            }
            finally
            {
                suppressCollectionChangedEvent = false;
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items));
        }
    }
}
