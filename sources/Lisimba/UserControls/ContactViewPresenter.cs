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
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    class ContactViewPresenter
    {
        private readonly CurrentData currentData;

        public IContactView View { get; set; }

        public ContactViewPresenter(CurrentData currentData)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            this.currentData = currentData;
            currentData.ContactChanged += HandleCurrentContactChanged;
        }

        private void HandleCurrentContactChanged(object sender, EventArgs e)
        {
            if (View == null)
                return;

            if (currentData.Contact == null)
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
            }
            else
            {
                View.FirstName = currentData.Contact.Name.FirstName;
                View.MiddleName = currentData.Contact.Name.MiddleName;
                View.LastName = currentData.Contact.Name.LastName;
                View.Nickname = currentData.Contact.Name.Nickname;

                View.Birthday = currentData.Contact.Birthday.ToString();

                View.Notes = currentData.Contact.Notes;

                View.Phones = currentData.Contact.Phones;
                View.Emails = currentData.Contact.Emails;
                View.WebSites = currentData.Contact.WebSites;
                View.Addresses = currentData.Contact.Addresses;
                View.Dates = currentData.Contact.Dates;
                View.MessengerIds = currentData.Contact.MessengerIds;
            }
        }
    }
}