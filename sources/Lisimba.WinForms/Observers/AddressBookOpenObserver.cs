// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Observers
{
    class AddressBookOpenObserver : IObserver
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly ApplicationStatus applicationStatus;
        private readonly UserInterface userInterface;

        public AddressBookOpenObserver(OpenedAddressBooks openedAddressBooks, ApplicationStatus applicationStatus, UserInterface userInterface)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.openedAddressBooks = openedAddressBooks;
            this.applicationStatus = applicationStatus;
            this.userInterface = userInterface;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookOpened += HandleAddressBookOpened;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookOpened -= HandleAddressBookOpened;
        }

        private void HandleAddressBookOpened(object sender, AddressBookOpenedEventArgs e)
        {
            DisplayOpenSuccessMessage();
            DisplayWarnings(e.Result.Warnings);
            DisplayBirthdays();
        }

        private void DisplayOpenSuccessMessage()
        {
            if (openedAddressBooks.Current != null)
            {
                if (openedAddressBooks.Current.Status == AddressBookStatus.New)
                {
                    applicationStatus.StatusText = "A new address book was created.";
                }
                else
                {
                    int contactsCount = openedAddressBooks.Current.AddressBook.Contacts.Count;
                    applicationStatus.StatusText = string.Format(Resources.OpenAddressBook_SuccessStatusText, contactsCount);
                }
            }
        }

        private void DisplayWarnings(IEnumerable<Exception> warnings)
        {
            if (warnings == null)
                return;

            StringBuilder sb = new StringBuilder();

            foreach (Exception warning in warnings)
            {
                sb.AppendLine(warning.Message);
                sb.AppendLine();
            }

            if (sb.Length > 0)
                userInterface.DisplayWarning(sb.ToString());
        }

        private void DisplayBirthdays()
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(7);
            List<Contact> contacts = openedAddressBooks.Current.AddressBook.GetBirthdays(startDate, endDate).ToList();

            if (contacts.Count <= 0)
                return;

            StringBuilder sb = new StringBuilder();

            double totalDays = (endDate - startDate).TotalDays;
            sb.AppendLine("The birthdays for the next " + totalDays + " days are:");
            sb.AppendLine();

            foreach (Contact contact in contacts)
            {
                string line = string.Format("{0} - {1}", contact.Name, contact.Birthday.ToShortString());
                sb.AppendLine(line);
            }

            userInterface.DisplayInfo(sb.ToString());
        }
    }
}