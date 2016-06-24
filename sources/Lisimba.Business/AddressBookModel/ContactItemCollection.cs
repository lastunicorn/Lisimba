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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Searching;

namespace DustInTheWind.Lisimba.Egg.AddressBookModel
{
    public class ContactItemCollection : CustomObservableCollection<ContactItem>
    {
        private readonly Dictionary<Type, List<ContactItem>> itemsByType = new Dictionary<Type, List<ContactItem>>();

        public List<Type> ItemTypes
        {
            get
            {
                return itemsByType.Keys
                    .Distinct()
                    .ToList();
            }
        }

        /// <summary>
        /// Returns the <see cref="ContactItem"/> object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The <see cref="ContactItem"/> object that match or <c>null</c>.</returns>
        public ContactItem SearchByDescription(string text, SearchMode searchMode)
        {
            foreach (ContactItem item in Items)
            {
                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (item.Description.CompareTo(text) == 0)
                            return item;
                        break;

                    case SearchMode.StartingWith:
                        if (item.Description.StartsWith(text))
                            return item;
                        break;

                    case SearchMode.EndingWith:
                        if (item.Description.EndsWith(text))
                            return item;
                        break;

                    case SearchMode.Containing:
                        if (item.Description.IndexOf(text) > 0)
                            return item;
                        break;
                }
            }

            return null;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItemsToCategories(e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItemsFromCategories(e.OldItems);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    RemoveItemsFromCategories(e.OldItems);
                    AddItemsToCategories(e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    RemoveItemsFromCategories(e.OldItems);
                    break;
            }

            base.OnCollectionChanged(e);
        }

        private void AddItemsToCategories(IEnumerable itemsToAdd)
        {
            foreach (ContactItem item in itemsToAdd)
            {
                Type itemType = item.GetType();
                List<ContactItem> items = GetOrCreateListForType(itemType);

                items.Add(item);
            }
        }

        private void RemoveItemsFromCategories(IEnumerable itemsToRemove)
        {
            foreach (ContactItem item in itemsToRemove)
            {
                Type itemType = item.GetType();
                List<ContactItem> items = GetOrCreateListForType(itemType);

                items.Remove(item);

                if (items.Count == 0)
                    itemsByType.Remove(itemType);
            }
        }

        private List<ContactItem> GetOrCreateListForType(Type itemType)
        {
            if (itemsByType.ContainsKey(itemType))
                return itemsByType[itemType];

            List<ContactItem> items = new List<ContactItem>();
            itemsByType.Add(itemType, items);

            return items;
        }

        public IEnumerable<ContactItem> GetItems(Type itemsType)
        {
            return itemsByType.ContainsKey(itemsType)
                ? (IEnumerable<ContactItem>)itemsByType[itemsType]
                : new ContactItem[0];
        }

        public override bool Equals(object obj)
        {
            ContactItemCollection webSites = obj as ContactItemCollection;

            return Equals(webSites);
        }

        public bool Equals(ContactItemCollection contactItems)
        {
            if (contactItems == null)
                return false;

            if (contactItems.Count != Count)
                return false;

            return contactItems.All(x => Enumerable.Contains(Items, x));
        }
    }
}