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
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.ContactEdit
{
    internal class ContactEditorViewModel : ViewModelBase
    {
        private Contact contact;
        private bool isInitializationMode;
        private Date birthday;
        private ZodiacSign zodiacSign;
        private string notes;
        private bool enabled;
        private CustomObservableCollection<ContactItem> contactItems;
        private PersonName name;

        public IContactEditorView View { get; set; }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                if (contact != null)
                    contact.Changed -= HandleContactChanged;

                contact = value;

                if (contact != null)
                    contact.Changed += HandleContactChanged;

                RefreshData();
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

        public ZodiacSign ZodiacSign
        {
            get { return zodiacSign; }
            set
            {
                zodiacSign = value;
                OnPropertyChanged();
            }
        }

        public string Notes
        {
            get { return notes; }
            set
            {
                if (notes == value)
                    return;

                notes = value;
                OnPropertyChanged();

                if (!isInitializationMode)
                    contact.Notes = notes;
            }
        }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                OnPropertyChanged();
            }
        }

        public CustomObservableCollection<ContactItem> ContactItems
        {
            get { return contactItems; }
            set
            {
                contactItems = value;
                OnPropertyChanged();
            }
        }

        public PersonName Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            if (View == null)
                return;

            isInitializationMode = true;

            try
            {
                if (contact == null)
                    ClearView();
                else
                    DisplayContactInView();
            }
            finally
            {
                isInitializationMode = false;
            }
        }

        private void DisplayContactInView()
        {
            Name = Contact.Name;
            Birthday = contact.Birthday;
            ZodiacSign = contact.ZodiacSign;

            Notes = contact.Notes;

            ContactItems = contact.Items;

            Enabled = true;
        }

        private void ClearView()
        {
            Name = null;
            Birthday = null;
            ZodiacSign = ZodiacSign.NotSpecified;

            Notes = string.Empty;

            ContactItems = null;

            Enabled = false;
        }

        public void AddAddressWasClicked()
        {
            View.AddAddress(contact.Items);
        }

        public void AddDateWasClicked()
        {
            View.AddDate(contact.Items);
        }

        public void AddEmailWasClicked()
        {
            View.AddEmail(contact.Items);
        }

        public void AddSocialProfileIdWasClicked()
        {
            View.AddSocialProfileId(contact.Items);
        }

        public void AddPhoneWasClicked()
        {
            View.AddPhone(contact.Items);
        }

        public void AddWebSiteClicked()
        {
            View.AddWebSite(contact.Items);
        }
    }
}