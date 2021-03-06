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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace DustInTheWind.Lisimba.Business.AddressBookModel
{
    public class CustomObservableCollection<T> : ObservableCollection<T>
        where T : class, IObservableEntity
    {
        public event EventHandler<ItemChangedEventArgs<T>> ItemChanged;

        public CustomObservableCollection()
        {
        }

        public CustomObservableCollection(IEnumerable<T> contacts)
            : base(contacts)
        {
        }

        protected virtual void OnItemChanged(ItemChangedEventArgs<T> e)
        {
            EventHandler<ItemChangedEventArgs<T>> handler = ItemChanged;

            if (handler != null)
                handler(this, e);
        }

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

        public void AddRange(IList<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            if (items.Count == 0)
                return;

            int startigIndex = Items.Count;

            foreach (T item in items)
            {
                Items.Add(item);
                item.Changed += HandleItemChanged;
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)items, startigIndex));
        }

        public void CopyFrom(IList<T> items)
        {
            Clear();
            AddRange(items);
        }

        public override bool Equals(object obj)
        {
            CustomObservableCollection<T> items = obj as CustomObservableCollection<T>;

            if (items == null)
                return false;

            if (items.Count != Count)
                return false;

            return items.All(x => Enumerable.Contains(Items, x));
        }
    }
}