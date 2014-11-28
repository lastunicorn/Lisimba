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
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.UserControls;

namespace DustInTheWind.Lisimba.Presenters
{
    class ContactViewPresenter
    {
        private readonly Zodiac zodiac;

        private Contact contact;
        private bool isInitializationMode;

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

        public ContactViewPresenter(Zodiac zodiac)
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
                {
                    View.FirstName = string.Empty;
                    View.MiddleName = string.Empty;
                    View.LastName = string.Empty;
                    View.Nickname = string.Empty;

                    View.Birthday = string.Empty;

                    View.Notes = string.Empty;

                    View.Phones = null;
                    View.Emails = null;
                    View.WebSites = null;
                    View.Addresses = null;
                    View.Dates = null;
                    View.MessengerIds = null;

                    View.Enabled = false;
                }
                else
                {
                    View.FirstName = contact.Name.FirstName;
                    View.MiddleName = contact.Name.MiddleName;
                    View.LastName = contact.Name.LastName;
                    View.Nickname = contact.Name.Nickname;

                    View.Birthday = contact.Birthday.ToString();

                    View.ZodiacSignImage = zodiac.GetZodiacImage(contact.ZogiacSign);
                    //toolTip1.SetToolTip(pictureBoxZodiacSign, contact.ZogiacSign.ToString());
                    View.ZodiacSignText = zodiac.GetZodiacSignName(contact.ZogiacSign);

                    View.Notes = contact.Notes;

                    View.Phones = contact.Phones;
                    View.Emails = contact.Emails;
                    View.WebSites = contact.WebSites;
                    View.Addresses = contact.Addresses;
                    View.Dates = contact.Dates;
                    View.MessengerIds = contact.MessengerIds;

                    View.Enabled = true;
                }
            }
            finally
            {
                isInitializationMode = false;
            }
        }

        public void FirstNameWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Name.FirstName = View.FirstName;
        }

        public void MiddleNameWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Name.MiddleName = View.MiddleName;
        }

        public void LastNameWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Name.LastName = View.LastName;
        }

        public void NicknameWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Name.Nickname = View.Nickname;
        }

        public void NotesWasChanged()
        {
            if (isInitializationMode)
                return;

            contact.Notes = View.Notes;
        }

        public void BirthdayEditWasRequested()
        {
            View.EditBirthday(contact.Birthday);
        }
    }
}