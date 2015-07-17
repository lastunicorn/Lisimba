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
using System.Drawing;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.ContactEdit
{
    class ContactEditorViewModel : ViewModelBase
    {
        private readonly Zodiac zodiac;

        private Contact contact;
        private bool isInitializationMode;
        private string firstName;
        private string middleName;
        private string lastName;
        private string nickname;
        private string birthday;
        private string zodiacSignText;
        private string notes;
        private Image zodiacSignImage = new Bitmap(1, 1);
        private bool enabled;
        private PhoneCollection phones;
        private EmailCollection emails;
        private WebSiteCollection webSites;
        private AddressCollection addresses;
        private DateCollection dates;
        private MessengerIdCollection messengerIds;

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

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                OnPropertyChanged();
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
                notes = value;
                OnPropertyChanged();
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

        public AddressCollection Addresses
        {
            get { return addresses; }
            set
            {
                addresses = value;
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

        public MessengerIdCollection MessengerIds
        {
            get { return messengerIds; }
            set
            {
                messengerIds = value;
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
            FirstName = contact.Name.FirstName;
            MiddleName = contact.Name.MiddleName;
            LastName = contact.Name.LastName;
            Nickname = contact.Name.Nickname;

            Birthday = contact.Birthday.ToString();

            ZodiacSignImage = zodiac.GetZodiacImage(contact.ZodiacSign);
            ZodiacSignText = zodiac.GetZodiacSignName(contact.ZodiacSign);

            Notes = contact.Notes;

            Phones = contact.Phones;
            Emails = contact.Emails;
            WebSites = contact.WebSites;
            Addresses = contact.Addresses;
            Dates = contact.Dates;
            MessengerIds = contact.MessengerIds;

            Enabled = true;
        }

        private void ClearView()
        {
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            Nickname = string.Empty;

            Birthday = string.Empty;

            ZodiacSignImage = zodiac.GetEmptyImage();
            ZodiacSignText = string.Empty;

            Notes = string.Empty;

            Phones = null;
            Emails = null;
            WebSites = null;
            Addresses = null;
            Dates = null;
            MessengerIds = null;

            Enabled = false;
        }

        public void FirstNameWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Name.FirstName = FirstName;
        }

        public void MiddleNameWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Name.MiddleName = MiddleName;
        }

        public void LastNameWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Name.LastName = LastName;
        }

        public void NicknameWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Name.Nickname = Nickname;
        }

        public void NotesWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Notes = Notes;
        }

        public void BirthdayEditWasRequested()
        {
            View.EditBirthday(contact.Birthday);
        }
    }
}