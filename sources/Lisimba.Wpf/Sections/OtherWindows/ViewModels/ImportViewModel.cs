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
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.OtherWindows.ViewModels
{
    internal class ImportViewModel : ViewModelBase
    {
        private CustomObservableCollection<Contact> originalContactsCollection;
        private string logs;
        private List<ImportGridItem> items;

        public string Logs
        {
            get { return logs; }
            private set
            {
                logs = value;
                OnPropertyChanged();
            }
        }

        public List<ImportGridItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        public OpenAddressBookCommand OpenAddressBookCommand { get; private set; }

        public ImportViewModel(AddressBooks addressBooks, OpenAddressBookCommand openAddressBookCommand)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (openAddressBookCommand == null) throw new ArgumentNullException("openAddressBookCommand");

            SetContacts(addressBooks.Current.AddressBook.Contacts);

            addressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;

            OpenAddressBookCommand = openAddressBookCommand;
            openAddressBookCommand.ImportFinished += HandleImportFinished;
        }

        private void HandleImportFinished(object sender, EventArgs eventArgs)
        {
            Logs = OpenAddressBookCommand.Result;
            Items = OpenAddressBookCommand.AddressBookImporter.ImportRules
                .SelectMany(x =>
                {
                    List<ImportGridItem> list = new List<ImportGridItem> { new ImportGridItem(x) };
                    list.AddRange(x.ItemImports.Select(y => new ImportGridItem(y)));
                    return list;
                })
                .ToList();
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            ClearContacts();

            if (e.NewAddressBook != null)
                SetContacts(e.NewAddressBook.AddressBook.Contacts);
        }

        private void ClearContacts()
        {
            if (originalContactsCollection != null)
                originalContactsCollection.ItemChanged -= ContactsItemChanged;

            originalContactsCollection = null;
        }

        private void SetContacts(CustomObservableCollection<Contact> contactCollection)
        {
            originalContactsCollection = contactCollection;
            originalContactsCollection.ItemChanged += ContactsItemChanged;
        }

        private void ContactsItemChanged(object sender, ItemChangedEventArgs<Contact> itemChangedEventArgs)
        {
        }
    }
}