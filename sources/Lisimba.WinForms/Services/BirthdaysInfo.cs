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
using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Services
{
    class BirthdaysInfo
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly UserInterface userInterface;

        private DateTime startDate;
        private DateTime endDate;

        public BirthdaysInfo(OpenedAddressBooks openedAddressBooks, UserInterface userInterface)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.openedAddressBooks = openedAddressBooks;
            this.userInterface = userInterface;
        }

        public void Show()
        {
            startDate = DateTime.Today;
            endDate = DateTime.Today.AddDays(7);

            IEnumerable<Contact> contacts = RetrieveContacts();

            if (!contacts.Any())
                return;

            string birthdaysInfo = BuildInfoText(contacts);
            userInterface.DisplayInfo(birthdaysInfo);
        }

        private IEnumerable<Contact> RetrieveContacts()
        {
            return openedAddressBooks.Current.AddressBook.GetBirthdays(startDate, endDate)
                .ToList();
        }

        private string BuildInfoText(IEnumerable<Contact> contacts)
        {
            StringBuilder sb = new StringBuilder();

            double totalDays = (endDate - startDate).TotalDays;
            sb.AppendLine("The birthdays for the next " + totalDays + " days are:");
            sb.AppendLine();

            foreach (Contact contact in contacts)
            {
                string line = string.Format("{0} - {1}", contact.Name, contact.Birthday.ToShortString());
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}