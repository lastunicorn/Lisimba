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
using System.Drawing;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.ContactEdit
{
    internal class ContactEditorViewModel : ViewModelBase
    {
        private readonly Zodiac zodiac;

        private Contact contact;
        private bool isInitializationMode;
        private string birthday;
        private string zodiacSignText;
        private string notes;
        private Image zodiacSignImage = new Bitmap(1, 1);
        private bool enabled;
        private PhoneCollection phones;
        private EmailCollection emails;
        private WebSiteCollection webSites;
        private PostalAddressCollection postalAddresses;
        private DateCollection dates;
        private SocialProfileIdCollection socialProfileIds;
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

        public string Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged();
            }
        }

        public Image ZodiacSignImage
        {
            get { return zodiacSignImage; }
            set
            {
                zodiacSignImage = value ?? new Bitmap(1, 1);
                OnPropertyChanged();
            }
        }

        public string ZodiacSignText
        {
            get { return zodiacSignText; }
            set
            {
                zodiacSignText = value;
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

        public PhoneCollection Phones
        {
            get { return phones; }
            set
            {
                phones = value;
                OnPropertyChanged();
            }
        }

        public EmailCollection Emails
        {
            get { return emails; }
            set
            {
                emails = value;
                OnPropertyChanged();
            }
        }

        public WebSiteCollection WebSites
        {
            get { return webSites; }
            set
            {
                webSites = value;
                OnPropertyChanged();
            }
        }

        public PostalAddressCollection PostalAddresses
        {
            get { return postalAddresses; }
            set
            {
                postalAddresses = value;
                OnPropertyChanged();
            }
        }

        public DateCollection Dates
        {
            get { return dates; }
            set
            {
                dates = value;
                OnPropertyChanged();
            }
        }

        public SocialProfileIdCollection SocialProfileIds
        {
            get { return socialProfileIds; }
            set
            {
                socialProfileIds = value;
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

        public ContactEditorViewModel(Zodiac zodiac)
        {
            if (zodiac == null)
                throw new ArgumentNullException("zodiac");

            this.zodiac = zodiac;
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
            Birthday = contact.Birthday.ToShortString();

            ZodiacSignImage = zodiac.GetZodiacImage(contact.ZodiacSign);
            ZodiacSignText = zodiac.GetZodiacSignName(contact.ZodiacSign);

            Notes = contact.Notes;

            Phones = contact.Phones;
            Emails = contact.Emails;
            WebSites = contact.WebSites;
            PostalAddresses = contact.PostalAddresses;
            Dates = contact.Dates;
            SocialProfileIds = contact.SocialProfileIds;

            Enabled = true;
        }

        private void ClearView()
        {
            Name = null;
            Birthday = string.Empty;

            ZodiacSignImage = zodiac.GetEmptyImage();
            ZodiacSignText = string.Empty;

            Notes = string.Empty;

            Phones = null;
            Emails = null;
            WebSites = null;
            PostalAddresses = null;
            Dates = null;
            SocialProfileIds = null;

            Enabled = false;
        }

        public void BirthdayEditWasRequested()
        {
            View.EditBirthday(contact.Birthday);
        }

        public void AddAddressWasClicked()
        {
            View.AddAddress(contact.PostalAddresses);
        }

        public void AddDateWasClicked()
        {
            View.AddDate(contact.Dates);
        }

        public void AddEmailWasClicked()
        {
            View.AddEmail(contact.Emails);
        }

        public void AddSocialProfileIdWasClicked()
        {
            View.AddSocialProfileId(contact.SocialProfileIds);
        }

        public void AddPhoneWasClicked()
        {
            View.AddPhone(contact.Phones);
        }

        public void AddWebSiteClicked()
        {
            View.AddWebSite(contact.WebSites);
        }
    }
}