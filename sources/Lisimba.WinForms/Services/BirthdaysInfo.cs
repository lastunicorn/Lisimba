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
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Services
{
    class BirthdaysInfo
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly WindowSystem windowSystem;

        private DateTime startDate;
        private DateTime endDate;

        public BirthdaysInfo(OpenedAddressBooks openedAddressBooks, WindowSystem windowSystem)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.openedAddressBooks = openedAddressBooks;
            this.windowSystem = windowSystem;
        }

        public void Show()
        {
            startDate = DateTime.Today;
            endDate = DateTime.Today.AddDays(7);

            IEnumerable<Contact> contacts = RetrieveContacts();

            if (!contacts.Any())
                return;

            windowSystem.DisplayBirthdays(contacts, startDate, endDate);
        }

        private IEnumerable<Contact> RetrieveContacts()
        {
            return openedAddressBooks.Current.AddressBook.GetBirthdays(startDate, endDate)
                .ToList();
        }
    }
}