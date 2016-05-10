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
using System.Windows.Media.Imaging;
using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Business.Actions;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class ContactEditorViewModel : ViewModelBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private string name;
        private string notes;
        private BitmapSource picture;
        private ZodiacSignViewModel zodiacSignViewModel;
        private readonly AddContactItemClickCommand addContactItemClickCommand;
        private List<Tuple<Type, IEnumerable<ContactItem>>> contactItems;
        private Date birthday;
        private bool isInitializationMode;
        private bool canAddItems;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public BitmapSource Picture
        {
            get { return picture; }
            set
            {
                picture = value;
                OnPropertyChanged();
            }
        }

        public ZodiacSignViewModel ZodiacSignViewModel
        {
            get { return zodiacSignViewModel; }
            set
            {
                zodiacSignViewModel = value;
                OnPropertyChanged();
            }
        }

        public List<Tuple<Type, IEnumerable<ContactItem>>> ContactItems
        {
            get { return contactItems; }
            set
            {
                contactItems = value;
                OnPropertyChanged();
            }
        }

        public bool CanAddItems
        {
            get { return canAddItems; }
            set
            {
                canAddItems = value;
                OnPropertyChanged();
            }
        }

        public List<ContactItemInfo> ContactItemTypes { get; private set; }

        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged();

                if (!isInitializationMode)
                {
                    IAction action = new ChangeContactNotesAction(openedAddressBooks.CurrentContact, notes);

                    if (openedAddressBooks.Current.ActionQueue != null)
                        openedAddressBooks.Current.ActionQueue.Do(action);
                    else
                        action.Do();
                }
            }
        }

        public Date Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged();
            }
        }

        public ImageClickCommand ImageClickCommand { get; private set; }

        public ContactEditorViewModel(OpenedAddressBooks openedAddressBooks, ZodiacSignViewModel zodiacSignViewModel,
            ImageClickCommand imageClickCommand, AddContactItemClickCommand addContactItemClickCommand)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (zodiacSignViewModel == null) throw new ArgumentNullException("zodiacSignViewModel");
            if (imageClickCommand == null) throw new ArgumentNullException("imageClickCommand");
            if (addContactItemClickCommand == null) throw new ArgumentNullException("addContactItemClickCommand");

            this.openedAddressBooks = openedAddressBooks;
            this.zodiacSignViewModel = zodiacSignViewModel;
            this.addContactItemClickCommand = addContactItemClickCommand;

            ImageClickCommand = imageClickCommand;

            ContactItemTypes = new List<ContactItemInfo>
            {
                new ContactItemInfo { Text = "Phone", Command = addContactItemClickCommand, ItemType = typeof(Phone), Icon = Resources.phone.ToBitmapSource() },
                new ContactItemInfo { Text = "Email", Command = addContactItemClickCommand, ItemType = typeof(Email), Icon = Resources.email.ToBitmapSource() },
                new ContactItemInfo { Text = "Address", Command = addContactItemClickCommand, ItemType = typeof(PostalAddress), Icon = Resources.address.ToBitmapSource() },
                new ContactItemInfo { Text = "Date", Command = addContactItemClickCommand, ItemType = typeof(Date), Icon = Resources.date.ToBitmapSource() },
                new ContactItemInfo { Text = "Social Profile", Command = addContactItemClickCommand, ItemType = typeof(SocialProfile), Icon = Resources.mesengerid.ToBitmapSource() },
                new ContactItemInfo { Text = "Web Site", Command = addContactItemClickCommand, ItemType = typeof(WebSite), Icon = Resources.webaddress.ToBitmapSource() }
            };

            openedAddressBooks.ContactChanging += HandleCurrentContactChanging;
            openedAddressBooks.ContactChanged += HandleCurrentContactChanged;

            RefreshDisplayedData();
        }

        private void HandleCurrentContactChanging(object sender, EventArgs e)
        {
            Contact currentContact = openedAddressBooks.CurrentContact;

            if (currentContact != null)
                currentContact.Changed -= HandleContactChanged;
        }

        private void HandleCurrentContactChanged(object sender, EventArgs e)
        {
            Contact currentContact = openedAddressBooks.CurrentContact;

            if (currentContact != null)
                currentContact.Changed += HandleContactChanged;

            RefreshDisplayedData();
        }

        private void RefreshDisplayedData()
        {
            isInitializationMode = true;

            try
            {
                CanAddItems = openedAddressBooks.CurrentContact != null;
                addContactItemClickCommand.Contact = openedAddressBooks.CurrentContact;

                if (openedAddressBooks.CurrentContact == null)
                {
                    Name = string.Empty;
                    Picture = Resources.no_user_128.ToBitmapSource();
                    Birthday = null;
                    zodiacSignViewModel.ZodiacSign = ZodiacSign.NotSpecified;
                    ContactItems = null;
                    Notes = string.Empty;

                    ImageClickCommand.Contact = null;
                }
                else
                {
                    Contact currentContact = openedAddressBooks.CurrentContact;

                    List<Tuple<Type, IEnumerable<ContactItem>>> all = currentContact.Items.ItemTypes
                        .Select(x => new Tuple<Type, IEnumerable<ContactItem>>(x, currentContact.Items.GetItems(x)))
                        .ToList();

                    Name = currentContact.Name.ToString();
                    Picture = currentContact.Picture.ToBitmapSource() ?? Resources.no_user_128.ToBitmapSource();
                    Birthday = currentContact.Birthday;
                    zodiacSignViewModel.ZodiacSign = currentContact.ZodiacSign;
                    ContactItems = all;
                    Notes = currentContact.Notes;

                    ImageClickCommand.Contact = currentContact;
                }
            }
            finally
            {
                isInitializationMode = false;
            }
        }

        private void HandleContactChanged(object sender, EventArgs eventArgs)
        {
            RefreshDisplayedData();
        }
    }
}