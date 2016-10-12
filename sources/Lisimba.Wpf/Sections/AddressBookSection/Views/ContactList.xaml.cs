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
using System.Windows.Controls;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.Views
{
    /// <summary>
    /// Interaction logic for ContactList.xaml
    /// </summary>
    public partial class ContactList : UserControl
    {
        private Contact lastSelectedContact;

        public ContactList()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When a new contact is selected, scroll the contact into view.

            ListBox listBox = sender as ListBox;

            if (listBox == null)
                return;

            if (lastSelectedContact != null)
                lastSelectedContact.Changed -= HandleLastSelectedContactChanged;

            if (listBox.SelectedItem != null)
            {
                lastSelectedContact = listBox.SelectedItem as Contact;

                if (lastSelectedContact != null)
                    lastSelectedContact.Changed += HandleLastSelectedContactChanged;

                listBox.Dispatcher.BeginInvoke((Action)(() =>
                {
                    listBox.UpdateLayout();

                    if (listBox.SelectedItem != null)
                        listBox.ScrollIntoView(listBox.SelectedItem);
                }));
            }
        }

        private void HandleLastSelectedContactChanged(object sender, EventArgs e)
        {
            // When the currently selected contact is modified, scroll the contact into view.

            if (ContactsListBox.SelectedItem != null)
            {
                ContactsListBox.Dispatcher.BeginInvoke((Action)(() =>
                {
                    ContactsListBox.UpdateLayout();

                    if (ContactsListBox.SelectedItem != null)
                        ContactsListBox.ScrollIntoView(ContactsListBox.SelectedItem);
                }));
            }
        }
    }
}
