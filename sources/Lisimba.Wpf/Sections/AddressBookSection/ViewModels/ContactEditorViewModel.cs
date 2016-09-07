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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Business.Actions;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Wpf.Properties;
using DustInTheWind.Lisimba.Wpf.Sections.OtherWindows;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.ViewModels
{
    internal class ContactEditorViewModel : ViewModelBase
    {
        private readonly AddressBooks addressBooks;
        private string name;
        private string notes;
        private BitmapSource picture;
        private ZodiacSignViewModel zodiacSignViewModel;
        private readonly AddContactItemClickCommand addContactItemClickCommand;
        //private List<ContactItemSetViewModel> contactItems;
        private List<object> contactItems;
        private BirthdayViewModel birthdayViewModel;
        private bool isInitializationMode;
        private bool canAddItems;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();

                if (!isInitializationMode)
                {
                    NameParser nameParser = new NameParser(value);

                    if (!nameParser.Success)
                        throw new Exception();

                    IAction action = new UpdateContactItemAction(addressBooks.CurrentContact.Name, nameParser.Result);

                    if (addressBooks.Current.ActionQueue != null)
                        addressBooks.Current.ActionQueue.Do(action);
                    else
                        action.Do();
                }
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

        //public List<ContactItemSetViewModel> ContactItems
        //{
        //    get { return contactItems; }
        //    set
        //    {
        //        contactItems = value;
        //        OnPropertyChanged();
        //    }
        //}

        public List<object> ContactItems
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

        public List<ContactItemAddViewModel> ContactItemTypes { get; private set; }

        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged();

                if (!isInitializationMode)
                {
                    IAction action = new ChangeContactNotesAction(addressBooks.CurrentContact, notes);

                    if (addressBooks.Current.ActionQueue != null)
                        addressBooks.Current.ActionQueue.Do(action);
                    else
                        action.Do();
                }
            }
        }

        public BirthdayViewModel BirthdayViewModel
        {
            get { return birthdayViewModel; }
            set
            {
                birthdayViewModel = value;
                OnPropertyChanged();
            }
        }

        public ImageClickCommand ImageEditCommand { get; private set; }
        public BirthdayEditCommand BirthdayEditCommand { get; private set; }

        public ContactEditorViewModel(AddressBooks addressBooks, ZodiacSignViewModel zodiacSignViewModel, BirthdayViewModel birthdayViewModel,
            ImageClickCommand imageEditCommand, BirthdayEditCommand birthdayEditCommand, AddContactItemClickCommand addContactItemClickCommand)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (zodiacSignViewModel == null) throw new ArgumentNullException("zodiacSignViewModel");
            if (imageEditCommand == null) throw new ArgumentNullException("imageEditCommand");
            if (birthdayEditCommand == null) throw new ArgumentNullException("birthdayEditCommand");
            if (addContactItemClickCommand == null) throw new ArgumentNullException("addContactItemClickCommand");

            this.addressBooks = addressBooks;
            this.zodiacSignViewModel = zodiacSignViewModel;
            this.birthdayViewModel = birthdayViewModel;
            this.addContactItemClickCommand = addContactItemClickCommand;

            ImageEditCommand = imageEditCommand;
            BirthdayEditCommand = birthdayEditCommand;

            ContactItemTypes = new List<ContactItemAddViewModel>
            {
                new ContactItemAddViewModel { Text = "Phone", Command = addContactItemClickCommand, ItemType = typeof(Phone), Icon = Resources.phone.ToBitmapSource() },
                new ContactItemAddViewModel { Text = "Email", Command = addContactItemClickCommand, ItemType = typeof(Email), Icon = Resources.email.ToBitmapSource() },
                new ContactItemAddViewModel { Text = "Address", Command = addContactItemClickCommand, ItemType = typeof(PostalAddress), Icon = Resources.address.ToBitmapSource() },
                new ContactItemAddViewModel { Text = "Date", Command = addContactItemClickCommand, ItemType = typeof(Date), Icon = Resources.date.ToBitmapSource() },
                new ContactItemAddViewModel { Text = "Social Profile", Command = addContactItemClickCommand, ItemType = typeof(SocialProfileId), Icon = Resources.mesengerid.ToBitmapSource() },
                new ContactItemAddViewModel { Text = "Web Site", Command = addContactItemClickCommand, ItemType = typeof(WebSite), Icon = Resources.webaddress.ToBitmapSource() }
            };

            addressBooks.ContactChanging += HandleCurrentContactChanging;
            addressBooks.ContactChanged += HandleCurrentContactChanged;

            RefreshDisplayedData();
        }

        private void HandleCurrentContactChanging(object sender, EventArgs e)
        {
            Contact currentContact = addressBooks.CurrentContact;

            if (currentContact != null)
                currentContact.Changed -= HandleContactChanged;
        }

        private void HandleCurrentContactChanged(object sender, EventArgs e)
        {
            Contact currentContact = addressBooks.CurrentContact;

            if (currentContact != null)
                currentContact.Changed += HandleContactChanged;

            RefreshDisplayedData();
        }

        private void RefreshDisplayedData()
        {
            isInitializationMode = true;

            try
            {
                CanAddItems = addressBooks.CurrentContact != null;
                addContactItemClickCommand.Contact = addressBooks.CurrentContact;

                if (addressBooks.CurrentContact == null)
                {
                    Name = null;
                    Picture = Resources.no_user_128.ToBitmapSource();
                    BirthdayViewModel.Date = null;
                    zodiacSignViewModel.ZodiacSign = ZodiacSign.NotSpecified;
                    ContactItems = null;
                    Notes = string.Empty;

                    ImageEditCommand.Contact = null;
                }
                else
                {
                    Contact currentContact = addressBooks.CurrentContact;

                    Name = currentContact.Name.ToString();

                    if (currentContact.Picture != null && currentContact.Picture.Image != null)
                        Picture = currentContact.Picture.Image.ToBitmapSource() ?? Resources.no_user_128.ToBitmapSource();
                    else
                        Picture = Resources.no_user_128.ToBitmapSource();

                    BirthdayViewModel.Date = currentContact.Birthday;
                    zodiacSignViewModel.ZodiacSign = currentContact.ZodiacSign;

                    Dictionary<Type, Type> viewModelTypes = new Dictionary<Type, Type>
                    {
                        { typeof(Phone), typeof(PhoneViewModel) },
                        { typeof(Date), typeof(DateViewModel) }
                    };

                    ContactItems = currentContact.Items
                        .Select(x =>
                        {
                            Type t = x.GetType();

                            return viewModelTypes.ContainsKey(t)
                                ? Activator.CreateInstance(viewModelTypes[t], x)
                                : x as object;
                        })
                        .ToList();

                    Notes = currentContact.Notes;

                    ImageEditCommand.Contact = currentContact;
                }
            }
            finally
            {
                isInitializationMode = false;
            }
        }

        private static ImageSource GetIconForItemType(Type type)
        {
            if (type == typeof(Phone))
                return Resources.phone.ToBitmapSource();

            if (type == typeof(Email))
                return Resources.email.ToBitmapSource();

            if (type == typeof(PostalAddress))
                return Resources.address.ToBitmapSource();

            if (type == typeof(Date))
                return Resources.date.ToBitmapSource();

            if (type == typeof(SocialProfileId))
                return Resources.mesengerid.ToBitmapSource();

            if (type == typeof(WebSite))
                return Resources.webaddress.ToBitmapSource();

            return Resources.phone.ToBitmapSource();
        }

        private static string GetTextForItemType(Type type)
        {
            if (type == typeof(Phone))
                return "Phone";

            if (type == typeof(Email))
                return "Email";

            if (type == typeof(PostalAddress))
                return "Address";

            if (type == typeof(Date))
                return "Date";

            if (type == typeof(SocialProfileId))
                return "Social Profile";

            if (type == typeof(WebSite))
                return "Web Site";

            return string.Empty;
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            RefreshDisplayedData();
        }
    }
}